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
    
    public partial class uqinger_report_resumeView
    {
        public long id { get; set; }
        public long uid { get; set; }
        public string username { get; set; }
        public long resume_id { get; set; }
        public string resume_realname { get; set; }
        public long resume_addtime { get; set; }
        public int report_type { get; set; }
        public int audit { get; set; }
        public string content { get; set; }
        public long addtime { get; set; }
    }
}
