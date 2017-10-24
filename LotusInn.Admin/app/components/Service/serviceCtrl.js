angular.module('serviceModule').controller('articleAddEditCtrl', function ($scope, $http, $alert, $modal) {
    $scope.service = {};
    $scope.alerts = [];
    $scope.options = {
        language: 'en',
        allowedContent: true,
        entities: false
    };        
    
    $scope.init = function () {
        var url = LOTUS_INN_URL + '/api/services/getServices';
        var request = {
            method: 'GET',
            url: url,
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            }
        };
        $http(request).success(function (data) {
            $scope.service = data;
        });
    };

    $scope.save = function () {
        var url = LOTUS_INN_URL + '/api/services/save';
        var request = {
            method: 'POST',
            url: url,
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            },
            data: $scope.service
        };
        $http(request).success(function () {
            $alert.success($scope.alerts, 'Data saved.');
        }).error(function (err) {
            $alert.error($scope.alerts, err.ExceptionMessage);
        });
    };

    $scope.back = function () {
        window.location.href = "/";
    }

});