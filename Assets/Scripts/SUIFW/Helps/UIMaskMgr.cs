// UI遮罩管理器      
// 功能： 负责“弹出窗体”的模态实现。

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


namespace SUIFW
{
    public class UIMaskMgr : MonoBehaviour
    {
        /* 字段 */
        //本脚本私有单例实例
        private static UIMaskMgr _Instance;                 
        //UI根节点对象
        private GameObject _GoCanvasRoot = null;
        //UI脚本节点（加载各种管理脚本的节点）
        private Transform _CanTransformUIScripts = null;
        //顶层面板
        private GameObject _GoTopPlane;                     
        //遮罩面板
        private GameObject _GoMaskPlane;                    
        //UI摄像机
        private Camera _UICamear;                           
        //原始UI摄像机的层深
        private float _OriginalUICameraDepth;              


        /// <summary>
        /// 得到实例(单例)
        /// </summary>
        /// <returns></returns>
        public static UIMaskMgr GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_UIMaskMgr").AddComponent<UIMaskMgr>();
            }
            return _Instance;
        }

        void Awake()
        {
            //得到UI根节点、UI脚本节点                    
            _GoCanvasRoot = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS);
            _CanTransformUIScripts = UnityHelper.FindTheChild(_GoCanvasRoot, SysDefine.SYS_CANVAS_UISCRIPTS_NODE_NAME);
            //把本脚本实例，作为Canvas的子节点
            UnityHelper.AddChildToParent(_CanTransformUIScripts, this.gameObject.transform);

            //得到“顶层面板”与“遮罩面板”
            _GoTopPlane = _GoCanvasRoot;
            _GoMaskPlane = UnityHelper.FindTheChild(_GoCanvasRoot.gameObject, SysDefine.SYS_CANVAS_UIMASKPANELS_NODE_NAME).gameObject;

            //得到UI摄像机的原始“层深”
            _UICamear = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_UICAMERA).GetComponent<Camera>();
            if (_UICamear != null)
            {
                _OriginalUICameraDepth = _UICamear.depth;
            }
            else
            {
                Log.Write(GetType() + "/Start()/_UICamera is Null ,please Check!");
            }
        }

        /// <summary>
        /// 设置遮罩状态
        /// </summary>
        /// <param name="goDisplayPlane">需要显示的窗体</param>
        public void SetMaskWindow(GameObject goDisplayPlane,UIFormsLucencyType UILucencyType=UIFormsLucencyType.Lucency)
        {
            //顶层窗体下移。
            _GoTopPlane.transform.SetAsLastSibling();

            //启用遮罩窗体与透明度
            switch (UILucencyType)
            {
                 case UIFormsLucencyType.Lucency:
                        _GoMaskPlane.SetActive(true);
                        Color newColor1 = new Color(SysDefine.SYS_UIMASK_LUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_LUCENCY_COLOR_A);
                        _GoMaskPlane.GetComponent<Image>().color = newColor1;
                    break;
                 case UIFormsLucencyType.Translucence:
                        _GoMaskPlane.SetActive(true);
                        Color newColor2 = new Color(SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_RGB, SysDefine.SYS_UIMASK_TRANSLUCENCY_COLOR_A);
                        _GoMaskPlane.GetComponent<Image>().color = newColor2;
                    break;
                 case UIFormsLucencyType.Impenetrable:
                        _GoMaskPlane.SetActive(true);
                        Color newColor3 = new Color(SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_RGB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_RGB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_RGB, SysDefine.SYS_UIMASK_IMPENETRABLE_COLOR_A);
                        _GoMaskPlane.GetComponent<Image>().color = newColor3;
                    break;
                 case UIFormsLucencyType.Penetrate:
                        if (_GoMaskPlane.activeInHierarchy)
                        {
                            _GoMaskPlane.SetActive(false);
                        }
                    break;
                 default:
                    break;
            }
            //遮罩窗体下移
            _GoMaskPlane.transform.SetAsLastSibling();
            //显示窗体下移
            goDisplayPlane.transform.SetAsLastSibling();
            //增加当前UI摄像机的“层深”
            if (_UICamear != null)
            {
                _UICamear.depth = _UICamear.depth + SysDefine.SYS_UICAMERA_DEPTH_INCREMENT;
            }
        }

        /// <summary>
        /// 取消遮罩窗体
        /// </summary>
        public void CancleMaskWindow()
        {
            //顶层窗体上移
            _GoTopPlane.transform.SetAsFirstSibling();
            //禁用遮罩窗体
            if (_GoMaskPlane.activeInHierarchy)
            {
                _GoMaskPlane.SetActive(false);
            }
            //回复UI摄像机的原来的“层深”
            _UICamear.depth = _OriginalUICameraDepth;
        }	
    }//Class_end
}