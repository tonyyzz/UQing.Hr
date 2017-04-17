$(function () {
	(function (window) {
		init();
		function init() {
			getSerUserInfo();
		}
		function getSerUserInfo() {
			$.ajax({
				url: "/company/getseruserinfo",
				type: "post",
				datatype: "json",
				timeout: 5000,
				success: function (resp) {
					if (resp.result == 1) {
						setSerUserInfoHtml(resp.data.serUserInfo);
					} else {

					}
				},
				complete: function (XMLHttpRequest, status) {
					if (status == 'timeout') {	//超时

					} else if (XMLHttpRequest.status != "200") {	//返回失败状态

					}
				}
			})
		}
		function setSerUserInfoHtml(serUserInfo) {
			$("#hrName").html(setHrNameHTml(serUserInfo));
			$("#compnayName").text(serUserInfo.Company);
			$("#companyInfo").html(setCompanyInfo(serUserInfo));
			$("#serContact").html(setSerContact(serUserInfo));
			$("#recruitingPostCount").html(serUserInfo.PostCount);
		}
		function setSerContact(serUserInfo) {
			var htmlArr = [];
			if (!!serUserInfo.Phone) {
				htmlArr.push('<a href="javascript:void(0)" class="btns btn2 ok">' + serUserInfo.Phone + '</a>');
			}
			if (!!serUserInfo.Email) {
				htmlArr.push('<a href="javascript:void(0)" class="btns btn3 ok">' + serUserInfo.Email + '</a>');
			}
			return htmlArr.join('|');
		}
		function setCompanyInfo(serUserInfo) {
			var htmlArr = [];
			if (!!serUserInfo.Trade) {
				htmlArr.push(serUserInfo.Trade);
			}
			if (!!serUserInfo.WorkCity) {
				htmlArr.push(serUserInfo.WorkCity);
			}
			return htmlArr.join('|');
		}
		function setHrNameHTml(serUserInfo) {
			var html = '';
			html += '<span>早上好，亲爱的' + serUserInfo.RealName + '</span>';
			html += '<br>今天是' + getTimeNowStrHtml(yHelper.date.getDateTimeArr(new Date())) + '';
			html += '<div class="clear"></div>';
			return html;
		}

		function getTimeNowStrHtml(timeArr) {
			return timeArr[0] + '年' + timeArr[1] + '月' + timeArr[2] + '日&nbsp;&nbsp;&nbsp;' + timeArr[7] + '';
		}

		eventBind();
		function eventBind() {

		}
	})(window)
})
