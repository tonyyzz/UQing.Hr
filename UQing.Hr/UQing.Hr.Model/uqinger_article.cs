//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UQing.Hr.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class uqinger_article
    {
        public long id { get; set; }
        public int type_id { get; set; }
        public int parentid { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string tit_color { get; set; }
        public bool tit_b { get; set; }
        public string Small_img { get; set; }
        public string author { get; set; }
        public string source { get; set; }
        public byte focos { get; set; }
        public byte is_display { get; set; }
        public string is_url { get; set; }
        public string seo_keywords { get; set; }
        public string seo_description { get; set; }
        public long click { get; set; }
        public long addtime { get; set; }
        public int article_order { get; set; }
        public byte robot { get; set; }
    }
}
