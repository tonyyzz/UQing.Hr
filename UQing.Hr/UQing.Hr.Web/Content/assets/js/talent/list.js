$(function () {
	(function (window) {
		var talentlist = {
			//人才搜索按钮
			getBtnTalentSearchObj: function () { return $("#btnTalentSearch"); },
			getTalentListObj: function () { return $("#talentList"); },
			getPaginationObj: function () { return $("#pagination"); },
			//简历操作
			getResumeOperateObj: function () { return $("#resumeOperate"); },
			getPageShowObj: function () { return $("#pageShow"); },
			getKeywordsObj: function () { return $("#key"); },
			//展开所有
			getSpreadAllObj: function () { return $("#spreadAll"); },
			//折叠所有
			getFoldAllObj: function () { return $("#foldAll"); },
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
			getTalentsFromServer({
				key: '',
				//searchType: searchPostType
			});
		}

		eventBind();
		function eventBind() {
			//searchTypeSwitch();
			goTalentSearchClick();
			setTimeout(function () {
				talentlist.getKeywordsObj().focus();
			}, 300);
		}

		//工作搜索按钮
		function goTalentSearchClick() {
			var talentSearchObj = talentlist.getBtnTalentSearchObj();
			talentSearchObj.unbind("click");
			talentSearchObj.bind("click", function () {
				var keywordsObj = talentlist.getKeywordsObj();
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
				getTalentsFromServer({
					key: keywords,
					pageIndex: comHelper.pageInfo.PageIndex,
					//searchType: searchPostType
				});
			});
		}

		//从服务器获取数据
		function getTalentsFromServer(obj) {
			if (!obj.key) {
				obj.key = '';
			}
			if (!obj.pageIndex || obj.pageIndex <= 0) {
				obj.pageIndex = comHelper.pageInfo.PageIndex;
			}
			//if (!obj.searchType) {
			//	obj.searchType = comHelper.searchType.all;
			//}
			$.ajax({
				url: "/talent/search",
				type: "post",
				datatype: "json",
				data: {
					pageIndex: obj.pageIndex,
					pageSize: comHelper.pageInfo.PageSize,
					//searchType: obj.searchType,
					key: obj.key,
				},
				timeout: 5000,
				success: function (resp) {
					if (resp.result == 1) {
						setTalentListhtml(resp.data.list, resp.data.pageInfo);
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

		function setTalentListhtml(list, pageInfo) {
			var html = '';
			if (!list || list.length <= 0) {
				html += ''
					+ '<div class="listb J_allListBox">'
					+ '	<div class="list_empty_group">'
					+ '		<div class="list_empty">'
					+ '			<div class="list_empty_left"></div>'
					+ '			<div class="list_empty_right">'
					+ '				<div class="sorry_box">对不起，没有找到符合您条件的简历！</div>'
					+ '				<div class="stips_box">放宽您的查找条件也许有更多合适您的简历哦~</div>'
					+ '			</div>'
					+ '		<div class="clear"></div>'
					+ '		</div>'
					+ '	</div>'
					+ '</div>';
				talentlist.getTalentListObj().html(html);
				talentlist.getPaginationObj().hide();
				talentlist.getResumeOperateObj().hide();
				return;
			}
			var talentItem = null;
			for (var i in list) {
				talentItem = list[i];
				html += ''
					+ '<div class="J_resumeList yli">'
					+ '	<div class="td1">'
					+ '		<div class="J_allList radiobox"></div>'
					+ '	</div>'
					+ '	<div class="td2 link_blue substring">'
					+ '		<a href="/talent/show?id=' + talentItem.PerID + '">' + talentItem.RealName + '</a>'
					+ '	</div>'
					+ '	<div class="td3 substring">'
					+ '		' + (!!talentItem.Sex ? "男" : "女")
					//+ '	<span>|</span>20岁'
					+ '		<span>|</span>' + talentItem.Education
					+ '		<span>|</span>' + talentItem.WorkLife
					+ '	</div>'
					+ '	<div class="td4 substring">' + talentItem.EngagePost + '</div>'
					+ '	<div class="td5 substring">' + (!!talentItem.City ? talentItem.City : "&nbsp;") + '</div>'
					+ '	<div class="td6">' + yHelper.date.getDayStr(talentItem.CreateTime, '-') + '</div>'
					+ '	<div class="td7">'
					+ '		<div class="J_resumeStatus hide"></div>'
					+ '	</div>'
					+ '	<div class="clear"></div>'
					//明细开始
					+ '	<div class="detail">'
					+ '		<div class="ltx">'
					+ '			<div class="photo">'
					+ '				<img src="' + comHelper.resourceHost + talentItem.Photo + '" onerror="this.src=\'/Content/assets/img/no_photo_male.png\'" border="0">'
					//+ '				<img src="/Content/assets/img/no_photo_male.png" onload="this.src=\'' + comHelper.resourceHost + talentItem.Photo + '\'" border="0">'
					+ '			</div>'
					+ '			<div class="tcent">'
					+ '				<div class="txt font_gray6">'
					//+ '兼职'
					+ '					' + talentItem.DemandPay
					//+ '				<span>|</span>'+'我目前在职，但考虑换个新环境'
					+ '				</div>'
					+ '				<div class="dlabs">'
					//+ '					该简历没有填写自我描述'
					+ '					<div class="clear"></div>'
					+ '				</div>'
					+ '			</div>'
					+ '			<div class="clear"></div>'
					+ '		</div>'
					+ '		<div class="rbtn">'
					+ '			<div class="deliver J_downResume">下载简历</div>'
					//+ '			<div class="favorites J_collectForResume">收藏</div>'
					+ '		</div>'
					+ '		<div class="clear"></div>'
					+ '	</div>'
					+ '</div>';
			}
			talentlist.getTalentListObj().html(html);
			talentlist.getPaginationObj().show();
			talentlist.getResumeOperateObj().show();
			setPagination(pageInfo);
		}
		//设置分页
		function setPagination(pageInfo) {
			pageOption.pageCount = pageInfo.PageCount;
			pageOption.pageIndex = pageInfo.PageIndex;
			yPagination.html(pageOption);

			var pageShowObj = talentlist.getPageShowObj();
			var html = '<span>' + pageInfo.PageIndex + '</span>/' + pageInfo.PageCount + '页<div class="clear"></div>';
			pageShowObj.html(html);
			pageAfterEventBind();
		}

		//分页页面设置完成后的处理事件
		function pageAfterEventBind() {
			var talentList = talentlist.getTalentListObj();
			var spreadAllObj = talentlist.getSpreadAllObj(); //展开所有
			var foldAllObj = talentlist.getFoldAllObj(); //折叠所有
			//显示隐藏
			var resumeStatusObj = talentList.find(".J_resumeStatus");
			//折叠单个
			resumeStatusObj.unbind("click");
			resumeStatusObj.bind("click", function () {
				var $that = $(this);
				$that.toggleClass("show").parent().nextAll(".detail").toggle();
			});
			//展开所有
			spreadAllObj.unbind("click");
			spreadAllObj.bind("click", function () {
				spreadAllObj.addClass("select");
				foldAllObj.removeClass("select");
				talentList.find(".J_resumeStatus").removeClass("show").parent().nextAll(".detail").show();
			});
			//折叠所有
			foldAllObj.unbind("click");
			foldAllObj.bind("click", function () {
				foldAllObj.addClass("select");
				spreadAllObj.removeClass("select");
				talentList.find(".J_resumeStatus").addClass("show").parent().nextAll(".detail").hide();
			});
		}

		//点击页码的跳转处理事件
		function goThisPage(pageIndex) {
			//do something...
			var keywordsObj = talentlist.getKeywordsObj();
			var keywords = keywordsObj.val();
			if (!keywords) {
				keywords = '';
			}
			keywords = $.trim(keywords);
			keywordsObj.val(keywords);
			getTalentsFromServer({
				key: keywords,
				pageIndex: pageIndex,
				//searchType: searchPostType
			});
		}

	})(window)
})