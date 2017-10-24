angular.module('serviceModule').controller('socialServiceCtrl', function($scope, $http, $alert, $modal) {
    $scope.services = [];
    $scope.alerts = [];

    $scope.allServices = [
        {
            Name: 'facebook',
            Link:'',
        },
        {
            Name: 'instagram',
            Link: '',
        },
        {
            Name: 'twitter',
            Link: '',
        },
        {
            Name: 'googleplus',
            Link: '',
        },
        {
            Name: 'skype',
            Link: '',
        },
    ];

    $scope.init = function() {
        var url = LOTUS_INN_URL + '/api/services/getSocialServices';
        var request = {
            method: 'GET',
            url: url,
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            }
        };
        $http(request).success(function(data) {
            $scope.services = data;
        });
    };

    $scope.remove = function(svc) {
        _.remove($scope.services, { Name: svc.Name });
    }

    $scope.add = function () {
        var serviceList = _.filter($scope.allServices, function(svc) {
            return !_.find($scope.services, { Name: svc.Name });
        });

        var modal = $modal.open({
            templateUrl: 'addSocialModal.html',
            controller: 'addSocialModalCtrl',
            resolve: {
                serviceList: function () { return serviceList; }
            }
        });

        modal.result.then(function(selectedSvc) {
            _.forEach(selectedSvc, function(item) {
                $scope.services.push(item);
            });
        });
    }

    $scope.save = function () {
        var request = {
            method: 'POST',
            url: LOTUS_INN_URL + '/api/services/savesocialservices',
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            },
            data: $scope.services
        };
        $http(request).then(function(response) {
            $alert.success($scope.alerts, 'Data saved.');
        });
    };

    $scope.back = function() {
        window.location.href = "/";
    }

}).controller('addSocialModalCtrl', function ($scope, $modalInstance, serviceList) {
    $scope.serviceList = serviceList;

    $scope.ok = function() {
        var selectedServices = _.filter($scope.serviceList, { checked: true });
        $modalInstance.close(selectedServices);
    }

    $scope.cancel = function() {
        $modalInstance.dismiss('cancel');
    }
});