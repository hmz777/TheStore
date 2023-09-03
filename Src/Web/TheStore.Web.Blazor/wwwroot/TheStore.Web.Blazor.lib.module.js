var beforeCalled, afterCalled;

export function beforeStart(options, extensions) {

    if (beforeCalled) {
        return;
    }

    let mudCss = document.createElement('link');
    mudCss.setAttribute("rel", "stylesheet");
    mudCss.setAttribute("href", "_content/MudBlazor/MudBlazor.min.css");

    let mainCss = document.createElement('link');
    mainCss.setAttribute("rel", "stylesheet");
    mainCss.setAttribute("href", "css/main.min.css");

    let blazorAuth = document.createElement('script');
    blazorAuth.setAttribute("src", "_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js");

    let mudJs = document.createElement('script');
    mudJs.setAttribute("src", "_content/MudBlazor/MudBlazor.min.js");

    let bootstrapJs = document.createElement('script');
    bootstrapJs.setAttribute("src", "vendor/bootstrap/js/bootstrap.bundle.min.js");

    let app = document.getElementById("app")

    document.head.appendChild(mudCss);
    document.head.appendChild(mainCss);

    app.after(blazorAuth);
    document.body.appendChild(mudJs);
    document.body.appendChild(bootstrapJs);

    beforeCalled = true;
}

export function afterStarted(blazor) {
    if (afterCalled) {
        return;
    }

    afterCalled = true;
}