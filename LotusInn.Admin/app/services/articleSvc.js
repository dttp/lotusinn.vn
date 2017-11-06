var module = angular.module('serviceModule');

module.factory('$article',
    function ($http) {
        var svc = {
            getByName: function(name) {
                var url = LOTUS_INN_URL + '/api/article/getarticle?name=' + name;
                return $http.get(url);
            },
            getById: function(id) {
                var url = LOTUS_INN_URL + '/api/article/getbyid?id=' + id;
                return $http.get(url);
            },
            create: function(article) {
                var url = LOTUS_INN_URL + '/api/article/create';
                return $http.post(url, article);
            },
            update: function(article) {
                var url = LOTUS_INN_URL + '/api/article/update';
                return $http.post(url, article);
            },
            delete: function(id) {
                var url = LOTUS_INN_URL + '/api/article/delete?id=' + id;
                return $http.delete(url);
            }
        };

        return svc;
    });
