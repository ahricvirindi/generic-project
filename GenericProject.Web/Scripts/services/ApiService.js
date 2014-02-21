'use strict';
/* Services */

var module = angular.module('GenericApp');

module.factory('Api', ['$resource', 'notificationService', function ($resource, notify) {
    var url = '/api/:service';
    var defaults = {};
    var actions = {
        update: { method: 'PUT' },
        paginate: {
            method: 'GET',
            isArray: false
        }
    };

    var Api = $resource(url, defaults, actions);

    Api.Busy = false;
    Api.isBusy = function () {
        return Api.Busy;
    };

    Api.prototype.onBeforeQuery = function () {
        notify.info('querying ' + this.service);
    };

    Api.prototype.onError = function () {
        notify.error('Error communicating with server.');
        Api.Busy = false;
    };

    Api.prototype.onSuccess = function (results) {
        notify.info(this.service + ' loaded successfully');
        Api.Busy = false;
    };

    Api.prototype.paginate = function (paginationArgs, successCallBack, errorCalLBack) {
        Api.Busy = true;
        this.onBeforeQuery();
        var args = { service: this.service };
        args = angular.extend(args, paginationArgs);
        var results = Api.paginate(args, this.onSuccess.bind(this), this.onError.bind(this));
        results.$promise.then(successCallBack, errorCalLBack);
        return results;
    };

    return Api;
}]);
