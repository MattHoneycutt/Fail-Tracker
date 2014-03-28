(function () {
	'use strict';

	angular.module('failtrackerApp').factory("$exceptionHandler",
		['$log', 'alerts', function ($log, alerts) {
			return function (exception, cause) {
				alerts.error("There was a problem with your last action.  Please reload the page, then try again.");
				$log.error(exception, cause);
			};
		}]);

})();
