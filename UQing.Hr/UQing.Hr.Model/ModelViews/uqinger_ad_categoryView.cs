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
    
    public partial class uqinger_ad_categoryView
    {
        public int id { get; set; }
        public string theme { get; set; }
        public string org { get; set; }
        public string alias { get; set; }
        public long type_id { get; set; }
        public string categoryname { get; set; }
        public short width { get; set; }
        public short height { get; set; }
        public bool @float { get; set; }
        public short floating_left { get; set; }
        public short floating_right { get; set; }
        public short floating_top { get; set; }
        public byte admin_set { get; set; }
    }
}
