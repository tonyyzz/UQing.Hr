//求职者的求职信息页面的js
$(function () {
	(function (window) {



		init();
		function init() {
			getTalentInfo();
		}
		eventBind();
		function eventBind() {

		}

		//获取人才求职者的求职信息
		function getTalentInfo() {
			var request = yHelper.request.getParams();
			var perId = request["id"];
			if (!perId) {
				yHelper.response.redirect("/error/notfound");
				return;
			}
			$.ajax({
				url: "/talent/getperinfo",
				type: "post",
				datatype: "json",
				data: {
					perId: perId,
				},
				timeout: 3000,
				success: function (resp) {
					if (resp.result == 1) {
						setPersonInfoHtml(resp.data.personInfo);
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
		function setPersonInfoHtml(personInfo) {
			if (!personInfo) {
				return;
			}
			if (!!personInfo.Photo) {
				$("#perHeadImg").attr({ "src": comHelper.resourceHost + personInfo.Photo })
			} else {
				$("#perHeadImg").attr({ "src": "/Content/assets/img/no_photo_male.png" })
			}
			$("#perUpdateTime").html("更新时间：" + yHelper.date.getDayStr(personInfo.RewardTime));
			$("#perName").html(personInfo.RealName);
			$("#perIntro").html(getPerIntro(personInfo));
			$("#perNowAddress").html("现居住：" + (personInfo.City ? personInfo.City : "保密"));
			$("#jobIntension").html(getJobIntension(personInfo));
			$("#perContact").html(getPerInf(personInfo));
		}
		function getPerInf(personInfo) {
			var html = '';
			if (!!personInfo.Phne) {
				html += '<div class="it tel">' + personInfo.Phne + '</div>';
			}
			if (!!personInfo.Email) {
				html += '<div class="it email">' + personInfo.Email + '</div>';
			}
			if ((!!personInfo.Phne) || (!!personInfo.Email)) {
				html += '<div class="J_downbtn downbtn">获取联系方式</div>';
			}
			html += '<div class="clear"></div>';
			return html;
		}
		//求职意向html
		function getJobIntension(personInfo) {
			var html = '';
			html += ''
				+ '<span> 期望职位：</span>' + personInfo.EngagePost + '<br>'
				+ '<span> 期望行业：</span>' + personInfo.Trade + '<br>'
				+ '<span> 期望薪资：</span>' + personInfo.DemandPay + '<u></u>' + (!!personInfo.OneDes ? personInfo.OneDes : "") + '<br>'
				//+ '<span> 求职状态：</span>我目前在职，但考虑换个新环境<br>'
				+ '';
			return html;

		}
		function getPerIntro(personInfo) {
			var html = '';
			html += ''
				+ (!!personInfo.Sex ? "男" : "女")
				+ '<span>|</span>'
				+ comHelper.getYearAge(personInfo.Birth) + '岁'
				+ '<span>|</span>'
				+ personInfo.Education
				+ '<span>|</span>'
				+ personInfo.WorkLife + '工作经验';
			return html;
		}
	})(window)
})