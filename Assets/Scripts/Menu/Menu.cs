using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {

        SceneManager.LoadSceneAsync("PlayerScene");
    }

    public void StartForge()
    {
        SceneManager.LoadSceneAsync("NumberForge");
    }
    public void PointerEnter()
    {

        transform.localScale = transform.localScale * 1.1f; ;
        Debug.Log("Pointer Enter");
    }
    public void PointerExit()
    {
        transform.localScale = transform.localScale / 1.1f; ;
        Debug.Log("Pointer Exit");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartTutorial()
    {
        SceneManager.LoadSceneAsync("TutorialScene");
    }
    public void StartTutorialForge()
    {
        SceneManager.LoadSceneAsync("TutorialForge");
    }
}

