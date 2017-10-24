angular.module('fancybox', [])
    .directive('fancybox', function() {
        var linker = function (scope, element, attr) {
            if (scope.$last) setTimeout(function () {
                $('.fancybox').fancybox({});
                $('.fancy').fancybox({ autoSize: true, maxWidth: '50%' });
                $(".fancybox-gallery").fancybox({
                    helpers: {
                        title: {
                            type: 'outside'
                        },
                        thumbs: {
                            width: 50,
                            height: 50
                        }
                    }
                });
            }, 1);
        };
        return {
            restrict: "A",
            link: linker
        }
    });