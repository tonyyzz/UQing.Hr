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
    
    public partial class View_SerUserRewordView
    {
        public int PerRewMatID { get; set; }
        public Nullable<int> SerUserID { get; set; }
        public Nullable<int> SerPostID { get; set; }
        public Nullable<System.DateTime> MatchingTime { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<System.DateTime> RefTime { get; set; }
        public int PerRewardID { get; set; }
        public string EngagePost { get; set; }
        public string DemandPay { get; set; }
        public string JobCity { get; set; }
        public Nullable<decimal> RewardMoney { get; set; }
        public Nullable<int> RewardState { get; set; }
        public string Education { get; set; }
        public string WorkLife { get; set; }
    }
}
