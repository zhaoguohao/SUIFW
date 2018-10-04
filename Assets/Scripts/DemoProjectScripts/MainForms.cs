// 主界面

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using SUIFW;


namespace DemoProject
{
    public class MainForms : BaseUIForms
    {

        void Awake()
        {
            //本窗体状态
            CurrentUIType.UIForms_Type = UIFormsType.Normal;
            CurrentUIType.UIForms_ShowMode = UIFormsShowMode.HideOther;

            //事件注册
            RigisteButtonObjectEvent("Btn_Market",
                p => ShowUIForms(UIFormsID.MarketForms.ToString())
            );            
            RigisteButtonObjectEvent("Btn_Exit",   //这个退出游戏，需要调试？？？
                p => CloseOrReturnUIForms()
            );
        }//Awake_end

        //测试
        //IEnumerator Start()
        //{
        //    yield return new WaitForSeconds(2F);
        //    UIManager.GetInstance().Test_DisplayArrayCount();
        //    Log.Write("--  --");
        //    Log.Write("--  --");
        //    Log.Write("--  --");
        //    Log.Write("--  --");
        //}

        #region  窗体生命周期
        //public override void Display()
        //{
        //    base.Display();
        //    Log.Write(GetType() + "/Display()");
        //}

        //public override void Redisplay()
        //{
        //    base.Redisplay();
        //    Log.Write(GetType() + "/Redisplay()");
        //}

        //public override void Freeze()
        //{
        //    base.Freeze();
        //    Log.Write(GetType() + "/Freeze()");
        //}

        //public override void Hiding()
        //{
        //    base.Hiding();
        //    Log.Write(GetType() + "/Hiding()");
        //}
        #endregion
    }//Class_end
}