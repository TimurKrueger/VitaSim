using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AssessmentManager : MonoBehaviour
{
    public List<GameObject> questionPanels;
    private int currentQuestionIndex = 0;
    private List<float> answers;

    public Slider stressLevelSlider;
    public TextMeshProUGUI valueText;

    void Start()
    {
        stressLevelSlider.value = 0;
        UpdateValueText(stressLevelSlider.value);
        stressLevelSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        answers = new List<float>(new float[questionPanels.Count]);
        ShowQuestion(currentQuestionIndex);
    }

    void ValueChangeCheck()
    {
        UpdateValueText(stressLevelSlider.value);
    }

    void UpdateValueText(float value)
    {
        valueText.text = value.ToString("0");
    }

    public void NextQuestion()
    {
        // Save the answer
        SaveAnswer(currentQuestionIndex);

        // Hide current question
        questionPanels[currentQuestionIndex].SetActive(false);

        // Increment question index
        currentQuestionIndex++;

        // Check if there are more questions
        if (currentQuestionIndex < questionPanels.Count)
        {
            // Show the next question
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            // All questions answered, proceed to the next scene or process results
            Debug.Log("Questionnaire completed");
            foreach (var answer in answers)
            {
                Debug.Log("Answer: " + answer);
            }
            SceneManager.LoadScene("Hospital");
        }
    }

    void ShowQuestion(int index)
    {
        questionPanels[index].SetActive(true);
    }

    void SaveAnswer(int index)
    {
        // Find the slider in the current question panel and save its value
        Slider slider = questionPanels[index].GetComponentInChildren<Slider>();
        if (slider != null)
        {
            answers[index] = slider.value;
        }
    }
}
