'use strict';

angular.module('GenericApp', [
  'ngCookies',
  'ngResource',
  'ngSanitize',
  'ngRoute',
  'ui.utils',
  'ui.notify',
  'ui.bootstrap'
])
    .config(['$routeProvider', '$locationProvider', 'notificationServiceProvider',
        function ($routeProvider, $locationProvider, notificationServiceProvider) {
            notificationServiceProvider.setDefaults({
                history: false,
                delay: 5000,
                styling: 'bootstrap',
                closer: false,
                closer_hover: false,
                before_open: function (pnotify) {
                    pnotify.css({
                        "left": ($(window).width() / 2) - (pnotify.width() / 2)
                    });
                }
            });

            $routeProvider
                .when('/', {
                    templateUrl: '/Scripts/views/main.html',
                    controller: 'MainController'
                })
                .when('/peeps', {
                    templateUrl: '/Scripts/views/peeps.html',
                    controller: 'PeepsController',
                    reloadOnSearch: false
                })
                .when('/style', {
                    templateUrl: '/Scripts/views/style.html',
                    controller: 'StyleController'
                })
                .otherwise({
                    redirectTo: '/'
                });
            
            $locationProvider.html5Mode(true);
  }]);