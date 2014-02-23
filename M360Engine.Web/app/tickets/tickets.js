(function () {
    'use strict';
    var controllerId = 'tickets';
    angular.module('app').controller(controllerId, ['common', tickets]);

    function tickets(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Tickets';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Tickets View'); });
        }
    }
})();