(function () {
    'use strict';
    var controllerId = 'bestPractices';
    angular.module('app').controller(controllerId, ['common', bestPractices]);

    function bestPractices(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Best Practices';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Best Practices View'); });
        }
    }
})();