using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AssesmentManager : MonoBehaviour
{
    public Slider stressLevelSlider;
    public TextMeshProUGUI valueText;

    void Start()
    {
        // Initialize the slider value and text
        stressLevelSlider.value = 0;
        UpdateValueText(stressLevelSlider.value);

        // Add listener to update text when slider value changes
        stressLevelSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    void ValueChangeCheck()
    {
        // Update the text with the current slider value
        UpdateValueText(stressLevelSlider.value);
    }

    void UpdateValueText(float value)
    {
        // Format and update the value text
        valueText.text = value.ToString("0");
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Hospital");
    }
}