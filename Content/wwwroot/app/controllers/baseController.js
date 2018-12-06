angular.module('main').controller('baseController', function ($scope, $location, appShareData) {
    if (typeof appShareData.formMode == "undefined") {
        appShareData.formMode = "add";
    }

    if (typeof appShareData.detailFormMode == "undefined") {
        appShareData.detailFormMode = "add";
    }

    $scope.setDirty = function (form) {
        angular.forEach(form, function (val, key) {
            if (!key.match(/\$/)) {
                val.$dirty = true;
            }
        });
    }

    $scope.appShareData = appShareData;
});