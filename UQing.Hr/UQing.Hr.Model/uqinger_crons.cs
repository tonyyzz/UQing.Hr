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
    
    public partial class uqinger_crons
    {
        public int cronid { get; set; }
        public bool available { get; set; }
        public bool admin_set { get; set; }
        public string name { get; set; }
        public string filename { get; set; }
        public long lastrun { get; set; }
        public long nextrun { get; set; }
        public bool weekday { get; set; }
        public sbyte day { get; set; }
        public sbyte hour { get; set; }
        public string minute { get; set; }
    }
}