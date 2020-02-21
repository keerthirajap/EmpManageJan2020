class AuthController {
    constructor() {
      
    }

    registerUserOnBegin (xhr, data) {
        sharedController.showLoadingIndicator();
    }

      registerUserOnComplete  (xhr, data) {
        sharedController.hideLoadingIndicator();
    }

     registerUserOnSuccess  (data, status, xhr) {
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
                showConfirmButton: false,
                allowOutsideClick: false,
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

     registerUserOnfailure  (xMLHttpRequest, textStatus, errorThrown) {
        sharedController.hideLoadingIndicator();
        sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
    }

    // #endregion Register User

    // #region login User

     loginOnBegin (xhr, data) {
        sharedController.showLoadingIndicator();
    }

      loginOnComplete  (xhr, data) {
        sharedController.hideLoadingIndicator();
    }

      loginOnSuccess (data, status, xhr) {
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

     loginOnfailure (xMLHttpRequest, textStatus, errorThrown) {
        sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
    }

};

