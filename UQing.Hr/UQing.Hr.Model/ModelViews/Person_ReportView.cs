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
    
    public partial class Person_ReportView
    {
        public int ID { get; set; }
        public Nullable<int> PerID { get; set; }
        public Nullable<int> SerUserID { get; set; }
        public string ReportReason { get; set; }
        public Nullable<System.DateTime> ReportTime { get; set; }
        public string ColValue { get; set; }
    }
}
