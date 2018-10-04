// 基础UI窗体     
// 功能：所有用户UI窗体的父类
// 1：定于四个“UI窗体”的状态
// Display:    显示状态
// Hiding:     隐藏状态(即：不能看见，不能操作)
// Redisplay:  重新显示状态
// Freeze:     冻结状态(即：在其他窗体下面，看见但不能操作)

using DemoProject;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace SUIFW
{
    public class BaseUIForms : MonoBehaviour
    {
        /*  字段  */
        //当前(基类)窗口的类型
        private UIType _CurrentUIType=new UIType();

        /*  属性  */
        /// <summary>
        /// 属性_当前UI窗体类型
        /// </summary>
        internal UIType CurrentUIType
        {
            set
            {
                _CurrentUIType = value;
            }

            get
            {
                return _CurrentUIType;
            }
        }

        #region 窗体生命周期
        //页面显示
        public virtual void Display()
        {
            this.gameObject.SetActive(true);
            if (_CurrentUIType.UIForms_Type == UIFormsType.PopUp) 
            {
                //添加UI遮罩处理
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject,_CurrentUIType.UIForms_LucencyType);                
            }
        }
        //页面隐藏(不在“栈”集合中)
        public virtual void Hiding()
        {
            this.gameObject.SetActive(false);
            if (_CurrentUIType.UIForms_Type == UIFormsType.PopUp)
            {
                //添加UI遮罩处理
                UIMaskMgr.GetInstance().CancleMaskWindow();
            }
        }
        //页面重新显示
        public virtual void Redisplay()
        {
            this.gameObject.SetActive(true);
            if (_CurrentUIType.UIForms_Type == UIFormsType.PopUp)
            {
                //添加UI遮罩处理
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForms_LucencyType);
            }
        }
        //页面冻结(还在“栈”集合中)
        public virtual void Freeze()
        {
            this.gameObject.SetActive(true);
        } 
        #endregion

        #region 给子类封装的方法
        /// <summary>
        /// 注册按钮对象事件
        /// </summary>
        /// <param name="strButtonName">(UI预设)需要注册事件的按钮名称</param>
        /// <param name="delHandle">([委托类型]按钮的注册方法)</param>
        protected void RigisteButtonObjectEvent(string strButtonName, EventTriggerListener.VoidDelegate delHandle)
        {
            GameObject goNeedRigistButton = UnityHelper.FindTheChild(this.gameObject, strButtonName).gameObject;
            EventTriggerListener.Get(goNeedRigistButton).onClick = delHandle;
        }

        /// <summary>
        /// 关闭与返回UI窗体  
        /// </summary>
        protected void CloseOrReturnUIForms()
        {
            string strUIFomrsName = null;
            int intPosition = -1;


            strUIFomrsName = GetType().ToString();
            intPosition = strUIFomrsName.IndexOf('.');
            if (intPosition != -1)
            {
                strUIFomrsName = strUIFomrsName.Substring(intPosition + 1);
            }
            UIManager.GetInstance().CloseOrReturnUIForms(strUIFomrsName);
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="strUIFormsName"></param>
        protected void ShowUIForms(string strUIFormsName)
        {
            UIManager.GetInstance().ShowUIForms(strUIFormsName);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="strMsgType">消息大类</param>
        /// <param name="strSmallClassType">消息小类</param>
        /// <param name="strMsgContent">消息内容</param>
        protected void SendMessage(string strMsgType, string strSmallClassType, object objMsgContent)
        {
            KeyValuesUpdate kv = new KeyValuesUpdate(strSmallClassType, objMsgContent);
            MessageCenter.SendMessage(strMsgType, kv);
        }

        /// <summary>
        /// 显示语言信息
        /// </summary>
        /// <param name="info"></param>
        protected string Show(string info)
        {
            return LauguageMgr.GetInstance().ShowText(info);
        }

        #endregion

    }//Class_end
}
