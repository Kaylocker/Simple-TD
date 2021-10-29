using UnityEngine;

public interface IData<T> where T: ScriptableObject
{
    T GetData { get; }
}
