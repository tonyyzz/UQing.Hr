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
    
    public partial class PresentApplication_BatchView
    {
        public int BatchID { get; set; }
        public string Batch_No { get; set; }
        public Nullable<System.DateTime> Pay_Date { get; set; }
        public Nullable<int> Batch_Num { get; set; }
        public string Batch_Fee { get; set; }
        public Nullable<int> BatchState { get; set; }
        public Nullable<int> PayAdminID { get; set; }
        public string PayAdminName { get; set; }
        public Nullable<System.DateTime> CrateTime { get; set; }
    }
}
