angular.module('roomTypeModule', ['ckeditor', 'angularFileUpload', 'fancybox', 'ui.bootstrap', 'alertService']);
angular.module('portfolioModule', ['angularFileUpload', 'fancybox', 'ui.bootstrap', 'alertService']);
angular.module('serviceModule', ['ckeditor', 'alertService', 'ui.bootstrap']);
var lotusInnAdmin = angular.module('lotusInnAdmin',
    ['ckeditor',
     'ngMap',
     'ipCookie',
     'localytics.directives',
     'angularFileUpload',
     'fancybox',
     'ui.bootstrap',
     'alertService',
     'roomTypeModule',
     'portfolioModule',
     'serviceModule'
    ]);

lotusInnAdmin.run(function ($rootScope, ipCookie) {
    function checkLogin() {
        var authId = ipCookie('AuthData');
        if (!authId) {
            location.href = '/login';
        } 
    }

    checkLogin();
});


lotusInnAdmin.config(['$httpProvider', function ($httpProvider) {
    //Reset headers to avoid OPTIONS request (aka preflight)
    $httpProvider.defaults.headers.common = {};
    $httpProvider.defaults.headers.post = {};
    $httpProvider.defaults.headers.put = {};
    $httpProvider.defaults.headers.patch = {};
    $httpProvider.defaults.headers.common['Content-Type'] = 'application/json';
}]);