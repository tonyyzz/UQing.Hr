$(function () {
	(function (window) {
		var identity = "p"; //身份类型，p：默认求职者，s：经纪人
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
			}
		}

		eventBind();
		//事件绑定
		function eventBind() {
			selectIdtEvent();
			focusEvent();
			blurEvent();
			next1ClickEvent();
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
						email: email,
					},
					timeout: 3000,
					success: function (resp) {     //*****************要改逻辑
						if (resp.result == 0) {
							if (resp.data.flag == 1) { //邮箱非法

							} else { //非法操作

							}
						} else if (resp.result == 1) { //存在该邮箱，根据返回的idt属性区分是求职者还是经纪人的邮箱

						} else if (resp.result == 2) { //邮箱未被注册过

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