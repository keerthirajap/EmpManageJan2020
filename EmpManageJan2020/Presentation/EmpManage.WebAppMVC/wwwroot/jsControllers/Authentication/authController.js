(
    function (publicMethod, $) {
        publicMethod.registerUserOnBegin = function (xhr, data) {
            sharedController.showLoadingIndicator();
        }

        publicMethod.registerUserOnComplete = function (xhr, data) {
            sharedController.hideLoadingIndicator();
        }

        publicMethod.registerUserOnSuccess = function (data, status, xhr) {
            if (jQuery.type(data.Status) === "undefined") {
            }

            else {
                swalWithBootstrapButtons.fire({
                    icon: 'success',
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
                        sharedController.redirectToHomePage();
                    }
                })
            }
        }

        publicMethod.registerUserOnfailure = function (xMLHttpRequest, textStatus, errorThrown) {
            sharedController.hideLoadingIndicator();
            sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
        }

        publicMethod.loginOnBegin = function (xhr, data) {
            sharedController.showLoadingIndicator();
        }

        publicMethod.loginOnComplete = function (xhr, data) {
            sharedController.hideLoadingIndicator();
        }

        publicMethod.loginOnSuccess = function (data, status, xhr) {
            if (jQuery.type(data.Status) === "undefined") {
            }
            else if (data.Status == "Warning") {
                $('#modalMessageShowPopUpHeaderTitle').text(data.Title);
                $('#modalMessageShowPopUpMessage').text(data.Message);
                $('#modalMessageShowPopUp').modal('show');
            }
            else if (data.Status == "Success") {
                sharedController.redirectToHomePage();
            }
        }

        publicMethod.loginOnfailure = function (xMLHttpRequest, textStatus, errorThrown) {
            sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);          
        }

        

    }(window.authController = window.authController || {}, jQuery)
);