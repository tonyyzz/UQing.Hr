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
    
    public partial class uqinger_audit_reasonView
    {
        public long id { get; set; }
        public long jobs_id { get; set; }
        public long company_id { get; set; }
        public long resume_id { get; set; }
        public string status { get; set; }
        public string reason { get; set; }
        public string audit_man { get; set; }
        public bool famous { get; set; }
        public int addtime { get; set; }
    }
}