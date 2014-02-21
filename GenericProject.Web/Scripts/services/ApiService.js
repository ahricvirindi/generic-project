'use strict';
/* Services */

var module = angular.module('GenericApp');

module.factory('Api', ['$resource', 'Notify', function ($resource, notify) {
    var url = '/api/:service';
    var defaults = {};
    var actions = {};
    var busy = false;

    var Api = $resource(url, defaults, actions);

    Api.isBusy = function () {
        return busy;
    };

    Api.paginate = function (params) {
        notify.request(params);
        busy = true;
        var results = Api.get(params,
            function (result) {
                busy = false;
                return notify.response(result, params);
            },
            function (error) {
                busy = false;
                return notify.error(error, params);
            }
        );
        return results;
    };

    return Api;
}]);
