using UI;
using UnityEngine;

public static class ResourceLoader
{
    public static GameObject LoadPrefab(ResourcePath path)
    {
        return Resources.Load<GameObject>(path.PathResource);
    }

    public static T LoadObject<T>(ResourcePath path) where T:Object
    {
        return Resources.Load<T>(path.PathResource);
    }

    public static T LoadAndInstantiateView<T>(ResourcePath path, Transform uiRoot) where T:Component, IView
    {
        var prefab = Resources.Load<GameObject>(path.PathResource);
        var go = GameObject.Instantiate(prefab, uiRoot);
        return go.GetComponent<T>();
    }

    public static BaseDataSource<T> LoadDataSource<T>(ResourcePath path) where T : ScriptableObject
    {
        return Resources.Load<BaseDataSource<T>>(path.PathResource);
    }
} 
