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
    
    public partial class uqinger_resume_imgView
    {
        public long id { get; set; }
        public long uid { get; set; }
        public long resume_id { get; set; }
        public string img { get; set; }
        public string title { get; set; }
        public long addtime { get; set; }
        public bool audit { get; set; }
    }
}