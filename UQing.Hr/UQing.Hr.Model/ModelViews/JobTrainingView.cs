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
    
    public partial class JobTrainingView
    {
        public int JobTraID { get; set; }
        public string JobTraType { get; set; }
        public string JobTraName { get; set; }
        public string Phone { get; set; }
        public string JobTraDes { get; set; }
        public Nullable<int> Sort { get; set; }
        public string Imgurl { get; set; }
        public string AbsDes { get; set; }
        public string JobTraTitle { get; set; }
    }
}
