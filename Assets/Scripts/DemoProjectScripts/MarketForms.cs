// 商城

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using SUIFW;


namespace DemoProject
{
    public class MarketForms : BaseUIForms
    {
        //商城标题
        public Text TxtTitle;
        public Text TxtReturn;              

        void Awake()
        {
            //本窗体属性
            CurrentUIType.IsClearReverseChange = true;
            CurrentUIType.UIForms_Type = UIFormsType.PopUp;
            CurrentUIType.UIForms_ShowMode =UIFormsShowMode.ReverseChange;
            //CurrentUIType.UIForms_LucencyType = UIFormsLucencyType.Impenetrable;

            //事件注册
            RigisteButtonObjectEvent("Btn_Good1",
                p =>
                {
                    ShowUIForms(UIFormsID.GoodsInfoForms.ToString());
                    SendMessage(SysConst.MarketInfo, SysConst.MarketInfo_PropDetailInfo, Show("ClozeDetailInfo"));
                }
                );
            RigisteButtonObjectEvent("Btn_Good3",
                p =>
                {
                    ShowUIForms(UIFormsID.GoodsInfoForms.ToString());
                    SendMessage(SysConst.MarketInfo, SysConst.MarketInfo_PropDetailInfo, Show("shoeDetailInfo"));
                }
            );
            RigisteButtonObjectEvent("Btn_Return",
                p => CloseOrReturnUIForms()
            );

        }//Awake_end

        void Start()
        {
            if (TxtTitle)
            {
                TxtTitle.text = Show("MarketSys");
            }
            if (TxtReturn)
            {
                TxtReturn.text = Show("Return");
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

    }//Class_end
}