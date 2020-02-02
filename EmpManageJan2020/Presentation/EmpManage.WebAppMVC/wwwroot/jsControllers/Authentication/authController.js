(
    function (publicMethod, $) {
        publicMethod.registerUserOnBegin = function (xhr, data) {
            homeController.showLoadingIndicator();
        }

        publicMethod.registerUserOnComplete = function (xhr, data) {
            homeController.hideLoadingIndicator();
        }

        publicMethod.registerUserOnSuccess = function (data, status, xhr) {
            if (jQuery.type(data.Status) === "undefined") {
            }

            else {
                swalWithBootstrapButtons.fire({
                    title: data.Title,
                    text: data.Message,
                    timer: 4000,
                    timerProgressBar: true,
                    showCancelButton: false,
                    confirmButtonText: '<i class="fas fa-home"></i> Go to Home',
                    onBeforeOpen: () => {
                    },
                    onClose: () => {
                    }
                }).then((result) => {
                    if (result.dismiss === Swal.DismissReason.timer) {
                        homeController.redirectToHomePage();
                    }
                })
            }
        }

        publicMethod.registerUserOnfailure = function (xMLHttpRequest, textStatus, errorThrown) {
        }

        publicMethod.loginOnBegin = function (xhr, data) {
            homeController.showLoadingIndicator();
        }

        publicMethod.loginOnComplete = function (xhr, data) {
            homeController.hideLoadingIndicator();
        }

        publicMethod.loginOnSuccess = function (data, status, xhr) {
        }

        publicMethod.loginOnfailure = function (xMLHttpRequest, textStatus, errorThrown) {
        }
    }(window.authController = window.authController || {}, jQuery)
);