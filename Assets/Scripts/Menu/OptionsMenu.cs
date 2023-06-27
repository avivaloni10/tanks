using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public int MainMenuScene = 0;

    public void BlueTankButton() {
        SharedVariables.isDefaultTank = true;
        SceneManager.LoadScene(this.MainMenuScene);
    }

    public void RedTankButton() {
        SharedVariables.isDefaultTank = false;
        SceneManager.LoadScene(this.MainMenuScene);
    }

    public void Back() {
        SceneManager.LoadScene(this.MainMenuScene);
    }
}
