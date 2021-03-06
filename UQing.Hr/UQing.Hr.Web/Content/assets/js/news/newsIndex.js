﻿$(function () {
	(function (window) {
		var pageIndex = comHelper.pageInfo.PageIndex;
		init();
		function init() {
			getNewsTypes();
			getNews();
		}

		function getNews(obj) {
			var key = $("#key").val();
			if (!key) {
				key = '';
			}
			key = $.trim(key);
			$("#key").val(key);
			$.ajax({
				url: "/news/getnewslist",
				type: "post",
				datatype: "json",
				timeout: 5000,
				data: {
					key: key,
					pageIndex: (((!!obj) && (!!obj.pageIndex)) ? obj.pageIndex : comHelper.pageInfo.PageIndex),
					pageSize: comHelper.pageInfo.PageSize,
				},
				success: function (resp) {
					if (resp.result == 1) {
						setNewsHtml(resp.data.newsList);
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

		function setNewsHtml(newsList) {
			if (!newsList || newsList.length <= 0) {
				$("#lookMore").html("没有更多最新资讯了").unbind("click");
			} else {
				var html = '';
				for (var i in newsList) {
					var item = newsList[i];
					html += ''
						+ '<div class="listb">'
						+ '	<div class="bl">'
						+ '		<div class="pic">'
						+ '			<a target="_blank" style="display:inline-block;" href="' + item.NewsCon + '">'
						+ '				<img src="' + comHelper.resourceHost + item.ImgUrl + '" onerror="this.src=\'/Content/assets/img/no_img_news.png\'" border="0">'
						+ '			</a>'
						+ '		</div>'
						+ '	</div>'
						+ '	<div class="br link_gray6">'
						+ '		<div class="t substring">'
						+ '			<a target="_blank" href="' + item.NewsCon + '">' + item.Title + '</a>'
						+ '		</div>'
						+ '		<div class="time substring">' + yHelper.date.getDayStr(item.CreateTime, '-') + '</div>'
						+ '		<div class="summary">' + (item.AbsDes ? item.AbsDes : "") + '</div>'
						+ '	</div>'
						+ '	<div class="clear"></div>'
						+ '</div>';
				}
				$("#news_list").append(html);
				$("#lookMore").html("查看更多");
				lookMoreEvent();
			}
		}

		function getNewsTypes() {
			$.ajax({
				url: "/news/getnewstype",
				type: "post",
				datatype: "json",
				timeout: 5000,
				success: function (resp) {
					if (resp.result == 1) {
						setNewsTypesHtml(resp.data.newsTypeList);
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

		function setNewsTypesHtml(newsTypeList) {
			if (!newsTypeList || newsTypeList.length <= 0) {
				return;
			} else {
				var html = '';
				for (var i in newsTypeList) {
					var item = newsTypeList[i];
					html += ''
						+ '<a href="/news/list?id=' + item.NewsTypeID + '">' + item.Name + '</a>';
				}
				html += '<div class="clear"></div>';
			}
			$("#newstypes").html(html);

		}

		eventBind();
		function eventBind() {
			searchEvent();
			lookMoreEvent();
		}
		function searchEvent() {
			$("#btnSearch").unbind("click");
			$("#btnSearch").bind("click", function () {
				var key = $("#key").val();
				if (!key) {
					key = '';
				}
				key = $.trim(key);
				$("#key").val(key);
				if (!key) {
					$("#key").focus();
					return;
				}
				$("#news_list").empty();
				pageIndex = comHelper.pageInfo.PageIndex;
				getNews({ pageIndex: pageIndex });
			});
		}
		function lookMoreEvent() {
			$("#lookMore").unbind("click");
			$("#lookMore").bind("click", function () {
				pageIndex = parseInt(pageIndex) + 1;
				getNews({ pageIndex: pageIndex });
			});
		}
	})(window)
})