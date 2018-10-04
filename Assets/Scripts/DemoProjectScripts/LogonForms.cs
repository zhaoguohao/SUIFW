// 登录游戏

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using SUIFW;


namespace DemoProject
{
    public class LogonForms : BaseUIForms
    {
        public Text TxtTitleLogonSystem;
        public Text TxtTitleLogonConfirm;

        void Awake()
        {
            //定义本UI窗体的类型
            //base.CurrentUIType.UIForms_Type = SysDefine.UIFormsType.Normal;
            base.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.Normal;
            
            //注册事件
            RigisteButtonObjectEvent("Btn_LogonSys", LogonSysDeal);

        }//Awake_end

        void Start()
        {
            if (TxtTitleLogonSystem)
            {
                TxtTitleLogonSystem.text = Show("LogonSystem");
            }
            if (TxtTitleLogonConfirm)
            {
                TxtTitleLogonConfirm.text = Show("Logon");
            }
        }//Start_end

        #region  窗体生命周期
        public override void Display()
        {
            base.Display();
            Log.Write(GetType() + "/Display()");
        }

        public override void Redisplay()
        {
            base.Redisplay();
            Log.Write(GetType() + "/Redisplay()");
        }

        public override void Freeze()
        {
            base.Freeze();
            Log.Write(GetType() + "/Freeze()");
        }

        public override void Hiding()
        {
            base.Hiding();
            Log.Write(GetType() + "/Hiding()");
        } 
        #endregion

        //登陆游戏，后台处理
        private void LogonSysDeal(GameObject go)
        {
            //后台网络验证逻辑处理。
            //Todo......

            //验证成功后，转到下一个页面
            //ShowUIForms(UIFormsID.SelectHeroForms.ToString());
            ShowUIForms(SysConst.SelectHeroForms);
        }
    }//Class_end
}