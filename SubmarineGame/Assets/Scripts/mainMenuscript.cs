using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuscript : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("finalScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
