'use strict';

angular.module('spin')
  .directive('spin', ['spin', function (spin) {
      return function (scope, elm, attrs) {
          var spinner = new Spinner(spin.defaults);
          scope.$watch(attrs.spinOpts, function (value) {
              if (!value) return;
              value = angular.extend(spin.defaults, value);
              spinner = new Spinner(value);
          });
          scope.$watch(attrs.spinWhen, function (value) {
              if (value) {
                  spinner.spin();
                  elm.append(spinner.el);
              }
              else {
                  spinner.stop(elm);
              }
          });
      };
  }]);