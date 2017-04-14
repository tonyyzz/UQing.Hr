
$(function () {
	(function (window) {
		init();
		function init() {
			getPerInfo();

		}

		function getPerInfo() {
			$.ajax({
				url: "/m/getperinfo",
				type: "post",
				datatype: "json",
				timeout: 5000,
				success: function (resp) {
					if (resp.result == 1) {
						if (resp.data.idt == 1) {
							setPerInfoHtml(resp.data.person);
						} else {

						}
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

		function setPerInfoHtml(person) {
			$("#perHeadImg").attr("src", ((!!person.Photo) ? comHelper.resourceHost + person.Photo : "/Content/assets/img/m/no_photo_male.png"));
			$("#realname").val((person.RealName ? person.RealName : ""));
			showSexHtml(person);
			$("#residence").val((!!person.City) ? person.City : "");
			$("#education").html((!!person.Education) ? person.Education : "本科");
			$("#workLife").html((!!person.WorkLife) ? person.WorkLife : "应届毕业生");
			$("#phone").val((!!person.Phne) ? person.Phne : "");
			$("#email").val((!!person.Email) ? person.Email : "");
		}
		function showSexHtml(person) {
			var isWoman = (person.Sex === false);
			var $gender = $("#gender");
			$gender.children(".J_radioitme").removeClass("checked");
			if (!isWoman) {
				$gender.children(".J_radioitme").eq(0).addClass("checked");
			} else {
				$gender.children(".J_radioitme").eq(1).addClass("checked");
			}
		}

		eventBind();
		function eventBind() {
			genderSwitchEvent();
			saveEvent();
		}
		function genderSwitchEvent() {
			var $gender = $("#gender");
			$gender.children(".J_radioitme").unbind("click");
			$gender.children(".J_radioitme").bind("click", function () {
				var $that = $(this);
				$gender.children(".J_radioitme").removeClass("checked");
				$that.addClass("checked");
			});
		}
		function saveEvent() {
			$("#btnSavePerInfo").unbind("click");
			$("#btnSavePerInfo").bind("click", function () {
				var realname = $("#realname").val();
				if (!realname || !$.trim(realname)) {
					comTipsHelper.disappearTips('remind', '请输入姓名');
					return;
				}
				var $gender = $("#gender");
				var $genderChecked = $gender.children(".checked");
				var gender = 1;
				if (!$genderChecked || $genderChecked.length <= 0) {
					comTipsHelper.disappearTips('remind', '请选择性别');
					return;
				}
				gender = $genderChecked.data("gender");
				if (!gender || (gender != 1 && gender != 2)) {
					gender = 1; //1：男(默认)（2：女）
				}
				var residence = $("#residence").val();
				if (!residence || !$.trim(residence)) {
					residence = '';
				}
				var education = $("#education").text();
				if (!education || !$.trim(education)) {
					education = '本科';
				}
				var workLife = $("#workLife").text();
				if (!workLife || !$.trim(workLife)) {
					workLife = '应届毕业生';
				}
				var phone = $("#phone").val();
				if (!phone || !$.trim(phone)) {
					comTipsHelper.disappearTips('remind', '请填写手机号');
					return;
				}
				if (!memberHelper.isMatchPhone(phone)) {
					comTipsHelper.disappearTips('remind', '请填写正确的手机号');
					return;
				}
				var email = $("#email").val();
				if (!email || !$.trim(email)) {
					comTipsHelper.disappearTips('remind', '请填写邮箱');
					return;
				}
				if (!memberHelper.isMatchEmail(email)) {
					comTipsHelper.disappearTips('remind', '请填写正确的邮箱');
					return;
				}
				$.ajax({
					url: "/m/saveperinfo",
					type: "post",
					datatype: "json",
					data: {
						realname: realname,
						gender: gender,
						residence: residence,
						education: education,
						workLife: workLife,
						phone: phone,
						email: email
					},
					timeout: 5000,
					success: function (resp) {
						if (resp.result == 1) {
							//保存成功
							comTipsHelper.disappearTips('success', '保存成功', function () {
								yHelper.response.redirect("/m");
							});
						} else if (resp.result == 2) {
							if (resp.result.flag == 1) { //求职者不存在

							} else if (resp.result.flag == 2) { //身份错误

							} else { //非法操作

							}
						} else if (resp.result == 0) {
							if (resp.result.flag == 1) { //姓名不能为空
								comTipsHelper.disappearTips('remind', '请输入姓名');
							} else if (resp.result.flag == 2) { //学历不能为空
								comTipsHelper.disappearTips('remind', '请输入学历');
							} else if (resp.result.flag == 3) { //工作经验不能为空
								comTipsHelper.disappearTips('remind', '请输入工作经验');
							} else if (resp.result.flag == 4) { //手机号码非法
								comTipsHelper.disappearTips('remind', '请填写正确的手机号');
							} else if (resp.result.flag == 5) { //邮箱非法
								comTipsHelper.disappearTips('remind', '请填写正确的邮箱');
							} else { //非法操作

							}
						} else { //非法操作

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
