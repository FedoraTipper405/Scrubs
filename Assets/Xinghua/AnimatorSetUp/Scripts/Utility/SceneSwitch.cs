using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null )
        {
            Debug.Log("scene switch");
            if (SceneLoader.Instance == null)
            {

                Debug.Log("SceneLoader null");
                SceneLoader sceneLoader = FindAnyObjectByType<SceneLoader>();
               
            }
            LoadScene();


        }
    }

    public void EndLevel()
    {
        //if the boss died and no enemy left
    }
    private void LoadScene()
    {

        Debug.Log("scene current:" + SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "XHGym")
        {
            Debug.Log("scene switch to 2");
            SceneLoader.Instance.LoadScene("LevelSelectionMenu");
            GameManager.Instance.ResetPlayerPosition();
        }
        else if (SceneManager.GetActiveScene().name == "XHSubway")
        {
            SceneLoader.Instance.LoadScene("MainMenu");
            Debug.Log("scene switch to main menu");

        }
    }
}
