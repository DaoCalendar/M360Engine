(function () {
    'use strict';
    var controllerId = 'sales';
    angular.module('app').controller(controllerId, ['common', sales]);

    function sales(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Sales';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Sales View'); });
        }
    }
})();