'use strict';

angular.module('GenericApp')
    .controller('PeepsController', ['$scope', 'Api', '$routeParams', '$location', '$log',
        function ($scope, Api, $routeParams, $location, $log) {
            $scope.Busy = Api.isBusy;

            $scope.PeepFilter = {
                Search: $routeParams.Search || '',
                ZipCode: $routeParams.ZipCode || '',
                OrderBy: $routeParams.OrderBy || '',
                CurrentPage: $routeParams.CurrentPage || 1
            };

            $scope.PaginationState = {
                Items: [],
                PaginationRequest: {
                    PageSize: 20,
                    PageNumber: 1,
                    OrderBy: null
                },
                FilterArgs: {},
                PageCount: 1,
                TotalCount: 0,
                PageSize: 20
            };

            var buildPaginationRequest = function () {
                var req = {
                    'FilterArgs[0].Key': 'Search',
                    'FilterArgs[0].Value': $scope.PeepFilter.Search,
                    'FilterArgs[1].Key': 'ZipCode',
                    'FilterArgs[1].Value': $scope.PeepFilter.ZipCode,
                    PageSize: 5,
                    PageNumber: $scope.PeepFilter.CurrentPage,
                    OrderBy: $scope.PeepFilter.OrderBy
                };
                return req;
            };

            $scope.gotoPage = function (newPage) {
                $scope.PeepFilter.CurrentPage = newPage || 1;
            };

            $scope.edit = function (id) {
                $location.url('peeps/edit/' + id);
            };

            $scope.resetFilter = function () {
                $scope.PeepFilter.Search = '';
                $scope.PeepFilter.ZipCode = '';
            };

            $scope.refreshData = function () {
                var params = buildPaginationRequest();
                angular.extend(params, { service: 'peeps' });
                $scope.PaginationState = Api.paginate(params);
                $log.info($scope.PaginationState);
            };

            $scope.$watch('PeepFilter', function (val) {
                var searchParams = angular.extend($location.$$search, val);
                $location.search(searchParams);
            }, true);

            $scope.$watch(function() {
                return $location.$$search;
            }, function (val) {
                $scope.PeepFilter = angular.extend($scope.PeepFilter, val);
                $scope.Ascending = ($scope.PeepFilter.OrderBy || '').match(/ASC/);
                $scope.refreshData();
            }, true);

        }]);