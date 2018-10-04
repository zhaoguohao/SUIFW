// Unity帮助类     
// 功能： 提供程序用户常用功能集。

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace SUIFW
{
    public class UnityHelper : MonoBehaviour
    {
        //是否是第一次加载游戏,默认是
        public static bool isFirstLoad = true;

        /// <summary>
        /// 获取指定范围内随机数
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static int GetRandom(int num1, int num2)
        {
            if (num1 < num2)
            {
                return UnityEngine.Random.Range(num1, num2+1);
            }
            else
            {
                return UnityEngine.Random.Range(num2, num1+1);
            }
        }
               
        /// <summary>
        /// 清理内存(一般在切换场景的时候调用)
        /// </summary>
        public static void ClearMemory()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        /// <summary>
        /// 查找子对象
        /// </summary>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static Transform FindTheChild(GameObject goParent, string childName)
        {
            Transform searchTrans = goParent.transform.Find(childName);
            if (searchTrans == null)
            {
                foreach (Transform trans in goParent.transform)
                {
                    searchTrans = FindTheChild(trans.gameObject, childName);
                    if (searchTrans != null)
                    {
                        return searchTrans;
                    }
                }
            }
            return searchTrans;
        }
       
        /// <summary>
        /// 获取子物体的脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T GetTheChildComponent<T>(GameObject goParent, string childName) where T : Component
        {
            Transform searchTrans = FindTheChild(goParent, childName);
            if (searchTrans != null)
            {
                return searchTrans.gameObject.GetComponent<T>();
            }
            else
            {
                return null;
            }
        }
               
        /// <summary>
        /// 给子物体添加脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T AddTheChildComponent<T>(GameObject goParent, string childName) where T : Component
        {
            Transform searchTrans = FindTheChild(goParent, childName);
            if (searchTrans != null)
            {
                T[] theComponentsArr = searchTrans.GetComponents<T>();
                for (int i = 0; i < theComponentsArr.Length; i++)
                {
                    if (theComponentsArr[i] != null)
                    {
                        Destroy(theComponentsArr[i]);
                    }
                }
                return searchTrans.gameObject.AddComponent<T>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 给子物体添加父对象
        /// </summary>
        /// <param name="parentTrs">父对象的方位</param>
        /// <param name="childTrs">子对象的方位</param>
        public static void AddChildToParent(Transform parentTrs, Transform childTrs)
        {
            //childTrs.parent = parentTrs; //Original Method
            childTrs.SetParent(parentTrs,false);
            childTrs.localPosition = Vector3.zero;
            childTrs.localScale = Vector3.one;
            childTrs.localEulerAngles = Vector3.zero;
        }

        //加载场景的开关   ???
        public static void OpenLoadSceneHelper()
        {
            GameObject uiRoot = GameObject.FindGameObjectWithTag("CanvasRoot");
            if (uiRoot != null)
            {
                GameObject helpGo = FindTheChild(uiRoot, "LoadSceneHelper").gameObject;
                if (helpGo.activeInHierarchy == false)
                {
                    helpGo.SetActive(true);
                }
            }
        }

    }//Class_end
}
