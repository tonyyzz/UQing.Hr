$(function () {
	(function (window) {
		var searchPostType = comHelper.searchType.all; //默认职位搜索类型为搜索全部
		var joblist = {
			//工作搜索按钮
			getBtnJobSearchObj: function () { return $("#btnJobSearch"); },
			getKeywordsObj: function () { return $("#key"); },
			getJobListObj: function () { return $("#jobList"); },
			getSearchTypeObj: function () { return $("#searchType"); },
			getPaginationObj: function () { return $("#pagination"); },
			//展开所有
			getSpreadAllObj: function () { return $("#spreadAll"); },
			//折叠所有
			getFoldAllObj: function () { return $("#foldAll"); },
			getPageShowObj: function () { return $("#pageShow"); },
			//职位操作
			getPostOperateObj: function () { return $("#postOperate"); },
		}
		//分页属性
		var pageOption = {
			bindId: "pagination",	//必填（绑定的id）
			pageCount: 19,	//必填（总页数）
			pageIndex: 6,	//选填（当前页数）默认为1
			callback: function (pageIndex) { //选填（点击页码按钮后的回调函数，pageIndex为当前点击的页码）如果不填，默认不执行操作
				goThisPage(pageIndex);
			},

			homePageText: '首页',	//选填（显示的首页按钮文本）默认为'[首页]'
			homePageShow: false,	//选填（首页按钮是否显示）默认为true
			endPageText: '末页',		//选填（显示的末页按钮文本）默认为'[末页]'
			endPageShow: false,		//选填（末页按钮是否显示）默认为true
			prevText: '上一页',		//选填（上一页按钮显示文本）默认为'[上一页]'
			nextText: '下一页',		//选填（下一页按钮显示文本）默认为'[下一页]'
			prevShow: true,			//选填（上一页按钮是否显示）默认为true
			nextShow: true,			//选填（下一页按钮是否显示）默认为true
			ellipseText: '...',		//选填（省略的页数用什么文字表示）默认为'...'
			jumpShow: false,		//选填（跳转按钮选项是否显示）默认为true
			maxNumDisplay: 5		//选填（连续分页主体部分显示的最大分页条目数）默认为10
		};


		init();
		//初始化方法
		function init() {
			getJobsFromServer({
				key: '',
				searchType: searchPostType
			});
		}

		eventBind();
		function eventBind() {
			searchTypeSwitch();
			goJobSearchClick();
			setTimeout(function () {
				joblist.getKeywordsObj().focus();
			}, 300);
		}
		//搜索类型切换
		function searchTypeSwitch() {
			var searchTypeObj = joblist.getSearchTypeObj();
			var selectLi = searchTypeObj.children(".J_sli");
			selectLi.unbind("click");
			selectLi.bind("click", function () {
				var $that = $(this);
				selectLi.removeClass("select");
				$that.addClass("select");
				var type = $that.data("type");
				switch (type) {
					case 1: searchPostType = comHelper.searchType.work; break;
					case 2: searchPostType = comHelper.searchType.company; break;
					case 3: searchPostType = comHelper.searchType.all; break;
					default: searchPostType = comHelper.searchType.all; break;
				}
			});
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
				keywordsObj.val(keywords);
				if (!keywords) {
					keywordsObj.val("").focus();
					//return;
				}
				getJobsFromServer({
					key: keywords,
					pageIndex: comHelper.pageInfo.PageIndex,
					searchType: searchPostType
				});
			});
		}
		//从服务器获取数据
		function getJobsFromServer(obj) {
			if (!obj.key) {
				obj.key = '';
			}
			if (!obj.pageIndex || obj.pageIndex <= 0) {
				obj.pageIndex = comHelper.pageInfo.PageIndex;
			}
			if (!obj.searchType) {
				obj.searchType = comHelper.searchType.all;
			}
			$.ajax({
				url: "/jobs/search",
				type: "post",
				datatype: "json",
				data: {
					pageIndex: obj.pageIndex,
					pageSize: comHelper.pageInfo.PageSize,
					searchType: obj.searchType,
					key: obj.key,
				},
				timeout: 5000,
				success: function (resp) {
					if (resp.result == 1) {
						setJobsListhtml(resp.data.list, resp.data.pageInfo);
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
		function setJobsListhtml(list, pageInfo) {
			var html = '';
			if (!list || list.length <= 0) {
				html += ''
					+ '<div class="list_empty_group">'
					+ '	<div class="list_empty">'
					+ '		<div class="list_empty_left"></div>'
					+ '		<div class="list_empty_right">'
					+ '			<div class="sorry_box">对不起，没有找到符合您条件的职位！</div>'
					+ '			<div class="stips_box">放宽您的查找条件也许有更多合适您的职位哦~</div>'
					+ '		</div>'
					+ '		<div class="clear"></div>'
					+ '	</div>'
					+ '</div>';
				joblist.getJobListObj().html(html);
				joblist.getPaginationObj().hide();
				joblist.getPostOperateObj().hide();
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
					+ '				' + (!!jobItem.Trade ? jobItem.Trade : "")
					+ "&nbsp;&nbsp;"
					+ '地点：' + jobItem.WorkAdress
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
			joblist.getJobListObj().html(html);
			joblist.getPaginationObj().show();
			joblist.getPostOperateObj().show();
			setPagination(pageInfo);
		}
		//设置分页
		function setPagination(pageInfo) {
			pageOption.pageCount = pageInfo.PageCount;
			pageOption.pageIndex = pageInfo.PageIndex;
			yPagination.html(pageOption);

			var pageShowObj = joblist.getPageShowObj();
			var html = '<span>' + pageInfo.PageIndex + '</span>/' + pageInfo.PageCount + '页<div class="clear"></div>';
			pageShowObj.html(html);
			pageAfterEventBind();
		}
		//分页页面设置完成后的处理事件
		function pageAfterEventBind() {
			var joblistObj = joblist.getJobListObj();
			var spreadAllObj = joblist.getSpreadAllObj(); //展开所有
			var foldAllObj = joblist.getFoldAllObj(); //折叠所有
			//显示隐藏
			var jobsStatusObj = joblistObj.find(".J_jobsStatus");
			//折叠单个
			jobsStatusObj.unbind("click");
			jobsStatusObj.bind("click", function () {
				var $that = $(this);
				$that.toggleClass("show").parent().nextAll(".detail").toggle();
			});
			//展开所有
			spreadAllObj.unbind("click");
			spreadAllObj.bind("click", function () {
				spreadAllObj.addClass("select");
				foldAllObj.removeClass("select");
				joblistObj.find(".J_jobsStatus").removeClass("show").parent().nextAll(".detail").show();
			});
			//折叠所有
			foldAllObj.unbind("click");
			foldAllObj.bind("click", function () {
				foldAllObj.addClass("select");
				spreadAllObj.removeClass("select");
				joblistObj.find(".J_jobsStatus").addClass("show").parent().nextAll(".detail").hide();
			});
		}
		//点击页码的跳转处理事件
		function goThisPage(pageIndex) {
			//do something...
			var keywordsObj = joblist.getKeywordsObj();
			var keywords = keywordsObj.val();
			if (!keywords) {
				keywords = '';
			}
			keywords = $.trim(keywords);
			keywordsObj.val(keywords);
			getJobsFromServer({
				key: keywords,
				pageIndex: pageIndex,
				searchType: searchPostType
			});
		}
	})(window)
})