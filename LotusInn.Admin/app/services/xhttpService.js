angular.module('ngHttp',
    [
        'alertService'
    ])
    .factory('$xhttp', function ($http, $q, $alert) {
        var service = {
            get: function(url) {
                var defer = $q.defer();                
                $http.get(url).then(function(response) {
                    defer.resolve(response);
                }, function (err) {
                    if (err.data && err.data.Message) {
                        $alert.addError(err.data.Message);
                    }
                    console.log(err.data);
                    defer.reject(err);
                });
                return defer.promise;
            },

            post: function(url, data) {
                var defer = $q.defer();
                $http.post(url, data).then(function(response) {
                    defer.resolve(response);
                }, function (err) {
                    if (err.data && err.data.Message) {
                        $alert.addError(err.data.Message);
                    }                    
                    console.log(err.data);
                    defer.reject(err);
                });
                return defer.promise;
            },
            
            delete: function(url) {
                var defer = $q.defer();
                $http.delete(url).then(function(response) {
                    defer.resolve(response);
                }, function (err) {
                    if (err.data && err.data.Message) {
                        $alert.addError(err.data.Message);
                    }                    
                    console.log(err.data);
                    defer.reject(err);
                });

                return defer.promise;
            }
        };

        return service;
    });
