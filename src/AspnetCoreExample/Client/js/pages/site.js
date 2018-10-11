import Pjax from "pjax";
import topbar from "topbar";

topbar.config({
	barColors: {
		"0": "#ffc107"
	}
});

class Site {
	constructor() {
		document.addEventListener("pjax:send", this.handlePjaxSend);
		document.addEventListener("pjax:complete", this.handlePjaxComplete);

		var pjax = new Pjax();
	}

	handlePjaxSend() {
		topbar.show();
	}

	handlePjaxComplete() {
		topbar.hide();
	}
}

new Site();
