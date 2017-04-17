
$(function () {
	(function (window) {
		init();
		function init() {
			getViewPerInfo();
		}
		function getViewPerInfo() {
			$.ajax({
				url: "/m/getviewperinfo",
				type: "post",
				datatype: "json",
				timeout: 5000,
				success: function (resp) {
					if (resp.result == 1) {
						setViewPerInfoHtml(resp.data.personInfo);
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
		function setViewPerInfoHtml(person) {
			$("#perHeadImg").attr("src", ((!!person.Photo) ? comHelper.resourceHost + person.Photo : "/Content/assets/img/m/no_photo_male.png"));
			$("#userContact").html(setUserContactHtml(person));
			$("#postCount").text(person.PostCount);
			$("#postInfo").html(setPostInfo(person));
		}
		function setPostInfo(person) {
			var html = '';
			if (!!person.EngagePost) {
				html += '期望职位：' + person.EngagePost + '<br>';
			} else {
				html += '期望职位：暂无<br>';
			}
			if (!!person.DemandPay) {
				html += '期望薪资：' + person.DemandPay + '<br>';
			} else {
				html += '期望薪资：暂无<br>';
			}
			if (!!person.WorkLife) {
				html += '工作经验：' + person.WorkLife + '<br>';
			} else {
				html += '工作经验：暂无<br>';
			}
			return html;
		}
		function setUserContactHtml(person) {
			var html = '';
			html += '<div class="td1 substring"><strong id="realname">' + (person.RealName ? '您好，' + person.RealName : "") + '</strong></div>';
			if (!!person.Phne) {
				html += '<div class="td2 link_gray6"><a href="javascript:void(0)">' + person.Phne + '</a></div>';
			}
			if (!!person.Email) {
				html += '<div class="td3 link_gray6"><a href="javascript:void(0)">' + person.Email + '</a></div>';
			}
			html += '<div class="clear"></div>';
			return html;
		}
		eventBind();
		function eventBind() {

		}
	})(window)
})
