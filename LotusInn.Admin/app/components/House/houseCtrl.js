lotusInnAdmin.controller('houseCtrl', function ($scope, $http, $modal, $house) {
    $scope.houses = [];

    $scope.createNew = function () {
        $house.createHouse().then(function(response) {
            $scope.init();
        });
    }

    $scope.edit = function(house) {
        window.location.href = '/house/edit?hid=' + house.Id;
    }

    $scope.delete = function (house) {
        $house.delete(house.Id).then(function() {
            $scope.init();
        });
    }

    $scope.confirmDelete = function(house) {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'confirmDeleteModal.html',
            controller: 'ModalInstanceCtrl'
        });
        modalInstance.result.then(function () {
            $scope.delete(house);
        }, function () {
        });
    }

    $scope.init = function () {
        $house.getHouses().then(function(response) {
            $scope.houses = response.data;
        });
    }
});

lotusInnAdmin.controller('ModalInstanceCtrl', function ($scope, $modalInstance) {
    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
})