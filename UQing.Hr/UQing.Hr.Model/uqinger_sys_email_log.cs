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
    
    public partial class uqinger_sys_email_log
    {
        public long id { get; set; }
        public string send_from { get; set; }
        public string send_to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public short state { get; set; }
        public int sendtime { get; set; }
    }
}