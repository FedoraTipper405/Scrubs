using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
  
    public bool isWin = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void CheckLevelState()
    {

        //check the boss and enemy amout ,now just check boss
        isWin = true;
    }
}
