/*
Made by Melbert Bolocon
*/

using Sirenix.OdinInspector;
using UnityEngine;

public class MonoManager<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = Instantiator.CreateManager(typeof(T).Name) as GameObject;
                instance = obj.GetComponent<MonoBehaviour>() as T;
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
    }
    protected virtual void Start()
    {
    }
    protected virtual void OnDestroy()
    {
    }

    public virtual void Create()
    {

    }
}