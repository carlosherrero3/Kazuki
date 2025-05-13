using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    Animator anim;
    public string newGameSceneName;
    public int quickSaveSlotID;
    [Header("Options Panel")]
    public GameObject MainOptionsPanel;
    public GameObject StartGameOptionsPanel;
    public GameObject GamePanel;
    public GameObject ControlsPanel;
    public GameObject GfxPanel;
    public GameObject LoadGamePanel;
    public int level = 3;
    public int Health = 40;
    public TextMeshPro saveName;
    
    void Start()
    {
      anim= GetComponent<Animator>();   

       PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);
    }
    #region Open Different panels
    public void openOptions()
    {
        MainOptionsPanel.SetActive(true);
        StartGameOptionsPanel.SetActive(false);

        anim.Play("buttonTweenAnims_on");
        playClickSound();

    }
    public void openStartGameOptions()
    {

    }

}
