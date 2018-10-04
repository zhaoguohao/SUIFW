// 确认

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using SUIFW;

namespace DemoProject
{
    public class ConfirmForms : BaseUIForms
    {
        public Text TextConfirmWindow;
        public Text TextConfirm;
        public Text TextConfirmCancle;

        void Awake()
        {
            //本窗体属性
            CurrentUIType.UIForms_Type = UIFormsType.PopUp;
            CurrentUIType.UIForms_ShowMode = UIFormsShowMode.ReverseChange;

            //事件注册
            RigisteButtonObjectEvent("BtnConfirm",ConfirmPurcherGoods);
            RigisteButtonObjectEvent("BtnCancel",
                p =>CloseOrReturnUIForms()
            );
        }//Awake_end

        void Start()
        {
            if (TextConfirmWindow)
            {
                TextConfirmWindow.text = Show("ConfirmWindow");
            }
            if (TextConfirm)
            {
                TextConfirm.text = Show("Confirm");
            }
            if (TextConfirmCancle)
            {
                TextConfirmCancle.text = Show("Cancle");
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

        /// <summary>
        /// 确认(购买物品)
        /// </summary>
        private void ConfirmPurcherGoods(GameObject go)
        {
            //todo...
        }
    }//Class_end
}