using Family_Feud.Data;
using Family_Feud.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
// These are namespaces - importing libraries we need
// Family_Feud.Data = our database context
// Family_Feud.Services = our AuthService
// JwtBearer = for JWT token authentication
// EntityFrameworkCore = for database operations
// OpenApi = for Swagger documentation

var builder = WebApplication.CreateBuilder(args);
// Create application builder
// This is the starting point of our application
// args = command line arguments (if any)

// ========== DATABASE CONFIGURATION ==========
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Register our database context with dependency injection
// UseSqlServer = we're using SQL Server (Azure SQL)
// GetConnectionString reads "DefaultConnection" from appsettings.json
// This connection string contains server address, database name, username, password

// ========== JWT AUTHENTICATION SETUP ==========
var jwtKey = builder.Configuration["Jwt:Key"];
// Read the secret key from appsettings.json
// This key is used to sign and verify JWT tokens

var key = Encoding.ASCII.GetBytes(jwtKey!);
// Convert the string key to byte array
// JWT library needs bytes, not string

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Configure authentication to use JWT Bearer tokens
// DefaultAuthenticateScheme = how to authenticate incoming requests
// DefaultChallengeScheme = what to do when authentication fails

.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    // Allow HTTP (not just HTTPS) - useful for development

    options.SaveToken = true;
    // Save the token in AuthenticationProperties
    // Allows us to retrieve it later if needed

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        // Verify that the token was signed with our secret key
        // Prevents token forgery

        IssuerSigningKey = new SymmetricSecurityKey(key),
        // The key used to validate signature

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        // Check that token was issued by our server ("FamilyFeudAPI")

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        // Check that token is intended for our app ("FamilyFeudClient")

        ValidateLifetime = true,
        // Check that token hasn't expired
        // Tokens expire after 1440 minutes (24 hours)

        ClockSkew = TimeSpan.Zero
        // No tolerance for expiration time
        // Token expires exactly when it should
    };
});

builder.Services.AddAuthorization();
// Enable authorization (checking user roles like "Admin", "Player")

// ========== CORS CONFIGURATION ==========
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Allow requests from any domain
              .AllowAnyMethod()   // Allow GET, POST, PUT, DELETE, etc.
              .AllowAnyHeader();  // Allow any HTTP headers
    });
});
// CORS = Cross-Origin Resource Sharing
// By default, browsers block requests to different domains
// This policy allows our frontend to call our API
// In production, you might want to restrict to specific domains

// ========== DEPENDENCY INJECTION ==========
builder.Services.AddScoped<IAuthService, AuthService>();
// Register AuthService for dependency injection
// AddScoped = create new instance for each HTTP request
// When a controller asks for IAuthService, it gets AuthService
// This allows easy testing (can replace with mock service)

builder.Services.AddControllers();
// Enable API controllers
// Controllers handle HTTP requests (AuthController, QuestionsController, etc.)

builder.Services.AddEndpointsApiExplorer();
// Required for Swagger to discover our API endpoints

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Family Feud API",
        Version = "v1",
        Description = "API for Family Feud Game - Public API for external developers"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter your JWT token (without 'Bearer ' prefix)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,  // <-- ApiKey -> Http dəyişdik
        Scheme = "bearer",               // <-- kiçik hərf
        BearerFormat = "JWT"             // <-- yeni əlavə
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();
// Build the application with all configured services

// ========== AUTO-MIGRATE DATABASE ==========
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
// Automatically apply pending migrations when app starts
// This creates/updates database tables
// Useful for deployment - no manual migration needed

// ========== MIDDLEWARE PIPELINE ==========
// Order is important! Requests flow through these in sequence

app.UseSwagger();
app.UseSwaggerUI();
// Enable Swagger documentation at /swagger
// Anyone can see API documentation

app.UseHttpsRedirection();
// Redirect HTTP requests to HTTPS
// Ensures secure communication

app.UseDefaultFiles();
// Look for default files (index.html) in wwwroot
// When user visits root URL, serve index.html

app.UseStaticFiles();
// Serve static files from wwwroot folder
// This serves our frontend (HTML, CSS, JavaScript)

app.UseCors("AllowAll");
// Apply CORS policy we defined earlier
// Must be before Authentication/Authorization

app.UseAuthentication();
// Process JWT token from request header
// Sets User.Identity if token is valid
// Must come before UseAuthorization

app.UseAuthorization();
// Check if user has permission to access endpoint
// Uses [Authorize] and [Authorize(Roles = "Admin")] attributes

app.MapControllers();
// Map HTTP requests to controller methods
// e.g., POST /api/auth/login -> AuthController.Login()

app.Run();
// Start the server and begin listening for requests
// Application runs until stopped