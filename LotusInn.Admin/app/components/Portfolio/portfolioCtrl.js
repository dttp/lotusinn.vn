// ReSharper disable once InconsistentNaming
angular.module('portfolioModule')
    .controller("portfolioCtrl", function($scope, $http, $timeout, $modal, FileUploader, $alert, $house, $album) {
        $scope.alerts = [];
        $scope.houses = [];
        $scope.selectedHouse = {};

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

        $scope.albums = {};

        $scope.refresh = function() {
            $album.getByHouseId($scope.selectedHouse.Id).then(function(response) {
                $scope.albums = response.data;
            });
        }

        $scope.init = function () {
            $house.getHouses().then(function(response) {
                $scope.houses = response.data;
                if ($scope.houses.length > 0) {
                    $scope.selectedHouse = $scope.houses[0];
                    $scope.refresh();
                }
            });
        }

        $scope.saveChanges = function(album) {
            $album.update(album).then(function(){
                $alert.success($scope.alerts,"Changes has been saved.");
            });
        }

        $scope.remove = function(album) {
            $album.delete(album.Id).then(function() {
                _.remove($scope.albums, { Id: album.Id });
            });
        }

        $scope.createAlbum = function() {
            var album = {
                Name: 'New Album',
                Description: '',
                HouseId: $scope.selectedHouse.Id
            }

            $album.create($scope.selectedHouse.Id, album).then(function (response) {
                $scope.albums.push(response.data);
            }, function (err) {
                $alert.error($scope.alerts, err.ExceptionMessage);
            });                
        }

        $scope.showAlbum = function(album) {
            window.location.href = "/portfolio/album?id=" + album.Id;
        }


        $scope.setThumbnail = function(album) {
            uploader.url = LOTUS_INN_URL + "/api/album/setThumbnail?albumId=" + album.Id;
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