using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Extend the default Unity functions
/// </summary>
/// <author>
/// Peter Schreuder @ 2019
/// </author>
public static class ExtentionMethods
{

    #region Vector3

    #region Change

    /// <summary>
    /// Changes a single axis of the Vector (X)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="value2">float Value that replace the current value of the axis</param>
    /// <returns>Vector3</returns>
    public static Vector3 ChangeX(this Vector3 value, float value2)
    {
        return new Vector3(value2, value.y, value.z);
    }

    /// <summary>
    /// Changes a single axis of the Vector (Y)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="value2">float Value that replace the current value of the axis</param>
    /// <returns>Vector3</returns>
    public static Vector3 ChangeY(this Vector3 value, float value2)
    {
        return new Vector3(value.x, value2, value.z);
    }

    /// <summary>
    /// Changes a single axis of the Vector (Z)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="value2">float Value that replace the current value of the axis</param>
    /// <returns>Vector3</returns>
    public static Vector3 ChangeZ(this Vector3 value, float value2)
    {
        return new Vector3(value.x, value.y, value2);
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds a given value to a single axis of the Vector (X)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="add">float Amount to add to the axis</param>
    /// <returns></returns>
    public static Vector3 AddX(this Vector3 value, float add)
    {
        return new Vector3(value.x + add, value.y, value.z);
    }

    /// <summary>
    /// Adds a given value to a single axis of the Vector (Y)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="add">float Amount to add to the axis</param>
    /// <returns></returns>
    public static Vector3 AddY(this Vector3 value, float add)
    {
        return new Vector3(value.x, value.y + add, value.z);
    }

    /// <summary>
    /// Adds a given value to a single axis of the Vector (Z)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="add">float Amount to add to the axis</param>
    /// <returns></returns>
    public static Vector3 AddZ(this Vector3 value, float add)
    {
        return new Vector3(value.x, value.y, value.z + add);
    }

    #endregion

    #region Functions

    /// <summary>
    /// Rounds all the axis of the Vector3
    /// </summary>
    /// <param name="_vector3"></param>
    /// <returns>Vector3</returns>
    public static Vector3 Round(this Vector3 _vector3)
    {
        return new Vector3(Mathf.Round(_vector3.x), Mathf.Round(_vector3.y), Mathf.Round(_vector3.z));
    }

    /// <summary>
    /// Multiplies the Vector3 with a given Vector3
    /// </summary>
    /// <param name="_v1"></param>
    /// <param name="_v2">The Vector3 multiplier that the Vector3 needs to be multiplied with</param>
    /// <returns>Vector3</returns>
    public static Vector3 Multiply(this Vector3 _v1, Vector3 _v2)
    {
        return new Vector3(_v1.x * _v2.x, _v1.y * _v2.y, _v1.z * _v2.z);
    }

    /// <summary>
    /// Multiplies all the axis of the Vector3 with a given float
    /// </summary>
    /// <param name="_v"></param>
    /// <param name="_scale">float Scale, this will multiply all the axis</param>
    /// <returns>Vector3</returns>
    public static Vector3 Multiply(this Vector3 _v, float _scale)
    {
        return new Vector3(_v.x * _scale, _v.y * _scale, _v.z * _scale);
    }

    /// <summary>
    /// Clamps the Vector3 axis with given Min, Max Vector3's
    /// </summary>
    /// <param name="_vector3"></param>
    /// <param name="_min">Dont let the axis be less than this</param>
    /// <param name="_max">Dont let the axis be more than this</param>
    /// <returns>Vector3</returns>
    public static Vector3 Clamp(this Vector3 _vector3, Vector3 _min, Vector3 _max)
    {
        return new Vector3(Mathf.Clamp(_vector3.x, _min.x, _max.x), Mathf.Clamp(_vector3.y, _min.y, _max.y), Mathf.Clamp(_vector3.z, _min.z, _max.z));
    }

    /// <summary>
    /// Clamps the Vector3 magnitude with given Min, Max Vector3's
    /// </summary>
    /// <param name="_vector3"></param>
    /// <param name="_min">Dont let the magnitude be less than this</param>
    /// <param name="_max">Dont let the magnitude be more than this</param>
    /// <returns>Vector3</returns>
    public static Vector3 ClampMagnitudeMinMax(this Vector3 _vector3, float max, float min)
    {
        double sm = _vector3.sqrMagnitude;
        if (sm > (double)max * (double)max) return _vector3.normalized * max;
        else if (sm < (double)min * (double)min) return _vector3.normalized * min;
        return _vector3;
    }

    #endregion

    #endregion

    #region Quaternion

    #region Change

    /// <summary>
    /// Adds a given value to a single axis of the Quaternion (X)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="add">float Amount to add to the axis</param>
    /// <returns>Quaternion</returns>
    public static Quaternion AddX(this Quaternion value, float add)
    {
        return Quaternion.Euler(value.x + add, value.y, value.z);
    }

    /// <summary>
    /// Adds a given value to a single axis of the Quaternion (Y)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="add">float Amount to add to the axis</param>
    /// <returns>Quaternion</returns>
    public static Quaternion AddY(this Quaternion value, float add)
    {
        return Quaternion.Euler(value.x + add, value.y + add, value.z);
    }

    /// <summary>
    /// Adds a given value to a single axis of the Quaternion (Z)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="add">float Amount to add to the axis</param>
    /// <returns>Quaternion</returns>
    public static Quaternion AddZ(this Quaternion value, float add)
    {
        return Quaternion.Euler(value.x, value.y, value.z + add);
    }

    #endregion

    #region Functions

    /// <summary>
    /// Multiplies the Vector3 with a given Vector3
    /// </summary>
    /// <param name="value"></param>
    /// <param name="scale">The Vector3 multiplier that the Vector3 needs to be multiplied with</param>
    /// <returns></returns>
    public static Quaternion Multiply(this Quaternion value, Vector3 scale)
    {
        return Quaternion.Euler(value.x * scale.x, value.y * scale.y, value.z * scale.z);
    }

    /// <summary>
    /// Multiplies all the axis of the Quaternion with a given float
    /// </summary>
    /// <param name="_q"></param>
    /// <param name="_scale">float Scale, this will multiply all the axis</param>
    /// <returns></returns>
    public static Quaternion Multiply(this Quaternion _q, float _scale)
    {
        return new Quaternion(_q.x * _scale, _q.y * _scale, _q.z * _scale, _q.w * _scale);
    }

    #endregion

    #endregion

    /// <summary>
    /// Rounds the float
    /// </summary>
    /// <param name="_float"></param>
    /// <returns>float</returns>
    public static float Round(this float _float)
    {
        return Mathf.Round(_float);
    }

    /// <summary>
    /// Extends the Button class to check if the mouse is being held on the button
    /// </summary>
    /// <param name="_button"></param>
    /// <returns>Button</returns>
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
    /// Extends from bool. Will convert an bool to a float
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

    /// <summary>
    /// Gets a random value from the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static T GetRandomValue<T>(this Array array)
    {
        if ((array == null) || (array.Length < 1)) return default(T);
        return (T)array.GetValue(UnityEngine.Random.Range(0, array.Length));
    }
}

