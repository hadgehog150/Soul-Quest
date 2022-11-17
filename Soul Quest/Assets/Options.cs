using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider slider;

    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;

    public Toggle toggle4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void volume()
    {
        AudioListener.volume = slider.value;
    }

    public void graphic() {
        if (toggle1.isOn == true)
        {
            QualitySettings.currentLevel = QualityLevel.Fast;
            toggle1.isOn = true;
        }
        if (toggle2.isOn == true)
        {
            QualitySettings.currentLevel = QualityLevel.Simple;
            toggle2.isOn = true;
        }
        if (toggle3.isOn == true)
        {
            toggle3.isOn = true;
        }
    }

    public void filter()
    {
        if (toggle4.isOn == true)
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
            toggle4.isOn = true;
        }
        else {
            toggle4.isOn = false;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
    }
        } 
}
