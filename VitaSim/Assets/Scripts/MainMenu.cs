using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Assessment");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
