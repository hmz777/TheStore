let navbar = document.getElementById("navbar");
let blazorRef, navDetached, oldScroll;

export function InitNavbarObservability(blazor) {

    blazorRef = blazor;

    window.onscroll = function (e) {

        navDetached = (this.scrollY >= 5);
        blazorRef.invokeMethodAsync("DetachNavBar", navDetached);

        if (navDetached) {
            if ((oldScroll > this.scrollY)) {
                navbar.classList.remove("is-hidden");
            }
            else {
                navbar.classList.add("is-hidden");
            }
        }

        oldScroll = this.scrollY;
    }
}