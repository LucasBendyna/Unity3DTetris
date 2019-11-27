using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // A fade-out scene transition effect.
    public GameObject fadeOut;

    /*
    * Quit the application.
    */
    public void Quit()
    {
        Application.Quit();
    }

    /*
    * Set the "Game" scene.
    */
    public void StartGame()
    {
        StartCoroutine(LoadAfterDelay("Game"));
    }

    /*
    * Set the "Controls" scene.
    */
    public void Controls()
    {
        StartCoroutine(LoadAfterDelay("Controls"));
    }

    /*
    * Set the "Credits" scene.
    */
    public void Credits()
    {
        StartCoroutine(LoadAfterDelay("Credits"));
    }

    /*
    * Set the "Menu" scene.
    */
    public void Menu()
    {
        StartCoroutine(LoadAfterDelay("Menu"));
    }

    /*
    * Set the scene after a transition animation.
    * 
    * Parameters :
    *   string name - The scene name
    *   
    * Return :
    *   IEnumerator - Allow the use of a coroutine.
    */
    IEnumerator LoadAfterDelay(string name)
    {
        FindObjectOfType<AudioManager>().buttonClicked.Play(0);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(name);
    }
}
