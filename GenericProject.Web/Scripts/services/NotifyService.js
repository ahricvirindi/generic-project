var module = angular.module('GenericApp');

module.factory('Notify', ['$q', '$log', 'notificationService',
    function ($q, log, notify) {
        return {
            request: function (config) {
                log.info('request', arguments);
                notify.info('querying ' + config.service);
            },
            response: function (results, config) {
                log.info('response', arguments);
                notify.info(config.service + ' loaded successfully');
                return results;
            },
            error: function (error) {
                log.info('error', arguments);
                notify.error('Error communicating with Server');
                return error;
            }

        };
    }
]);