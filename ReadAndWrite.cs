using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    使用说明:
        1. 比如要存一个整数: ReadAndWrite<int> temp = new ReadAndWrite<int>(key); 此时可以直接操控temp来进行磁盘化存储,temp.value = 1;或者temp.value++;
        2. <注意!!!> 创建之后没有默认初始化值,必须手动去设置个值
        3. 比直接使用 PlayerPrefs 好的地方在于:将key值存了起来,对外只提供一个对象,可以通过操作该对象来进行存取.
*/

/// <summary>
/// 使用PlayerPrefs去存储 . <注意!!>必须初始化
/// </summary>
/// <typeparam name="T">Only Support : float, int, string</typeparam>
public class ReadAndWrite<T> 
{
    private bool hasChange = false;
    private string _key;
    public string key
    {
        get
        {
            return _key;
        }
    }
    private T _value;
    public T value
    {
        get
        {
            if (hasChange)
            {
                hasChange = false;
                return _value = Get(_key);
            }
            else
            {
                return _value;
            }
        }
        set
        {
            Set(_key, value);
            hasChange = true;
        }
    }
    private void Set(string k, T v)
    {
        if (typeof(T) == typeof(int))
        {
            PlayerPrefs.SetInt(k, int.Parse(v.ToString()));
        }
        else if (typeof(T) == typeof(float))
        {
            PlayerPrefs.SetFloat(k, float.Parse(v.ToString()));
        }
        else if (typeof(T) == typeof(string))
        {
            PlayerPrefs.SetString(k, v.ToString());
        }
    }
    private T Get(string k)
    {
        if (!PlayerPrefs.HasKey(_key))
        {
            throw new UnityEngine.UnityException(_key + " has not init !");
        }

        object t = 0;
        if (typeof(T) == typeof(int))
        {
            t = PlayerPrefs.GetInt(k);
        }
        else if (typeof(T) == typeof(float))
        {
            t = PlayerPrefs.GetFloat(k);
        }
        else if (typeof(T) == typeof(string))
        {
            t = PlayerPrefs.GetString(k);
        }
        return (T) t;
    }

    public ReadAndWrite(string Key)
    {
        _key = Key;
    }

}
