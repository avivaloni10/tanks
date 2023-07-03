using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOpener : MonoBehaviour
{
    public string menuSceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenuScene();
        }
    }

    void OpenMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}