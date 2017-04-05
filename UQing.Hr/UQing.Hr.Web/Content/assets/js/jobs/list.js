$(function () {
	(function (window) {
		var joblist = {
			//工作搜索按钮
			getBtnJobSearchObj: function () {
				return $("#btnJobSearch");
			},
			getKeywordsObj: function () {
				return $("#key");
			},
			getJobListObj: function () {
				return $("#jobList");
			},
		}
		eventBind();
		function eventBind() {
			goJobSearchClick();

			setTimeout(function () {
				joblist.getKeywordsObj().focus();
			}, 300);
		}
		//工作搜索按钮
		function goJobSearchClick() {
			var jobSearchObj = joblist.getBtnJobSearchObj();
			jobSearchObj.unbind("click");
			jobSearchObj.bind("click", function () {
				var keywordsObj = joblist.getKeywordsObj();
				var keywords = keywordsObj.val();
				if (!keywords) {
					keywords = '';
				}
				keywords = $.trim(keywords);
				if (!keywords) {
					keywordsObj.val("").focus();
					return;
				}
				getJobsFromServer({ key: keywords, searchType: comHelper.searchType.all });
			});
		}
		//从服务器获取数据
		function getJobsFromServer(obj) {
			if (!obj.key) {
				obj.key = '';
			}
			if (!obj.searchType) {
				obj.searchType = comHelper.searchType.all;
			}
			$.ajax({
				url: "/jobs/search",
				type: "post",
				datatype: "json",
				data: {
					pageIndex: comHelper.pageInfo.PageIndex,
					pageSize: comHelper.pageInfo.PageSize,
					searchType: obj.searchType,
					key: obj.key,
				},
				timeout: 5000,
				success: function (resp) {
					if (resp.result == 1) {
						setJobsListhtml(resp.data.list);
					} else { //非法操作

					}

				},
				complete: function (XMLHttpRequest, status) {
					if (status == 'timeout') {	//超时

					} else if (XMLHttpRequest.status != "200") {	//返回失败状态

					}
				}
			})
		}
		function setJobsListhtml(list) {
			var html = '';
			if (!list || list.length <= 0) {
				return;
			}
			var jobItem = null;
			for (var i in list) {
				jobItem = list[i];
				html += ''
					+ '<div class="J_jobsList yli">'
					+ '	<div class="td1">'
					+ '		<div class="J_allList radiobox"></div>'
					+ '	</div>'
					+ '	<div class="td2 link_blue">'
					+ '		<a class="line_substring" href="javascript:void(0)" target="_blank">' + jobItem.PostName + '</a>'
					+ '	</div>'
					+ '	<div class="td3 link_gray6">'
					+ '		<a class="line_substring" href="javascript:void(0)" target="_blank">' + jobItem.Company + '</a>'
					+ '		<div class="clear"></div>'
					+ '	</div>'
					+ '	<div class="td4">' + jobItem.Salary + '</div>'
					+ '	<div class="td5">' + yHelper.date.getDayStr(jobItem.CreateTime, '-') + '</div>'
					+ '	<div class="td6">'
					+ '		<div class="J_jobsStatus hide"></div>'
					+ '	</div>'
					+ '	<div class="clear"></div>'
					+ '	<div class="detail">'
					+ '		<div class="ltx">'
					+ '			<div class="txt font_gray6">'
					+ '地点：' + jobItem.WorkAdress + "&nbsp;&nbsp;"
					+ '				' + (!!jobItem.Trade ? jobItem.Trade : "")
					+ '			</div>'
					+ '			<div class="dlabs">'
					+ ''
					+ '				<div class="clear"></div>'
					+ '			</div>'
					+ '		</div>'
					+ '		<div class="rbtn">'
					+ '			<div class="deliver J_applyForJob">投递简历</div>'
					+ '			<div class="favorites J_collectForJob">收藏</div>'
					+ '		</div>'
					+ '		<div class="clear"></div>'
					+ '	</div>'
					+ '</div>';
			}
			joblist.getJobListObj().empty().html(html);
		}
	})(window)
})