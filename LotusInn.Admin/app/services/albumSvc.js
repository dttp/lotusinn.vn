var module = angular.module('serviceModule');

module.factory('$album',
    function ($http) {
        var svc = {
            getByHouseId: function (houseId) {
                var url = LOTUS_INN_URL + '/api/album/getbyhouseid?houseId=' + houseId;
                return $http.get(url);
            },
            getById: function (id) {
                var url = LOTUS_INN_URL + '/api/album/getbyid?id=' + id;
                return $http.get(url);
            },
            create: function (houseId, album) {
                var url = LOTUS_INN_URL + '/api/album/insert?houseId=' + houseId;
                return $http.post(url, album);
            },
            update: function (roomType) {
                var url = LOTUS_INN_URL + '/api/album/update';
                return $http.post(url, roomType);
            },
            delete: function (id) {
                var url = LOTUS_INN_URL + '/api/album/delete?id=' + id;
                return $http.delete(url);
            },
            updateImage: function (albumId, image) {
                var url = LOTUS_INN_URL + '/api/album/updateimage?albumId=' + albumId;
                return $http.post(url, image);
            },
            deleteImage: function (albumId, imageId) {
                var url = LOTUS_INN_URL + '/api/album/RemoveImage?albumId=' + albumId+ '&imageId=' + imageId;
                return $http.delete(url);
            }
        };

        return svc;
    });
