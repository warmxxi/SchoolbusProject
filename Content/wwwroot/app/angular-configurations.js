angular.module('main').config(['$routeProvider', '$locationProvider', '$httpProvider', function ($routeProvider, $locationProvider, $httpProvider) {
    $locationProvider.html5Mode({
        enables: true,
        requireBase: false
    }).hashPrefix('!');

    $httpProvider.defaults.headers.common['Cache-Control'] = "no-cache";
    $httpProvider.defaults.headers.common.Pragma = "no-cache";
    $httpProvider.defaults.headers.common["If-Modified-Since"] = "0";

    var pendingRequest = 0;

    $httpProvider.interceptors.push(function ($q, $rootScope) {
        return {
            request: function (config) {
                loadingOn();
                $rootScope.errorList = [];

                var body = document.body,
                    html = document.documentElement;

                var value = Math.max(body.scrollHeight, body.offsetHeight,
                    html.clientHeight, html.scrollHeight, html.offsetHeight);

                pendingRequest++;

                return config;
            }, requestError: function (rejection) {
                pendingRequest--;
                if (pendingRequest <= 0) {
                    pendingRequest = 0;
                    //$(".loading").hide();
                    loadingOff();
                }

                if ($rootScope.errorList == undefined) {
                    $rootScope.errorList = [];
                }
                $rootScope.errorList = rejection.data.errors;

                angular.injector(['ng', 'main']).get("vcRecaptchaService").reload();

                return $q.reject(rejection);
            }, response: function (response) {
                pendingRequest--;
                if (pendingRequest <= 0) {
                    pendingRequest = 0;
                    //$(".loading").hide();
                    loadingOff();
                }

                if ($rootScope.errorList == undefined) {
                    $rootScope.errorList = [];
                }

                if (response.headers('ExceptionOccured') == 'ExceptionOccured') {
                    for (i = 0; i < response.data.errors.length; i++) {
                        $rootScope.errorList.push(response.data.errors[i]);
                    }

                    $('html, body').animate({ scrollTop: 0 }, 200);
                    angular.injector(['ng', 'main']).get("vcRecaptchaService").reload();

                    return $q.reject(response);
                }
                else {
                    return response;
                }
            }, responseError: function (rejection) {
                pendingRequest--;

                if (pendingRequest <= 0) {
                    pendingRequest = 0;
                    //$(".loading").hide();
                    loadingOff();
                }

                if ($rootScope.errorList == undefined) {
                    $rootScope.errorList = [];
                }

                for (i = 0; i < rejection.data.errors.length; i++) {
                    $rootScope.errorList.push(rejection.data.errors[i]);
                }

                $('html, body').animate({ scrollTop: 0 }, 200);

                angular.injector(['ng', 'main']).get("vcRecaptchaService").reload();

                return $q.reject(rejection);
            }
        };
    });
}]);

angular.module('main').factory("appShareData", function () {
    return {

    };
});

angular.module('main').filter('currency', ['$filter', function ($filter) {
    return function (input) {
        input = parseFloat(input);

        if (input % 1 === 0) {
            input = input.toFixed(0);
        }
        else {
            input = input.toFixed(2);
        }

        return input.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    };
}]);