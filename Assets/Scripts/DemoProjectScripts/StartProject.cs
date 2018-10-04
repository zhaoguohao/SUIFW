// 游戏开始

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

using SUIFW;
using UnityEngine.Windows.Speech;


namespace DemoProject
{
    public class StartProject : BaseUIForms
    {
        void Start()
        {
            //游戏开始
            UIManager.GetInstance().ShowUIForms(SysConst.LogonForms);
        }

    }//Class_end
}