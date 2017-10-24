lotusInnAdmin.controller('articleAddEditCtrl', function ($scope, $http, $alert) {
    $scope.article = {};
    $scope.alerts = [];
    $scope.options = {
        language: 'en',
        allowedContent: true,
        entities: false
    };

    $scope.articles = [
        { 'name': 'about', 'value': 'About Us' },        
        { 'name': 'contact', 'value': 'Contact Us' }
    ];

    $scope.selectedArticle = $scope.articles[0];

    $scope.$watch('selectedArticle', function(newValue, oldValue) {
        $scope.getPage(newValue.name);
    }, true);

    $scope.getPage = function(name) {
        var url = LOTUS_INN_URL + '/api/article/getarticle?page=' + name;
        var request = {
            method: 'GET',
            url: url,
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            }
        };
        $http(request).success(function (data) {
            $scope.article = data;
        });
    }

    $scope.init = function () {
        $scope.getPage('about');
    };

    $scope.save = function() {
        var url = LOTUS_INN_URL + '/api/article/SaveArticle?page=' + $scope.selectedArticle.name;
        var request = {
            method: 'POST',
            url: url,
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json'
            },
            data: $scope.article
        };
        $http(request).success(function() {
            $alert.success($scope.alerts, 'Article saved.');
        }).error(function(err) {
            $alert.error($scope.alerts, err.Message);
        });
    };

    $scope.back = function() {
        window.location.href = "/";
    }

});