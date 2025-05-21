using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EspecialDontDestroid : MonoBehaviour
{
    public List<GameObject> objectsToKeep;
    public List<string> scenesToKeepObjects;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        foreach (GameObject obj in objectsToKeep)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!scenesToKeepObjects.Contains(scene.name))
        {
            foreach (GameObject obj in objectsToKeep)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}