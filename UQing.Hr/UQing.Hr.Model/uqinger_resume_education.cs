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
    
    public partial class uqinger_resume_education
    {
        public long id { get; set; }
        public long pid { get; set; }
        public long uid { get; set; }
        public int startyear { get; set; }
        public int startmonth { get; set; }
        public int endyear { get; set; }
        public int endmonth { get; set; }
        public string school { get; set; }
        public string speciality { get; set; }
        public int education { get; set; }
        public string education_cn { get; set; }
        public long todate { get; set; }
        public long campus_id { get; set; }
    }
}
