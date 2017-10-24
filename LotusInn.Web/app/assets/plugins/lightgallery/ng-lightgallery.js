angular
    .module('ngLightGallery',[])
    .directive('lightGallery', function() {

        return {
            restrict: 'AE',
            link: function(scope, element, attr) {
                if (scope.$last) {

                    // ng-repeat is completed
                    element.parent().lightGallery({
                        download: false
                    });
                }
            }
        }
    });