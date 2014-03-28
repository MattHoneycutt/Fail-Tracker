(function () {
	'use strict';

	var serviceId = 'alerts';

	angular.module('failtrackerApp').factory(serviceId, function () {
		return window.alerts;
	});

})();
