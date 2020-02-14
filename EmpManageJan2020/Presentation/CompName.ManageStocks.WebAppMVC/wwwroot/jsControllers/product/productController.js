(
    function (publicMethod, $) {
        publicMethod.addProductOnBegin = function (xhr, data) {
            alert();
            sharedController.showLoadingIndicator();
        }

        publicMethod.addProductOnComplete = function (xhr, data) {
            sharedController.hideLoadingIndicator();
        }

        publicMethod.addProductOnSuccess = function (data, status, xhr) {
           
            
        }

        publicMethod.addProductOnfailure = function (xMLHttpRequest, textStatus, errorThrown) {
            sharedController.hideLoadingIndicator();
            sharedController.showAjaxErrorMessagePopUp(xMLHttpRequest, textStatus, errorThrown);
        }
    }(window.productController = window.productController || {}, jQuery)
);