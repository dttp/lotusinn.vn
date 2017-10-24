lotusInnAdmin.controller('houseCtrl', function ($scope, $http, $modal) {
    $scope.houses = [];

    $scope.createNew = function() {
        var url = LOTUS_INN_URL + '/api/house/createHouse';
        var request = {
            url: url,
            method: 'POST',
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            }
        };
        $http(request).success(function(data) {
            $scope.houses.push(data);
        });
    }

    $scope.edit = function(house) {
        window.location.href = '/house/edit?hid=' + house.Id;
    }

    $scope.delete = function(house) {
        var url = LOTUS_INN_URL + '/api/house/DeleteHouse?id=' + house.Id ;
        var request = {
            url: url,
            method: 'DELETE',
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            }
        };
        $http(request).success(function (data) {
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
        // ---- get houses
        var url = LOTUS_INN_URL + '/api/house/GetHouses';
        $http.get(url).success(function(data) {
            $scope.houses = data.Houses;
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