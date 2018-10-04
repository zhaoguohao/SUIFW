using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUIFW
{
    [Serializable]
    class KeyValuesInfo
    {
        //语言信息
        public List<KeyValuesNode> ConfigInfo = null;
    }

    [Serializable]
    class KeyValuesNode
    {
        //键
        public string Key = null;
        //值
        public string Value = null;
    }
}
