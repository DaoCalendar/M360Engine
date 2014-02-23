
(function () {
    'use strict';
    var controllerId = 'finance';
    angular.module('app').controller(controllerId, ['common', finance]);

    function finance(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Finance';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Finance View'); });
        }
    }
})();