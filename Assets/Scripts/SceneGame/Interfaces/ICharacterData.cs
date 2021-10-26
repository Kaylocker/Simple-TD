using UnityEngine;

public interface ICharacterData 
{
    T GetCharacterData<T>() where T : ScriptableObject;

    int Level { get; }
}
