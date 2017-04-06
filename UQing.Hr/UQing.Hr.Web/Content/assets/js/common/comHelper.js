(function (window) {
	var comHelper = {
		//分页对象
		pageInfo: {
			PageIndex: 1,
			PageSize: 10,
			PageCount: 0,
			TotalCount: 0
		},
		//搜索类型
		searchType: {
			work: 1, //工作
			company: 2, //企业（公司）
			all: 3 //所有
		}
	}
	window.comHelper = comHelper;
})(window)