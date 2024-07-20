export function HookSelectDropdownAwayClick(blazor) {
    document.body.addEventListener('click', function (e) {
        let classList = e.target.classList;

        for (var i = 0; i < classList.length; i++) {
            if (classList[i].includes('c-select') == false) {
                blazor.invokeMethodAsync('ShowOptions', false);
            }
        }
    });
}