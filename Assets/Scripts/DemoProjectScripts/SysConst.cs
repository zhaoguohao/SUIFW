using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DemoProject
{
    public class SysConst
    {
        /* UI预设名称（UI窗体ID）(特别注意： 这里常量字符串与配置文件定义的必须相同)
         */
        public const string LogonForms = "LogonForms";
        public const string SelectHeroForms = "SelectHeroForms";
        public const string HeroInfo = "HeroInfo";
        public const string MainForms = "MainForms";
        public const string MarketForms = "MarketForms";
        public const string GoodsInfoForms = "GoodsInfoForms";
        public const string ConfirmForms = "ConfirmForms";

        //MsgType 消息(监听)的类型
        public const string MarketInfo = "MarketInfo";             //商城信息
        //商城信息--道具详细信息
        public const string MarketInfo_PropDetailInfo = "MarketInfo_PropDetailInfo";            

        public const string PackageInfo= "PackageInfo";            //背包信息
        public const string PlayerKernalInfo= "PlayerKernalInfo";  //玩家核心信息
        public const string PlayerExternInfo= "PlayerExternInfo";  //玩家扩展信息
    }
}


