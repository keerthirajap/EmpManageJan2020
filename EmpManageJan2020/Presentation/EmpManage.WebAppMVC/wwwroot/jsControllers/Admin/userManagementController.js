(
    function (publicMethod, $) {
        publicMethod.saveUserDetailsOnBegin = function (xhr, data) {
            sharedController.showLoadingIndicator();
        }

        publicMethod.saveUserDetailsOnComplete = function (xhr, data) {
            sharedController.hideLoadingIndicator();
        }

        publicMethod.saveUserDetailsOnSuccess = function (data, status, xhr) {
            if (jQuery.type(data.Status) === "undefined") {
            }

            else {
            }
        }

        publicMethod.saveUserDetailsOnfailure = function (xMLHttpRequest, textStatus, errorThrown) {
            sharedController.hideLoadingIndicator();
            sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
        }

        publicMethod.updateUserAccountActiveStatus = function (actionUrl) {
            sharedController.showLoadingIndicator();

            var userId = $('#hdnUserId').val();
            var isActive = $('#chkIsActive').is(":checked");

            $.ajax({
                type: "POST",
                url: actionUrl,
                data: { userId: userId, isActive: isActive },
                datatype: "json",
                headers: {
                    "RequestVerificationToken": $('input[name = __RequestVerificationToken]').val()
                },
                begin: function () {
                },
                complete: function () {
                    sharedController.hideLoadingIndicator();
                },
                success: function (data) {
                    if (data.Status == "Success") {
                        swalWithBootstrapButtons.fire({
                            icon: 'success',
                            text: data.Message,
                            timer: 3000,
                            timerProgressBar: true,
                            showCancelButton: false,
                            showConfirmButton: false,
                            onBeforeOpen: () => {
                            },
                            onClose: () => {
                            }
                        }).then((result) => {
                            if (result.dismiss === Swal.DismissReason.timer) {
                            }
                        })
                    }
                },
                error: function (xMLHttpRequest, textStatus, errorThrown) {
                    sharedController.hideLoadingIndicator();
                    sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
                }
            });

            $('#modalChangeUserAccountStatusPopUp').modal('hide');

        }
    }(window.userManagementController = window.userManagementController || {}, jQuery)
);