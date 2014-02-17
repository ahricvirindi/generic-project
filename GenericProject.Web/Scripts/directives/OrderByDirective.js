'use strict';

// TODO: A better directive name than 'gen-order-by'
angular.module('GenericApp')
  .directive('genOrderBy', ['$log', '$parse', function ($log, $parse) {
      return {
          scope: true,
          template: '{{genName|inflector}}<i class="glyphicon" />',
          link: function (scope, elm, attrs) {
              var $elm = $(elm),
                  orderByExpr = $parse(attrs.genOrderBy),
                  ascending = $parse(attrs.genAscending || 'Ascending');

              elm.addClass('btn  btn-default');
              scope.genName = attrs.genName;

              scope.$watch(ascending, function (val) {
                  $log.info(val);
                  if (orderByExpr(scope).match(attrs.genName)) {
                      elm.removeClass('ordered-desc ordered');
                      if (val) {
                          elm.addClass('ordered-desc');
                      }
                      else {
                          elm.addClass('ordered');
                      }
                  }
              });

              scope.$watch(orderByExpr, function (val, prev) {
                  if (!val) return;
                  if (val.match(attrs.genName)) {
                      $elm.siblings()
                          .removeClass('ordered')
                          .removeClass('ordered-desc');
                  }
              });

              elm.on('click', function () {
                  scope.$apply(function () {
                      ascending.assign(scope.$parent, !ascending(scope.$parent));
                      scope.genOrderBy = attrs.genName;
                      var direction = ascending(scope.$parent) ? 'ASC' : 'DESC';
                      orderByExpr.assign(scope.$parent, scope.genOrderBy + ' ' + direction);
                  })
              });
          }
      };
  }]);