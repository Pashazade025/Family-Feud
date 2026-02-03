// ========================================
// FAMILY FEUD - COMPLETE APPLICATION
// ========================================

// Configuration
const CONFIG = {
    API_BASE_URL: '/api',
    TOKEN_KEY: 'familyfeud_token',
    USER_KEY: 'familyfeud_user',
    LANG_KEY: 'familyfeud_lang',
    SETTINGS_KEY: 'familyfeud_settings',
    QUESTION_TIME: 45
};
// Default Game Settings (Admin tərəfindən dəyişdirilə bilər)
const DEFAULT_SETTINGS = {
    timer: 45,
    strikes: 3,
    questionsPerGame: 10,
    bonusMultiplier: 2
};

// Translations
const TRANSLATIONS = {
    en: {
        login: 'Login',
        register: 'Register',
        email: 'Email',
        password: 'Password',
        username: 'Username',
        confirmPassword: 'Confirm Password',
        loginBtn: 'Sign In',
        registerBtn: 'Create Account',
        noAccount: "Don't have an account?",
        haveAccount: 'Already have an account?',
        signUp: 'Sign Up',
        signIn: 'Sign In',
        welcome: 'Welcome,',
        logout: 'Logout',
        admin: 'Admin',
        playGame: 'Play Game',
        playDesc: 'Start a new game session',
        rules: 'How to Play',
        rulesDesc: 'Learn the rules',
        back: 'Back',
        score: 'Score:',
        strikes: 'Strikes:',
        time: 'Time:',
        loadingQuestion: 'Loading question...',
        typeAnswer: 'Type your answer...',
        submit: 'Submit',
        nextQuestion: 'Next Question',
        endGame: 'End Game',
        adminPanel: 'Admin Panel',
        questions: 'Questions',
        users: 'Users',
        statistics: 'Statistics',
        addQuestion: 'Add Question',
        totalUsers: 'Total Users',
        totalGames: 'Total Games',
        totalQuestions: 'Total Questions',
        gameOver: 'Game Over!',
        finalScore: 'Final Score:',
        questionsPlayed: 'Questions Played:',
        correctAnswers: 'Correct Answers:',
        timeBonus: 'Time Bonus:',
        playAgain: 'Play Again',
        backToDashboard: 'Back to Dashboard',
        loading: 'Loading...',
        loginSuccess: 'Login successful!',
        registerSuccess: 'Registration successful! Please login.',
        loginError: 'Invalid email or password',
        registerError: 'Registration failed. Try again.',
        networkError: 'Network error. Please check your connection.',
        correctAnswer: 'Correct!',
        wrongAnswer: 'Wrong answer!',
        timeUp: 'Time is up!',
        answerText: 'Answer text',
        points: 'Points',
        noQuestions: 'No questions available.',
        deleteConfirm: 'Are you sure you want to delete this?',
        deleted: 'Deleted successfully',
        saved: 'Saved successfully',
        allQuestionsComplete: 'All questions completed! Great job!',
        settingsSaved: 'Settings saved successfully!',
        apiKeyGenerated: 'API Key generated successfully!',
        apiKeyRevoked: 'API Key revoked successfully!',
        roleUpdated: 'User role updated successfully!',
        copySuccess: 'Copied to clipboard!',
        adminDesc: 'Manage users, questions & settings',
        timerDuration: 'Timer Duration',
        maxStrikes: 'Max Strikes',
        questionsPerGameLabel: 'Questions per Game',
        timeBonusMultiplier: 'Time Bonus Multiplier',
        unlimited: 'Unlimited',
        seconds: 'seconds',
        developer: 'Developer',
        keyPreview: 'Key Preview',
        status: 'Status',
        created: 'Created',
        actions: 'Actions',
        revoke: 'Revoke',
        makeAdmin: 'Make Admin',
        makePlayer: 'Make Player',
        active: 'Active',
        inactive: 'Inactive',
        revoked: 'Revoked',
        category: 'Category',
        difficulty: 'Difficulty',
        answers: 'Answers',
        easy: 'Easy',
        medium: 'Medium',
        hard: 'Hard'
    },
    pl: {
        login: 'Logowanie',
        register: 'Rejestracja',
        email: 'E-mail',
        password: 'Hasło',
        username: 'Nazwa użytkownika',
        confirmPassword: 'Potwierdź hasło',
        loginBtn: 'Zaloguj się',
        registerBtn: 'Utwórz konto',
        noAccount: 'Nie masz konta?',
        haveAccount: 'Masz już konto?',
        signUp: 'Zarejestruj się',
        signIn: 'Zaloguj się',
        welcome: 'Witaj,',
        logout: 'Wyloguj',
        admin: 'Admin',
        playGame: 'Graj',
        playDesc: 'Rozpocznij nową grę',
        rules: 'Jak grać',
        rulesDesc: 'Poznaj zasady',
        back: 'Wróć',
        score: 'Wynik:',
        strikes: 'Błędy:',
        time: 'Czas:',
        loadingQuestion: 'Ładowanie pytania...',
        typeAnswer: 'Wpisz odpowiedź...',
        submit: 'Wyślij',
        nextQuestion: 'Następne pytanie',
        endGame: 'Zakończ grę',
        adminPanel: 'Panel Admina',
        questions: 'Pytania',
        users: 'Użytkownicy',
        statistics: 'Statystyki',
        addQuestion: 'Dodaj pytanie',
        totalUsers: 'Łączna liczba użytkowników',
        totalGames: 'Łączna liczba gier',
        totalQuestions: 'Łączna liczba pytań',
        gameOver: 'Koniec gry!',
        finalScore: 'Końcowy wynik:',
        questionsPlayed: 'Zagrane pytania:',
        correctAnswers: 'Poprawne odpowiedzi:',
        timeBonus: 'Bonus czasowy:',
        playAgain: 'Zagraj ponownie',
        backToDashboard: 'Wróć do menu',
        loading: 'Ładowanie...',
        loginSuccess: 'Zalogowano pomyślnie!',
        registerSuccess: 'Rejestracja udana! Proszę się zalogować.',
        loginError: 'Nieprawidłowy e-mail lub hasło',
        registerError: 'Rejestracja nie powiodła się.',
        networkError: 'Błąd sieci. Sprawdź połączenie.',
        correctAnswer: 'Poprawnie!',
        wrongAnswer: 'Błędna odpowiedź!',
        timeUp: 'Czas minął!',
        answerText: 'Tekst odpowiedzi',
        points: 'Punkty',
        noQuestions: 'Brak dostępnych pytań.',
        deleteConfirm: 'Czy na pewno chcesz to usunąć?',
        deleted: 'Usunięto pomyślnie',
        saved: 'Zapisano pomyślnie',
        allQuestionsComplete: 'Wszystkie pytania ukończone! Świetna robota!',
        settingsSaved: 'Ustawienia zapisane pomyślnie!',
        apiKeyGenerated: 'Klucz API wygenerowany pomyślnie!',
        apiKeyRevoked: 'Klucz API unieważniony pomyślnie!',
        roleUpdated: 'Rola użytkownika zaktualizowana!',
        copySuccess: 'Skopiowano do schowka!',
        adminDesc: 'Zarządzaj użytkownikami, pytaniami i ustawieniami',
        timerDuration: 'Czas trwania',
        maxStrikes: 'Maksymalne błędy',
        questionsPerGameLabel: 'Pytania na grę',
        timeBonusMultiplier: 'Mnożnik bonusu czasowego',
        unlimited: 'Bez limitu',
        seconds: 'sekund',
        developer: 'Deweloper',
        keyPreview: 'Podgląd klucza',
        status: 'Status',
        created: 'Utworzono',
        actions: 'Akcje',
        revoke: 'Unieważnij',
        makeAdmin: 'Ustaw jako Admin',
        makePlayer: 'Ustaw jako Gracz',
        active: 'Aktywny',
        inactive: 'Nieaktywny',
        revoked: 'Unieważniony',
        category: 'Kategoria',
        difficulty: 'Trudność',
        answers: 'Odpowiedzi',
        easy: 'Łatwe',
        medium: 'Średnie',
        hard: 'Trudne'

    }
};

// State Management
const state = {
    currentLang: localStorage.getItem(CONFIG.LANG_KEY) || 'en',
    token: localStorage.getItem(CONFIG.TOKEN_KEY) || null,
    user: (() => {
        try {
            const data = localStorage.getItem(CONFIG.USER_KEY);
            return data ? JSON.parse(data) : null;
        } catch {
            return null;
        }
    })(),
    settings: (() => {
        try {
            const data = localStorage.getItem(CONFIG.SETTINGS_KEY);
            return data ? JSON.parse(data) : DEFAULT_SETTINGS;
        } catch {
            return DEFAULT_SETTINGS;
        }
    })(),
    currentGame: null,
    currentQuestion: null,
    allQuestions: [],
    score: 0,
    strikes: 0,
    questionsPlayed: 0,
    correctAnswers: 0,
    revealedAnswers: [],
    usedQuestionIds: [],
    answerFieldCount: 3,
    timer: null,
    timeLeft: 45,
    totalTimeBonus: 0
};

// ========================================
// UTILITY FUNCTIONS
// ========================================

function t(key) {
    return TRANSLATIONS[state.currentLang][key] || key;
}

function updateTranslations() {
    document.querySelectorAll('[data-i18n]').forEach(el => {
        const key = el.getAttribute('data-i18n');
        if (TRANSLATIONS[state.currentLang][key]) {
            el.textContent = TRANSLATIONS[state.currentLang][key];
        }
    });
    
    document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
        const key = el.getAttribute('data-i18n-placeholder');
        if (TRANSLATIONS[state.currentLang][key]) {
            el.placeholder = TRANSLATIONS[state.currentLang][key];
        }
    });
}

function showLoading() {
    document.getElementById('loading-overlay').classList.add('active');
}

function hideLoading() {
    document.getElementById('loading-overlay').classList.remove('active');
}

function showToast(message, type = 'success') {
    const toast = document.getElementById('toast');
    const icon = toast.querySelector('.toast-icon');
    const msg = toast.querySelector('.toast-message');
    
    toast.className = 'toast show ' + type;
    icon.textContent = type === 'success' ? '✓' : type === 'error' ? '✗' : '⚠';
    msg.textContent = message;
    
    setTimeout(() => {
        toast.classList.remove('show');
    }, 3000);
}

function showAuthMessage(message, type = 'error') {
    const messageEl = document.getElementById('auth-message');
    messageEl.textContent = message;
    messageEl.className = `message ${type}`;
}

function hideAuthMessage() {
    document.getElementById('auth-message').classList.add('hidden');
}

function showSection(sectionId) {
    stopTimer();
    
    document.querySelectorAll('.section').forEach(section => {
        section.classList.remove('active');
    });
    document.getElementById(sectionId).classList.add('active');
}

function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString() + ' ' + date.toLocaleTimeString([], {hour: '2-digit', minute:'2-digit'});
}

// Sualın dilə görə mətnini al
function getQuestionText(question) {
    if (state.currentLang === 'pl') {
        return question.questionTextPL || question.questionTextEN || question.text;
    }
    return question.questionTextEN || question.text;
}

// Cavabın dilə görə mətnini al
function getAnswerText(answer) {
    if (state.currentLang === 'pl') {
        return answer.answerTextPL || answer.answerTextEN || answer.text;
    }
    return answer.answerTextEN || answer.text;
}

// ========================================
// TIMER FUNCTIONS
// ========================================

function startTimer() {
    state.timeLeft = state.settings.timer;
    updateTimerDisplay();
    
    state.timer = setInterval(() => {
        state.timeLeft--;
        updateTimerDisplay();
        
        if (state.timeLeft <= 0) {
            timeUp();
        }
    }, 1000);
}

function stopTimer() {
    if (state.timer) {
        clearInterval(state.timer);
        state.timer = null;
    }
}

function updateTimerDisplay() {
    const timerEl = document.getElementById('timer-value');
    if (timerEl) {
        timerEl.textContent = state.timeLeft;
        
        if (state.timeLeft <= 10) {
            timerEl.style.color = '#ff4444';
            timerEl.classList.add('pulse');
        } else if (state.timeLeft <= 20) {
            timerEl.style.color = '#ffaa00';
            timerEl.classList.remove('pulse');
        } else {
            timerEl.style.color = '#ffd700';
            timerEl.classList.remove('pulse');
        }
    }
}

function timeUp() {
    stopTimer();
    showToast(t('timeUp'), 'error');
    
    const question = state.currentQuestion;
    if (question && question.answers) {
        for (let i = 0; i < question.answers.length; i++) {
            if (!state.revealedAnswers.includes(i)) {
                state.revealedAnswers.push(i);
            }
        }
        renderQuestion();
    }
    
    document.getElementById('next-question').style.display = 'block';
    document.getElementById('answer-input').disabled = true;
    document.getElementById('submit-answer').disabled = true;
}

function calculateTimeBonus() {
    return state.timeLeft * state.settings.bonusMultiplier;
}

// ========================================
// API FUNCTIONS
// ========================================

async function apiRequest(endpoint, options = {}) {
    const url = `${CONFIG.API_BASE_URL}${endpoint}`;
    
    const headers = {
        'Content-Type': 'application/json',
        ...options.headers
    };
    
    if (state.token) {
        headers['Authorization'] = `Bearer ${state.token}`;
    }
    
    try {
        const response = await fetch(url, {
            ...options,
            headers
        });
        
        if (!response.ok) {
            const errorData = await response.json().catch(() => ({}));
            throw new Error(errorData.message || `HTTP ${response.status}`);
        }
        
        const text = await response.text();
        return text ? JSON.parse(text) : null;
    } catch (error) {
        console.error('API Error:', error);
        throw error;
    }
}

// Auth API
async function login(email, password) {
    const data = await apiRequest('/auth/login', {
        method: 'POST',
        body: JSON.stringify({ email, password })
    });
    
    state.token = data.token;
    state.user = {
        id: data.userId,
        username: data.username,
        email: data.email,
        role: data.role,
        preferredLanguage: data.preferredLanguage
    };
    
    localStorage.setItem(CONFIG.TOKEN_KEY, data.token);
    localStorage.setItem(CONFIG.USER_KEY, JSON.stringify(state.user));
    
    return data;
}
async function register(username, email, password) {
    return await apiRequest('/auth/register', {
        method: 'POST',
        body: JSON.stringify({ username, email, password })
    });
}

function logout() {
    state.token = null;
    state.user = null;
    localStorage.removeItem(CONFIG.TOKEN_KEY);
    localStorage.removeItem(CONFIG.USER_KEY);
    showSection('auth-section');
}

// Questions API
async function getQuestions() {
    return await apiRequest('/questions');
}

async function createQuestion(questionData) {
    return await apiRequest('/questions', {
        method: 'POST',
        body: JSON.stringify(questionData)
    });
}

async function deleteQuestion(id) {
    return await apiRequest(`/questions/${id}`, {
        method: 'DELETE'
    });
}

// Admin API
async function getAdminStats() {
    return await apiRequest('/admin/stats');
}

async function getAdminUsers() {
    return await apiRequest('/admin/users');
}

async function getAdminQuestions() {
    return await apiRequest('/admin/questions');
}

async function updateUserRole(userId, role) {
    return await apiRequest(`/admin/users/${userId}/role`, {
        method: 'PUT',
        body: JSON.stringify({ role })
    });
}

async function getApiKeys() {
    return await apiRequest('/admin/apikeys');
}

async function generateApiKey(developerName, developerEmail) {
    return await apiRequest('/admin/apikeys', {
        method: 'POST',
        body: JSON.stringify({ developerName, developerEmail })
    });
}

async function revokeApiKey(id) {
    return await apiRequest(`/admin/apikeys/${id}`, {
        method: 'DELETE'
    });
}

// ========================================
// AUTH HANDLERS
// ========================================

function initAuthHandlers() {
    document.getElementById('show-register').addEventListener('click', (e) => {
        e.preventDefault();
        document.getElementById('login-form').classList.remove('active');
        document.getElementById('register-form').classList.add('active');
        hideAuthMessage();
    });
    
    document.getElementById('show-login').addEventListener('click', (e) => {
        e.preventDefault();
        document.getElementById('register-form').classList.remove('active');
        document.getElementById('login-form').classList.add('active');
        hideAuthMessage();
    });
    
    document.getElementById('login-btn').addEventListener('click', async () => {
        const email = document.getElementById('login-email').value.trim();
        const password = document.getElementById('login-password').value;
        
        if (!email || !password) {
            showAuthMessage('Please fill in all fields', 'error');
            return;
        }
        
        showLoading();
        hideAuthMessage();
        
        try {
            await login(email, password);
            showToast(t('loginSuccess'));
            initDashboard();
            showSection('dashboard-section');
        } catch (error) {
            showAuthMessage(t('loginError'), 'error');
        } finally {
            hideLoading();
        }
    });
    
    document.getElementById('register-btn').addEventListener('click', async () => {
        const username = document.getElementById('register-username').value.trim();
        const email = document.getElementById('register-email').value.trim();
        const password = document.getElementById('register-password').value;
        const confirm = document.getElementById('register-confirm').value;
        
        if (!username || !email || !password || !confirm) {
            showAuthMessage('Please fill in all fields', 'error');
            return;
        }
        
        if (password !== confirm) {
            showAuthMessage('Passwords do not match', 'error');
            return;
        }
        
        if (password.length < 6) {
            showAuthMessage('Password must be at least 6 characters', 'error');
            return;
        }
        
        showLoading();
        hideAuthMessage();
        
        try {
            await register(username, email, password);
            showAuthMessage(t('registerSuccess'), 'success');
            
            setTimeout(() => {
                document.getElementById('register-form').classList.remove('active');
                document.getElementById('login-form').classList.add('active');
                document.getElementById('login-email').value = email;
            }, 1500);
        } catch (error) {
            showAuthMessage(error.message || t('registerError'), 'error');
        } finally {
            hideLoading();
        }
    });
    
    // Enter key support for auth forms
    document.querySelectorAll('.auth-form input').forEach(input => {
        input.addEventListener('keypress', (e) => {
            if (e.key === 'Enter') {
                const form = input.closest('.auth-form');
                if (form.id === 'login-form') {
                    document.getElementById('login-btn').click();
                } else {
                    document.getElementById('register-btn').click();
                }
            }
        });
    });
}

// ========================================
// DASHBOARD
// ========================================
function initDashboard() {
    if (state.user) {
        document.getElementById('display-username').textContent = state.user.username || state.user.email;
        
        // Role badge göstər
        const roleEl = document.getElementById('display-role');
        if (roleEl) {
            if (state.user.role === 'Admin') {
                roleEl.textContent = 'ADMIN';
                roleEl.style.display = 'inline-block';
            } else {
                roleEl.style.display = 'none';
            }
        }
        
        // Admin elementlərini göstər/gizlə
        const isAdmin = state.user.role === 'Admin';
        
        // Admin button in header
        const adminBtn = document.getElementById('admin-btn');
        if (adminBtn) {
            adminBtn.style.display = isAdmin ? 'flex' : 'none';
        }
        
        // Admin panel card in dashboard
        const adminCard = document.getElementById('admin-panel-card');
        if (adminCard) {
            adminCard.style.display = isAdmin ? 'block' : 'none';
        }
        
        console.log('User role:', state.user.role, 'isAdmin:', isAdmin); // Debug üçün
    }
}

function initDashboardHandlers() {
    document.getElementById('logout-btn').addEventListener('click', logout);
    
    document.getElementById('play-game-card').addEventListener('click', startNewGame);
    
    document.getElementById('rules-card').addEventListener('click', () => {
        document.getElementById('rules-modal').classList.add('active');
    });
    
    document.getElementById('admin-btn').addEventListener('click', openAdminPanel);
    
    document.getElementById('close-rules').addEventListener('click', () => {
        document.getElementById('rules-modal').classList.remove('active');
    });
    
    // Admin Panel Card click handler
    const adminPanelCard = document.getElementById('admin-panel-card');
    if (adminPanelCard) {
        adminPanelCard.addEventListener('click', openAdminPanel);
    }
}

// ========================================
// GAME LOGIC
// ========================================

async function startNewGame() {
    showLoading();
    
    try {
        // Database-dən bütün sualları al
        state.allQuestions = await getQuestions();
        
        if (!state.allQuestions || state.allQuestions.length === 0) {
            showToast(t('noQuestions'), 'error');
            hideLoading();
            return;
        }
        
        // Reset game state
        state.score = 0;
        state.strikes = 0;
        state.questionsPlayed = 0;
        state.correctAnswers = 0;
        state.revealedAnswers = [];
        state.usedQuestionIds = [];
        state.totalTimeBonus = 0;
        state.currentGame = { id: 'game-' + Date.now() };
        
        showSection('game-section');
        updateGameUI();
        await loadNextQuestion();
    } catch (error) {
        showToast(t('networkError'), 'error');
    } finally {
        hideLoading();
    }
}

async function loadNextQuestion() {
    showLoading();
    stopTimer();
    
    document.getElementById('answer-input').disabled = false;
    document.getElementById('submit-answer').disabled = false;
    document.getElementById('next-question').style.display = 'none';
    
    // Sual limiti yoxla
    const limit = state.settings.questionsPerGame;
    if (limit > 0 && state.questionsPlayed >= limit) {
        showToast(t('allQuestionsComplete'), 'success');
        endCurrentGame();
        hideLoading();
        return;
    }
    
    // İstifadə olunmamış sual tap
    const availableQuestions = state.allQuestions.filter(q => !state.usedQuestionIds.includes(q.id));
    
    if (availableQuestions.length === 0) {
        showToast(t('allQuestionsComplete'), 'success');
        endCurrentGame();
        hideLoading();
        return;
    }
    
    // Random sual seç
    const randomIndex = Math.floor(Math.random() * availableQuestions.length);
    const question = availableQuestions[randomIndex];
    
    state.currentQuestion = question;
    state.usedQuestionIds.push(question.id);
    state.revealedAnswers = [];
    state.strikes = 0;
    state.questionsPlayed++;
    
    renderQuestion();
    updateGameUI();
    startTimer();
    
    document.getElementById('answer-input').value = '';
    document.getElementById('answer-input').focus();
    
    hideLoading();
}

function renderQuestion() {
    const question = state.currentQuestion;
    if (!question) return;
    
    // Dilə görə sual mətni
    document.getElementById('current-question').textContent = getQuestionText(question);
    
    const answersBoard = document.getElementById('answers-board');
    const answers = question.answers || [];
    
    // Cavabları rank-a görə sırala
    const sortedAnswers = [...answers].sort((a, b) => a.rank - b.rank);
    
    answersBoard.innerHTML = sortedAnswers.map((answer, index) => {
        const isRevealed = state.revealedAnswers.includes(index);
        return `
            <div class="answer-slot ${isRevealed ? 'revealed' : ''}" data-index="${index}">
                <span class="answer-number">${index + 1}</span>
                <span class="answer-text">${isRevealed ? getAnswerText(answer) : '???'}</span>
                <span class="answer-points">${isRevealed ? answer.points : '??'}</span>
            </div>
        `;
    }).join('');
}

function updateGameUI() {
    document.getElementById('current-score').textContent = state.score;
    document.getElementById('question-number').textContent = state.questionsPlayed;
    
    const strikes = document.querySelectorAll('.strike');
    strikes.forEach((strike, index) => {
        strike.classList.toggle('active', index < state.strikes);
    });
    
    const question = state.currentQuestion;
    const allRevealed = question && question.answers && 
        state.revealedAnswers.length === question.answers.length;
    const maxStrikes = state.strikes >= state.settings.strikes;
    
    document.getElementById('next-question').style.display = 
        (allRevealed || maxStrikes) ? 'block' : 'none';
}

function checkAnswer(userAnswer) {
    const question = state.currentQuestion;
    if (!question || !question.answers) return false;
    
    const normalizedInput = userAnswer.toLowerCase().trim();
    const sortedAnswers = [...question.answers].sort((a, b) => a.rank - b.rank);
    
    for (let i = 0; i < sortedAnswers.length; i++) {
        if (state.revealedAnswers.includes(i)) continue;
        
        const answer = sortedAnswers[i];
        // Hər iki dildə yoxla
        const answerEN = (answer.answerTextEN || answer.text || '').toLowerCase();
        const answerPL = (answer.answerTextPL || '').toLowerCase();
        
        if (answerEN.includes(normalizedInput) || 
            normalizedInput.includes(answerEN) ||
            answerEN === normalizedInput ||
            (answerPL && (answerPL.includes(normalizedInput) || 
            normalizedInput.includes(answerPL) ||
            answerPL === normalizedInput))) {
            
            state.revealedAnswers.push(i);
            state.score += answer.points;
            state.correctAnswers++;
            
            renderQuestion();
            updateGameUI();
            
            // Bütün cavablar tapıldı?
            if (state.revealedAnswers.length === sortedAnswers.length) {
                const bonus = calculateTimeBonus();
                state.totalTimeBonus += bonus;
                state.score += bonus;
                updateGameUI();
                
                stopTimer();
                showToast(`All answers found! +${bonus} time bonus!`, 'success');
                document.getElementById('next-question').style.display = 'block';
                document.getElementById('answer-input').disabled = true;
                document.getElementById('submit-answer').disabled = true;
            }
            
            return true;
        }
    }
    
    // Yanlış cavab
    state.strikes++;
    updateGameUI();
    
    if (state.strikes >= state.settings.strikes) {
        stopTimer();
        
        for (let i = 0; i < sortedAnswers.length; i++) {
            if (!state.revealedAnswers.includes(i)) {
                state.revealedAnswers.push(i);
            }
        }
        renderQuestion();
        
        document.getElementById('answer-input').disabled = true;
        document.getElementById('submit-answer').disabled = true;
    }
    
    return false;
}

function initGameHandlers() {
    document.getElementById('submit-answer').addEventListener('click', () => {
        const input = document.getElementById('answer-input');
        const answer = input.value.trim();
        
        if (!answer) return;
        
        const correct = checkAnswer(answer);
        
        if (correct) {
            showToast(t('correctAnswer'), 'success');
        } else {
            showToast(t('wrongAnswer'), 'error');
        }
        
        input.value = '';
        input.focus();
    });
    
    document.getElementById('answer-input').addEventListener('keypress', (e) => {
        if (e.key === 'Enter') {
            document.getElementById('submit-answer').click();
        }
    });
    
    document.getElementById('next-question').addEventListener('click', loadNextQuestion);
    document.getElementById('end-game').addEventListener('click', endCurrentGame);
    
    document.getElementById('back-to-dashboard').addEventListener('click', () => {
        if (confirm('Are you sure you want to leave?')) {
            stopTimer();
            showSection('dashboard-section');
        }
    });
    
    document.getElementById('play-again').addEventListener('click', () => {
        document.getElementById('game-over-modal').classList.remove('active');
        startNewGame();
    });
    
    document.getElementById('go-dashboard').addEventListener('click', () => {
        document.getElementById('game-over-modal').classList.remove('active');
        showSection('dashboard-section');
    });
}

function endCurrentGame() {
    stopTimer();
    
    document.getElementById('final-score-value').textContent = state.score;
    document.getElementById('questions-played').textContent = state.questionsPlayed;
    document.getElementById('correct-answers').textContent = state.correctAnswers;
    document.getElementById('time-bonus').textContent = state.totalTimeBonus;
    document.getElementById('game-over-modal').classList.add('active');
}

// ========================================
// ADMIN PANEL
// ========================================

async function openAdminPanel() {
    showSection('admin-section');
    await loadAdminDashboard();
    initAdminHandlers();
}

function initAdminHandlers() {
    // Tab switching
    document.querySelectorAll('.tab-btn').forEach(btn => {
        btn.addEventListener('click', async () => {
            document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
            document.querySelectorAll('.tab-content').forEach(c => c.classList.remove('active'));
            
            btn.classList.add('active');
            const tab = btn.dataset.tab;
            document.getElementById(`${tab}-tab`).classList.add('active');
            
            // Tab data yüklə
            if (tab === 'dashboard') await loadAdminDashboard();
            else if (tab === 'users') await loadAdminUsers();
            else if (tab === 'questions') await loadAdminQuestions();
            else if (tab === 'apikeys') await loadApiKeys();
            else if (tab === 'settings') loadSettings();
        });
    });
    
    document.getElementById('admin-back').addEventListener('click', () => {
        showSection('dashboard-section');
    });
    
    // Add Question Modal
    initAddQuestionModal();
    
    // API Key Modal
    initApiKeyModal();
    
    // Settings
    initSettingsHandlers();
}

// ========================================
// ADMIN - DASHBOARD TAB
// ========================================

async function loadAdminDashboard() {
    showLoading();
    
    try {
        const stats = await getAdminStats();
        
        document.getElementById('total-users').textContent = stats.totalUsers || 0;
        document.getElementById('total-games').textContent = stats.totalGames || 0;
        document.getElementById('total-questions').textContent = stats.totalQuestions || 0;
        document.getElementById('total-apikeys').textContent = stats.totalApiKeys || 0;
        document.getElementById('active-games').textContent = stats.activeGames || 0;
        document.getElementById('completed-games').textContent = stats.completedGames || 0;
    } catch (error) {
        console.error('Failed to load admin stats:', error);
    } finally {
        hideLoading();
    }
}

// ========================================
// ADMIN - USERS TAB
// ========================================

async function loadAdminUsers() {
    showLoading();
    
    try {
        const users = await getAdminUsers();
        const tbody = document.getElementById('users-list');
        
        if (!users || users.length === 0) {
            tbody.innerHTML = '<tr><td colspan="7" style="text-align: center; color: var(--text-muted);">No users found</td></tr>';
            hideLoading();
            return;
        }
        
        tbody.innerHTML = users.map(user => `
            <tr data-user-id="${user.id}">
                <td>${user.id}</td>
                <td>${user.username}</td>
                <td>${user.email}</td>
                <td>
                    <span class="${user.role === 'Admin' ? 'role-admin' : 'role-player'}">
                        ${user.role}
                    </span>
                </td>
                <td>${user.preferredLanguage || 'en'}</td>
                <td>${formatDate(user.createdAt)}</td>
                <td>
                    ${user.role !== 'Admin' 
                        ? `<button class="action-btn make-admin-btn" data-user-id="${user.id}">Make Admin</button>`
                        : `<button class="action-btn make-player-btn" data-user-id="${user.id}">Make Player</button>`
                    }
                </td>
            </tr>
        `).join('');
        
        // Role change button listeners
        tbody.querySelectorAll('.make-admin-btn').forEach(btn => {
            btn.addEventListener('click', () => changeUserRole(btn.dataset.userId, 'Admin'));
        });
        
        tbody.querySelectorAll('.make-player-btn').forEach(btn => {
            btn.addEventListener('click', () => changeUserRole(btn.dataset.userId, 'Player'));
        });
        
    } catch (error) {
        console.error('Failed to load users:', error);
        document.getElementById('users-list').innerHTML = 
            '<tr><td colspan="7" style="text-align: center; color: var(--error);">Failed to load users</td></tr>';
    } finally {
        hideLoading();
    }
}

async function changeUserRole(userId, newRole) {
    if (!confirm(`Are you sure you want to change this user's role to ${newRole}?`)) {
        return;
    }
    
    showLoading();
    
    try {
        await updateUserRole(userId, newRole);
        showToast(t('roleUpdated'));
        await loadAdminUsers();
    } catch (error) {
        showToast('Failed to update role', 'error');
    } finally {
        hideLoading();
    }
}

// ========================================
// ADMIN - QUESTIONS TAB
// ========================================

async function loadAdminQuestions() {
    showLoading();
    
    try {
        const questions = await getAdminQuestions();
        const tbody = document.getElementById('questions-list');
        
        if (!questions || questions.length === 0) {
            tbody.innerHTML = '<tr><td colspan="7" style="text-align: center; color: var(--text-muted);">No questions found</td></tr>';
            hideLoading();
            return;
        }
        
        tbody.innerHTML = questions.map(q => `
            <tr data-question-id="${q.id}">
                <td>${q.id}</td>
                <td title="${q.questionTextEN}">${q.questionTextEN ? q.questionTextEN.substring(0, 40) + '...' : 'N/A'}</td>
                <td>${q.category || 'General'}</td>
                <td>${q.difficulty || 1}</td>
                <td>${q.answersCount || 0}</td>
                <td>
                    <span class="${q.isActive ? 'status-active' : 'status-inactive'}">
                        ${q.isActive ? 'Active' : 'Inactive'}
                    </span>
                </td>
                <td>
                    <button class="action-btn delete delete-question-btn" data-question-id="${q.id}">🗑️ Delete</button>
                </td>
            </tr>
        `).join('');
        
        // Delete button listeners
        tbody.querySelectorAll('.delete-question-btn').forEach(btn => {
            btn.addEventListener('click', () => handleDeleteQuestion(btn.dataset.questionId));
        });
        
    } catch (error) {
        console.error('Failed to load questions:', error);
        document.getElementById('questions-list').innerHTML = 
            '<tr><td colspan="7" style="text-align: center; color: var(--error);">Failed to load questions</td></tr>';
    } finally {
        hideLoading();
    }
}

async function handleDeleteQuestion(questionId) {
    if (!confirm(t('deleteConfirm'))) {
        return;
    }
    
    showLoading();
    
    try {
        await deleteQuestion(questionId);
        showToast(t('deleted'));
        await loadAdminQuestions();
    } catch (error) {
        showToast('Failed to delete question', 'error');
    } finally {
        hideLoading();
    }
}

// ========================================
// ADMIN - ADD QUESTION MODAL
// ========================================

function initAddQuestionModal() {
    const modal = document.getElementById('add-question-modal');
    const form = document.getElementById('question-form');
    
    document.getElementById('add-question-btn').addEventListener('click', () => {
        modal.classList.add('active');
        state.answerFieldCount = 3;
        renderAnswerFields();
    });
    
    document.getElementById('close-add-question').addEventListener('click', () => {
        modal.classList.remove('active');
        form.reset();
    });
    
    document.getElementById('cancel-question').addEventListener('click', () => {
        modal.classList.remove('active');
        form.reset();
    });
    
    document.getElementById('add-answer-field').addEventListener('click', () => {
        if (state.answerFieldCount < 8) {
            state.answerFieldCount++;
            renderAnswerFields();
        }
    });
    
    form.addEventListener('submit', handleAddQuestion);
}

function renderAnswerFields() {
    const container = document.getElementById('answers-inputs');
    
    container.innerHTML = Array.from({ length: state.answerFieldCount }, (_, i) => `
        <div class="answer-input-row">
            <input type="text" placeholder="Answer EN ${i + 1}" data-field="en" required>
            <input type="text" placeholder="Answer PL ${i + 1}" data-field="pl">
            <input type="number" placeholder="Points" min="1" max="100" value="${Math.max(50 - (i * 10), 5)}" required>
            ${i >= 3 ? '<button type="button" class="remove-answer-btn">✕</button>' : '<span></span>'}
        </div>
    `).join('');
    
    container.querySelectorAll('.remove-answer-btn').forEach(btn => {
        btn.addEventListener('click', (e) => {
            e.target.closest('.answer-input-row').remove();
            state.answerFieldCount--;
        });
    });
}

async function handleAddQuestion(e) {
    e.preventDefault();
    
    const questionTextEN = document.getElementById('question-text-en').value.trim();
    const questionTextPL = document.getElementById('question-text-pl').value.trim();
    const category = document.getElementById('question-category').value;
    const difficulty = parseInt(document.getElementById('question-difficulty').value);
    
    const answersContainer = document.getElementById('answers-inputs');
    const answers = [];
    
    answersContainer.querySelectorAll('.answer-input-row').forEach((row, index) => {
        const answerEN = row.querySelector('[data-field="en"]').value.trim();
        const answerPL = row.querySelector('[data-field="pl"]').value.trim() || answerEN;
        const points = parseInt(row.querySelector('input[type="number"]').value) || 0;
        
        if (answerEN && points > 0) {
            answers.push({
                answerTextEN: answerEN,
                answerTextPL: answerPL,
                points: points,
                rank: index + 1
            });
        }
    });
    
    if (!questionTextEN || !questionTextPL || answers.length < 3) {
        showToast('Please fill question in both languages and add at least 3 answers', 'error');
        return;
    }
    
    showLoading();
    
    try {
        await createQuestion({
            questionTextEN: questionTextEN,
            questionTextPL: questionTextPL,
            category: category,
            difficulty: difficulty,
            answers: answers
        });
        
        showToast(t('saved'));
        document.getElementById('add-question-modal').classList.remove('active');
        document.getElementById('question-form').reset();
        await loadAdminQuestions();
    } catch (error) {
        showToast('Failed to save question: ' + error.message, 'error');
    } finally {
        hideLoading();
    }
}

// ========================================
// ADMIN - API KEYS TAB
// ========================================

async function loadApiKeys() {
    showLoading();
    
    try {
        const apiKeys = await getApiKeys();
        const tbody = document.getElementById('apikeys-list');
        
        if (!apiKeys || apiKeys.length === 0) {
            tbody.innerHTML = '<tr><td colspan="7" style="text-align: center; color: var(--text-muted);">No API keys found</td></tr>';
            hideLoading();
            return;
        }
        
        tbody.innerHTML = apiKeys.map(key => `
            <tr data-key-id="${key.id}">
                <td>${key.id}</td>
                <td>${key.developerName}</td>
                <td>${key.developerEmail}</td>
                <td><code>${key.keyPreview}</code></td>
                <td>
                    <span class="${key.isActive ? 'status-active' : 'status-inactive'}">
                        ${key.isActive ? 'Active' : 'Revoked'}
                    </span>
                </td>
                <td>${formatDate(key.createdAt)}</td>
                <td>
                    ${key.isActive 
                        ? `<button class="action-btn delete revoke-key-btn" data-key-id="${key.id}">Revoke</button>`
                        : '-'
                    }
                </td>
            </tr>
        `).join('');
        
        // Revoke button listeners
        tbody.querySelectorAll('.revoke-key-btn').forEach(btn => {
            btn.addEventListener('click', () => handleRevokeApiKey(btn.dataset.keyId));
        });
        
    } catch (error) {
        console.error('Failed to load API keys:', error);
        document.getElementById('apikeys-list').innerHTML = 
            '<tr><td colspan="7" style="text-align: center; color: var(--error);">Failed to load API keys</td></tr>';
    } finally {
        hideLoading();
    }
}

async function handleRevokeApiKey(keyId) {
    if (!confirm('Are you sure you want to revoke this API key? This cannot be undone.')) {
        return;
    }
    
    showLoading();
    
    try {
        await revokeApiKey(keyId);
        showToast(t('apiKeyRevoked'));
        await loadApiKeys();
    } catch (error) {
        showToast('Failed to revoke API key', 'error');
    } finally {
        hideLoading();
    }
}

function initApiKeyModal() {
    const modal = document.getElementById('apikey-modal');
    const form = document.getElementById('apikey-form');
    
    document.getElementById('generate-apikey-btn').addEventListener('click', () => {
        modal.classList.add('active');
        document.getElementById('apikey-result').classList.add('hidden');
        document.getElementById('apikey-form').style.display = 'block';
        form.reset();
    });
    
    document.getElementById('close-apikey-modal').addEventListener('click', () => {
        modal.classList.remove('active');
        form.reset();
    });
    
    document.getElementById('cancel-apikey').addEventListener('click', () => {
        modal.classList.remove('active');
        form.reset();
    });
    
    form.addEventListener('submit', handleGenerateApiKey);
    
    document.getElementById('copy-apikey').addEventListener('click', () => {
        const apiKey = document.getElementById('generated-apikey').textContent;
        navigator.clipboard.writeText(apiKey).then(() => {
            showToast(t('copySuccess'));
        });
    });
}

async function handleGenerateApiKey(e) {
    e.preventDefault();
    
    const developerName = document.getElementById('developer-name').value.trim();
    const developerEmail = document.getElementById('developer-email').value.trim();
    
    if (!developerName || !developerEmail) {
        showToast('Please fill all fields', 'error');
        return;
    }
    
    showLoading();
    
    try {
        const result = await generateApiKey(developerName, developerEmail);
        
        document.getElementById('generated-apikey').textContent = result.apiKey;
        document.getElementById('apikey-result').classList.remove('hidden');
        document.getElementById('apikey-form').style.display = 'none';
        
        showToast(t('apiKeyGenerated'));
        await loadApiKeys();
    } catch (error) {
        showToast('Failed to generate API key: ' + error.message, 'error');
    } finally {
        hideLoading();
    }
}

// ========================================
// ADMIN - SETTINGS TAB
// ========================================

function initSettingsHandlers() {
    // Setting buttons
    document.querySelectorAll('.setting-btn').forEach(btn => {
        btn.addEventListener('click', () => {
            const setting = btn.dataset.setting;
            const value = parseInt(btn.dataset.value);
            
            // Update active state
            document.querySelectorAll(`.setting-btn[data-setting="${setting}"]`).forEach(b => {
                b.classList.remove('active');
            });
            btn.classList.add('active');
            
            // Update state
            if (setting === 'timer') state.settings.timer = value;
            else if (setting === 'strikes') state.settings.strikes = value;
            else if (setting === 'questions') state.settings.questionsPerGame = value;
            else if (setting === 'bonus') state.settings.bonusMultiplier = value;
        });
    });
    
    // Save settings button
    document.getElementById('save-settings-btn').addEventListener('click', saveSettings);
}

function loadSettings() {
    // Set active buttons based on current settings
    document.querySelectorAll('.setting-btn').forEach(btn => {
        const setting = btn.dataset.setting;
        const value = parseInt(btn.dataset.value);
        let currentValue;
        
        if (setting === 'timer') currentValue = state.settings.timer;
        else if (setting === 'strikes') currentValue = state.settings.strikes;
        else if (setting === 'questions') currentValue = state.settings.questionsPerGame;
        else if (setting === 'bonus') currentValue = state.settings.bonusMultiplier;
        
        btn.classList.toggle('active', value === currentValue);
    });
}

function saveSettings() {
    localStorage.setItem(CONFIG.SETTINGS_KEY, JSON.stringify(state.settings));
    showToast(t('settingsSaved'));
}

// ========================================
// LANGUAGE
// ========================================

function initLanguage() {
    const langBtns = document.querySelectorAll('.lang-btn');
    
    langBtns.forEach(btn => {
        btn.classList.toggle('active', btn.dataset.lang === state.currentLang);
    });
    
    langBtns.forEach(btn => {
        btn.addEventListener('click', () => {
            const newLang = btn.dataset.lang;
            
            if (newLang !== state.currentLang) {
                state.currentLang = newLang;
                localStorage.setItem(CONFIG.LANG_KEY, newLang);
                
                langBtns.forEach(b => b.classList.remove('active'));
                btn.classList.add('active');
                
                updateTranslations();
                
                // Əgər oyun davam edirsə, sualı yenilə
                if (state.currentQuestion) {
                    renderQuestion();
                }
            }
        });
    });
    
    updateTranslations();
}

// ========================================
// MODALS
// ========================================

function initModalHandlers() {
    document.querySelectorAll('.modal').forEach(modal => {
        modal.addEventListener('click', (e) => {
            if (e.target === modal) {
                modal.classList.remove('active');
            }
        });
    });
}

// ========================================
// INITIALIZATION
// ========================================

function checkAuthOnLoad() {
    if (state.token && state.user) {
        initDashboard();
        showSection('dashboard-section');
    } else {
        showSection('auth-section');
    }
}

document.addEventListener('DOMContentLoaded', () => {
    initLanguage();
    initAuthHandlers();
    initDashboardHandlers();
    initGameHandlers();
    initModalHandlers();
    checkAuthOnLoad();
    
    // Set API base URL in admin panel
    const apiUrlEl = document.getElementById('api-base-url');
    if (apiUrlEl) {
        apiUrlEl.textContent = CONFIG.API_BASE_URL;
    }
    
    console.log('Family Feud v2.0 - Full Admin Panel initialized!');
});