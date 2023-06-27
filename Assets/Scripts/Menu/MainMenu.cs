using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int Play1v1Scene = 1;
    public int PlayTrainingScene = 2;
    public int OptionsMenuScene = 3;

    public void Play1v1() {
        SceneManager.LoadScene(this.Play1v1Scene);
    }

    public void PlayTraining() {
        SceneManager.LoadScene(this.PlayTrainingScene);
    }

    public void OpenOptionsMenu() {
        SceneManager.LoadScene(this.OptionsMenuScene);
    }

    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
