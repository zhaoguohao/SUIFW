// 英雄信息

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using SUIFW;

namespace DemoProject
{
    public class HeroInfo : BaseUIForms
    {

        void Awake()
        {
            //本窗体状态
            CurrentUIType.UIForms_Type = UIFormsType.Fixed;
            CurrentUIType.UIForms_ShowMode = UIFormsShowMode.Normal;
        }//Awake_end

        void Start()
        {

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
    }//Class_end
}