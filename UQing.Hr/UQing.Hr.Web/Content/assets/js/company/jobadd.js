$(function () {
	(function (window) {
		init();
		function init() {

		}

		eventBind();
		function eventBind() {
			addjobOption();
		}
		function addjobOption() {

			$("#btnAddjob").unbind("click");
			$("#btnAddjob").bind("click", function () {

				//js验证
				var jobName = $("#jobName").val();
				if (!jobName) {
					comTipsHelper.disappearTips('remind', '请填写职位名称');
					return;
				}
				var jobclassify = $("#jobclassify").val();
				if (!jobclassify) {
					comTipsHelper.disappearTips('remind', '请填写职位类别');
					return;
				}
				var workAddress = $("#workAddress").val();
				if (!workAddress) {
					comTipsHelper.disappearTips('remind', '请填写工作地点');
					return;
				}
				var salary = $("#salary").val();
				if (!salary) {
					salary = '';
				}
				var salaryArr = ['3k以下', '3k-5k', '5k-10k', '10k以上'];
				if (salaryArr.indexOf(salary) < 0) {
					salary = salaryArr[0];
				}

				var sellingPoints = $("#sellingPoints").val();
				if (!sellingPoints) {
					sellingPoints = '';
				}

				var postDuty = $("#postDuty").val();
				if (!postDuty) {
					comTipsHelper.disappearTips('remind', '请填写职位描述');
					return;
				}

				$.ajax({
					url: "/company/addjobopt",
					type: "post",
					datatype: "json",
					timeout: 5000,
					data: {
						jobName: jobName,
						jobclassify: jobclassify,
						workAddress: workAddress,
						salary: salary,
						sellingPoints: sellingPoints,
						postDuty: postDuty
					},
					success: function (resp) {
						if (resp.result == 1) {
							//发布成功
							comTipsHelper.disappearTips('success', '职位发布成功');
						} else if (resp.result == 0) { //后台字段验证失败

						} else if (resp.result == -1) { //身份错误 或 未登录

						} else if (resp.result == 3) { //保存失败

						} else {

						}
					},
					complete: function (XMLHttpRequest, status) {
						if (status == 'timeout') {	//超时

						} else if (XMLHttpRequest.status != "200") {	//返回失败状态

						}
					}
				})
			});
		}
	})(window)
})