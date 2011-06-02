var Site = {};
var ViewBag = {};

$(function () {
	//Apply defaults
	$("input[type=button], input[type=submit], a.button, span.button").button();

	$.extend($.ui.dialog.prototype.options, {
		width: 500,
		height: 350
	});

	$.ajaxSetup({ traditional: true });

	//Initialize Site object
	var statusTimeout = setTimeout(function () { }, 1);
	Site.showStatus = function (message, type) {

		clearTimeout(statusTimeout);

		var statusMessage = $("#status-message");
		var container = statusMessage.parent();
		var status = $("#status-message");

		status.stop(true, true)
			.removeClass()
			.addClass(type)
			.text(message)
			.show()
			.position({
				my: "center top",
				at: "center top",
				of: container
			});

		statusTimeout = setTimeout(function () { 
			status.fadeOut(1500);
		}, 5000);
			
	};

	Site.confirm = function (options) {

		options = $.extend({
			confirmAction: function () { },
			cancelAction: function () { },
			title: "Are you sure?",
			text: "Do you wish to continue?"
		}, options);

		$("#dialog-confirm")
			.find("span.text")
				.text(options.text)
				.end()
			.dialog({
				title: options.title,
				resizable: false,
				height: 180,
				modal: true,
				buttons: {
					Continue: function () {
						$(this).dialog("close");
						options.confirmAction();
					},
					Cancel: function () {
						$(this).dialog("close");
						options.cancelAction();
					}
				}
			});
	};

	//Attach global AJAX error handler
	$(document).ajaxError(function (event, request, options) {
		//Close all open dialogs
		$(".ui-dialog-content").dialog("close");
		if (request.status === 401) {
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