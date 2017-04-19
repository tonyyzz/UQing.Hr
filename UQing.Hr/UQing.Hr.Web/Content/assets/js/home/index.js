$(function () {
	(function (window) {
		var recruitPageIndex = comHelper.pageInfo.PageIndex + 1; //最新招聘的pageIndex，从2开始，因为首页已经查过一次了
		var pageSize = 20;
		init();
		function init() {
			loginDialogInfo();
			searchOption();
			changeBatchEvent();

			changeNewsTypeEvent();
			$("#newsTypeDiv").children(".J_news_list_title").eq(4).trigger("click");

			//焦点资讯、热点新闻点击切换事件
			newsFocusEvent();
		}
		function newsFocusEvent() {
			$("#newsFoucsDiv").children(".newstli").unbind("click");
			$("#newsFoucsDiv").children(".newstli").bind("click", function () {
				var $that = $(this);
				$that.addClass("select").siblings(".newstli").removeClass("select");
			});
		}
		//切换新闻资讯选项卡点击事件
		function changeNewsTypeEvent() {
			$("#newsTypeDiv").children(".J_news_list_title").unbind("click");
			$("#newsTypeDiv").children(".J_news_list_title").bind("click", function () {
				var $that = $(this);
				$that.addClass("select").siblings().removeClass("select");
				var typeid = $that.attr("data-typeid");
				$.ajax({
					url: "/news/getnewslist",
					type: "post",
					datatype: "json",
					timeout: 5000,
					data: {
						typeid: typeid,
						pageIndex: 1,
						pageSize: 17 //最多能放17个
					},
					success: function (resp) {
						if (resp.result == 1) {
							setNewsListHtml(resp.data.newsList);
						} else {

						}
					},
					complete: function (XMLHttpRequest, status) {
						if (status == 'timeout') {	//超时

						} else if (XMLHttpRequest.status != "200") {	//返回失败状态

						}
					}
				})
			});
		}
		function setNewsListHtml(newsList) {
			if (!newsList || newsList.length <= 0) {
				$("#newsList").empty();
				return;
			}
			else {
				var html = '';
				html += ''
					+ '<div class="imgnews">'
					+ '	<div class="imgs">'
					+ '		<a target="_blank" href="' + newsList[0].NewsCon + '">'
					+ '			<img src="/Content/assets/img/no_img_news.png" border="0">'
					+ '		</a>'
					+ '	</div>'
					+ '	<div class="txt substring link_yellow">'
					+ '		<a target="_blank" href="' + newsList[0].NewsCon + '">' + newsList[0].Title + '</a>'
					+ '	</div>'
					+ '</div>';
				if (newsList.length > 1) {
					var item = null;
					html += '<div class="txtnews link_gray6">';
					for (var i = 1; i < newsList.length; i++) {
						item = newsList[i];
						html += ''
							+ '<div class="nlist substring">'
							+ '	<a target="_blank" href="' + item.NewsCon + '">' + item.Title + '</a>'
							+ '</div>';
					}
					html += '<div class="clear"></div>';
					html += '</div>';
				}
				$("#newsList").html(html);
			}

		}
		//-------------------- 换一批 开始 ----------------------------
		function changeBatchEvent() {
			//最新招聘
			$("#changeNewsestRecruit").unbind("click");
			$("#changeNewsestRecruit").bind("click", function () {
				getNewsestRecruitFromServer(recruitPageIndex);
			});
			function getNewsestRecruitFromServer(pageIndex) {
				$.ajax({
					url: "/home/changerecruit",
					type: "post",
					datatype: "json",
					timeout: 5000,
					data: {
						pageIndex: recruitPageIndex,
						pageSize: pageSize
					},
					success: function (resp) {
						if (resp.result == 1) {
							setNewsestRecruitHtml(resp.data.newsestRecruits);
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
			function setNewsestRecruitHtml(newsestRecruits) {
				if (!newsestRecruits || newsestRecruits.length <= 0) {
					recruitPageIndex = comHelper.pageInfo.PageIndex;
					getNewsestRecruitFromServer(recruitPageIndex);
				} else {
					recruitPageIndex++;
					var html = '';
					var item = null;
					for (var i in newsestRecruits) {
						item = newsestRecruits[i];
						html += ''
							+ '<div class="jli j' + (parseInt(i) + 1) + '">'
							+ '	<div class="jcom_name_box link_gray6">'
							+ '		<a class="line_substring" href="/jobs/company?id=' + item.SerUserID + '" target="_blank">' + item.Company + '</a>'
							+ '		<div class="clear"></div>'
							+ '	</div>'
							+ '	<div class="jobs_gourp link_gray6">'
							+ '		<a target="_blank" href="/jobs/show?id=' + item.SerUserPostID + '" class="a_job">' + item.PostName + '</a>'
							+ '		<div class="clear"></div>'
							+ '	</div>'
							+ '</div>';
					}
					$("#newsestRecruitDiv").html(html);
				}
			}
		}
		//-------------------- 换一批 结束 ----------------------------



		function loginDialogInfo() {
			var $home_noLoginDiv = $("#home_noLogin");
			if (!!$home_noLoginDiv && $home_noLoginDiv.length > 0) {
				//登录操作
				noLoginOption();
			}
			var $home_logoutDiv = $("#home_logout");
			if (!!$home_logoutDiv && $home_logoutDiv.length > 0) {
				//退出
				logoutOption();
			}
		}

		//-------------------- 登录操作 开始 ----------------------------
		function noLoginOption() {
			//身份类型（p：求职者(默认)，s：经纪人）
			var identity = 'p';
			var login = {
				getIdentityObj: function () {
					return $("#identity");
				},
				getNameObj: function () {
					return $("#username");
				},
				getPwdObj: function () {
					return $("#password");
				},
				getBtnLoginObj: function () {
					return $("#btnLogin");
				},
				getAutoLoginObj: function () {
					return $("#autoLogin");
				}
			}
			var tips = {
				setTips: function (str) {
					var tipsObj = $("#tips");
					tipsObj.text(str).show();
				},
				hideTips: function () {
					var tipsObj = $("#tips");
					tipsObj.hide();
				}
			}
			selectIdentity();
			loginClickEvent();
			//身份类型选择
			function selectIdentity() {
				var identityObj = login.getIdentityObj();
				identityObj.children(".item").unbind("click");
				identityObj.children(".item").bind("click", function () {
					tips.hideTips();
					var $that = $(this);
					identityObj.children(".item").removeClass("active");
					$that.addClass("active");
					var idt = $that.data("idt");
					if (idt == 1) {
						identity = 'p';
					} else if (idt == 2) {
						identity = 's';
					} else {
						identity = '';
					}
					login.getNameObj().select();
				});
			}
			//登录按钮点击事件
			function loginClickEvent() {
				var btnLogin = login.getBtnLoginObj();
				btnLogin.unbind("click");
				btnLogin.bind("click", function () {
					var nameObj = login.getNameObj();
					var pwdObj = login.getPwdObj();
					tips.hideTips();
					var name = nameObj.val();
					if (!name) {
						name = "";
					}
					name = $.trim(name);
					nameObj.val(name);
					if (!name) {
						nameObj.focus();
						tips.setTips("请填写手机号/会员名/邮箱");
						return;
					}
					if (!pwdObj.val()) {
						pwdObj.val("").focus();
						tips.setTips("请填写密码");
						return;
					}
					var autoLoginObj = login.getAutoLoginObj();
					var isChecked = autoLoginObj.prop("checked");
					$.ajax({
						url: "/member/login",
						type: "post",
						datatype: "json",
						data: {
							username: name,
							pwd: pwdObj.val(),
							idt: identity,
							autoLogin: (!!isChecked ? "1" : "")
						},
						timeout: 8000,
						success: function (resp) {
							console.log(resp);
							if (resp.result == 1) {
								//登录成功
								location.reload(true);
							} else if (resp.result == 0) { //验证非法
								if (resp.data.flag == 1) {
									tips.setTips("请填写手机号/会员名/邮箱");
									login.getNameObj().focus();
								} else if (resp.data.flag == 2) {
									tips.setTips("请填写密码");
									login.getPwdObj().focus();
								} else {
									tips.setTips("非法操作");
								}
							} else if (resp.result == 2) {
								tips.setTips("用户名或密码错误");
							} else {
								tips.setTips("非法操作");
							}
						},
						complete: function (XMLHttpRequest, status) {
							if (status == 'timeout') {	//超时
								tips.setTips("服务器繁忙，请稍后重试！");
							} else if (XMLHttpRequest.status != "200") {	//返回失败状态
								tips.setTips("服务器错误！");
							}
						}
					})
				})
			}
		}
		//退出登录
		function logoutOption() {
			$("#home_logout").unbind("click");
			$("#home_logout").bind("click", function () {
				loginTopHelper.logout({
					logoutFunc: function (resp) {
						if (resp.result == 1) { //注销成功
							location.reload(true);
						} else if (resp.result == 0) { //注销失败

						} else { //操作非法

						}
					}
				})
			});
		}
		//-------------------- 登录操作 结束 ----------------------------


		//-------------------- 职位搜索 开始 ----------------------------
		function searchOption() {
			//var searchType = 1; //1：搜职位 默认（2：搜简历）
			searchTypeSwitchShow();
			searchEvent();
			function searchTypeSwitchShow() {
				$("#searchBoxDiv").children(".selecttype").unbind("click");
				$("#searchBoxDiv").children(".selecttype").bind("click", function () {
					var $that = $(this);
					$that.next(".selecttype_down").toggle();
					searchTypeSwitch();
				});
			}
			function searchTypeSwitch() {
				var $listDiv = $("#searchBoxDiv").find(".down_list");
				$listDiv.unbind("click");
				$listDiv.bind("click", function () {
					var $that = $(this);
					var type1 = $("#searchBoxDiv").children(".selecttype").attr("data-type");
					var txt1 = $("#searchBoxDiv").children(".selecttype").text();
					var type2 = $that.attr("data-type");
					var txt2 = $that.text();
					$("#searchBoxDiv").children(".selecttype").attr("data-type", type2);
					$that.attr("data-type", type1);
					$("#searchBoxDiv").children(".selecttype").text(txt2);
					$that.text(txt1);
					setTimeout(function () {
						$("#searchBoxDiv").children(".selecttype_down").css('display', 'none');
					})
				});
			}
			function searchEvent() {
				$("#btnSearch").unbind("click");
				$("#btnSearch").bind("click", function () {
					var $keyObj = $("#key");
					var key = $keyObj.val();
					if (!key) {
						key = '';
					}
					key = $.trim(key);
					if (!key) {
						$keyObj.focus();
						return;
					}
					var type = $("#searchBoxDiv").children(".selecttype").attr("data-type");
					if (type == 1) { //搜职位
						yHelper.response.redirect("/jobs/list?key=" + encodeURIComponent(key));
					} else { //搜简历
						yHelper.response.redirect("/talent/list?key=" + encodeURIComponent(key));
					}
				});
			}
		}
		//-------------------- 职位搜索 结束 ----------------------------

		eventBind();
		function eventBind() {

		}
	})(window)
})
