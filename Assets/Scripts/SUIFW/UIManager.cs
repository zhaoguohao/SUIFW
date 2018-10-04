// 主题： UI管理器     
// 功能：整个UI框架的核心，用户程序通过调用本类，来调用本框架的大多数功能。  
// 功能1：关于入“栈”与出“栈”的UI窗体4个状态的定义逻辑
//       入栈状态：
//           Freeze();   （上一个UI窗体）冻结
//           Display();  （本UI窗体）显示
//       出栈状态： 
//           Hiding();    (本UI窗体) 隐藏
//           Redisplay(); (上一个UI窗体) 重新显示
// 功能2：增加“非栈”缓存集合。 


using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


namespace SUIFW
{
    public class UIManager : MonoBehaviour
    {
        /* 字段  */
        //本类实例
        private static UIManager _Instance = null;
        //存储所有“UI窗体预设(Prefab)”路径
        //参数含义： 第1个string 表示“窗体预设”名称，后一个string 表示对应的路径
        private Dictionary<string, string> _DicUIFormsPaths;
        //缓存所有已经打开的“UI窗体预设(Prefab)”
        //参数含义： 第1个string 表示“窗体预设”名称，后一个BaseUI 表示对应的“窗体预设”
        private Dictionary<string, BaseUIForms> _DicALLUIForms;
        //“栈”结构表示的“当前UI窗体”集合。
        private Stack<BaseUIForms> _StaCurrentUIForms;
        //当前显示状态的UI窗体集合
        private Dictionary<string, BaseUIForms> _DicCurrentShowUIForms;
        //UI根节点
        private Transform _CanvasTransform = null;
        //普通全屏界面节点
        private Transform _CanTransformNormal = null;
        //固定界面节点
        private Transform _CanTransformFixed = null;
        //弹出模式节点
        private Transform _CanTransformPopUp = null;
        //UI脚本节点（加载各种管理脚本的节点）
        private Transform _CanTransformUIScripts = null;




        /// <summary>
        /// 得到本类实例
        /// </summary>
        /// <returns></returns>
        public static UIManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
            }
            return _Instance;
        }

        void Awake()
        {
            //字段初始化
            _DicUIFormsPaths = new Dictionary<string, string>();
            _DicALLUIForms = new Dictionary<string, BaseUIForms>();
            _StaCurrentUIForms = new Stack<BaseUIForms>();
            _DicCurrentShowUIForms = new Dictionary<string, BaseUIForms>();

            //初始化项目开始必须的资源加载
            InitRootCanvasLoading();

            //得到UI根节点、及其重要子节点                     
            _CanvasTransform = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;
            //得到普通全屏界面节点、固定界面节点、弹出模式节点、UI脚本节点
            _CanTransformNormal = UnityHelper.FindTheChild(_CanvasTransform.gameObject, SysDefine.SYS_CANVAS_NORMAL_NODE_NAME);
            _CanTransformFixed = UnityHelper.FindTheChild(_CanvasTransform.gameObject, SysDefine.SYS_CANVAS_FIXED_NODE_NAME);
            _CanTransformPopUp = UnityHelper.FindTheChild(_CanvasTransform.gameObject, SysDefine.SYS_CANVAS_POPUP_NODE_NAME);
            _CanTransformUIScripts = UnityHelper.FindTheChild(_CanvasTransform.gameObject, SysDefine.SYS_CANVAS_UISCRIPTS_NODE_NAME);

            //把本脚本实例，作为Canvas的子节点
            UnityHelper.AddChildToParent(_CanTransformUIScripts, this.gameObject.transform);

            //本UI节点信息，场景转换时，不允许销毁
            DontDestroyOnLoad(_CanvasTransform);
            //初始化“UI窗体预设”路径数据
            InitUIFormsPathsData();
        }

        /// <summary>
        /// 显示UI窗体
        /// </summary>
        /// <param name="strUIFormName">UI窗体的名称</param>
        public void ShowUIForms(string strUIFormName)
        {
            BaseUIForms baseUIForms;                        //UI窗体基类

            //参数检查
            if (string.IsNullOrEmpty(strUIFormName)) return;

            //加载“UI窗体名称”，到“所有UI窗体缓存”中
            baseUIForms = LoadUIFormsToAllUIFormsCatch(strUIFormName);
            if (baseUIForms == null) return;

            //判断是否清空“栈”结构体集合
            if (baseUIForms.CurrentUIType.IsClearReverseChange)
            {
                ClearStackArray();
            }

            //判断不同的窗体显示模式，分别进行处理
            switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormsShowMode.Normal:
                    EnterUIFormsCache(strUIFormName);
                    break;
                case UIFormsShowMode.ReverseChange:
                    PushUIForms(strUIFormName);
                    break;
                case UIFormsShowMode.HideOther:
                    EnterUIFormstToCacheHideOther(strUIFormName);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 关闭或返回上一个UI窗体(关闭当前UI窗体)
        /// </summary>
        public void CloseOrReturnUIForms(string strUIFormName)
        {
            BaseUIForms baseUIForms = null;                   //UI窗体基类

            /* 参数检查 */
            if (string.IsNullOrEmpty(strUIFormName)) return;
            //“所有UI窗体缓存”如果没有记录，则直接返回。
            _DicALLUIForms.TryGetValue(strUIFormName, out baseUIForms);
            if (baseUIForms == null) return;

            /* 判断不同的窗体显示模式，分别进行处理 */
            switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormsShowMode.Normal:
                    ExitUIFormsCache(strUIFormName);
                    break;
                case UIFormsShowMode.ReverseChange:
                    PopUIForms();
                    break;
                case UIFormsShowMode.HideOther:
                    ExitUIFormsFromCacheAndShowOther(strUIFormName);
                    break;
                default:
                    break;
            }

        }

        #region 私有方法
        /// <summary>
        /// 根据指定UI窗体名称，加载到“所有UI窗体”缓存中。
        /// </summary>
        /// <param name="strUIFormName">UI窗体名称</param>
        /// <returns></returns>
        private BaseUIForms LoadUIFormsToAllUIFormsCatch(string strUIFormName)
        {
            BaseUIForms baseUI;                             //UI窗体

            //判断“UI预设缓存集合”是否有指定的UI窗体,否则新加载窗体
            _DicALLUIForms.TryGetValue(strUIFormName, out baseUI);
            if (baseUI == null)
            {
                //加载指定路径的“UI窗体”
                baseUI = LoadUIForms(strUIFormName);
            }

            return baseUI;
        }

        /// <summary>
        /// 加载UI窗体到“当前显示窗体集合”缓存中。
        /// </summary>
        /// <param name="strUIFormsName"></param>
        private void EnterUIFormsCache(string strUIFormsName)
        {
            BaseUIForms baseUIForms;                        //UI窗体基类
            BaseUIForms baseUIFormsFromAllCache;            //"所有窗体集合"中的窗体基类

            //“正在显示UI窗体缓存”集合里有记录，则直接返回。
            _DicCurrentShowUIForms.TryGetValue(strUIFormsName, out baseUIForms);
            if (baseUIForms != null) return;

            //把当前窗体，加载到“正在显示UI窗体缓存”集合里
            _DicALLUIForms.TryGetValue(strUIFormsName, out baseUIFormsFromAllCache);
            if (baseUIFormsFromAllCache != null)
            {
                _DicCurrentShowUIForms.Add(strUIFormsName, baseUIFormsFromAllCache);
                baseUIFormsFromAllCache.Display();
            }
        }

        /// <summary>
        /// 卸载UI窗体从“当前显示窗体集合”缓存中。
        /// </summary>
        /// <param name="strUIFormsName"></param>
        private void ExitUIFormsCache(string strUIFormsName)
        {
            BaseUIForms baseUIForms;                        //UI窗体基类

            //“正在显示UI窗体缓存”集合没有记录，则直接返回。
            _DicCurrentShowUIForms.TryGetValue(strUIFormsName, out baseUIForms);
            if (baseUIForms == null) return;

            //指定UI窗体，运行隐藏状态，且从“正在显示UI窗体缓存”集合中移除。
            baseUIForms.Hiding();
            _DicCurrentShowUIForms.Remove(strUIFormsName);
        }

        /// <summary>
        /// 加载UI窗体到“当前显示窗体集合”缓存中,且隐藏其他正在显示的页面
        /// </summary>
        /// <param name="strUIFormsName"></param>
        private void EnterUIFormstToCacheHideOther(string strUIFormsName)
        {
            BaseUIForms baseUIForms;                        //UI窗体基类
            BaseUIForms baseUIFormsFromAllCache;            //"所有窗体集合"中的窗体基类

            //“正在显示UI窗体缓存”集合里有记录，则直接返回。
            _DicCurrentShowUIForms.TryGetValue(strUIFormsName, out baseUIForms);
            if (baseUIForms != null) return;

            //“正在显示UI窗体缓存”与“栈缓存”集合里所有窗体进行隐藏处理。
            foreach (BaseUIForms baseUIFormsItem in _DicCurrentShowUIForms.Values)
            {
                baseUIFormsItem.Hiding();
            }
            foreach (BaseUIForms basUIFormsItem in _StaCurrentUIForms)
            {
                basUIFormsItem.Hiding();
            }

            //把当前窗体，加载到“正在显示UI窗体缓存”集合里
            _DicALLUIForms.TryGetValue(strUIFormsName, out baseUIFormsFromAllCache);
            if (baseUIFormsFromAllCache != null)
            {
                _DicCurrentShowUIForms.Add(strUIFormsName, baseUIFormsFromAllCache);
                baseUIFormsFromAllCache.Display();
            }
        }

        /// <summary>
        /// 卸载UI窗体从“当前显示窗体集合”缓存中,且显示其他原本需要显示的页面
        /// </summary>
        /// <param name="strUIFormsName"></param>
        private void ExitUIFormsFromCacheAndShowOther(string strUIFormsName)
        {
            BaseUIForms baseUIForms;                        //UI窗体基类

            //“正在显示UI窗体缓存”集合没有记录，则直接返回。
            _DicCurrentShowUIForms.TryGetValue(strUIFormsName, out baseUIForms);
            if (baseUIForms == null) return;

            //指定UI窗体，运行隐藏状态，且从“正在显示UI窗体缓存”集合中移除。
            baseUIForms.Hiding();
            _DicCurrentShowUIForms.Remove(strUIFormsName);

            //“正在显示UI窗体缓存”与“栈缓存”集合里所有窗体进行再次显示处理。
            foreach (BaseUIForms baseUIFormsItem in _DicCurrentShowUIForms.Values)
            {
                baseUIFormsItem.Redisplay();
            }
            foreach (BaseUIForms basUIFormsItem in _StaCurrentUIForms)
            {
                basUIFormsItem.Redisplay();
            }
        }

        /// <summary>
        /// UI窗体入栈
        /// 功能1： 判断栈里是否已经有窗体，有则“冻结”
        ///     2： 先判断“UI预设缓存集合”是否有指定的UI窗体,有则处理。
        ///     3： 指定UI窗体入"栈"
        /// </summary>
        /// <param name="strUIFormsName"></param>
        private void PushUIForms(string strUIFormsName)
        {
            BaseUIForms baseUI;                             //UI预设窗体


            //判断栈里是否已经有窗体，有则“冻结”
            if (_StaCurrentUIForms.Count > 0)
            {
                BaseUIForms topUIForms = _StaCurrentUIForms.Peek();
                topUIForms.Freeze();
            }

            //先判断“UI预设缓存集合”是否有指定的UI窗体,有则处理。
            _DicALLUIForms.TryGetValue(strUIFormsName, out baseUI);
            if (baseUI != null)
            {
                baseUI.Display();
            }
            else
            {
                Log.Write(GetType() + string.Format("/PushUIForms()/ baseUI==null! 核心错误，请检查 strUIFormsName={0} ", strUIFormsName), Log.Level.High);
            }

            //指定UI窗体入"栈"
            _StaCurrentUIForms.Push(baseUI);
        }

        /// <summary>
        /// UI窗体出栈逻辑
        /// </summary>
        private void PopUIForms()
        {
            if (_StaCurrentUIForms.Count >= 2)
            {
                /* 出栈逻辑 */
                BaseUIForms topUIForms = _StaCurrentUIForms.Pop();
                //出栈的窗体，进行隐藏处理
                topUIForms.Hiding();
                //出栈窗体的下一个窗体逻辑
                BaseUIForms nextUIForms = _StaCurrentUIForms.Peek();
                //下一个窗体"重新显示"处理
                nextUIForms.Redisplay();
            }
            else if (_StaCurrentUIForms.Count == 1)
            {
                /* 出栈逻辑 */
                BaseUIForms topUIForms = _StaCurrentUIForms.Pop();
                //出栈的窗体，进行"隐藏"处理
                topUIForms.Hiding();
            }
        }

        /// <summary>
        /// 加载与显示UI窗体
        /// 功能：
        ///    1：根据“UI窗体预设”名称，加载预设克隆体。
        ///    2：预设克隆体添加UI“根节点”为父节点。
        ///    3：隐藏刚创建的UI克隆体。
        ///    4：新创建的“UI窗体”，加入“UI窗体缓存”中
        /// </summary>
        private BaseUIForms LoadUIForms(string strUIFormsName)
        {
            string strUIFormsPaths = null;                  //UI窗体的路径
            GameObject goCloneUIPrefab = null;              //克隆的"窗体预设"
            BaseUIForms baseUIForm;                         //UI窗体


            //得到UI窗体的路径
            _DicUIFormsPaths.TryGetValue(strUIFormsName, out strUIFormsPaths);

            //加载指定路径的“UI窗体”
            if (!string.IsNullOrEmpty(strUIFormsPaths))
            {
                goCloneUIPrefab = ResourcesMgr.GetInstance().LoadAsset(strUIFormsPaths, false);
            }

            //设置“UI窗体”克隆体的父节点，以及隐藏处理与加入“UI窗体缓存”中
            if (_CanvasTransform != null && goCloneUIPrefab != null)
            {
                baseUIForm = goCloneUIPrefab.GetComponent<BaseUIForms>();
                if (baseUIForm == null)
                {
                    Log.Write(GetType() + string.Format("/LoadUIForms()/ baseUIForm==null！,请先确认克隆对象上是否加载了BaseUIForms的子类。参数 strUIFormsName='{0}' ", strUIFormsName), Log.Level.High);
                    return null;
                }
                switch (baseUIForm.CurrentUIType.UIForms_Type)
                {
                    case UIFormsType.Normal:
                        goCloneUIPrefab.transform.SetParent(_CanTransformNormal, false);
                        break;
                    case UIFormsType.Fixed:
                        goCloneUIPrefab.transform.SetParent(_CanTransformFixed, false);
                        break;
                    case UIFormsType.PopUp:
                        goCloneUIPrefab.transform.SetParent(_CanTransformPopUp, false);
                        break;
                    default:
                        break;
                }

                goCloneUIPrefab.SetActive(false);
                //新创建的“UI窗体”，加入“UI窗体缓存”中
                _DicALLUIForms.Add(strUIFormsName, baseUIForm);
                return baseUIForm;
            }
            else
            {
                Log.Write(GetType() + string.Format("/LoadUIForms()/‘_CanvasTransform’ Or ‘goCloneUIPrefab’==NULL!  , 方法参数 strUIFormsName={0}，请检查！", strUIFormsName), Log.Level.High);
            }

            Log.Write(GetType() + string.Format("/LoadUIForms()/ 出现不可预知错误，请检查！ 方法参数 strUIFormsName={0} ", strUIFormsName), Log.Level.High);
            return null;
        }

        /// <summary>
        /// 初始化项目开始必须的资源加载
        /// </summary>
        private void InitRootCanvasLoading()
        {
            if (UnityHelper.isFirstLoad)
            {
                ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_PATH_CANVAS, false);
            }
        }

        /// <summary>
        /// 初始化“UI窗体预设”路径数据
        /// </summary>
        private void InitUIFormsPathsData()
        {
            //测试也成功
            IConfigManager configMgr = new ConfigManagerByJson(SysDefine.SYS_PATH_UIFormConfigJson);
            if (_DicUIFormsPaths != null)
            {
                _DicUIFormsPaths = configMgr.AppSetting;
            }
        }

        /// <summary>
        /// 清空“栈”结构体集合
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (_StaCurrentUIForms != null && _StaCurrentUIForms.Count >= 1)
            {
                _StaCurrentUIForms.Clear();
                return true;
            }
            return false;
        }

        #endregion

        #region  测试方法
        //测试核心集合中数量
        public void Test_DisplayArrayCount()
        {
            if (_DicALLUIForms != null)
            {
                Log.Write(GetType() + "/Test_DisplayArrayCount()/_DicALLUIForms.count=" + _DicALLUIForms.Count);
            }
            if (_DicCurrentShowUIForms != null)
            {
                Log.Write(GetType() + "/Test_DisplayArrayCount()/_DicCurrentShowUIForms.count=" + _DicCurrentShowUIForms.Count);
            }
            if (_StaCurrentUIForms != null)
            {
                Log.Write(GetType() + "/Test_DisplayArrayCount()/_StaCurrentUIForms.count=" + _StaCurrentUIForms.Count);
            }
        }
        #endregion
    }//Class_end
}
