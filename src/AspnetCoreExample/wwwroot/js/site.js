// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var pjax = new Pjax({
});

function handlePjaxComplete(e) {
	console.log("handlePjaxComplete", e);
	//pjax.reload();
}

document.addEventListener("pjax:complete", handlePjaxComplete);
