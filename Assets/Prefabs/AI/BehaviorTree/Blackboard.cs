using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum  BlackboardType { Int, Float, Bool, Vector3, GameoObject }

[Serializable]
public class BlackboardEntry
{
    [SerializeField] string keyName;
    [SerializeField] object value;
    [SerializeField] string runtimeValue;
    [SerializeField] BlackboardType type;

    public string GetKeyName() { return keyName; }

    public BlackboardEntry(string keyname, BlackboardType type)
    {
        this.keyName = keyname;
        this.value = null;
        this.runtimeValue = "";
        this.type = type;
    }

    public bool SetVal<T>(T val)
    {
        if(val == null)
        {
            value = null;
            runtimeValue = "null";
            return true;
        }

        if(val.GetType() != typeDictionary[type])
        {
            return false;
        }

        value = val;
        runtimeValue = val.ToString();
        return true;
    }

    public bool GetVal<T>(out T val)
    {
        val = default;
        if (value == null) return false;
        if(typeof(T) != typeDictionary[type])
        {
            return false;
        }

        val = (T)value;
        return true;
    }

    static Dictionary<BlackboardType, System.Type> typeDictionary = new Dictionary<BlackboardType, Type>
    {
        {BlackboardType.Float, typeof(float) },
        {BlackboardType.Int, typeof(int) },
        {BlackboardType.Bool, typeof(bool) },
        {BlackboardType.Vector3, typeof(Vector3) },
        {BlackboardType.GameoObject, typeof(GameObject) }
    };

    internal object GetRawValue()
    {
        return value;
    }

    public void ClearEntryValue()
    {
        this.value = null;
        this.runtimeValue = "";
    }
}
[CreateAssetMenu(menuName = "BehaviorTree/Blackboard")]
public class Blackboard : ScriptableObject
{
    [SerializeField] List<BlackboardEntry> blackboardData;

    public delegate void OnBlackboardValueChanged(BlackboardEntry entry);
    public event OnBlackboardValueChanged onBlackboardValueChanged;

    public bool SetBlackboardDat<T>(string keyName, T val)
    {
        foreach(var entry in blackboardData)
        {
            if(entry.GetKeyName() == keyName)
            {
                if(entry.SetVal(val))
                {
                    onBlackboardValueChanged?.Invoke(entry);
                    return true;
                }
                else
                {
                    return false;
                }
            }
   
        }

        return false;
    }

    public bool GetBlackboardData<T>(string keyName, out T val)
    {
        foreach(var entry in blackboardData)
        {
            if(entry.GetKeyName() == keyName)
            {
                return entry.GetVal(out val);
            }
        }

        val = default;
        return false;
    }

    internal object GetBlackboardRawData(string keyName)
    {
        foreach(var entry in blackboardData)
        {
            if(entry.GetKeyName() == keyName)
            {
                return entry.GetRawValue();
            }
        }

        return null;
    }

    public void ClearBlackboardData(string keyname)
    {
        foreach (var entry in blackboardData)
        {
            if (entry.GetKeyName() == keyname)
            {
                entry.ClearEntryValue();
            }
        }
    }
}
