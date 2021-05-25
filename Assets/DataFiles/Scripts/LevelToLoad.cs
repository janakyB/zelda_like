using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToLoad : MonoBehaviour 
{
	public string LevelToLoad1;

    void OnTriggerEnter2D(Collider2D col)
    {
		if(col.gameObject.tag == "Player"){
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().saveGame();
			SceneManager.LoadScene(LevelToLoad1);
		}
	}

	
     public void Play()
    {
        //Permet de cliquer sur le bouton 
        SceneManager.LoadScene(LevelToLoad1);
    }

    public void Quit()
    {
        Application.Quit();
    }

   public void Menu()
   {
       SceneManager.LoadScene("MainMenu");
   }
}
