using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    
    使用说明 :
        1.需要成为单例的类继承它, T 为那个单例类 (如:class Weapon:Singleton<Weapon>)
        2.可以手动拖进一个对象中, 也可以通过代码直接调用  (如 Weapon.Instance)
        3. <注意!!!> 继承的类的Awake中需要手动调用一下 Base.Init ,用于初始化对象以及一些检错处理
        4. <注意!!!> 全场景单例和单场景单例决不能放一块. 同一方的单例不建议放一块.
        5.isAllScene为true时,代表是一个全局游戏类,会放在All Scene Manager下;为false时,代表是一个普通的场景单例类,会放在Scene Manager下.(默认为true)
*/
[DisallowMultipleComponent]
public class Singleton<T> : MonoBehaviour
    where T : Component
{
    [SerializeField]
    private bool isAllScene;
    protected static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject it = new GameObject(typeof(T).Name);
                _instance = it.AddComponent<T>();
            }
            return _instance;
        }
    }
    /// <summary>
    /// 单例初始化
    /// </summary>
    /// <param name="target">当前对象的值</param>
    /// <param name="isallscene">是否全场景单例(即切换场景不销毁)</param>
    protected void Init(T target, bool isallscene = true)
    {
        // Ensure singleton
        if (_instance)
        {
            throw new UnityEngine.UnityException(typeof(T).Name + " is not singleton. There are multiple componet");
        }
        else
        {
            _instance = target;
        }

        // Init
        isAllScene = isallscene;
        if (isAllScene)
        {
            GameObject allscenemanager = GameObject.Find("All Scene Manager");
            if (allscenemanager == null)
            {
                allscenemanager = new GameObject("All Scene Manager");
                GameObject.DontDestroyOnLoad(allscenemanager);
            }
            _instance.transform.parent = allscenemanager.transform;
            _instance.name = typeof(T).Name;
            GameObject.DontDestroyOnLoad(_instance);
        }
        else
        {
            GameObject scenemanager = GameObject.Find("Scene Manager");
            if (scenemanager == null)
            {
                scenemanager = new GameObject("Scene Manager");
            }
            _instance.transform.parent = scenemanager.transform;
            _instance.name = typeof(T).Name;
        }
    }
}
