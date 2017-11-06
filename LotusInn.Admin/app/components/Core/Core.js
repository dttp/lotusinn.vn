lotusInnAdmin.controller("core", function ($scope, ipCookie) {

    $scope.menu = {
        items: [
            { href: '/', icon: 'icon-home', title: 'Dashboard', selected: false },
            { href: '/house', icon: 'icon-home', title: 'Houses', selected: false },
            { href: '/article/', icon: 'icon-doc', title: 'Articles', selected: false },            
            { href: '/service/social/', icon: 'icon-wrench', title: 'Social Services', selected: false },
            { href: '/portfolio', icon: 'icon-picture', title: 'Photo Gallery', selected: false }
        ]
    }

    $scope.user = {};

    $scope.init = function () {
        var href = window.location.pathname.toLowerCase();
        for (var i = 0; i < $scope.menu.items.length; i ++) {
            var item = $scope.menu.items[i];
            if (href === item.href) {
                item.selected = true;
                break;
            }
        }

        $scope.user = ipCookie("AuthData");
    }

    $scope.logout = function() {
        ipCookie("AuthData", null);
    }
});