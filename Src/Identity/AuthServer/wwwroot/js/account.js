document.querySelectorAll(".c-form").forEach(function (form) {
    form.addEventListener('submit', function () {
        var formElements = form.querySelectorAll(".form-element");

        formElements.forEach(function (formElement) {
            formElement.querySelectorAll('[type="submit"]').forEach(function (input) {
                formElement.classList.add("disabled");
                input.disabled = true;
            })
        });
    });
});

document.querySelectorAll(".edit-form-btn").forEach(function (editBtn) {
    editBtn.addEventListener('click', function () {
        let element = editBtn;
        let elementText = editBtn.textContent;
        let elementToggleText = editBtn.dataset.toggleText;

        while (element = element.parentElement) {
            if (element.classList.contains('info-section')) {
                element.querySelectorAll('.form-element').forEach(function (formElement) {
                    if (formElement.classList.contains("disabled")) {
                        formElement.classList.remove("disabled");
                    }
                    else {
                        formElement.classList.add("disabled");
                    }

                    formElement.querySelectorAll('input').forEach(function (input) {
                        input.disabled = !input.disabled;
                    });
                });

                editBtn.textContent = elementToggleText;
                editBtn.dataset.toggleText = elementText;

                break;
            }
        }
    })
});