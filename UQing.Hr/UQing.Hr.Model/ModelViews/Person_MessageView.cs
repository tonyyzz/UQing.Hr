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
    
    public partial class Person_MessageView
    {
        public int PerMesID { get; set; }
        public Nullable<int> PerID { get; set; }
        public Nullable<int> MesType { get; set; }
        public Nullable<int> DataID { get; set; }
        public string MesCon { get; set; }
        public Nullable<System.DateTime> SendTime { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<System.DateTime> ReadTime { get; set; }
        public string Colvalue { get; set; }
        public Nullable<int> TargetID { get; set; }
    }
}
