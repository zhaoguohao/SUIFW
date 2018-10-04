// 主题： 配置管理器接口   
// 功能： 对于XML（核心数据）配置文件的读取处理。
 
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


namespace SUIFW
{
    public interface IConfigManager
    {
        /// <summary>
        /// 属性： 应用设置
        /// </summary>
        Dictionary<string, string> AppSetting { get; }

        /// <summary>
        /// 得到AppSetting的最大数量
        /// </summary>
        int GetAppSettingMaxNumber();
    }//Class_end
}