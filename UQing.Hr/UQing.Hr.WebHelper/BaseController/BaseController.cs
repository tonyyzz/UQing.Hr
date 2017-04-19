﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。          
// </auto-generated> 
//------------------------------------------------------------------------------

namespace UQing.Hr.WebHelper
{
    using System.Web.Mvc;
    using UQing.Hr.IServices;
    /// <summary>
    /// 控制器的父类，将来被此网站的所有控制器继承
    /// </summary>
    public partial class BaseController : Controller
    {
        //1.0 定义当前系统中所有的业务逻辑层的接口成员
        protected ICareerPlanningServices _CareerPlanningServices;
        protected IChannelCnvestmentServices _ChannelCnvestmentServices;
        protected IChatReacordServices _ChatReacordServices;
        protected ICooperativePartnerServices _CooperativePartnerServices;
        protected IFeedBackServices _FeedBackServices;
        protected IFriendApplyServices _FriendApplyServices;
        protected IFriendServices _FriendServices;
        protected IJobTrainingServices _JobTrainingServices;
        protected IOrderTimeServices _OrderTimeServices;
        protected IPersonServices _PersonServices;
        protected IPerson_BlackListServices _Person_BlackListServices;
        protected IPerson_CollectionServices _Person_CollectionServices;
        protected IPerson_EvaluateServices _Person_EvaluateServices;
        protected IPerson_ExpectWorkServices _Person_ExpectWorkServices;
        protected IPerson_FollowServices _Person_FollowServices;
        protected IPerson_MessageServices _Person_MessageServices;
        protected IPerson_PayCheckServices _Person_PayCheckServices;
        protected IPerson_ProjectServices _Person_ProjectServices;
        protected IPerson_ReportServices _Person_ReportServices;
        protected IPerson_RepresentationsServices _Person_RepresentationsServices;
        protected IPerson_RewardServices _Person_RewardServices;
        protected IPerson_Reward_MatchingServices _Person_Reward_MatchingServices;
        protected IPerson_SkillServices _Person_SkillServices;
        protected IPerson_TestServices _Person_TestServices;
        protected IPerson_WorkServices _Person_WorkServices;
        protected IPostInfoServices _PostInfoServices;
        protected IPostTypeServices _PostTypeServices;
        protected IPresentApplicationServices _PresentApplicationServices;
        protected IPresentApplication_BatchServices _PresentApplication_BatchServices;
        protected IReward_OrderServices _Reward_OrderServices;
        protected IRoleServices _RoleServices;
        protected IServerUserServices _ServerUserServices;
        protected IServerUser_BlackListServices _ServerUser_BlackListServices;
        protected IServerUser_CollectionServices _ServerUser_CollectionServices;
        protected IServerUser_EducationServices _ServerUser_EducationServices;
        protected IServerUser_EvaluateServices _ServerUser_EvaluateServices;
        protected IServerUser_FollowServices _ServerUser_FollowServices;
        protected IServerUser_MessageServices _ServerUser_MessageServices;
        protected IServerUser_PostServices _ServerUser_PostServices;
        protected IServerUser_ReportServices _ServerUser_ReportServices;
        protected IServerUser_TagServices _ServerUser_TagServices;
        protected IServerUser_WorkServices _ServerUser_WorkServices;
        protected ISys_LogsServices _Sys_LogsServices;
        protected ITransactionRecordServices _TransactionRecordServices;
        protected IUser_ManagersServices _User_ManagersServices;
        protected IWelfareTagServices _WelfareTagServices;
        protected IaTempServices _aTempServices;
        protected INewsServices _NewsServices;
        protected INewsTypeServices _NewsTypeServices;
        protected IPerson_EducationServices _Person_EducationServices;
        protected IPhoneCodeServices _PhoneCodeServices;
        protected Isys_CityServices _sys_CityServices;
        protected IView_NewsServices _View_NewsServices;
        protected IView_Person_EvaluateServices _View_Person_EvaluateServices;
        protected IView_Person_OrderServices _View_Person_OrderServices;
        protected IView_PersonFriendServices _View_PersonFriendServices;
        protected IView_PersonFriendApplyServices _View_PersonFriendApplyServices;
        protected IView_RepresentationsServices _View_RepresentationsServices;
        protected IView_Reward_OrderServices _View_Reward_OrderServices;
        protected IView_RewardListServices _View_RewardListServices;
        protected IView_SerUser_Reward_Mat_PostServices _View_SerUser_Reward_Mat_PostServices;
        protected IView_SerUserFriendServices _View_SerUserFriendServices;
        protected IView_SerUserOrderDetailServices _View_SerUserOrderDetailServices;
        protected IView_SerUserRewardDetailServices _View_SerUserRewardDetailServices;
        protected IView_SerUserRewordServices _View_SerUserRewordServices;
        protected IView_ServerUser_EvaluateServices _View_ServerUser_EvaluateServices;
        protected IView_ServerUser_PostServices _View_ServerUser_PostServices;
        protected IView_ServerUserFriendApplyServices _View_ServerUserFriendApplyServices;
        protected IView_SysLogServices _View_SysLogServices;
        protected IView_User_ManagersServices _View_User_ManagersServices;
        protected IWorkPostFilterInfoServices _WorkPostFilterInfoServices;
        protected IWorkPostFilterTypeServices _WorkPostFilterTypeServices;
        protected IView_WorkPostFilterInfoServices _View_WorkPostFilterInfoServices;
        protected IView_CompnayInfoServices _View_CompnayInfoServices;
        protected IView_PersonInfoServices _View_PersonInfoServices;
        protected IView_ServerUserInfoServices _View_ServerUserInfoServices;
        protected IView_NewsestRecruitServices _View_NewsestRecruitServices;
    }
}
