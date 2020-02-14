(
    function (publicMethod, $) {
        publicMethod.updateUserDetailsOnBegin = function (xhr, data) {
            sharedController.showLoadingIndicator();
        }

        publicMethod.updateUserDetailsOnComplete = function (xhr, data) {
            sharedController.hideLoadingIndicator();
        }

        publicMethod.updateUserDetailsOnSuccess = function (data, status, xhr) {
            if (jQuery.type(data.Status) === "undefined") {
            }

            else {
                swalWithBootstrapButtons.fire({
                    icon: 'success',
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
                        location.reload();
                    }
                })
            }
        }

        publicMethod.updateUserDetailsOnfailure = function (xMLHttpRequest, textStatus, errorThrown) {
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

        publicMethod.UpdateUserAccountLockedStatus = function (actionUrl) {
            sharedController.showLoadingIndicator();

            var userId = $('#hdnUserId').val();
            var isLocked = $('#chkIsLocked').is(":checked");

            $.ajax({
                type: "POST",
                url: actionUrl,
                data: { userId: userId, isLocked: isLocked },
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

            $('#modalChangeUserAccountLockStatusPopUp').modal('hide');
        }

        publicMethod.clearUserDetailsOnClick = function () {
            $('#txtFirstName').val('');
            $('#txtLastName').val('');
            $("#selectUserTitle").prop('selectedIndex', 0);
            $("#selectUserGender").prop('selectedIndex', 0);
        }

        publicMethod.changePasswordOnClick = function (actionUrl) {
            sharedController.showLoadingIndicator();
            var userId = $('#hdnUserId').val();
            var userName = $('#txtUserName').val();

            $.ajax({
                async: true,
                type: "GET",
                url: actionUrl,
                data: { userId: userId, userName: userName },
                datatype: "json",
                headers: {
                    "RequestVerificationToken": $('input[name = __RequestVerificationToken]').val()
                },
                begin: function () {
                },
                complete: function () {
                },
                success: function (data) {
                    $('#loadChangePasswordPartialView').append(data);
                    setTimeout(
                        function () {
                            $('#modalChangeUserAccountPasswordPopUp').modal('show');
                            sharedController.hideLoadingIndicator();
                        }, 200);
                },
                error: function (xMLHttpRequest, textStatus, errorThrown) {
                    sharedController.hideLoadingIndicator();
                    sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
                }
            });
        }

     

            publicMethod.changeUserAccountPasswordOnBegin = function (xhr, data) {
            sharedController.showLoadingIndicator();
        }

        publicMethod.changeUserAccountPasswordOnComplete = function (xhr, data) {
          
            sharedController.hideLoadingIndicator();
        }

        publicMethod.changeUserAccountPasswordOnSuccess = function (data, status, xhr) {
            $('#modalChangeUserAccountPasswordPopUp').modal('hide');
            $('#modalChangeUserAccountPasswordPopUp').remove();
            if (jQuery.type(data.Status) === "undefined") {
            }

            else {
                swalWithBootstrapButtons.fire({
                    icon: 'success',
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
                        
                    }
                })
            }
        }

        publicMethod.changeUserAccountPasswordOnfailure = function (xMLHttpRequest, textStatus, errorThrown) {
            sharedController.hideLoadingIndicator();
            sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
        }
    }(window.userManagementController = window.userManagementController || {}, jQuery)
);