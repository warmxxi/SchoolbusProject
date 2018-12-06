angular.module('main').controller('rootController', ['$scope', '$route', '$routeParams', '$location', '$controller', '$filter', 'rootService', 'constants',
    function ($scope, $route, $routeParams, $location, $controller, $filter, rootService, constants) {
        $controller('baseController', { $scope: $scope });

        
    }
]);