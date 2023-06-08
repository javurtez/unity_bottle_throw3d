/*
Made by Melbert Bolocon
*/

using UnityEngine;

public static class Instantiator
{
    public static Object CreatePanel(string type)
    {
        Object obj = Object.Instantiate(Resources.Load("Panels/" + type), GameManager.Instance.panelParent);
        obj.name = type.ToString();
        return obj;
    }
    public static Object CreateManager(object type)
    {
        Object obj = Object.Instantiate(Resources.Load("Managers/" + type), GameManager.Instance.managerParent);
        obj.name = type.ToString();
        return obj;
    }
}