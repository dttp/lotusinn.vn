
lotusInnAdmin.controller('houseAddEditCtrl', function ($scope, $http, $modal, $alert) {
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

    $scope.save = function() {
        var url = LOTUS_INN_URL + '/api/house/saveHouse';
        var request = {
            url: url,
            method: 'POST',
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            },
            data: $scope.house
        };
        $http(request).success(function() {
            $alert.success($scope.alerts, 'House info saved.');
        }).error(function(err) {
            $alert.error($scope.alerts, 'Something went wrong. ' + err.Message);
        });
    }

    $scope.createRoomType = function() {
        var request = {
            url: LOTUS_INN_URL + '/api/house/CreateRoomType?houseId=' + $scope.house.Id,
            method: 'POST',
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            }
        };

        $http(request).success(function(data) {
            $scope.roomTypes.push(data);
        });
    }

    $scope.edit = function(room) {
        window.location.href = '/house/roomType?hid=' + room.HouseId + '&rid=' + room.Id;
    }

    $scope.delete = function(room) {
        var request = {
            url: LOTUS_INN_URL + '/api/house/DeleteRoomType?houseId=' + room.HouseId + '&roomTypeId=' + room.Id,
            method: 'DELETE',
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            }
        };

        $http(request).success(function() {
            for (var i = 0; i < $scope.roomTypes.length; i++) {
                if ($scope.roomTypes[i].Id === room.Id) {
                    $scope.roomTypes.splice(i, 1);
                    break;
                }
            }
        });
    }

    $scope.confirmDelete = function(room) {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'confirmDeleteModal.html',
            controller: 'ModalInstanceCtrl'
        });
        modalInstance.result.then(function () {
            $scope.delete(room);
        }, function () {
        });
    }

    $scope.init = function () {
        // ---- get house
        var hid = getParameterByName('hid');
        if (hid) {
            var url = LOTUS_INN_URL + '/api/house/GetHouse?houseId=' + hid;
            $http.get(url).success(function (data) {
                $scope.house = data;
                $scope.mapInfo.center = $scope.house.Latitude + ", " + $scope.house.Longitude;
                $scope.mapInfo.markers.push({
                    position: $scope.house.Latitude + ", " + $scope.house.Longitude
                });
            });

            var request = {
                url: LOTUS_INN_URL + '/api/house/GetRoomTypes?houseId=' + hid,
                method: 'GET',
                headers: {
                    'Content-type': 'application/json',
                    'Accept': 'application/json'
                }
            }

            $http(request).success(function (data) {
                if (data)
                    $scope.roomTypes = data.RoomTypes;
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