﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class ExtentionMethods
{

    #region Vector3

    //- Change
    public static Vector3 ChangeX(this Vector3 value, float value2)
    {
        return new Vector3(value2, value.y, value.z);
    }

    public static Vector3 ChangeY(this Vector3 value, float value2)
    {
        return new Vector3(value.x, value2, value.z);
    }

    public static Vector3 ChangeZ(this Vector3 value, float value2)
    {
        return new Vector3(value.x, value.y, value2);
    }

    //- Add
    public static Vector3 AddX(this Vector3 value, float add)
    {
        return new Vector3(value.x + add, value.y, value.z);
    }

    public static Vector3 AddY(this Vector3 value, float add)
    {
        return new Vector3(value.x, value.y + add, value.z);
    }

    public static Vector3 AddZ(this Vector3 value, float add)
    {
        return new Vector3(value.x, value.y, value.z + add);
    }

    public static Vector3 Round(this Vector3 _vector3)
    {
        return new Vector3(Mathf.Round(_vector3.x), Mathf.Round(_vector3.y), Mathf.Round(_vector3.z));
    }

    public static Vector3 Multiply(this Vector3 _v1, Vector3 _v2)
    {
        return new Vector3(_v1.x * _v2.x, _v1.y * _v2.y, _v1.z * _v2.z);
    }

    public static Vector3 Multiply(this Vector3 _v1, float _scale)
    {
        return new Vector3(_v1.x * _scale, _v1.y * _scale, _v1.z * _scale);
    }

    public static Vector3 Clamp(this Vector3 _vector3, Vector3 _min, Vector3 _max)
    {
        return new Vector3(Mathf.Clamp(_vector3.x, _min.x, _max.x), Mathf.Clamp(_vector3.y, _min.y, _max.y), Mathf.Clamp(_vector3.z, _min.z, _max.z));
    }

    #endregion

    #region Quaternion

    public static Quaternion AddX(this Quaternion value, float add)
    {
        return Quaternion.Euler(value.x + add, value.y, value.z);
    }

    public static Quaternion AddY(this Quaternion value, float add)
    {
        return Quaternion.Euler(value.x + add, value.y + add, value.z);
    }

    public static Quaternion AddZ(this Quaternion value, float add)
    {
        return Quaternion.Euler(value.x, value.y, value.z + add);
    }

    public static Quaternion Multiply(this Quaternion value, Vector3 scale)
    {
        return Quaternion.Euler(value.x * scale.x, value.y * scale.y, value.z * scale.z);
    }

    #endregion

    public static float Round(this float _float)
    {
        return Mathf.Round(_float);
    }

    public static Button OnDown(this Button _button)
    {
        Button _return = _button.OnDown();

        return _return;
    }

    /// <summary>
    /// Changes the Alpha of a Color
    /// </summary>
    /// <param name="_color"></param>
    /// <param name="_alpha">0 to 1</param>
    /// <returns></returns>
    public static Color ChangeAlpha(this Color _color, float _alpha)
    {
        return new Color(_color.r, _color.g, _color.b, _alpha);
    }

    /// <summary>
    /// Extend from bool
    /// </summary>
    /// <param name="_value"></param>
    /// <returns>false = 0f, true = 1f</returns>
    public static float ToFloat(this bool _value)
    {
        float _return = -1f;

        if (_value)
            _return = 1f;

        return _return;
    }

    public static T ArrayRandomValue<T>(this Array array)
    {
        if ((array == null) || (array.Length < 1)) return default(T);
        return (T)array.GetValue(UnityEngine.Random.Range(0, array.Length));
    }
}

