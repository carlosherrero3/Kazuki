using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SceneChangeTriggerWithSound : MonoBehaviour
{
    public string sceneName;            // Escena a cargar
    public AudioClip inputSound;        // Sonido al pulsar
    private AudioSource audioSource;
    private bool inputDetected = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!inputDetected && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            inputDetected = true;

            if (inputSound != null)
            {
                audioSource.PlayOneShot(inputSound);
                StartCoroutine(WaitAndLoadScene(inputSound.length));
            }
            else
            {
                LoadScene();
            }
        }
    }

    System.Collections.IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene();
    }

    void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Nombre de escena no asignado.");
        }
    }
}
