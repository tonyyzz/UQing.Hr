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
    
    public partial class uqinger_categoryView
    {
        public long c_id { get; set; }
        public long c_parentid { get; set; }
        public string c_alias { get; set; }
        public string c_name { get; set; }
        public int c_order { get; set; }
        public string c_index { get; set; }
        public string c_note { get; set; }
        public string stat_jobs { get; set; }
        public string stat_resume { get; set; }
    }
}
