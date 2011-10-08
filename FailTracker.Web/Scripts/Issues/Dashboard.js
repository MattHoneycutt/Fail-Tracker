$(function () {
	$("span.issue-delete").click(function () {
		var id = $(this).attr("data-issue-id");

		Site.confirm({
			title: "Delete this issue?",
			text: "Are you sure you wish to delete this issue?  You cannot undo this action.  All history will be lost!",
			confirmAction: function () {
				$.post("/delete");
			}
		});
	});
});
