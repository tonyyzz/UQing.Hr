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
    
    public partial class PostTypeView
    {
        public int ID { get; set; }
        public string PostTypeName { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> Sort { get; set; }
        public string ColValue { get; set; }
    }
}