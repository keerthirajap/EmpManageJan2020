class UserManagementController {
    constructor() {

    }

    updateUserDetailsOnBegin  (xhr, data) {
        sharedController.showLoadingIndicator();
    }

    updateUserDetailsOnComplete  (xhr, data) {
        sharedController.hideLoadingIndicator();
    }

    updateUserDetailsOnSuccess  (data, status, xhr) {
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

    updateUserDetailsOnfailure  (xMLHttpRequest, textStatus, errorThrown) {
        sharedController.hideLoadingIndicator();
        sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
    }

    updateUserAccountActiveStatus  (actionUrl) {
        sharedController.showLoadingIndicator();

        var userId = $('#txtUserId').val();
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

    UpdateUserAccountLockedStatus  (actionUrl) {
        sharedController.showLoadingIndicator();

        var userId = $('#txtUserId').val();
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

    clearUserDetailsOnClick  () {
        $('#txtFirstName').val('');
        $('#txtLastName').val('');
        $("#selectUserTitle").prop('selectedIndex', 0);
        $("#selectUserGender").prop('selectedIndex', 0);
    }

    changePasswordOnClick  (actionUrl) {
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

    changeUserAccountPasswordOnBegin  (xhr, data) {
        sharedController.showLoadingIndicator();
    }

    changeUserAccountPasswordOnComplete  (xhr, data) {
        sharedController.hideLoadingIndicator();
    }

    changeUserAccountPasswordOnSuccess  (data, status, xhr) {
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

    changeUserAccountPasswordOnfailure  (xMLHttpRequest, textStatus, errorThrown) {
        sharedController.hideLoadingIndicator();
        sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
    }

}
