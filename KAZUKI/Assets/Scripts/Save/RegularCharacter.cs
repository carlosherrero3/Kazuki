using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RegularCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Menu Inicio");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Menu Inicio");
        }
    }
}
