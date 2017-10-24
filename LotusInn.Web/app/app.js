angular
.module('lotusInnApp',
    [
        'fancybox',
        'ngMap',
        'ngSanitize',
        'localytics.directives',
        'ngBootstrap',
        'ngLightGallery'
    ])
.config(['$compileProvider', function ($compileProvider) {
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|sms|tel|skype):/);
}])
.controller('home', function ($scope, $http, $sce) {
    $scope.contactArticle = {};
    $scope.aboutArticle = {};
    $scope.portfolio = {};
    $scope.houses = [];
    $scope.mapInfo = {};
    $scope.serviceInfo = {};
    $scope.facilities = [];

    $scope.allRoomTypes = [];

    $scope.socialServices = [];

    $scope.feedback = {
        name: '',
        email: '',
        message: ''
    };

    $scope.showAlbum = function (album) {
        var el = $("a[rel='" + album.id + "']:eq(0)");
        $(el[0]).click();
    }

    $scope.trustAsHtml = function(content) {
        return $sce.trustAsHtml(content);
    }

    function chunk(arr, size) {
        var newArr = [];
        for (var i = 0; i < arr.length; i += size) {
            newArr.push(arr.slice(i, i + size));
        }
        return newArr;
    };

    $scope.getIcon = function(facilityItem) {
        return '/app/assets/images/' + facilityItem.Icon + '.png';
    }

    $scope.filterRoomTypesByHouse = function(house) {
        for (var i = 0; i < $scope.allRoomTypes.length; i++) {
            var list = $scope.allRoomTypes[i];
            if (list && list.RoomTypes && list.RoomTypes.length > 0 && list.RoomTypes[0].HouseId === house.Id) {
                return list.RoomTypes;
            }
        }
        return [];
    }

    $scope.reserve = function(room) {
        window.location.href = '/reservation?hid=' + room.HouseId + '&rid=' + room.Id;
    }

    $scope.showImages = function(room) {
        var el = $("a[rel='" + room.Id + "']:eq(0)");
        $(el[0]).click();
    }

    $scope.getAlbums = function (houseId) {
        var albums = [];
        if (!$scope.portfolio.albums) return albums;

        for (var i = 0; i < $scope.portfolio.albums.length; i++) {
            var album = $scope.portfolio.albums[i];
            if (album.houseId === houseId) {
                albums.push(album);
            }
        }
        return albums;
    }

    $scope.sendFeedback = function() {
        $http.post('api/feedback/post', $scope.feedback)
            .success(function(data) {
                $scope.feedback = {
                    name: '',
                    email: '',
                    message: ''
                }
            })
            .error(function(data) {

            });
    }

    $scope.init = function () {
        
        // ----- Loading article 
        $http.get("api/article/GetContactPage").success(function (data) {
            $scope.contactArticle = data;
        });

        $http.get("api/article/GetAboutPage").success(function (data) {
            $scope.aboutArticle = data;
        });

        // ----- Loading portfolio
        $http.get("api/portfolio/getportfolio").success(function (data) {
            $scope.portfolio = data;
        });

        // ----- Loading services 
        $http.get("api/services/getservices").success(function (data) {
            $scope.serviceInfo = data;
            $scope.facilities = chunk($scope.serviceInfo.Facilities, 4);
        });

        // ---- Loading Houses
        $http.get("api/house/GetHouses").success(function (data) {
            $scope.houses = data.Houses;

            $scope.mapInfo = {
                center: $scope.houses[0].Latitude + "," + $scope.houses[0].Longitude,
                zoom: 14,
                markers: []
            };

            var latitude = 0, longitude = 0;

            for (var i = 0; i < $scope.houses.length; i ++) {
                var house = $scope.houses[i];
                var lat = parseFloat(house.Latitude);
                var long = parseFloat(house.Longitude);

                latitude = latitude + lat;
                longitude = longitude + long;

                $scope.mapInfo.markers.push({
                    position: house.Latitude + ", " + house.Longitude
                });
            }

            latitude = latitude / $scope.houses.length;
            longitude = longitude / $scope.houses.length;

            $scope.mapInfo.center = latitude + ", " + longitude;
        });

        // ---- Loading all Room Types
        $http.get("/api/house/GetAllRoomTypes").success(function (data) {
            $scope.allRoomTypes = data;
        });

        $http.get('/api/services/getsocialservices').then(function(response) {
            $scope.socialServices = response.data;
        });
    }
})
.controller("reservationCtrl", function ($scope, $http) {

    $scope.wizardSteps = [
        {
            "number": 1,
            "desc": "House & Room Info"
        },
        {
            "number": 2,
            "desc": "Your Info"
        },
        {
            "number": 3,
            "desc": "Confirmation"
        },
        {
            "number": 4,
            "desc": "Finish"
        }
    ];

    $scope.houses = [];
    $scope.rooms = [];

    $scope.webBooking = {
        HouseId: "lotusInn1",
        RoomTypeId: "",
        From: undefined,
        To: undefined,
        NumberOfPersons: 1,
        NumberOfRooms: 1,
        FullName: "",
        IsMale: true,
        Phone: "",
        Email: ""
    };

    $scope.genders = [{ id: 'male', value: 'Male' }, { id: 'female', value: 'Female' }];
    $scope.gender = $scope.genders[0];

    $scope.$watch('gender', function(newVal, oldVal) {
        $scope.webBooking.isMale = newVal.id === 'male';
    }, true);

    $scope.getGender = function(isMale) {
        return isMale ? "Male" : "Female";
    }

    $scope.getHouse = function(houseId) {
        for (var i = 0; i < $scope.houses.length; i ++) {
            if ($scope.houses[i].Id === houseId)
                return $scope.houses[i].Name;
        }
    }

    $scope.getRoomType = function(roomTypeId) {
        for (var i = 0; i < $scope.rooms.length; i++) {
            if ($scope.rooms[i].Id === roomTypeId)
                return $scope.rooms[i].Name;
        }
    }

    $scope.datePicker = {
        date: { startDate: moment(), endDate: moment() }
    }

    $scope.$watch('datePicker', function (newVal, oldVal) {
        if (newVal.date.startDate && newVal.date.endDate) {
            $scope.webBooking.From = newVal.date.startDate.format("MM/DD/YYYY");
            $scope.webBooking.To = newVal.date.endDate.format("MM/DD/YYYY");
        }        
    }, true);

    $scope.$watch('webBooking.HouseId', function (newValue) {
        if (newValue) {
            $scope.getRoomTypeForHouse($scope.webBooking.HouseId);
        }        
    });

    $scope.currentStep = 1;
    $scope.complete = false;

    $scope.next = function() {
        $scope.currentStep = $scope.currentStep + 1;
        if ($scope.currentStep === 4) {
            $scope.reserve();
        }
    }

    $scope.back = function() {
        $scope.currentStep = $scope.currentStep - 1;
    }

    $scope.setStep = function (step) {        
        $scope.currentStep = step;
    }

    $scope.getProgress = function () {
        var progress = 25 * $scope.currentStep;
        if (progress === 100 && !$scope.complete)
            progress = 99;
        return 'progress-' + progress;
    }

    $scope.finish = function() {
        window.location.href = "/";
    }

    $scope.isFormInvalid = function() {
        if ($scope.currentStep === 1) {
            return !($scope.bookingForm.numPerson.$valid && $scope.bookingForm.numRooms.$valid);
        }

        if ($scope.currentStep === 2) {
            return $scope.bookingForm.fullname.$invalid || $scope.bookingForm.email.$invalid;
        }

        return false;
    }

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    $scope.getRoomTypeForHouse = function (houseId) {
        if (!houseId) return;
        $http.get("/api/house/getRoomTypes?houseId=" + houseId).success(function (roomTypesResult) {
            $scope.rooms = roomTypesResult.RoomTypes;

            if (getParameterByName('rid')) {
                $scope.webBooking.RoomTypeId = getParameterByName('rid');
            } else {
                $scope.webBooking.RoomTypeId = $scope.rooms[0].Id;
            }
        });
    }

    $scope.init = function() {
        $http.get('/api/house/gethouses').success(function(data) {
            $scope.houses = data.Houses;

            if (getParameterByName('hid')) {
                $scope.webBooking.HouseId = getParameterByName('hid');
            } else {
                $scope.webBooking.HouseId = $scope.houses[0].Id;
            }

            $scope.getRoomTypeForHouse($scope.webBooking.HouseId);
        });
    }

    $scope.getDisplayNameForRoomType = function(room) {
        return room.Name + ' - ' + room.Price + '$';
    }

    $scope.reserve = function() {
        $http.post('/api/booking/reserve', $scope.webBooking).success(function() {
            $scope.complete = true;
        }).error(function() {
            $scope.complete = true;
        });
    }
})