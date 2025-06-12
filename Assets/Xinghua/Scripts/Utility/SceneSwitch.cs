using System;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("SceneSwitch start");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("load level 2");
        if (collision.GetComponent<PlayerMovement>() != null )
        {
            if(SceneLoader.Instance != null)
            {
               
                SceneLoader.Instance.LoadScene("LevelSelectionMenu");
                GameManager.Instance.ResetPlayerPosition();
            }
            
        }
    }
}
