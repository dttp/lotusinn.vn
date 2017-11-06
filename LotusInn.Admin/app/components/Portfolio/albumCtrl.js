// ReSharper disable once InconsistentNaming
angular.module('portfolioModule')
.controller("albumCtrl", function ($scope, $http, $modal, FileUploader, $alert, $album, $house) {
    $scope.alerts = [];
    var uploader = $scope.uploader = new FileUploader({
        url: LOTUS_INN_URL + "/api/album/uploadImages",
        removeAfterUpload: true
    });

    uploader.onCompleteItem = function(item, response, status, headers) {
        $scope.refresh();
    }

    $scope.portfolio = {};
    $scope.currentAlbum = {};

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    $scope.refresh = function () {
        var id = getParameterByName("id");
        uploader.url = LOTUS_INN_URL + "/api/album/uploadImages?albumId=" + id;
        $album.getById(id).then(function(response) {
            $scope.currentAlbum = response.data;
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
        $album.deleteImage($scope.currentAlbum.Id, item.Id).then(function() {
            _.remove($scope.currentAlbum.Items, { Id: item.Id });
        });
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