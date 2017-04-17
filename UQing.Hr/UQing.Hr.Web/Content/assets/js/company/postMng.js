﻿
$(function () {
	(function (window) {
		init();
		function init() {
			getPostList();
		}
		function getPostList() {
			$.ajax({
				url: "/company/getpostlist",
				type: "post",
				datatype: "json",
				timeout: 5000,
				success: function (resp) {
					if (resp.result == 1) {
						setPostListHtml(resp.data.posts);
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
		function setPostListHtml(posts) {
			if (!posts || posts.length <= 0) {
				var html = '';
				html += ''
					+ '<div class="res_empty">'
					+ '	亲爱的HR，您还没有发布职位<br>'
					+ '	想要快速找到合适的人才，就赶紧发布职位招揽人才吧~'
					+ '</div>'
					+ '<div class="res_empty_addbox" style="text-align:center;">'
					+ '	<div class="btn_blue J_hoverbut btn_115_38" style="display:inline-block;">发布职位&gt;&gt;</div>'
					+ '</div>';
				$("#postAllList").html(html);
			} else {
				var html = '';
				for (var i in posts) {
					html += ''
						+ '<div class="jobsList">'
						+ '	<div class="selWrap">'
						+ '	</div>'
						+ '	<div class="jobs fl">'
						+ '		<div class="title">'
						+ '			<a target="_blank" href="javascript:void(0)" class="">SDFF</a>'
						+ '			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;扬州/邗江区/汊河镇'
						+ '		</div>'
						+ '		<div class="update">'
						+ '			待处理简历：'
						+ '			<a href="javascript:void(0)" class="">0</a>'
						+ '			&nbsp;&nbsp; | &nbsp;&nbsp;更新时间：2017-04-17 16:53'
						+ '		</div>'
						+ '		<div class="J_operation btns">'
						+ '			<a href="javascript:void(0)">修改</a>'
						+ '			<a href="javascript:void(0)" class="close">关闭</a>'
						+ '			<a href="javascript:void(0)" class="delete">删除</a>'
						+ '		</div>'
						+ '	</div>'
						+ '	<div class="clear"></div>'
						+ '</div>';
				}
				$("#postAllList").html(html);
			}
		}
		eventBind();
		function eventBind() {

		}
	})(window)
})