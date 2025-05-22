using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonSound : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;
    private HashSet<Button> registeredButtons = new HashSet<Button>();

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = clickSound;
    }

    void Start()
    {
        StartCoroutine(RegisterButtonsContinuously());
    }

    IEnumerator RegisterButtonsContinuously()
    {
        while (true)
        {
            RegisterButtonsInChildren(transform);
            yield return new WaitForSeconds(0.5f); // cada medio segundo revisa
        }
    }

    void RegisterButtonsInChildren(Transform parent)
    {
        Button[] buttons = parent.GetComponentsInChildren<Button>(true); // incluye inactivos

        foreach (Button button in buttons)
        {
            if (!registeredButtons.Contains(button))
            {
                button.onClick.AddListener(() => PlayClickSound());
                registeredButtons.Add(button);
            }
        }
    }

    void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
