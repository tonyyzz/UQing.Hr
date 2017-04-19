$(function () {
	(function (window) {
		var searchPostType = comHelper.searchType.all; //默认职位搜索类型为搜索全部
		var conditionShowLen = 2; //要显示的筛选条件类型的个数，多于两个conditionShowLen数量会隐藏
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
			//筛选条件显示的div Obj
			getConditionShowObj: function () { return $("#conditionShow"); },
			//更多筛选
			getMoreConditionObj: function () { return $("#moreCondition"); },
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
			//获取职位筛选信息
			getCondition();
			var request = yHelper.request.getParams();
			var keywords = request["key"];
			if (!keywords) {
				keywords = '';
			}
			if (!!keywords) {
				$("#key").val(decodeURIComponent(keywords));
			}
			getJobsFromServer({
				key: decodeURIComponent(keywords),
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
		//获取职位筛选信息
		function getCondition() {
			$.ajax({
				url: "/jobs/filter",
				type: "post",
				datatype: "json",
				success: function (resp) {
					if (resp.result == 1) {
						setConditionHtml(resp.data.list);
					} else { //非法数据

					}
				},
				complete: function (XMLHttpRequest, status) {
					if (status == 'timeout') {	//超时

					} else if (XMLHttpRequest.status != "200") {	//返回失败状态

					}
				}
			})
		}
		//设置职位筛选信息html
		function setConditionHtml(list) {
			if (!list || list.length <= 0) {
				return;
			}
			var len = Math.min(conditionShowLen, list.length);
			var html = '';

			for (var i = 0; i < len; i++) {
				var item = null;
				html += ''
					+ '<div class="lefttit">' + list[i][0].TypeName + '</div>'
					+ '<div class="rs" data-typeid="' + list[i][0].TypeId + '">'
					+ '	<div class="li select">'
					+ '		<a data-id="" href="javascript:void(0)">不限</a>'
					+ '	</div>';
				for (var k in list[i]) {
					item = list[i][k];
					html += ''
						+ '<div class="li" data-id="' + item.Id + '">'
						+ '	<a href="javascript:void(0)">' + item.Name + '</a>'
						+ '</div>';
				}
				html += ''
					+ '	<div class="clear"></div>'
					+ '</div>'
					+ '<div class="clear"></div>';
			}
			//joblist.getConditionShowObj().html(html);
			//事件绑定
			conditionShowEvent();

			if (len >= list.length) {
				return;
			}
			//设置隐藏的筛选条件（更多筛选）
			var html2 = '';
			html2 += '<div class="lefttit moreCondition">更多筛选</div>';
			for (var m = len; m < list.length; m++) {
				var item2 = null;
				html2 += ''
					+ '<div class="bli J_dropdown">'
					+ '	<span class="txt">' + list[m][0].TypeName + '</span>';

				if (list[m].length <= 8) {
					html2 += '<div class="dropdowbox1 J_dropdown_menu">'
							+ '<div class="dropdow_inner1">';
				} else {
					html2 += '<div class="dropdowbox2 J_dropdown_menu">'
							+ '<div class="dropdow_inner2">';
				}
				html2 += '<ul class="nav_box"  data-typeid="' + list[m][0].TypeId + '">';
				for (var n = 0; n < list[m].length; n++) {
					item2 = list[m][n];
					html2 += ''
						+ '<li data-id="' + item2.Id + '">'
						+ '	<a class="" href="javascript:void(0)">' + list[m][n].Name + '</a>'
						+ '</li>';
				}
				html2 += ''
					+ '			</ul>'
					+ '			<div class="clear"></div>'
					+ '		</div>'
					+ '	</div>'
					+ '	<div class="clear"></div>'
					+ '</div>';
			}
			html2 += '<div class="clear"></div>';
			//joblist.getMoreConditionObj().html(html2);
			//事件绑定
			moreConditionEvent();
		}
		//显示的筛选条件事件绑定（item点击事件）
		function conditionShowEvent() {
			var conditionShowObj = joblist.getConditionShowObj();
			var itemObj = conditionShowObj.children(".rs").children(".li");
			itemObj.unbind("click");
			itemObj.bind("click", function () {
				var $that = $(this);
				$that.siblings().removeClass("select").end().addClass("select");
				getJobsFromServer({
					pageIndex: comHelper.pageInfo.PageIndex,
					searchType: searchPostType
				});
			});
		}
		//'更多筛选'事件绑定
		function moreConditionEvent() {
			$(document).unbind("click");
			$(document).bind("click", function () {
				joblist.getMoreConditionObj().children(".J_dropdown").removeClass("open").css('', '');
			});
			var moreConditionObj = joblist.getMoreConditionObj();
			var dropdownObj = moreConditionObj.children(".J_dropdown");
			//下拉
			dropdownObj.unbind("click");
			dropdownObj.bind("click", function (e) {
				yHelper.stopPropagation(e);
				var $that = $(this);
				$that.siblings().removeClass("open").css('', '');
				$that.toggleClass("open");
				if ($that.hasClass("open")) {
					$that.css({ position: 'relative' });
				} else {
					$that.css('', '');
				}
			});
			//下拉的每一项点击
			var dropdownUlObj = dropdownObj.find(".nav_box");
			var dropdownLiObj = dropdownUlObj.children("li");
			dropdownLiObj.unbind("click");
			dropdownLiObj.bind("click", function () {
				var $that = $(this);
				$that.siblings().children("a").removeClass("select");
				$that.children("a").addClass("select");
				getJobsFromServer({
					pageIndex: comHelper.pageInfo.PageIndex,
					searchType: searchPostType
				});
			});
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
				if (!!keywords) {
					yHelper.response.redirect(location.origin + location.pathname + "?key=" + encodeURIComponent(keywords));
				} else {
					yHelper.response.redirect(location.origin + location.pathname);
				}
				//getJobsFromServer({
				//	pageIndex: comHelper.pageInfo.PageIndex,
				//	searchType: searchPostType
				//});
			});
		}
		//从服务器获取数据
		function getJobsFromServer(obj) {
			if (!obj.pageIndex || obj.pageIndex <= 0) {
				obj.pageIndex = comHelper.pageInfo.PageIndex;
			}
			if (!obj.searchType) {
				obj.searchType = comHelper.searchType.all;
			}
			if (!obj.key) {
				obj.key = '';
			}
			//var keywordsObj = joblist.getKeywordsObj();
			//var keywords = keywordsObj.val();
			//if (!keywords) {
			//	keywords = '';
			//}
			//keywords = $.trim(keywords);
			//keywordsObj.val(keywords);

			//获取各种筛选条件
			//------------------------------------
			var conditions = getAllConditions();
			$.ajax({
				url: "/jobs/search",
				type: "post",
				datatype: "json",
				data: {
					pageIndex: obj.pageIndex,
					pageSize: comHelper.pageInfo.PageSize,
					searchType: obj.searchType,
					key: obj.key,
					conditions: JSON.stringify(conditions)
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
		//获取顶部所有筛选条件的json对象
		function getAllConditions() {
			var params = new Object();
			var typeid = '';
			var ids = [];
			var conditionShowObj = joblist.getConditionShowObj();
			var $rsDiv = conditionShowObj.children(".rs");
			for (var i = 0; i < $rsDiv.length; i++) {
				var $selectDiv = $($rsDiv[i]).children(".select");
				if ($selectDiv.length > 0) {
					typeid = $($rsDiv[i]).data("typeid");
					ids = [];
					var id = 0;
					for (var j = 0; j < $selectDiv.length; j++) {
						id = $($selectDiv[j]).data("id");
						if (!id) {
							id = 0;
						}
						ids.push(id);
					}
					if (!!typeid && !params[typeid]) {
						params[typeid] = ids;
					}
				}
			}
			var moreConditionObj = joblist.getMoreConditionObj();
			var $dropdownDiv = moreConditionObj.children(".J_dropdown");
			var $navBoxUl = $dropdownDiv.find(".nav_box");
			for (var m = 0; m < $navBoxUl.length; m++) {
				var $selectA = $($navBoxUl[m]).children("li").children(".select");
				if ($selectA.length > 0) {
					typeid = $($navBoxUl[m]).data("typeid");
					ids = [];
					var id = 0;
					for (var n = 0; n < $selectA.parent().length; n++) {
						id = $($selectA.parent()[n]).data("id");
						if (!id) {
							id = 0;
						}
						ids.push(id);
					}
					if (!!typeid && !params[typeid]) {
						params[typeid] = ids;
					}
				}
			}
			var jsonObjArr = [];
			if (!!params) {
				for (var key in params) {
					jsonObjArr.push({ typeid: key, ids: params[key].join(',') });
				}
			}
			if (jsonObjArr.length <= 0) {
				jsonObjArr.push({ typeid: 0, ids: 0 });
			}
			return jsonObjArr;
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
					+ '		<a target="_blank" class="line_substring" href="/jobs/show?id=' + jobItem.SerUserPostID + '">' + jobItem.PostName + '</a>'
					+ '	</div>'
					+ '	<div class="td3 link_gray6">'
					+ '		<a class="line_substring" href="/jobs/company?id=' + jobItem.SerUserID + '">' + jobItem.Company + '</a>'
					//+ '			<a class="line_substring" href="javascript:void(0)">' + jobItem.Company + '</a>'
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
					//+ '			<div class="favorites J_collectForJob">收藏</div>'
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