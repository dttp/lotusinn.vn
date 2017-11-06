
lotusInnAdmin.controller('houseAddEditCtrl', function ($scope, $http, $modal, $alert, $house, $roomType) {
    $scope.house = {};
    $scope.roomTypes = [];
    $scope.alerts = [];
    $scope.mapInfo = {
        center: "0, 0",
        zoom: 14,
        markers: []
    };
    $scope.googleMapsUrl = "https://maps.googleapis.com/maps/api/js?key=AIzaSyDiLZcUtGLep2FLEkjG8xT9ZrXv1K0ERdc";

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    $scope.$on('mapInitialized', function (evt, evtMap) {        
        $scope.placeMarker = function (e) {
            $scope.mapInfo.markers = [];
            $scope.mapInfo.markers.push({
                position: e.latLng.lat() + "," + e.latLng.lng()
            });
            $scope.house.Latitude = e.latLng.lat();
            $scope.house.Longitude = e.latLng.lng();            
        }
    });

    $scope.save = function () {
        $house.update($scope.house).then(function() {
            $alert.success($scope.alerts, 'House info saved.');
        }, function(err) {
            $alert.error($scope.alerts, 'Something went wrong. ' + err.data.Message);
        });
    }

    $scope.createRoomType = function() {
        $roomType.create($scope.house.Id).then(function(response) {
            $scope.roomTypes.push(response.data);
        });        
    }

    $scope.editRoomType = function(room) {
        window.location.href = '/house/roomType?id=' + room.Id;
    }

    $scope.deleteRoomType = function (room) {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'confirmDeleteModal.html',
            controller: 'ModalInstanceCtrl'
        });
        modalInstance.result.then(function () {
            $roomType.delete(room.Id).then(function () {
                _.remove($scope.roomTypes, { Id: room.Id });
            });
        });
    }

    $scope.init = function () {
        // ---- get house
        var hid = getParameterByName('hid');
        if (hid) {
            $house.getById(hid).then(function(response) {
                $scope.house = response.data
                $scope.mapInfo.center = $scope.house.Latitude + ", " + $scope.house.Longitude;
                $scope.mapInfo.markers.push({
                    position: $scope.house.Latitude + ", " + $scope.house.Longitude
                });
            });

            $roomType.getByHouseId(hid).then(function(response) {
                $scope.roomTypes = response.data;
            });
        }        
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