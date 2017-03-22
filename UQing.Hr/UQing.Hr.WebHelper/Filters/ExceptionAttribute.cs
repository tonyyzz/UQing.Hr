using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UQing.Hr.WebHelper
{
    using System.Web.Mvc;
	using UQing.Hr.Common;

    /// <summary>
    /// 自定义异常捕获过滤器
    /// </summary>
    public class ExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception exp = filterContext.Exception;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var jsonResult = new JsonResult
                {
                    Data = new { status = (int)Enums.EAjaxState.Error, msg = exp.Message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                LogHelper.WriteErrorLog(exp);
                filterContext.Result = jsonResult;
            }
            else
            {
                var viewResult = new ViewResult
                {
                    ViewName = "/Views/Shared/Error.cshtml",
                };
                LogHelper.WriteErrorLog(exp);
                viewResult.ViewData["exp"] = exp;
                filterContext.Result = viewResult;
            }

            //告诉MVC框架异常已经被处理
            filterContext.ExceptionHandled = true;

            base.OnException(filterContext);
        }
    }
}
