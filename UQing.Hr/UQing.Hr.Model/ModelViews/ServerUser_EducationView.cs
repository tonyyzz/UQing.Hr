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
    
    public partial class ServerUser_EducationView
    {
        public int SerUserEduID { get; set; }
        public Nullable<int> SerUserID { get; set; }
        public string SchoolName { get; set; }
        public string Major { get; set; }
        public string Education { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string EduDes { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}
