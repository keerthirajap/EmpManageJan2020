(
    function (publicMethod, $) {
        publicMethod.showLoadingIndicator = function () {
            document.getElementById("myNav").style.height = "100%";
        }

        publicMethod.hideLoadingIndicator = function () {
            setTimeout(
                function () {
                    document.getElementById("myNav").style.height = "0%";
                }, 500);
        }

        publicMethod.redirectToHomePage = function () {
            homeController.showLoadingIndicator();
            var url = "/";
            window.location.href = url;
        }
    }(window.homeController = window.homeController || {}, jQuery)
);