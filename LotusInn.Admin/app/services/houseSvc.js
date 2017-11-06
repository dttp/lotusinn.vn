var module = angular.module('serviceModule');

module.factory('$house',
    function ($http) {
        var svc = {
            getHouses: function() {
                var url = LOTUS_INN_URL + '/api/house/gethouses';
                return $http.get(url);
            },
            getById: function(id) {
                var url = LOTUS_INN_URL + '/api/house/getbyid?id=' + id;
                return $http.get(url);
            },
            createHouse: function() {
                var url = LOTUS_INN_URL + '/api/house/createhouse';
                return $http.post(url);
            },
            update: function(house) {
                var url = LOTUS_INN_URL + '/api/house/update';
                return $http.post(url, house);
            },
            delete: function(id) {
                var url = LOTUS_INN_URL + '/api/house/delete?id=' + id;
                return $http.delete(url);
            }
        };

        return svc;
    });
