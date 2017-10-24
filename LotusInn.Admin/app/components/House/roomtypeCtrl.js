angular.module('roomTypeModule')
.controller('roomTypeCtrl', function ($scope, $http, $alert, $modal, FileUploader) {
    $scope.alerts = [];
    $scope.roomType = {};
    $scope.newFeature = { name: '' };

    var uploader = $scope.uploader = new FileUploader({
        url: LOTUS_INN_URL + "/api/house/uploadImages",
        removeAfterUpload: true
    });

    uploader.onCompleteItem = function (item, response, status, headers) {
        $scope.init();
    }

    $scope.getLink = function (item) {
        var url = LOTUS_INN_URL + item.imagePath;
        return url;
    }

    $scope.remove = function (item) {
        for (var i = 0; i < $scope.currentAlbum.items.length; i++) {
            var portfolioItem = $scope.currentAlbum.items[i];
            if (item.id === portfolioItem.id) {
                $scope.currentAlbum.items.splice(i, 1);
                $scope.saveChanges();
                break;
            }
        }
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

    $scope.deleteFeature = function(feature) {
        for (var i = 0; i < $scope.roomType.Features.length; i++) {
            if ($scope.roomType.Features[i] === feature) {
                $scope.roomType.Features.splice(i, 1);
                break;
            }
        }
    }

    $scope.removeImage = function(item) {
        for (var i = 0; i < $scope.roomType.Images.length; i ++) {
            if ($scope.roomType.Images[i].id === item.id) {
                $scope.roomType.Images.splice(i, 1);
                $scope.save();
                break;
            }
        }
    }

    $scope.showConfirm = function (item) {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'confirmDeleteModal.html',
            controller: 'ModalInstanceCtrl'
        });
        modalInstance.result.then(function () {
            $scope.removeImage(item);
        }, function () {
        });
    }

    $scope.save = function() {
        var hid = getParameterByName('hid');
        var request = {
            url: LOTUS_INN_URL + '/api/house/saveRoomType?houseId=' + hid ,
            method: 'POST',
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            },
            data: $scope.roomType
        };

        $http(request).success(function() {
            $alert.success($scope.alerts, 'Saved successfully');
        }).error(function(err) {
            $alert.error($scope.alerts, err.Message);
        });
    }

    $scope.back = function() {
        var hid = getParameterByName('hid');
        window.location.href = '/house/edit?hid=' + hid;
    }

    $scope.init = function () {
        var hid = getParameterByName('hid');
        var rid = getParameterByName('rid');
        var request = {
            url: LOTUS_INN_URL + '/api/house/getRoomType?houseId='+hid + '&roomTypeId=' + rid,
            method: 'GET',
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            }            
        };

        $http(request).success(function(data) {
            $scope.roomType = data;
        }).error(function(err) {
            $alert.error($scope.alerts, err.Message);
        });

        uploader.url = LOTUS_INN_URL + "/api/house/uploadImages?houseId=" + hid + "&roomTypeId=" + rid;
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