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
    
    public partial class uqinger_pageView
    {
        public long id { get; set; }
        public byte systemclass { get; set; }
        public byte pagetpye { get; set; }
        public string alias { get; set; }
        public string pname { get; set; }
        public string module { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string rewrite { get; set; }
        public byte url { get; set; }
        public long caching { get; set; }
        public string tag { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string keywords { get; set; }
        public string variate { get; set; }
    }
}
