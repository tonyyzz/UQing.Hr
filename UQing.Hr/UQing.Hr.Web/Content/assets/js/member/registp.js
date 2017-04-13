$(function () {
	(function (window) {
		var register = {
			getNameObj: function () {
				return $("#username");
			},
			getEmailObj: function () {
				return $("#email");
			},
			getPwdObj: function () {
				return $("#password");
			},
			getPwdConfirmObj: function () {
				return $("#pwdConfirm");
			},
			getBtnRegistObj: function () {
				return $("#btnRegist");
			}
		}

		eventBind();
		//事件绑定
		function eventBind() {
			focusEvent();
			pwdKeyupEvent();
			blurEvent();
			registClickEvent();
			//默认第一个聚焦
			setTimeout(function () {
				register.getNameObj().focus();
			}, 300);
		}
		//聚焦验证
		function focusEvent() {
			memberHelper.comFocusEvent({ jObj: register.getNameObj(), text: "中英文开头6-18位，无特殊符号" });
			memberHelper.comFocusEvent({ jObj: register.getEmailObj(), text: "邮箱用于接收简历及系统重要通知" });
			memberHelper.comFocusEvent({ jObj: register.getPwdObj(), text: "6-16位字符组成，区分大小写" });
			memberHelper.comFocusEvent({ jObj: register.getPwdConfirmObj(), text: "请再次输入密码" });
		}

		//检测密码安全性的事件
		function pwdKeyupEvent() {
			var pwdObj, pwd, strongRegex, mediumRegex, enoughRegex, thisGroup;
			register.getPwdObj().unbind("keyup");
			register.getPwdObj().bind("keyup", function () {
				pwdObj = $(this);
				pwd = pwdObj.val();
				strongRegex = new RegExp("^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
				mediumRegex = new RegExp("^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
				enoughRegex = new RegExp("(?=.{6,}).*", "g");
				thisGroup = pwdObj.closest('.J_validate_group').next();
				if (!enoughRegex.test(pwd)) {
					//密码小于六位的时候，密码强度图片都为灰色
					thisGroup.find('.slist').removeClass('select');
				} else if (!!strongRegex.test(pwd)) {
					//密码为八位及以上并且字母数字特殊字符三项都包括,强度最强
					thisGroup.find('.slist').removeClass('select');
					thisGroup.find('.t3').addClass('select');
				} else if (!!mediumRegex.test(pwd)) {
					//密码为七位及以上并且字母、数字、特殊字符三项中有两项，强度是中等 
					thisGroup.find('.slist').removeClass('select');
					thisGroup.find('.t2').addClass('select');
				} else {
					//如果密码为6为及以下，就算字母、数字、特殊字符三项都包括，强度也是弱的
					thisGroup.find('.slist').removeClass('select');
					thisGroup.find('.t1').addClass('select');
				}
			});
		}

		//失去焦点验证
		function blurEvent() {
			//用户名
			var nameObj = register.getNameObj();
			nameObj.blur(function () {
				var name = nameObj.val();
				if (!name) {
					return;
				}
				if (!memberHelper.isMatchName(name)) {
					memberHelper.setTipsHtml({ jObj: nameObj, type: "err", text: "中英文开头6-18位，无特殊符号" });
					return;
				}
				memberHelper.setTipsHtml({ jObj: nameObj, type: "ok" });
			})
			//邮箱
			var emailObj = register.getEmailObj();
			emailObj.blur(function () {
				var email = emailObj.val();
				if (!email) {
					return;
				}
				if (!memberHelper.isMatchEmail(email)) {
					memberHelper.setTipsHtml({ jObj: emailObj, type: "err", text: "邮箱格式不正确" });
					return;
				}
				memberHelper.setTipsHtml({ jObj: emailObj, type: "ok" });
			})
			//密码
			var pwdObj = register.getPwdObj();
			pwdObj.blur(function () {
				var pwd = pwdObj.val();
				if (!pwd) {
					return;
				}
				if (!memberHelper.isMatchPwd(pwd)) {
					memberHelper.setTipsHtml({ jObj: pwdObj, type: "err", text: "密码长度要求为6-16个字符" });
					return;
				}
				memberHelper.setTipsHtml({ jObj: pwdObj, type: "ok" });
			})
			//确认密码
			var pwdConfirmObj = register.getPwdConfirmObj();
			pwdConfirmObj.blur(function () {
				var pwdConfirm = pwdConfirmObj.val();
				if (!pwdConfirm) {
					return;
				}
				if (!memberHelper.isMatchPwd(pwdConfirm)) {
					memberHelper.setTipsHtml({ jObj: pwdConfirmObj, type: "err", text: "密码长度要求为6-16个字符" });
					return;
				}
				if (pwdConfirm != register.getPwdObj().val()) {
					memberHelper.setTipsHtml({ jObj: pwdConfirmObj, type: "err", text: "两次输入的密码不一致" });
					return;
				}
				memberHelper.setTipsHtml({ jObj: pwdConfirmObj, type: "ok" });
			})
		}

		//注册按钮点击事件
		function registClickEvent() {
			var registObj = register.getBtnRegistObj();
			registObj.unbind("click");
			registObj.bind("click", function () {
				var name = register.getNameObj().val();
				var email = register.getEmailObj().val();
				var pwd = register.getPwdObj().val();
				var pwdConfirm = register.getPwdConfirmObj().val();
				if (!memberHelper.isMatchName(name)) {
					memberHelper.setTipsHtml({ jObj: register.getNameObj(), type: "err", text: "中英文开头6-18位，无特殊符号" });
				} else {
					memberHelper.setTipsHtml({ jObj: register.getNameObj(), type: "ok" });
				}
				if (!memberHelper.isMatchEmail(email)) {
					memberHelper.setTipsHtml({ jObj: register.getEmailObj(), type: "err", text: "邮箱格式不正确" });
				} else {
					memberHelper.setTipsHtml({ jObj: register.getEmailObj(), type: "ok" });
				}
				if (!memberHelper.isMatchPwd(pwd)) {
					memberHelper.setTipsHtml({ jObj: register.getPwdObj(), type: "err", text: "密码长度要求为6-16个字符" });
				} else {
					memberHelper.setTipsHtml({ jObj: register.getPwdObj(), type: "ok" });
				}
				if (!memberHelper.isMatchPwd(pwdConfirm)) {
					memberHelper.setTipsHtml({ jObj: register.getPwdConfirmObj(), type: "err", text: "密码长度要求为6-16个字符" });
					return;
				}
				if (pwdConfirm != pwd) {
					memberHelper.setTipsHtml({ jObj: register.getPwdConfirmObj(), type: "err", text: "两次输入的密码不一致" });
					return;
				}
				memberHelper.setTipsHtml({ jObj: register.getPwdConfirmObj(), type: "ok" });
				//提交服务器验证
				$.ajax({
					url: "/member/registp",
					type: "post",
					datatype: "json",
					data: {
						username: name,
						email: email,
						pwd: pwd,
						pwdConfirm: pwdConfirm
					},
					timeout: 3000,
					success: function (resp) {
						if (resp.result == 1) {
							//注册成功
							yHelper.response.redirect("/member/login");
							return;
						} else if (reap.result == 0) {
							if (resp.data.flag == 1) { //用户名非法

							} else if (resp.data.flag == 2) { //邮箱非法

							} else if (resp.data.flag == 3) { //密码设置非法

							} else if (resp.data.flag == 4) { //密码不一致

							} else { //非法操作

							}
						} else if (reap.result == 2) {
							if (resp.data.flag == 1) { //用户名已经被注册

							} else if (resp.data.flag == 2) { //邮箱已经被绑定

							} else { //非法操作

							}
						} else if (reap.result == 3) { //注册失败
						} else { //非法操作

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

	})(window)
})