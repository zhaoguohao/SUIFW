// 主题： 框架本身系统核心参数定义集  
// 功能： 提供本框架范围内的如下系统定义：
// 1：系统常量。
// 2：全局性变量
// 3：系统枚举
// 4：委托定义
// 5：框架接口              

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


namespace SUIFW
{
    #region 系统枚举
    /// <summary>
    /// UI窗体类型
    /// </summary>
    public enum UIFormsType
    {
        Normal,             // 普通全屏界面(例如主城UI界面)
        Fixed,              // 固定界面(例如“英雄信息条” [HeroTopBar])
        PopUp,              // 弹出模式(小窗口)窗口 (例如：商场、背包、确认窗口等)
    }

    /// <summary>
    /// UI窗体显示类型
    /// </summary>
    public enum UIFormsShowMode
    {
        Normal,             //普通显示
        ReverseChange,      //反向切换      
        HideOther,          //隐藏其他界面 
    }

    /// <summary>
    /// UI窗体透明度类型
    /// </summary>
    public enum UIFormsLucencyType
    {
        Lucency,            //完全透明,但不能穿透。
        Translucence,       //半透明度,不能穿透。
        Impenetrable,       //低透明度,不能穿透,
        Penetrate,          //可以穿透
    }
    #endregion

    /// <summary>
    /// 系统定义静态类
    /// </summary>
    internal static class SysDefine
    {
        #region 系统常量
        /* 路径常量 */
        public const string SYS_PATH_CANVAS = "Canvas";
        public const string SYS_PATH_CNLauguageJson = "LauguageJSONConfig";
        public const string SYS_PATH_SysConfigJson = "SysConfigInfo";
        public const string SYS_PATH_UIFormConfigJson = "UIFormsConfigInfo";

        /* 标签常量 */
        public const string SYS_TAG_CANVAS = "_TagCanvas";
        public const string SYS_TAG_UICAMERA = "_TagUICamera";
        /* Canvas节点名称 */
        public const string SYS_CANVAS_NORMAL_NODE_NAME = "Normal";
        public const string SYS_CANVAS_FIXED_NODE_NAME = "Fixed";
        public const string SYS_CANVAS_POPUP_NODE_NAME = "PopUp";
        public const string SYS_CANVAS_UISCRIPTS_NODE_NAME = "_UIScripts";
        public const string SYS_CANVAS_UIMASKPANELS_NODE_NAME = "UIMaskPanels";
        /* 遮罩管理器常量 */
        //完全透明度
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB = 255F / 255F;
        public const float SYS_UIMASK_LUCENCY_COLOR_A = 0F / 255F;
        //半透明度
        public const float SYS_UIMASK_TRANSLUCENCY_COLOR_RGB = 220F / 255F;
        public const float SYS_UIMASK_TRANSLUCENCY_COLOR_A = 50F / 255F;
        //低透明度
        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB = 50F / 255F;
        public const float SYS_UIMASK_IMPENETRABLE_COLOR_A = 200F / 255F;
        /// <summary>
        /// UI摄像机，层深增加量
        /// </summary>
        public const int SYS_UICAMERA_DEPTH_INCREMENT = 100;
        #endregion

        #region 全局性变量（方法）
        //得到日志配置文件(XML)路径
        public static string GetLogPath()
        {
            string logPath = null;

            //Android 或者Iphone 环境
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                logPath = Application.streamingAssetsPath + "/LogConfigInfo.xml";
            }
            //Win环境
            else
            {
                logPath = "file://" + Application.streamingAssetsPath + "/LogConfigInfo.xml";
            }

            return logPath;
        }
        //得到日志配置文件(XML)根节点名称
        public static string GetLogRootNodeName()
        {
            string strReturnXMLRootNodeName = null;

            strReturnXMLRootNodeName = "SystemConfigInfo";
            return strReturnXMLRootNodeName;
        }

        //得到"UI窗体预设"配置文件(XML)路径
        public static string GetUIFormsConfigFilePath()
        {
            string logPath = null;

            //Android 或者Iphone 环境
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                logPath = Application.streamingAssetsPath + "/UIFormsConfigInfo.xml";
            }
            //Win环境
            else
            {
                logPath = "file://" + Application.streamingAssetsPath + "/UIFormsConfigInfo.xml";
            }

            return logPath;
        }
        //得到"UI窗体预设"配置文件(XML)的根节点名称
        public static string GetUIFormsConfigFileRootNodeName()
        {
            string strReturnXMLRootNodeName = null;

            strReturnXMLRootNodeName = "UIFormsConfigInfo";
            return strReturnXMLRootNodeName;
        }

        /* 由于使用Json 技术解析大量中文信息，所以不用本xml 路径了 */
        //得到"UI窗体预设"配置文件(XML)路径
        //public static string GetLauguageConfigFilePath()
        //{
        //    string logPath = null;

        //    //Android 或者Iphone 环境
        //    if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //    {
        //        logPath = Application.streamingAssetsPath + "/ChineseLauguageConfigData.xml";
        //    }
        //    //Win环境
        //    else
        //    {
        //        logPath = "file://" + Application.streamingAssetsPath + "/ChineseLauguageConfigData.xml";
        //    }

        //    return logPath;
        //}
        ////得到“中文XML”配置文件根节点名称
        //public static string GetLauguageConfigFileRootNodeName()
        //{
        //    string strReturnXMLRootNodeName = null;

        //    strReturnXMLRootNodeName = "ChineseConfigData";
        //    return strReturnXMLRootNodeName;
        //}

        #endregion

        #region 委托定义

        #endregion

        #region 框架接口

        #endregion
    }//Class_end

    /// <summary>
    /// UI（窗体）类型
    /// </summary>
    internal class UIType
    {
        //是否需要清空“反向切换”
        public bool IsClearReverseChange = false;
        //UI窗体类型
        public UIFormsType UIForms_Type = UIFormsType.Normal;
        //UI窗体显示类型
        public UIFormsShowMode UIForms_ShowMode = UIFormsShowMode.Normal;
        //UI窗体透明度类型
        public UIFormsLucencyType UIForms_LucencyType = UIFormsLucencyType.Lucency;
    }

}