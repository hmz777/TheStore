var alert = document.querySelectorAll(".c-alert-close").forEach(function (alert) {
    alert.addEventListener('click', function (event) {
        event.target.parentElement.parentElement.remove();
    })
});