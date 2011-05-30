var Site = {};
$(function () {
	//Apply defaults
	$("input[type=button], input[type=submit], a.button").button();

	$.extend($.ui.dialog.prototype.options, {
		width: 500,
		height: 350
	});

	$.ajaxSetup({traditional:true});

	//Initialize Site object
	Site.showStatus = function (message, type) {
		var statusMessage = $("#status-message");
		var container = statusMessage.parent();
		$("#status-message")
			.removeClass()
			.addClass(type)
			.text(message)
			.show()
			.position({
				my: "center top",
				at: "center top",
				of: container
			})
			.delay(5000)
			.fadeOut(1500);
	}

	//Attach global AJAX error handler
	$(document).ajaxError(function (event, request, options) {
		if (request.status === 401) {
			//Close all open dialogs
			$(".ui-dialog-content").dialog("close");
			$("#session-timeout-dialog").dialog({
				modal: true,
				buttons: {
					Ok: function () {
						$(this).dialog("close");
					}
				}
			});
		}
		else {
			Site.showStatus("There was a problem with your last request. Please reload this page and try again.", "error");
		}
	});
});