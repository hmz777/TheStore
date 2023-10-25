let appBackdrop = document.getElementById("app-backdrop");

export function ToggleBackdrop(state) {
    if (state) {
        appBackdrop.classList.add("enabled");
    }
    else {
        appBackdrop.classList.remove("enabled");
    }
}

export function InitBootstrapPopovers() {
    const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]')
    const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl))
}