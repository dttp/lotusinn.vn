var module = angular.module('serviceModule');

module.factory('$roomType',
    function ($http) {
        var svc = {
            getByHouseId: function (houseId) {
                var url = LOTUS_INN_URL + '/api/roomtype/getbyhouseid?houseId=' + houseId;
                return $http.get(url);
            },
            getById: function (id) {
                var url = LOTUS_INN_URL + '/api/roomtype/getbyid?id=' + id;
                return $http.get(url);
            },
            create: function (houseId) {
                var url = LOTUS_INN_URL + '/api/roomtype/create?houseId=' + houseId;
                return $http.post(url);
            },
            update: function (roomType) {
                var url = LOTUS_INN_URL + '/api/roomtype/update';
                return $http.post(url, roomType);
            },
            delete: function (id) {
                var url = LOTUS_INN_URL + '/api/roomtype/delete?id=' + id;
                return $http.delete(url);
            },
            updateImage: function(roomTypeId, image) {
                var url = LOTUS_INN_URL + '/api/roomtype/updateimage?roomtype=' + roomTypeId;
                return $http.post(url, image);
            },
            deleteImage: function(roomTypeId, imageId) {
                var url = LOTUS_INN_URL + '/api/roomtype/deleteimage?roomTypeId=' + roomTypeId + '&imageId=' + imageId;
                return $http.delete(url);
            }
        };

        return svc;
    });
