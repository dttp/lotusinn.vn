lotusInnAdmin.controller('articleAddEditCtrl', function ($scope, $http, $alert, $article) {
    $scope.article = {};
    $scope.alerts = [];
    $scope.options = {
        language: 'en',
        allowedContent: true,
        entities: false
    };

    $scope.articles = [
        { 'name': 'about', 'value': 'About Us' },        
        { 'name': 'contact', 'value': 'Contact Us' },
        { 'name': 'services', 'value': 'Services' }
    ];

    $scope.selectedArticle = $scope.articles[0];

    $scope.$watch('selectedArticle', function(newValue, oldValue) {
        $scope.getPage(newValue.name);
    }, true);

    $scope.getPage = function(name) {
        $article.getByName(name).then(function(response) {
            $scope.article = response.data;
            if (!$scope.article) {
                var newArticle = {
                    Name: name,
                    Description: '',
                    Title: _.find($scope.articles, { name: name }).value,
                    Content: ''
                };
                $article.create(newArticle).then(function(response) {
                    $scope.article = response.data;
                });
            }
        });
    }

    $scope.init = function () {
        $scope.getPage('about');
    };

    $scope.save = function() {
        
        $article.update($scope.article).success(function() {
            $alert.success($scope.alerts, 'Article saved.');
        }).error(function(err) {
            $alert.error($scope.alerts, err.Message);
        });
    };

    $scope.back = function() {
        window.location.href = "/";
    }

});