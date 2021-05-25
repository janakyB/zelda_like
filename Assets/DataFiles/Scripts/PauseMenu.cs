using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject ui;

   

    public string menuSceneName = "MainMenu";

	void Update () {
     // Appuyer sur  la touche échap ou p ppour mettre en pause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }

	}

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }

// permet de cliquer sur le bouton soit pour allé dans le menu de jeu ou bien le jeu
    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Toggle();
        SceneManager.LoadScene(menuSceneName);
    }

}
