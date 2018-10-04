// 配置管理器   
// 功能： 对于Json（核心数据）配置文件的读取处理。

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    public class ConfigManagerByJson:IConfigManager
    {
        //定义应用设置集合
        private static Dictionary<string, string> _AppSetting;

        /// <summary>
        /// 属性： 应用设置
        /// </summary>
        public Dictionary<string, string> AppSetting
        {
            get { return _AppSetting; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="JsonPath">Json文件路径</param>
        public ConfigManagerByJson(string JsonPath)
        {
            _AppSetting = new Dictionary<string, string>();
            //初始化解析Json数据,到集合中
            InitAndAnalysisJson(JsonPath);
        }

        /// <summary>
        /// 初始化解析XML数据，到集合中（_AppSetting）
        /// </summary>
        /// <param name="JsonPath">Json的路径</param>
        private void InitAndAnalysisJson(string JsonPath)
        {
            TextAsset configInfo = null;
            KeyValuesInfo keyValuesInfoObj = null;

            //参数检查
            if (string.IsNullOrEmpty(JsonPath)) return;

            try
            {
                configInfo = Resources.Load<TextAsset>(JsonPath);
                keyValuesInfoObj = JsonUtility.FromJson<KeyValuesInfo>(configInfo.text);
            }
            catch
            {
                throw new JsonAnalysisException(GetType() + "/InitAndAnalysisJson()/Json Analysis Exception! ,please Check Json file Or Json file Path! Parameter JsonPath= " + JsonPath);
            }            
            foreach (KeyValuesNode node in keyValuesInfoObj.ConfigInfo)
            {
                _AppSetting.Add(node.Key,node.Value);
            }     
        }

        /// <summary>
        /// 得到AppSetting的最大数量
        /// </summary>
        public int GetAppSettingMaxNumber()
        {
            if (_AppSetting != null && _AppSetting.Count >= 1)
            {
                return _AppSetting.Count;
            }
            else
            {
                return 0;
            }
        }
    }
}
