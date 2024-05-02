export function HookSelectDropdownAwayClick(blazor) {

    document.body.addEventListener('click', function (e) {
        if (e.target.classList.contains('c-select-options') == false &&
            e.target.classList.contains('c-select-option') == false) {
            blazor.invokeMethodAsync('ShowOptions', false);
        }
    });
}