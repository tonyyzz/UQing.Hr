//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UQing.Hr.Model.ModelViews
{
    using System;
    using System.Collections.Generic;
     
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Sys_LogsView
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
        public string IpAddress { get; set; }
    }
}
