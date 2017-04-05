$(function () {
	(function (window) {
		var identity = "p"; //身份类型，p：默认求职者，s：经纪人
		var theEmail = ''; //验证的正确邮箱
		var find = {
			getSelectIdtObj: function () {
				return $("#selectIdentity");
			},
			getEmailObj: function () {
				return $("#email");
			},
			//第一步的下一步
			getNext1Obj: function () {
				return $("#next1");
			},
			getVMailCodeObj: function () {
				return $("#vmailcode");
			},
			//第二步的下一步
			getNext2Obj: function () {
				return $("#next2");
			},
			getNewPwdObj: function () {
				return $("#newpwd");
			}
		}
		var findHelper = {
			//findHelper.gotoStep(1);  //第一步
			gotoStep: function (step) {
				$(".find_pwd .J_focus").hide();
				$("#step" + step).show();
				$(".find_pwd .step").removeClass("s1").removeClass("s2").removeClass("s3").addClass("s" + step);
				$(".find_pwd .steptxt .tli").removeClass("font_blue").eq(parseInt(step) - 1).addClass("font_blue").prevAll().addClass("font_blue");
			}
		}

		eventBind();
		//事件绑定
		function eventBind() {
			selectIdtEvent();
			focusEvent();
			blurEvent();
			next1ClickEvent();
			next2ClickEvent();
			//默认第一个聚焦
			setTimeout(function () {
				find.getEmailObj().focus();
			}, 300);
		}
		//切换身份
		function selectIdtEvent() {
			var selectIdtObj = find.getSelectIdtObj();
			selectIdtObj.children(".item").unbind("click");
			selectIdtObj.children(".item").bind("click", function () {
				var $that = $(this);
				selectIdtObj.children(".item").removeClass("active");
				$that.addClass("active");
				var idt = $that.data("idt");
				if (idt == 1) {
					identity = "p";
				} else if (idt == 2) {
					identity = "s";
				} else { //非法操作

				}
			});
		}
		//聚焦验证
		function focusEvent() {
			memberHelper.comFocusEvent({ jObj: find.getEmailObj(), text: "请填写账户绑定的邮箱" });
		}
		//失去焦点验证
		function blurEvent() {
			//邮箱
			var emailObj = find.getEmailObj();
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
			//验证随机码
			var vmailcodeObj = find.getVMailCodeObj();
			vmailcodeObj.blur(function () {
				var vmailcode = vmailcodeObj.val();
				if (!vmailcode) {
					memberHelper.comFocusEvent({ jObj: vmailcodeObj, text: "请填写验证随机码" });
					setTimeout(function () {
						vmailcodeObj.focus();
					}, 300);
					return;
				}
				memberHelper.setTipsHtml({ jObj: vmailcodeObj, type: "ok" });
			})
			//新密码
			var newpwdObj = find.getNewPwdObj();
			newpwdObj.blur(function () {
				var newpwd = newpwdObj.val();
				if (!newpwd) {
					memberHelper.comFocusEvent({ jObj: newpwdObj, text: "请填写新密码" });
					setTimeout(function () {
						newpwdObj.focus();
					}, 300);
					return;
				}
				if (!memberHelper.isMatchPwd(newpwd)) {
					memberHelper.setTipsHtml({ jObj: newpwdObj, type: "err", text: "密码长度要求为6-16个字符" });
					return;
				}
				memberHelper.setTipsHtml({ jObj: newpwdObj, type: "ok" });
			})
		}
		//第一步的下一步 点击
		function next1ClickEvent() {
			var next1Obj = find.getNext1Obj();
			next1Obj.unbind("click");
			next1Obj.bind("click", function () {
				var emailObj = find.getEmailObj();
				var email = emailObj.val();
				if (!email) {
					emailObj.focus();
					return;
				}
				if (!memberHelper.isMatchEmail(email)) {
					memberHelper.setTipsHtml({ jObj: emailObj, type: "err", text: "邮箱格式不正确" });
					return;
				}
				memberHelper.setTipsHtml({ jObj: emailObj, type: "ok" });
				//服务器验证是否存在
				$.ajax({
					url: "/member/existemail",
					type: "post",
					datatype: "json",
					data: {
						idt: identity,
						email: email,
					},
					//timeout: 5000,
					success: function (resp) {
						findHelper.gotoStep(2);
						theEmail = email;
						if (resp.result == 0) {
							if (resp.data.flag == 1) { //身份非法

							} else if (resp.data.flag == 2) { //邮箱非法

							} else { //非法操作

							}
						} else if (resp.result == 1) { //存在该邮箱，根据返回的idt属性区分是求职者还是经纪人的邮箱
							if (resp.data.idt == "p") { //求职者存在该邮箱

							} else if (resp.data.idt == "s") { //经纪人存在该邮箱

							} else { //非法操作

							}
						} else if (resp.result == 2) { //邮箱未被注册过
							if (resp.data.idt == "p") { //求职者不存在该邮箱

							} else if (resp.data.idt == "s") { //经纪人不存在该邮箱

							} else { //非法操作

							}
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
		//第二步的下一步 点击
		function next2ClickEvent() {
			var next2Obj = find.getNext2Obj();
			next2Obj.unbind("click");
			next2Obj.bind("click", function () {
				//验证随机码
				var vmailcodeObj = find.getVMailCodeObj();
				var vmailcode = vmailcodeObj.val();
				//新密码
				var newpwdObj = find.getNewPwdObj();
				var newpwd = newpwdObj.val();

				if (!vmailcode) {
					memberHelper.comFocusEvent({ jObj: vmailcodeObj, text: "请填写验证随机码" });
					setTimeout(function () {
						vmailcodeObj.focus();
					}, 300);
					return;
				}
				memberHelper.setTipsHtml({ jObj: vmailcodeObj, type: "ok" });
				if (!newpwd) {
					memberHelper.comFocusEvent({ jObj: newpwdObj, text: "请填写新密码" });
					setTimeout(function () {
						newpwdObj.focus();
					}, 300);
					return;
				}
				if (!memberHelper.isMatchPwd(newpwd)) {
					memberHelper.setTipsHtml({ jObj: newpwdObj, type: "err", text: "密码长度要求为6-16个字符" });
					return;
				}
				memberHelper.setTipsHtml({ jObj: newpwdObj, type: "ok" });
				//服务器验证
				$.ajax({
					url: "/member/newpwd",
					type: "post",
					datatype: "json",
					data: {
						idt: identity,
						vmailcode: vmailcode,
						newpwd: newpwd,
						theEmail: theEmail
					},
					//timeout: 5000,
					success: function (resp) {
						findHelper.gotoStep(3);
						if (resp.result == 0) {
							if (resp.data.flag == 1) { //身份非法

							} else if (resp.data.flag == 2) { //验证随机码为空

							} else if (resp.data.flag == 3) { //新密码设置非法

							} else if (resp.data.flag == 4) { //验证码为空或者超时

							} else if (resp.data.flag == 5) { //验证随机码错误

							} else if (resp.data.flag == 6) { //邮箱非法

							} else { //非法操作

							}
						} else if (resp.result == 1) { //修改成功
							if (resp.data.idt == "p") { //求职者

							} else if (resp.data.idt == "s") { //经纪人

							} else { //非法操作

							}
						} else if (resp.result == 2) { //信息不存在
							if (resp.data.idt == "p") { //求职者

							} else if (resp.data.idt == "s") { //经纪人

							} else { //非法操作

							}
						} else if (resp.result == 3) {  //修改失败
							if (resp.data.idt == "p") { //求职者

							} else if (resp.data.idt == "s") { //经纪人

							} else { //非法操作

							}
						} else { //非法操作

						}
					},
					complete: function (XMLHttpRequest, status) {
						if (status == 'timeout') {	//超时

						} else if (XMLHttpRequest.status != "200") {	//返回失败状态

						}
					}
				})
			})
		}
	})(window)
})