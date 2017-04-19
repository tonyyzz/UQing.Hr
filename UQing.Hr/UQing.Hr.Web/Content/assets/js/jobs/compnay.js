//找工作的某个公司详情页面的js
$(function () {
	(function (window) {

		eventBind();
		function eventBind() {
			tabSwitch();
		}

		init();
		function init() {
			getJobInfo();
			getRecruitingPosition();
			getAllPost();
		}


		//tab显示切换
		function tabSwitch() {
			$("#cmpnyHome").unbind("click");
			$("#cmpnyHome").bind("click", function () {
				$("#cmpnyHome").addClass("select");
				$("#allPost").removeClass("select");
				$("#cmpnyHomeTab").show();
				$("#allPostTab").hide();
			});
			$("#allPost").unbind("click");
			$("#allPost").bind("click", function () {
				$("#cmpnyHome").removeClass("select");
				$("#allPost").addClass("select");
				$("#cmpnyHomeTab").hide();
				$("#allPostTab").show();
			});
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
		//获取在招职位信息 top 1
		function getRecruitingPosition() {
			var request = yHelper.request.getParams();
			var serUserId = request["id"];
			if (!serUserId) {
				yHelper.response.redirect("/error/notfound");
				return;
			}
			$.ajax({
				url: "/jobs/getcmpnypost",
				type: "post",
				datatype: "json",
				data: {
					serUserId: serUserId,
					pageSize: 1
				},
				timeout: 3000,
				success: function (resp) {
					if (resp.result == 1) {
						setRecruitingPositionHtml(resp.data.postLi);
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
		//设置在招职位的html top 1
		function setRecruitingPositionHtml(postLi) {
			if (!postLi || postLi.length <= 0) {
				return;
			}
			var postInfo = postLi[0];
			var html = '';
			html += ''
				+ '<div class="t t3">在招职位</div>'
				+ '<div class="more link_gray6">'
				+ '	<a id="lookAllPost" href="javascript:void(0)">全部职位&gt;&gt;</a>'
				+ '</div>'
				+ '<div class="jobs">'
				+ '	<div class="jobsli link_blue">'
				+ '		<div class="ljob">'
				+ '			<a href="/jobs/show?id=' + postInfo.SerUserPostID + '">' + postInfo.PostName + '</a>'
				+ '			<span>[若干人]</span>'
				+ '		</div>'
				+ '		<div class="rjob c">' + postInfo.Salary + '</div>'
				+ '		<div class="ljob">'
				+ '' + postInfo.WorkAdress
				+ '			<span>&nbsp;</span>'
				+ '		</div>'
				+ '		<div class="rjob">' + yHelper.date.getDayStr(postInfo.CreateTime) + '</div>'
				+ '		<div class="clear"></div>'
				+ '	</div>'
				+ '	<div class="clear"></div>'
				+ '</div>';
			$("#recruitingPost").html(html);

			$("#lookAllPost").unbind("click");
			$("#lookAllPost").bind("click", function () {
				$("#allPost").trigger("click");
			});

		}

		//获取全部职位信息
		function getAllPost() {
			var request = yHelper.request.getParams();
			var serUserId = request["id"];
			if (!serUserId) {
				yHelper.response.redirect("/error/notfound");
				return;
			}
			$.ajax({
				url: "/jobs/getcmpnypost",
				type: "post",
				datatype: "json",
				data: {
					serUserId: serUserId,
					pageSize: 9999
				},
				timeout: 3000,
				success: function (resp) {
					if (resp.result == 1) {
						setAllPostHtml(resp.data.postLi);
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

		function setAllPostHtml(postLi) {
			if (!postLi || postLi.length <= 0) {
				return;
			}
			var html = '';
			var postInfo = null;
			for (var i in postLi) {
				postInfo = postLi[i];
				html += ''
					+ '<div class="infobox link_blue">'
					+ '	<div class="jobslist J_jobsList">'
					+ '		<div class="jname">'
					+ '			<a href="/jobs/show?id=' + postInfo.SerUserPostID + '">'
					+ '				<strong>' + postInfo.PostName + '</strong>'
					+ '			</a>'
					+ '			<span>[若干人]</span>'
					//+ '			<span>全职</span>'
					+ '			<span>'
					+ '				<u>' + yHelper.date.getDayStr(postInfo.CreateTime) + '</u>'
					+ '			</span>'
					+ '		</div>'
					+ '		<div class="jtxt">'
					+ '			<u>' + postInfo.Salary + '</u>'
					+ '			<span>|</span>'
					+ '' + postInfo.WorkAdress
					+ '		</div>'
					+ '		<div class="jobapp J_applyForJob">投递简历</div>'
					+ '	</div>'
					+ '</div>';
			}
			$("#allPostTab").html(html);
		}


		function setHtmlInfo(companyInfo) {
			//页面左侧
			$("#CompanyName").html(companyInfo.Company);
			$("#compnayIntro").html(getCompnayIntroHtml(companyInfo));
			$("#recruitPostCount").html(companyInfo.recruitPostCount + '个');
			$("#DevelopProspect").html(getDevelopProspectHtml(companyInfo.DevelopProspect));
			$("#allPost").html('在招职位<span>(' + companyInfo.recruitPostCount + ')</span>');
			$("#Phone").html(companyInfo.Phone);
			$("#Email").attr("title", companyInfo.Email).html(companyInfo.Email);
			$("#WorkCity").attr("title", companyInfo.WorkCity).html(companyInfo.WorkCity);


			loginTopHelper.getUserInfo({
				loginFunc: function (resp) { //登录
					$("#lookAfterLogin").html("");
				},
				notLoginFunc: function (resp) { //未登录
					$("#lookAfterLogin").html("登陆后查看").unbind("click");
					$("#lookAfterLogin").bind("click", function () {
						loginDlgHelper.showDlg();
					})
				},
			})

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
				+ '' + ((!!companyInfo.WorkCity) ? (companyInfo.WorkCity + '<span></span>') : "")
				+ '' + ((!!companyInfo.Nature) ? companyInfo.Nature + '<span>&nbsp;</span>' : "")
				+ '' + ((!!companyInfo.Trade) ? companyInfo.Trade + '<span>&nbsp;</span>' : "")
				+ '' + ((!!companyInfo.Scale) ? companyInfo.Scale + '<span>&nbsp;</span>' : "");
			return html;
		}



	})(window)
})