// ReSharper disable once InconsistentNaming
angular.module('portfolioModule')
.controller("albumCtrl", function ($scope, $http, $modal, FileUploader, $alert) {
        $scope.alerts = [];
    var uploader = $scope.uploader = new FileUploader({
        url: LOTUS_INN_URL + "/api/portfolio/uploadImages",
        removeAfterUpload: true
    });

    uploader.onCompleteItem = function(item, response, status, headers) {
        $scope.refresh();
    }

    function searchToObject() {
        var pairs = window.location.search.substring(1).split("&"),
          obj = {},
          pair,
          i;

        for (i in pairs) {
            if (pairs[i] === "") continue;

            pair = pairs[i].split("=");
            obj[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
        }

        return obj;
    }

    $scope.portfolio = {};
    $scope.currentAlbum = {};

    $scope.refresh = function () {
        var url = LOTUS_INN_URL + "/api/portfolio/getportfolio";
        $http.get(url).success(function (data) {
            $scope.portfolio = data;
            var search = searchToObject();
            var albumId = search.id;
            for (var i = 0; i < $scope.portfolio.albums.length; i ++) {
                var album = $scope.portfolio.albums[i];
                if (album.id === albumId) {
                    $scope.currentAlbum = album;
                    uploader.url = LOTUS_INN_URL + "/api/portfolio/uploadImages?albumId=" + $scope.currentAlbum.id;
                    break;
                }
            }
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
        });
    }

    $scope.getLink = function(item) {
        var url = LOTUS_INN_URL + item.imagePath;
        return url;
    }

    $scope.remove = function(item) {
        for (var i = 0; i < $scope.currentAlbum.items.length; i ++) {
            var portfolioItem = $scope.currentAlbum.items[i];
            if (item.id === portfolioItem.id) {
                $scope.currentAlbum.items.splice(i, 1);
                $scope.saveChanges();
                break;
            }
        }
    }

    $scope.selectFile = function() {
        $("#select-file").click();
    }

    $scope.startUpload = function() {         
        uploader.uploadAll();
    }

    function roundToTwo(num) {    
        return +(Math.round(num + "e+2")  + "e-2");
    }
    $scope.getSize = function(size) {
        if (size < 1024) return size + "bytes";
        if (size < 1024 * 1024) return roundToTwo(size / 1024, 2) + "KB";
        return roundToTwo( size / 1021 / 1024) + "MB";
    }

    $scope.showConfirm = function (item) {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'confirmDeleteModal.html',
            controller: 'ModalInstanceCtrl'
        });
        modalInstance.result.then(function () {
            $scope.remove(item);
        }, function () {
        });
    }

}).controller('ModalInstanceCtrl', function ($scope, $modalInstance) {
    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
})