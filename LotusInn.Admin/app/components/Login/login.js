var module = angular.module('lotusinn.admin.login',
[
    'ipCookie',
    'tp'
]);

module.controller('loginCtrl', function($scope, $http, ipCookie) {
    $scope.error = {
        message: ''
    };

    $scope.loginInfo = {
        userName: '',
        password: '',
    }

    $scope.login = function() {
        $http.post('/api/account/login', $scope.loginInfo).then(function(response) {
            ipCookie('AuthData', response.data, { path: '/', expires: 2, expirationUnit: 'hours' });
            location.href = "/";
        }, function(err) {
            $scope.error.message = err.data.ExceptionMessage;
        });
    }
});