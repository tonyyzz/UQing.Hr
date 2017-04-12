//单个工作的详情相关js
$(function () {
	(function (window) {
		init();
		function init() {
			getJobInfo();
		}
		function getJobInfo() {
			var request = yHelper.request.getParams();
			var postid = request["id"];
			if (!postid) {
				yHelper.response.redirect("/error/notfound");
				return;
			}
			$.ajax({
				url: "/jobs/getjob",
				type: "post",
				datatype: "json",
				data: {
					postid: postid,
				},
				timeout: 3000,
				success: function (resp) {
					if (resp.result == 1) {
						setHtmlInfo(resp.data.postInfo);
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

		function setHtmlInfo(postInfo) {
			//工作信息（左侧）
			$("#jobname").text(postInfo.PostName);
			$("#jobSalary").text(postInfo.Salary);
			$("#jobcreatetime").text(yHelper.date.getDayStr(postInfo.CreateTime));
			$("#J_jobs_click").text(postInfo.SeeCount + "次");
			$("#jobEntice").html(getEnticeHtml(postInfo.OtherPoint));
			$("#PostDuty").html(getPostDuty(postInfo.PostDuty));
			$("#jobServerUser").html("联系人：" + postInfo.RealName);
			$("#companyAndPost").html(postInfo.Position + "&nbsp;&nbsp;" + postInfo.Company);
			$("#WorkAdress").html('<span>工作地点</span>' + postInfo.WorkAdress + '');

			//公司信息（右侧）
			$("#companyName").attr({ "title": postInfo.Company, "href": "/jobs/company?id=" + postInfo.SerUserID }).html(postInfo.Company);
			$("#companyInfo").html(getCompanyInfoHtml(postInfo));

		}
		function getCompanyInfoHtml(postInfo) {
			var html = '';
			html += ''
				+ '<div class="info"><span>性质</span>' + postInfo.Nature + '</div>'
				+ '<div class="info"><span>行业</span>' + postInfo.Trade + '</div>'
				+ '<div class="info"><span>规模</span>' + postInfo.Scale + '</div>'
				+ '<div class="info"><span>地点</span>' + postInfo.Address + '</div>';
			return html;
		}
		function getPostDuty(PostDuty) {
			PostDuty = PostDuty.replace(/\n|\r\n|\r/mg, '<br />');
			return PostDuty;
		}
		function getEnticeHtml(enticeStr) {
			enticeStr = enticeStr.replace(/\s+/img, '');
			var pattern = /(([\u4e00-\u9fa5]|[a-zA-Z])+)/ig;
			var arr = yHelper.regex.getRegexArr(enticeStr, pattern);
			var html = '';
			if (arr.length > 0) {
				for (var i in arr) {
					html += '<div class="li">' + arr[i] + '</div>';
				}
			}
			html += '<div class="clear"></div>';
			return html;
		}
	})(window)
})