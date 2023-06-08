/*
Made by Melbert Bolocon
*/

using Sirenix.OdinInspector;
using UnityEngine;

public class BaseStaticPanel<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
{
    protected static T instance;

    public virtual bool IsActive
    {
        get { return gameObject.activeInHierarchy; }
    }
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = Instantiator.CreatePanel(typeof(T).Name) as GameObject;
                instance = obj.GetComponent<SerializedMonoBehaviour>() as T;
                obj.GetComponent<Canvas>().worldCamera = GameManager.Instance.uiCamera;
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    public void CloseWithDelay(float delay)
    {
        LeanTween.delayedCall(gameObject, delay, Close);
    }
}