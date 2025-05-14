using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Slider slider;
    public float sliderValor;
    public Image panelBrillo;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brillo",0f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b,slider.value);
    }
    public void ChangeSlider(float valor)
    {
        sliderValor = valor;
        PlayerPrefs.SetFloat("brillo", sliderValor);
        panelBrillo.color=new Color(panelBrillo.color.r,panelBrillo.color.g,panelBrillo.color.b, slider.value);
    }
}
