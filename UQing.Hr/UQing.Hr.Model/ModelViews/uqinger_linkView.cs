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
    
    public partial class uqinger_linkView
    {
        public long link_id { get; set; }
        public bool display { get; set; }
        public string alias { get; set; }
        public string link_name { get; set; }
        public string link_url { get; set; }
        public string link_logo { get; set; }
        public int show_order { get; set; }
        public string notes { get; set; }
    }
}