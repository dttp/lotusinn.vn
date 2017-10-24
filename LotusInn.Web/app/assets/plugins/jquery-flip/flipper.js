angular.module('flipper', [])
    .directive('flip', function ($interval) {
        return {
            restrict: 'E',
            scope: {
                speed: '@'             
            },
            template: '<div class="flip-card"><div class="front"></div><div class="back"></div></div>',
            replace: 'true',
            link: function ($scope, element, attr) {
                var r = Math.floor((Math.random() * 2) + 1);
                var axis = r === 1 ? 'x' : 'y';

                $(element).flip({
                    'trigger': 'manual',
                    'axis': axis,
                    'reverse': r === 1
                });
                $interval(function () {                                        
                    $(element).flip('toggle');
                }, $scope.speed);
            }
        }
    });