(function () {
    'use strict';
    var controllerId = 'home';
    angular.module('app').controller(controllerId, ['common', home]);

    function home(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Home';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Home View'); });
        }
    }
})();