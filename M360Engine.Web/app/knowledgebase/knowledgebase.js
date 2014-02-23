(function () {
    'use strict';
    var controllerId = 'knowledgebase';
    angular.module('app').controller(controllerId, ['common', knowledgebase]);

    function knowledgebase(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Knowledgebase';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Knowledgebase View'); });
        }
    }
})();