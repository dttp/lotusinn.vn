angular.module('roomTypeModule')
.controller('roomTypeCtrl', function ($scope, $http, $alert, $modal, FileUploader, $roomType) {
    $scope.alerts = [];
    $scope.roomType = {};
    $scope.newFeature = { name: '' };

    var uploader = $scope.uploader = new FileUploader({        
        removeAfterUpload: true
    });

    uploader.onCompleteItem = function (item, response, status, headers) {
        $scope.init();
    }

    $scope.selectFile = function () {
        $("#select-file").click();
    }

    $scope.startUpload = function () {
        uploader.uploadAll();
    }

    function roundToTwo(num) {
        return +(Math.round(num + "e+2") + "e-2");
    }
    $scope.getSize = function (size) {
        if (size < 1024) return size + "bytes";
        if (size < 1024 * 1024) return roundToTwo(size / 1024, 2) + "KB";
        return roundToTwo(size / 1021 / 1024) + "MB";
    }

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    $scope.addFeature = function() {
        $scope.roomType.Features.push($scope.newFeature.name);
        $scope.newFeature.name = '';
    }

    $scope.deleteFeature = function (feature) {
        _.remove($scope.roomType.Features,
            function(item) {
                return item === feature;
            });
    }

    $scope.deleteImage = function (item) {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'confirmDeleteModal.html',
            controller: 'ModalInstanceCtrl'
        });
        modalInstance.result.then(function () {
            $roomType.deleteImage($scope.roomType.Id, item.Id);
        });
    }

    $scope.updateImage = function(item) {
        $roomType.updateImage($scope.roomType.Id, item).then(function() {
            $alert.success($scope.alerts, 'Saved successfully');
        });
    }

    $scope.save = function () {
        $roomType.update($scope.roomType).then(function() {
            $alert.success($scope.alerts, 'Saved successfully');
        }, function(err) {
            $alert.error($scope.alerts, err.Message);
        });        
    }

    $scope.back = function() {        
        window.location.href = '/house/edit?hid=' + $scope.roomType.HouseId;
    }

    $scope.init = function () {        
        var id = getParameterByName('id');
        $roomType.getById(id).then(function(response) {
            $scope.roomType = response.data;            
        });
        uploader.url = LOTUS_INN_URL + '/api/roomtype/uploadimages?roomTypeId=' + id;
    }
})
.controller('ModalInstanceCtrl', function ($scope, $modalInstance) {
    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
})