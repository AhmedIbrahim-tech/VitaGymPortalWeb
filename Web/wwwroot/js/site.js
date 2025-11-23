const sidebar = document.getElementById('sidebar');
const sidebarToggle = document.getElementById('sidebarToggle');
const sidebarOverlay = document.getElementById('sidebarOverlay');
const mainContent = document.getElementById('mainContent');

sidebarToggle.addEventListener("click", () => {
    sidebar.classList.toggle("show");
    sidebarOverlay.classList.toggle("show");
});

sidebarOverlay.addEventListener("click", () => {
    sidebar.classList.remove("show");
    sidebarOverlay.classList.remove("show");
});

const html = document.documentElement;
const icon = document.getElementById('theme-icon');
const toggle = document.getElementById('theme-toggle');

function applyTheme(theme) {
    html.setAttribute('data-bs-theme', theme);
    icon.textContent = theme === 'dark' ? 'light_mode' : 'dark_mode';
}

function saveTheme(theme) {
    localStorage.setItem('theme', theme);
}

function getPreferredTheme() {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme) {
        return savedTheme;
    }
    // If not saved, detect system preference
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
}

function initTheme() {
    const theme = getPreferredTheme();
    applyTheme(theme);
}

// Initialize theme on page load
initTheme();

// Handle toggle click
toggle.addEventListener('click', () => {
    const current = html.getAttribute('data-bs-theme');
    const newTheme = current === 'dark' ? 'light' : 'dark';
    applyTheme(newTheme);
    saveTheme(newTheme);
});

// Listen for system theme changes (optional)
window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', e => {
    if (!localStorage.getItem('theme')) {
        const newTheme = e.matches ? 'dark' : 'light';
        applyTheme(newTheme);
    }
});