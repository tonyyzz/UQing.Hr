$(function () {
	(function (window) {
		var comHelper = {
			//分页对象
			pageInfo: {
				//comHelper.pageInfo.PageIndex
				PageIndex: 1,
				//comHelper.pageInfo.PageSize
				PageSize: 10,
				PageCount: 0,
				TotalCount: 0
			},
			//搜索类型
			searchType: {
				work: 1, //工作
				company: 2, //企业（公司）
				all: 3 //所有
			},
			//资源域名
			//comHelper.resourceHost
			resourceHost: 'http://120.76.240.175',

			//根据数据库得来的字段，计算有多少岁（做兼容）
			/*
			 传入格式如：1993年10月、1994.11
			*/
			//comHelper.getYearAge()
			getYearAge: function (str) {
				var age = 20;
				if (!!str) {
					str = str.replace(/\s+/g, '');
					var yearArr = yHelper.regex.getRegexArr(str, /(\d+)[年|\.]\d+([月|\.])?/g);
					if (yearArr.length > 0) {
						var nowYear = yHelper.date.getDateTimeArr(new Date())[0];
						age = nowYear - yearArr[0];
					}
				}
				return age;
			}
		}
		window.comHelper = comHelper;
	})(window)
})