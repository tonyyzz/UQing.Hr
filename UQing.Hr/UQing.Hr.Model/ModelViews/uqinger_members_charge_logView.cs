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
    
    public partial class uqinger_members_charge_logView
    {
        public long log_id { get; set; }
        public int log_uid { get; set; }
        public string log_username { get; set; }
        public int log_addtime { get; set; }
        public string log_value { get; set; }
        public decimal log_amount { get; set; }
        public bool log_ismoney { get; set; }
        public bool log_type { get; set; }
        public bool log_mode { get; set; }
        public bool log_utype { get; set; }
    }
}