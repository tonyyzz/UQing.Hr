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
    
    public partial class uqinger_smsView
    {
        public int id { get; set; }
        public string name { get; set; }
        public string config { get; set; }
        public string alias { get; set; }
        public string replace { get; set; }
        public bool filing { get; set; }
        public string remark { get; set; }
        public int create_time { get; set; }
        public int update_time { get; set; }
        public int ordid { get; set; }
        public bool status { get; set; }
    }
}
