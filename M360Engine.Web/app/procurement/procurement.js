(function () {
    'use strict';
    var controllerId = 'procurement';
    angular.module('app').controller(controllerId, ['common', procurement]);

    function procurement(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Procurement';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Procurement View'); });
        }
    }
})();