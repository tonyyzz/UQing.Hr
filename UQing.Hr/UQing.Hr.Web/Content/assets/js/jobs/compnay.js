//找工作的某个公司详情页面的js
$(function () {
	(function (window) {
		
		init();
		function init() {
			getJobInfo();
		}
		function getJobInfo() {
			var request = yHelper.request.getParams();
			var serUserId = request["id"];
			if (!serUserId) {
				yHelper.response.redirect("/error/notfound");
				return;
			}
			$.ajax({
				url: "/jobs/getcmpny",
				type: "post",
				datatype: "json",
				data: {
					serUserId: serUserId,
				},
				timeout: 3000,
				success: function (resp) {
					if (resp.result == 1) {
						setHtmlInfo(resp.data.companyInfo);
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

		function setHtmlInfo(companyInfo) {
			//页面左侧
			$("#CompanyName").html(companyInfo.Company);
			$("#compnayIntro").html(getCompnayIntroHtml(companyInfo));
			$("#recruitPostCount").html(companyInfo.recruitPostCount + '个');
			$("#DevelopProspect").html(getDevelopProspectHtml(companyInfo.DevelopProspect));
			$("#recruitPostTab").html('在招职位<span>(' + companyInfo.recruitPostCount + ')</span>');
			$("#Phone").html(companyInfo.Phone);
			$("#Email").attr("title", companyInfo.Email).html(companyInfo.Email);
			$("#WorkCity").attr("title", companyInfo.WorkCity).html(companyInfo.WorkCity);

		}
		function getDevelopProspectHtml(DevelopProspect) {
			var html = '';
			if (!!DevelopProspect) {
				html = DevelopProspect.replace(/\n|\r\n|\r/mg, '<br />');
			} else {
				html = '暂无简介';
			}
			return html;

		}
		function getCompnayIntroHtml(companyInfo) {
			var html = '';
			html += ''
				+ '' + companyInfo.WorkCity + '<span>&nbsp;</span>'
				+ '' + companyInfo.Nature + '<span>&nbsp;</span>'
				+ '' + companyInfo.Trade + '<span>&nbsp;</span>'
				+ '' + companyInfo.Scale + '<span>&nbsp;</span>';
			return html;
		}








	})(window)
})