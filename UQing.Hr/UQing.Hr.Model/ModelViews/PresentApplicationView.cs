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
    
    public partial class PresentApplicationView
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string RealName { get; set; }
        public Nullable<decimal> Money { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> AdminID { get; set; }
        public string AdminName { get; set; }
        public Nullable<System.DateTime> PassTime { get; set; }
        public Nullable<int> UserType { get; set; }
        public Nullable<System.DateTime> PayTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> PayAdminID { get; set; }
        public string PayAdminName { get; set; }
        public string WxAccount { get; set; }
        public string AliAccount { get; set; }
        public Nullable<int> PreType { get; set; }
        public string WxAccountName { get; set; }
        public string AliAccounttName { get; set; }
        public string PreNum { get; set; }
        public Nullable<int> BatchID { get; set; }
    }
}
