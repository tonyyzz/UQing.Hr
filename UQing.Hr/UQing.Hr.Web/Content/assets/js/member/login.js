//member login
$(function () {
	(function (window) {
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
		eventBind();
		function eventBind() {
			loginClickEvent();
			enterClickEvent();
			selectIdentity();
			registNowEvent();
			setTimeout(function () {
				login.getNameObj().focus();
			}, 300);
		}
		//立即注册
		function registNowEvent() {
			//$("#registNow").unbind("click");
			//$("#registNow").bind("click", function () {
			//	if (identity == 'p') {
			//		yHelper.response.redirect("/member/regist/1");
			//	} else if (identity == "s") {
			//		yHelper.response.redirect("/member/regist/2");
			//	} else {
			//		yHelper.response.redirect("/member/register");
			//	}
			//});
		}
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
				$.ajax({
					url: "/member/login",
					type: "post",
					datatype: "json",
					data: {
						username: name,
						pwd: pwdObj.val(),
						idt: identity
					},
					timeout: 5000,
					success: function (resp) {
						console.log(resp);
						if (resp.result == 1) {
							//登录成功
							tips.setTips("登录成功");
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
		//回车登录
		function enterClickEvent() {
			var nameObj = login.getNameObj();
			var pwdObj = login.getPwdObj();
			nameObj.unbind("keypress");
			nameObj.bind('keypress', function (event) {
				if (event.keyCode == "13") {
					pwdObj.val("").focus();
					tips.hideTips();
				}
			});
			pwdObj.unbind("keypress");
			pwdObj.bind('keypress', function (event) {
				if (event.keyCode == "13") {
					login.getBtnLoginObj().click();
				}
			});
		}

	})(window)
})