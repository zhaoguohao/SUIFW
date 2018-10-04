// Json解析异常     
// 专门定位于Json文件解析的异常，如果出现本异常，说明Json格式定义错误。  

using System;

namespace SUIFW
{
    public class JsonAnalysisException : Exception
    {
            public JsonAnalysisException() : base() { }
            public JsonAnalysisException(string excptionMessage) : base(excptionMessage) { }
    }
}
