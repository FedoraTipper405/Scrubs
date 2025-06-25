
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string name)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(name);
    }

    public void LoadSceneWhenLevelEnd()
    {
        if (SceneManager.GetActiveScene().name == "XHGym")
        {
            SceneLoader.Instance.LoadScene("LevelSelectionMenu");
            
            GameManager.Instance.ResetPlayerPosition();
        }
        else if (SceneManager.GetActiveScene().name == "XHSubway")
        {
            SceneLoader.Instance.LoadScene("MainMenu");
        }
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}