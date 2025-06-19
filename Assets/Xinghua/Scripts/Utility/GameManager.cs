using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerMovement player;
    public int dropItemCount;
    public int moneyCount;
    public int moneyValue = 0;

    public bool isWin = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dropItemCount = 0;
        moneyCount = 0;
        if(MenuManager.Instance != null)
        {
            MenuManager.Instance.DisplayMoneytText("0");
        }
       
    }
    //public void UpdateDropItem(int value)
    //{
    //    dropItemCount+= value;
    //    Debug.Log("game manager UpdateDropItem:" + dropItemCount);
    //}

    public void UpdatePlayerMoney()
    {
        int random = UnityEngine.Random.Range(1, 100);

        if(random >=1 && random < 50)
        {
            moneyValue = 1;
        }
        else if(random >=50 && random < 90)
        {
            moneyValue = 5;
        }
        else
        {
            moneyValue = 10;
        }

        Debug.Log("random money:" + moneyValue);
        moneyCount += moneyValue;
        Debug.Log("game manager add money:" + moneyValue);
        MenuManager.Instance.DisplayMoneytText(moneyCount.ToString());
    }
    public void ResetPlayerPosition()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        player.transform.position = Vector3.zero;
        CameraController cam = FindAnyObjectByType<CameraController>();
        cam.transform.position = Vector3.zero;

    }
    public void CheckLevelState()
    {

        //check the boss and enemy amout ,now just check boss
        isWin = true;
    }
}
