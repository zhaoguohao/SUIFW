
using UnityEngine;
using System;
using System.Collections.Generic;


namespace SUIFW
{
    public class LauguageMgr
    {
        //本类实例
        public static LauguageMgr _Instance;
        //语言翻译缓存集合
        private Dictionary<string, string> _DicLauguageTraCache;


        /// <summary>
        /// 私有构造函数
        /// </summary>
        private LauguageMgr()
        {
            _DicLauguageTraCache = new Dictionary<string, string>();
            //初始化语言缓存集合
            InitLauguageCache();
        }

        /// <summary>
        /// 得到本类实例
        /// </summary>
        /// <returns></returns>
        public static LauguageMgr GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new LauguageMgr();
            }
            return _Instance;
        }

        /// <summary>
        /// 得到显示文本
        /// </summary>
        /// <returns></returns>
        public string ShowText(string strLauguageId)
        {
            string strQueryResult = null;                   //查询结果

            //参数检查
            if (string.IsNullOrEmpty(strLauguageId)) return null;
            //查询处理
            if (_DicLauguageTraCache != null && _DicLauguageTraCache.Count >= 1)
            {
                _DicLauguageTraCache.TryGetValue(strLauguageId, out strQueryResult);
                if (!string.IsNullOrEmpty(strQueryResult))
                {
                    return strQueryResult;
                }
            }
            Log.Write(GetType() + string.Format("/GetDisplayText()/从集合中查询无法查询出有效结果。参数strLauguageId='{0}'", strLauguageId), Log.Level.Special);
            return null;
        }

        /// <summary>
        /// 初始化语言缓存集合
        /// </summary>
        private void InitLauguageCache()
        {
            IConfigManager config = new ConfigManagerByJson(SysDefine.SYS_PATH_CNLauguageJson);
            if (config != null)
            {
                _DicLauguageTraCache = config.AppSetting;
            }
        }

    }//Class_end
}