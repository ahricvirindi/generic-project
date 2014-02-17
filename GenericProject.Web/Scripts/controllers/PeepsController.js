'use strict';

angular.module('GenericApp')
    .controller('PeepsController', ['$scope', 'Api', '$routeParams', '$location', '$log',
        function ($scope, Api, $routeParams, $location, $log) {
            $scope.CurrentPage = $routeParams.page;
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
        $scope.PeepFilter = { Search: $routeParams.search, ZipCode: $routeParams.zipCode };
        $scope.OrderBy = $routeParams.orderBy;
        $scope.Ascending = ($scope.OrderBy || '').match(/ASC/);

        $log.info($routeParams);

        var buildPaginationRequest = function () {
            var req = {
                'FilterArgs[0].Key': 'Search',
                'FilterArgs[0].Value': $scope.PeepFilter.Search,
                'FilterArgs[1].Key': 'ZipCode',
                'FilterArgs[1].Value': $scope.PeepFilter.ZipCode,
                PageSize: 5,
                PageNumber: $scope.CurrentPage,
                OrderBy: $scope.OrderBy
            };
            return req;
        };

        $scope.gotoPage = function (newPage) {
            $scope.CurrentPage = newPage || 1;
        };

        $scope.resetFilter = function () {
            $scope.PeepFilter.Search = '';
            $scope.PeepFilter.ZipCode = '';
            $scope.refreshData();
        };

        $scope.refreshData = function () {
            $scope.PaginationState = api.paginate(buildPaginationRequest(), function (results) {
                $log.info(results);
            }, function () { });
        };

        $scope.$watch('CurrentPage',
        function (newValue, oldValue) {
            if (!newValue || newValue === oldValue) return;
            $location.search('page', $scope.CurrentPage);
            $scope.refreshData();
        }, true);


       $scope.$watch('OrderBy',
       function (newValue, oldValue) {
           if (!newValue || newValue === oldValue) {
               return;
           }
           $log.info(newValue, oldValue);
           $location.search('orderBy', $scope.OrderBy);
           $scope.refreshData();
       }, true);


        var api = new Api({ service: 'Peeps' });

        $scope.Busy = Api.isBusy;

        $scope.spin = {
            radius: 150,
            hwaccel: true,
            length: 20,
            width: 10,
            color: '#3388DD',
            shadow: true,
            className: 'spinner',
            top: 'auto',
            left: 'auto'
        };

        $scope.refreshData();
    }]);