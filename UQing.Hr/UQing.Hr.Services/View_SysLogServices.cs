//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UQing.Hr.Services
{
    using System;
    using System.Collections.Generic;
       
    using UQing.Hr.Model;
    using UQing.Hr.IServices;
    using UQing.Hr.IRepository;
    
    /// <summary>
    /// 负责每个数据表的业务逻辑操作
    /// </summary>
    public partial class View_SysLogServices : BaseServices<View_SysLog>, IView_SysLogServices
    {
        private IView_SysLogRepository _dal;
    
        #region 定义构造函数接收AutoFac将数据仓储层的具体实现类的对象注入到此类中
        public View_SysLogServices(IView_SysLogRepository dal)
        {
            base._baseDal = dal;
            this._dal = dal;
        }
        #endregion
    
        #region 针对此表的特殊操作写在此处
        
        #endregion
    }
}
