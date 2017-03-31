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
		var helper = {
			/*
			设置提示信息方法
			调用方式： //type:"err","ok","normal"
			helper.setTipsHtml({ jObj: nameObj, type: "err", text: "请输入用户名" });
			*/
			setTipsHtml: function (obj) {
				if (!obj.jObj || obj.jObj.length <= 0) {
					return;
				}
				if (!obj.type) {
					return;
				}
				var html = '';
				switch (obj.type) {
					case "normal": {
						html += ''
							+ '<div class="tip" style="display: block;">'
							+ '	<div class="ftxt">' + obj.text + '</div><div class="fimg"></div>'
							+ '</div>'
							+ '';
					} break;
					case "err": {
						html += ''
							+ '<div class="tip err" style="display: block;">'
							+ '	<div class="ftxt">' + obj.text + '</div><div class="fimg"></div>'
							+ '</div>'
							+ '';
					} break;
					case "ok": {
						html += ''
							+ '<div class="ok"></div>';
					} break;
				}
				obj.jObj.closest(".J_validate_group").children(".td2").html(html);
			},
			/*
			隐藏提示信息方法
			调用方式：
			helper.hideTipsHtml({jObj: nameObj });
			*/
			hideTipsHtml: function (obj) {
				if (!obj.jObj || obj.jObj.length <= 0) {
					return;
				}
				obj.jObj.closest(".J_validate_group").children(".td2").html("");
			},
			//helper.isMatchName(name);
			isMatchName: function (name) {
				return yHelper.regex.isMatch(name, /^([\u4e00-\u9fa5]|[a-zA-Z])([\u4e00-\u9fa5]|[0-9a-zA-Z]){5,17}$/g);
			},
			//helper.isMatchEmail(email);
			isMatchEmail: function (email) {
				return yHelper.regex.isMatch(email, /^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$/g);
			},
			//helper.isMatchPwd(pwd);
			isMatchPwd: function (pwd) {
				return yHelper.regex.isMatch(pwd, /^[\s|\S]{6,16}$/g);
			},
			/*
			公共聚焦检测事件
			调用方式：
			helper.comFocusEvent({jObj: nameObj, text: "中英文开头6-18位，无特殊符号" });
			*/
			comFocusEvent: function (obj) {
				if (!obj.jObj || obj.jObj.length <= 0) {
					return;
				}
				if (!obj.text) {
					return;
				}
				obj.jObj.focus(function () {
					if (!obj.jObj.val()) {
						helper.setTipsHtml({ jObj: obj.jObj, type: "normal", text: obj.text });
						return;
					}
				})
			}
		}

		eventBind();
		function eventBind() {
			focusEvent();
			pwdKeyupEvent();
			blurEvent();
			registClickEvent();
		}

		function focusEvent() {
			helper.comFocusEvent({ jObj: register.getNameObj(), text: "中英文开头6-18位，无特殊符号" });
			helper.comFocusEvent({ jObj: register.getEmailObj(), text: "邮箱用于接收简历及系统重要通知" });
			helper.comFocusEvent({ jObj: register.getPwdObj(), text: "6-16位字符组成，区分大小写" });
			helper.comFocusEvent({ jObj: register.getPwdConfirmObj(), text: "请再次输入密码" });
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
					thisGroup.find('.slist.t1').removeClass('select');
					thisGroup.find('.slist.t2').removeClass('select');
					thisGroup.find('.slist.t3').addClass('select');
				} else if (!!mediumRegex.test(pwd)) {
					//密码为七位及以上并且字母、数字、特殊字符三项中有两项，强度是中等 
					thisGroup.find('.slist.t1').removeClass('select');
					thisGroup.find('.slist.t2').addClass('select');
					thisGroup.find('.slist.t3').removeClass('select');
				} else {
					//如果密码为6为及以下，就算字母、数字、特殊字符三项都包括，强度也是弱的
					thisGroup.find('.slist.t1').addClass('select');
					thisGroup.find('.slist.t2').removeClass('select');
					thisGroup.find('.slist.t3').removeClass('select');
				}
			});
		}

		function blurEvent() {
			//用户名
			var nameObj = register.getNameObj();
			nameObj.blur(function () {
				var name = nameObj.val();
				if (!name) {
					return;
				}
				if (!helper.isMatchName(name)) {
					helper.setTipsHtml({ jObj: nameObj, type: "err", text: "中英文开头6-18位，无特殊符号" });
					return;
				}
				helper.setTipsHtml({ jObj: nameObj, type: "ok" });
			})
			//邮箱
			var emailObj = register.getEmailObj();
			emailObj.blur(function () {
				var email = emailObj.val();
				if (!email) {
					return;
				}
				if (!helper.isMatchEmail(email)) {
					helper.setTipsHtml({ jObj: emailObj, type: "err", text: "邮箱格式不正确" });
					return;
				}
				helper.setTipsHtml({ jObj: emailObj, type: "ok" });
			})
			//密码
			var pwdObj = register.getPwdObj();
			pwdObj.blur(function () {
				var pwd = pwdObj.val();
				if (!pwd) {
					return;
				}
				if (!helper.isMatchPwd(pwd)) {
					helper.setTipsHtml({ jObj: pwdObj, type: "err", text: "密码长度要求为6-16个字符" });
					return;
				}
				helper.setTipsHtml({ jObj: pwdObj, type: "ok" });
			})
			//确认密码
			var pwdConfirmObj = register.getPwdConfirmObj();
			pwdConfirmObj.blur(function () {
				var pwdConfirm = pwdConfirmObj.val();
				if (!pwdConfirm) {
					return;
				}
				if (!helper.isMatchPwd(pwdConfirm)) {
					helper.setTipsHtml({ jObj: pwdConfirmObj, type: "err", text: "密码长度要求为6-16个字符" });
					return;
				}
				if (pwdConfirm != register.getPwdObj().val()) {
					helper.setTipsHtml({ jObj: pwdConfirmObj, type: "err", text: "两次输入的密码不一致" });
					return;
				}
				helper.setTipsHtml({ jObj: pwdConfirmObj, type: "ok" });
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
				if (!helper.isMatchName(name)) {
					helper.setTipsHtml({ jObj: register.getNameObj(), type: "err", text: "中英文开头6-18位，无特殊符号" });
				} else {
					helper.setTipsHtml({ jObj: register.getNameObj(), type: "ok" });
				}
				if (!helper.isMatchEmail(email)) {
					helper.setTipsHtml({ jObj: register.getEmailObj(), type: "err", text: "邮箱格式不正确" });
				} else {
					helper.setTipsHtml({ jObj: register.getEmailObj(), type: "ok" });
				}
				if (!helper.isMatchPwd(pwd)) {
					helper.setTipsHtml({ jObj: register.getPwdObj(), type: "err", text: "密码长度要求为6-16个字符" });
				} else {
					helper.setTipsHtml({ jObj: register.getPwdObj(), type: "ok" });
				}
				if (!helper.isMatchPwd(pwdConfirm)) {
					helper.setTipsHtml({ jObj: register.getPwdConfirmObj(), type: "err", text: "密码长度要求为6-16个字符" });
					return;
				}
				if (pwdConfirm != pwd) {
					helper.setTipsHtml({ jObj: register.getPwdConfirmObj(), type: "err", text: "两次输入的密码不一致" });
					return;
				}
				helper.setTipsHtml({ jObj: register.getPwdConfirmObj(), type: "ok" });

				//提交服务器验证
				alert();
			});
		}

	})(window)
})