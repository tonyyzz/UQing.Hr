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
    
    public partial class uqinger_adView
    {
        public long id { get; set; }
        public string theme { get; set; }
        public string alias { get; set; }
        public bool is_display { get; set; }
        public short category_id { get; set; }
        public short type_id { get; set; }
        public string title { get; set; }
        public string note { get; set; }
        public long show_order { get; set; }
        public long addtime { get; set; }
        public long starttime { get; set; }
        public int deadline { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string text_color { get; set; }
        public string explain { get; set; }
        public int uid { get; set; }
    }
}
