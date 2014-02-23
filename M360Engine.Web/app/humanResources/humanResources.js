(function () {
    'use strict';
    var controllerId = 'humanResources';
    angular.module('app').controller(controllerId, ['common', humanResources]);

    function humanResources(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Human Resources';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Human Resources View'); });
        }
    }
})();