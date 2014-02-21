'use strict';

angular.module('GenericApp', [
  'ngCookies',
  'ngResource',
  'ngSanitize',
  'ngRoute',
  'ui.utils',
  'ui.notify',
  'ui.bootstrap',
  'spin'
])
    .config(['$routeProvider', '$locationProvider', 'notificationServiceProvider', 'spinProvider',
        function ($routeProvider, $locationProvider, notificationServiceProvider, spinProvider) {
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
                    templateUrl: '/Scripts/views/peeps/index.html',
                    controller: 'PeepsController',
                    reloadOnSearch: false
                })
                .when('/peeps/edit/:id', {
                    templateUrl: '/Scripts/views/peeps/edit.html',
                    controller: 'PeepsEditController'
                })
                .when('/style', {
                    templateUrl: '/Scripts/views/style.html',
                    controller: 'StyleController'
                })
                .otherwise({
                    redirectTo: '/'
                });

            $locationProvider.html5Mode(true);

            spinProvider.options = {
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
  }]);