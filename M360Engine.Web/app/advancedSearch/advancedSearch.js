(function () {
    'use strict';
    var controllerId = 'advancedSearch';
    angular.module('app').controller(controllerId, ['common', advancedSearch]);

    function advancedSearch(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Advanced Search';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Advanced Search View'); });
        }
    }
})();