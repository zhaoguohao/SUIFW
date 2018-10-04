// 消息中心     
// 功能：  负责本UI框架，以及整个项目的消息传递工作。
 
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;


namespace SUIFW
{
    public class MessageCenter
    {
        /// <summary>
        /// 委托：消息传递
        /// </summary>
        /// <param name="kv">消息类型与数值</param>
        public delegate void DelMessageDelivery(KeyValuesUpdate kv);

        //<所要监听的类型，监听到以后要执行的委托>
        public static Dictionary<string, DelMessageDelivery> dicMessages = new Dictionary<string, DelMessageDelivery>();

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="handler">消息委托</param>
        public static void AddMsgListener(string messageType, DelMessageDelivery handler)
        {
            if (!dicMessages.ContainsKey(messageType))
            {
                dicMessages.Add(messageType, null);
            }
            dicMessages[messageType] += handler;
        }

        /// <summary>
        /// 取消指定消息监听
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="handler">消息委托</param>
        public static void RemoveMsgListener(string messageType, DelMessageDelivery handler)
        {
            if (dicMessages.ContainsKey(messageType))
            {
                dicMessages[messageType] -= handler;
            }
        }

        /// <summary>
        /// 取消所有的消息监听
        /// </summary>
        public static void RemoveAllMsgListener()
        {
            dicMessages.Clear();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="kv"></param>
        public static void SendMessage(string messageType, KeyValuesUpdate kv)
        {
            DelMessageDelivery del;
            if (dicMessages.TryGetValue(messageType, out del))
            {
                if (del != null)
                {
                    del(kv);
                }
            }
        }
    }//Class_end

    /// <summary>
    /// 键值更新
    /// 功能：配合委托，实现委托数据传递
    /// </summary>
    public class KeyValuesUpdate
    {
        //键
        private string _Key;   
        //值
        private object _Values; 

        /* 只读属性 */
        public string Key
        {
            get { return _Key; }
        }
        public object Values
        {
            get { return _Values; }
        }

        public KeyValuesUpdate(string key, object Values)
        {
            _Key = key;
            _Values = Values;
        }
    }//Class_end

}