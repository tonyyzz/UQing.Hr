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
    
    public partial class uqinger_jobs_contactView
    {
        public long id { get; set; }
        public long pid { get; set; }
        public string contact { get; set; }
        public string qq { get; set; }
        public string telephone { get; set; }
        public string landline_tel { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public byte notify { get; set; }
        public sbyte notify_mobile { get; set; }
        public bool contact_show { get; set; }
        public bool telephone_show { get; set; }
        public bool email_show { get; set; }
        public bool qq_show { get; set; }
        public bool landline_tel_show { get; set; }
    }
}