// ReSharper disable once InconsistentNaming
angular.module('portfolioModule')
    .controller("portfolioCtrl", function($scope, $http, $timeout, $modal, FileUploader, $alert) {
        $scope.alerts = [];

        $scope.houses = [];

        var uploader = $scope.uploader = new FileUploader({
            removeAfterUpload: true,
            autoUpload: true
        });

        uploader.onCompleteAll = function() {
            $scope.refresh();
            $("#select-file").val('');
        }

        uploader.onErrorItem = function(item, response, status, headers) {
            console.log(item);
            console.log(response);
        }

        $scope.portfolio = {};

        $scope.refresh = function() {
            var url = LOTUS_INN_URL + "/api/portfolio/getportfolio";
            $http.get(url).success(function(data) {
                for (var i = 0; i < data.albums.length; i ++) {
                    data.albums[i].thumbnail = data.albums[i].thumbnail + "?rnd=" + (new Date()).getTime();
                }
                $scope.portfolio = data;
            });
        }

        $scope.init = function() {
            $scope.refresh();
            $http.get(LOTUS_INN_URL + "/api/house/gethouses")
                .success(function(data) {
                    $scope.houses = data.Houses;
                });
        }

        

        $scope.saveChanges = function() {
            var request = {
                method: 'POST',
                url: LOTUS_INN_URL + "/api/portfolio/saveportfolio",
                headers: {
                    'Content-type': 'application/json',
                    'Accept': 'application/json'
                },
                data: $scope.portfolio
            }
            $http(request).success(function () {
                $alert.success($scope.alerts,"Changes has been saved.");
            });
        }

        $scope.getLink = function(album) {
            var url = LOTUS_INN_URL + album.thumbnail;
            return url;
        }

        $scope.remove = function(album) {
            for (var i = 0; i < $scope.portfolio.albums.length; i ++) {
                var portfolioAlbum = $scope.portfolio.albums[i];
                if (album.id === portfolioAlbum.id) {
                    $scope.portfolio.albums.splice(i, 1);
                    $scope.saveChanges();
                    break;
                }
            }
        }

        $scope.createAlbum = function() {
            var request = {
                method: 'POST',
                url: LOTUS_INN_URL + "/api/portfolio/createAlbum",
                headers: {
                    'Content-type': 'application/json',
                    'Accept': 'application/json'
                }
            }

            $http(request).success(function(newAlbum) {
                $scope.portfolio.albums.push(newAlbum);
            }).error(function(err) {
                $alert.error($scope.alerts, err.ExceptionMessage);
            });
        }

        $scope.showAlbum = function(album) {
            window.location.href = "/portfolio/album?id=" + album.id;
        }


        $scope.setThumbnail = function(album) {
            uploader.url = LOTUS_INN_URL + "/api/portfolio/setThumbnail?albumId=" + album.id;
            setTimeout(function() {
                $("#select-file").click();
            }, 100);
        }

        $scope.showConfirm = function(album) {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'confirmDeleteModal.html',
                controller: 'ModalInstanceCtrl'
            });
            modalInstance.result.then(function() {
                $scope.remove(album);
            }, function() {
            });
        }
    }).controller('ModalInstanceCtrl', function($scope, $modalInstance) {
        $scope.ok = function() {
            $modalInstance.close();
        };

        $scope.cancel = function() {
            $modalInstance.dismiss('cancel');
        };
    });