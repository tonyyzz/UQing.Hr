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
    
    public partial class PhoneCodeView
    {
        public string Phone { get; set; }
        public string VerCode { get; set; }
        public Nullable<System.DateTime> SendTime { get; set; }
        public Nullable<int> SendType { get; set; }
    }
}
