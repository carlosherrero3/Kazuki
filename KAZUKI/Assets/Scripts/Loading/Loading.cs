using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private void Start()
    {
        string leveToLoad = LevelLoader.nextLevel;
        StartCoroutine(this.MakeTheLoad(leveToLoad)); //Corrutina operacion Asintona
    }

    IEnumerator MakeTheLoad(string level)
    {
        //QUITAR ESTO, ES SOLO PARA PROBAR
        yield return new WaitForSeconds(3f);


        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        while (operation.isDone == false)
        {
            yield return null;
        }
    }
}
