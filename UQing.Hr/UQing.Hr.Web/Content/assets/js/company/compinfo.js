$(function () {
	(function (window) {
		var tradeArr = ['计算机硬件', '计算机软件', '互联网/电子商务'
			, 'IT服务/系统集成', '通信/电信', '电子技术/半导体/集成电路'
			, '保险/金融', '贸易/进出口', '快速消费品', '生物/制药/医疗器械'
			, '钢铁/机械 ', '广告/媒体', '医疗·化工', '教育/培训'
			, '交通/运输/物流', '餐饮/酒店/娱乐'
			, '政府/非盈利机构', '中介/专业服务', '不限行业', '其他行业'];

		init();
		function init() {
			setTradeHtml();
			getCompInfo();
		}
		function setTradeHtml() {
			var html = '<option value="">--请选择--</option>';
			for (var i in tradeArr) {
				html += '<option value="' + tradeArr[i] + '">' + tradeArr[i] + '</option>';
			}
			$("#trade").html(html);
		}
		function getCompInfo() {
			$.ajax({
				url: "/company/getcompinfo",
				type: "post",
				datatype: "json",
				timeout: 5000,
				data: {

				},
				success: function (resp) {
					if (resp.result == 1) {
						setCompInfoHtml(resp.data.serUserInfo);
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
		function setCompInfoHtml(info) {
			$("#companyName").attr("title", ((!!info.Company) ? info.Company : "")).html(((!!info.Company) ? info.Company : ""));
			setTradeSelect(info);
			$("#serName").val(((!!info.RealName) ? info.RealName : ""));
			$("#phone").val(((!!info.Phone) ? info.Phone : ""));
			$("#email").val(((!!info.Email) ? info.Email : ""));
		}
		function setTradeSelect(info) {
			$("#trade").children('option').removeAttr("selected");
			if ((!!info.Trade) && (tradeArr.indexOf(info.Trade) >= 0)) {
				$("#trade").children('option[value="' + info.Trade + '"]').attr("selected", "selected");
			}
		}
		eventBind();
		function eventBind() {
			saveInfo();
		}

		function saveInfo() {
			$("#btnSaveInfo").unbind("click");
			$("#btnSaveInfo").bind("click", function () {
				var trade = $("#trade").val();
				if (!trade || tradeArr.indexOf(trade) < 0) {
					comTipsHelper.disappearTips('remind', '请选择所属行业');
					return;
				}
				var serName = $("#serName").val();
				if (!serName) {
					serName = '';
				}
				serName = $.trim(serName);
				$("#serName").val(serName);
				if (!serName || !memberHelper.isMatchName(serName)) {
					comTipsHelper.disappearTips('remind', '请重新填写联系人');
					return;
				}

				var phone = $("#phone").val();
				if (!phone || !memberHelper.isMatchPhone(phone)) {
					comTipsHelper.disappearTips('remind', '请填写正确的手机号码');
					return;
				}
				var email = $("#email").val();
				if (!email || !memberHelper.isMatchEmail(email)) {
					comTipsHelper.disappearTips('remind', '请填写正确的邮箱');
					return;
				}
				$.ajax({
					url: "/company/setcompinfo",
					type: "post",
					datatype: "json",
					timeout: 5000,
					data: {
						trade: trade,
						serName: serName,
						phone: phone,
						email: email
					},
					success: function (resp) {
						if (resp.result == 1) {
							//保存成功
							comTipsHelper.disappearTips('success', '保存成功', function () {
								yHelper.response.redirect("/company");
							});
						} else if (resp.result == 0) { //验证失败

						} else if (resp.result == 3) { //保存失败

						} else if (resp.result == 2) { //经济人不存在

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
