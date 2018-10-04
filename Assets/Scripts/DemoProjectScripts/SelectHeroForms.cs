// 选择英雄

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using SUIFW;


namespace DemoProject
{
    public class SelectHeroForms : BaseUIForms
    {
        public Text TxtEnterGame;

        void Awake()
        {
            //本窗体状态
            //CurrentUIType.UIForms_Type = SysDefine.UIFormsType.Normal;
            CurrentUIType.UIForms_ShowMode = UIFormsShowMode.Normal;

            //事件注册
            RigisteButtonObjectEvent("BtnOK", EnterMainUIForm);
            RigisteButtonObjectEvent("ImgReturn",p=>
            {
                CloseOrReturnUIForms();  
            });
        }//Awake_end

        void Start()
        {
            if (TxtEnterGame)
            {
                TxtEnterGame.text = Show("EnterGame");
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

        //进入游戏主窗体
        private void EnterMainUIForm(GameObject go)
        {
            ShowUIForms(UIFormsID.MainForms.ToString());
            ShowUIForms(UIFormsID.HeroInfo.ToString());
        }
    }//Class_end
}