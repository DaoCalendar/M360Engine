(function () {
    'use strict';
    var controllerId = 'management';
    angular.module('app').controller(controllerId, ['common', management]);

    function management(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Management';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Management View'); });
        }
    }
})();