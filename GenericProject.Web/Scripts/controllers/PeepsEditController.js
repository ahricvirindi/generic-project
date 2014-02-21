'use strict';

angular.module('GenericApp')
    .controller('PeepsEditController', ['$scope', 'Api', '$routeParams', '$location', '$log',
        function ($scope, Api, $routeParams, $location, $log) {
            $scope.peep = Api.get({
                service: 'peeps',
                id: $routeParams.id
            });

            $scope.save = function (peep) {
                peep.$save({ service: 'peeps' }, function() {
                    $location.url('peeps/main');
                });
            };


        }]);