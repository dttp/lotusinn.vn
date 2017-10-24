angular.module('fancybox', [])
    .directive('fancybox', function() {
        var linker = function (scope, element, attr) {
            if (scope.$last) setTimeout(function () {
                $('.fancybox').fancybox({});
            }, 1);
        };
        return {
            restrict: "A",
            link: linker
        }
    });