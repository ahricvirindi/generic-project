
var module = angular.module('spin', []);

module.factory('spin', [
    function () {
        var Spin = function(options) {
            this.defaults = options;
        };
        var spin = new Spin(this.options);
        return spin;
    }

]);

