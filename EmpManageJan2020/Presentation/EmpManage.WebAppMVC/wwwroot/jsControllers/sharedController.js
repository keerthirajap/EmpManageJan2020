(
    function (publicMethod, $) {
        publicMethod.showAjaxErrorMessagePopUp = function (xMLHttpRequest, textStatus, errorThrown) {
            if (xMLHttpRequest.status == '401') {
                $('#ajaxAccessDeniedMessagePopUp').modal('show');

                setTimeout(
                    function () {
                        sharedController.showLoadingIndicator();
                        return "false";
                    }, 3000);

                setTimeout(
                    function () {
                        location.reload();
                        return "false";
                    }, 3500);
            }
            else {
                if (xMLHttpRequest.getResponseHeader('RequestTime') != null) {
                    $('#RequestTime').text('Occured At : ' + xMLHttpRequest.getResponseHeader('RequestTime'));
                }
                else {
                    $('#RequestTime').text('');
                }

                if (xMLHttpRequest.getResponseHeader('RequestId') != null) {
                    $('#RequestId').text('Request Id : ' + xMLHttpRequest.getResponseHeader('RequestId'));
                }
                else {
                    $('#RequestId').text('');
                }

                $('#ajaxErrorMessagePopUp').modal('show');
            }

            sharedController.hideLoadingIndicator();
        }

        publicMethod.navActiveColorChange = function (navBarId) {
            $('[id^="nav-Item"]').removeClass('active');

            setTimeout(
                function () {
                    $("#" + navBarId).addClass('active');
                }, 200);
        }

        publicMethod.generateGuid = function () {
            return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
                return v.toString(16);
            });
        }

        publicMethod.showLoadingIndicator = function () {
            document.getElementById("myNav").style.height = "100%";
        }

        publicMethod.hideLoadingIndicator = function () {
            setTimeout(
                function () {
                    document.getElementById("myNav").style.height = "0%";
                }, 500);
        }

        publicMethod.redirectToUrl = function (url) {
            sharedController.showLoadingIndicator();
            window.location.href = url;
        },

        publicMethod.redirectToHomePage = function () {
            sharedController.showLoadingIndicator();
            var url = "/";
            window.location.href = url;
        }

        publicMethod.createCookie = function (cookieName, value, days) {
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                var expires = "; expires=" + date.toGMTString();
            }
            else var expires = "";

            document.cookie = name + "=" + value + expires + "; path=/";
        }

        publicMethod.readCookie = function (cookieName) {
            var name = cookieName + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }

        publicMethod.eraseCookie = function (cookieName) {
            document.cookie = cookieName + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
        }
    }(window.sharedController = window.sharedController || {}, jQuery)
);