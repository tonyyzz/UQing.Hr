$(function () {
	(function (window) {
		//member 帮助类
		var memberHelper = {
			/*
			设置提示信息方法
			调用方式： //type:"err","ok","normal"
			memberHelper.setTipsHtml({ jObj: nameObj, type: "err", text: "请输入用户名" });
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
			memberHelper.hideTipsHtml({jObj: nameObj });
			*/
			hideTipsHtml: function (obj) {
				if (!obj.jObj || obj.jObj.length <= 0) {
					return;
				}
				obj.jObj.closest(".J_validate_group").children(".td2").html("");
			},
			//memberHelper.isMatchCompanyName(companyName)
			isMatchCompanyName: function (companyName) {
				return yHelper.regex.isMatch(companyName, /^[\s|\S]{2,25}$/g);
			},
			//memberHelper.isMatchName(name)
			isMatchName: function (name) {
				return yHelper.regex.isMatch(name, /^([\u4e00-\u9fa5]|[a-zA-Z])([\u4e00-\u9fa5]|[0-9a-zA-Z]){5,17}$/g);
			},
			//memberHelper.isMatchPhone(phone)
			isMatchPhone: function (phone) {
				return yHelper.regex.isMatch(phone, /^1\d{10}$/g);
			},
			//memberHelper.isMatchEmail(email)
			isMatchEmail: function (email) {
				return yHelper.regex.isMatch(email, /^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$/g);
			},
			//memberHelper.isMatchPwd(pwd);
			isMatchPwd: function (pwd) {
				return yHelper.regex.isMatch(pwd, /^[\s|\S]{6,16}$/g);
			},
			/*
			公共聚焦检测事件
			调用方式：
			memberHelper.comFocusEvent({jObj: nameObj, text: "中英文开头6-18位，无特殊符号" });
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
						memberHelper.setTipsHtml({ jObj: obj.jObj, type: "normal", text: obj.text });
						return;
					}
				})
			}
		}
		window.memberHelper = memberHelper;
	})(window)
})