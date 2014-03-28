(function () {

	var alertService = {
		showAlert: showAlert,
		success: success,
		info: info,
		warning: warning,
		error: error
	};

	window.alerts = alertService;

	var alertContainer = $(".alert-container");

	var template = _.template("<div class='alert <%= alertClass %> alert-dismissable'>" +
		"<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" +
		"<%= message %>" +
		"</div>");

	function showAlert(alert) {
		var alertElement = $(template(alert));
		alertContainer.append(alertElement);

		window.setTimeout(function () {
			alertElement.fadeOut();
		}, 3000);
	}

	function success(message) {
		showAlert({ alertClass: "alert-success", message: message });
	}

	function info(message) {
		showAlert({ alertClass: "alert-info", message: message });
	}

	function warning(message) {
		showAlert({ alertClass: "alert-warning", message: message });
	}

	function error(message) {
		showAlert({ alertClass: "alert-danger", message: message });
	}

})();
