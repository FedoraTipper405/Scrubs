using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerMovement player;
    public int dropItemCount;
    public int moneyCount;
    public int moneyValue = 0;
    public MoneyData moneyData;

    public bool isWin = false;
    public bool isBossDied = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

       // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dropItemCount = 0;
       if(moneyData.hasPlayed == false)
        {
            moneyData.playerMoney = 0;
            moneyData.hasPlayed = true;
        }
        moneyCount = moneyData.playerMoney;
        Debug.Log("gamemanager  start with money:"+ moneyCount);
        Debug.Log("gamemanager  start with money:" + moneyData.playerMoney);
      //  UpdateMoneyText();
   

    }
    public void ResetPlayerMoneyWhenRestart()
    {
        if(SceneManager.GetActiveScene().name =="XHGym")
        {
            moneyData.playerMoney = 0;
        }
       
    }
    //public void UpdateDropItem(int value)
    //{
    //    dropItemCount+= value;
    //    Debug.Log("game manager UpdateDropItem:" + dropItemCount);
    //}

    public void UpdateMoneyText()
    {
        Debug.Log("gamemanager  UpdateMoneyText:" + moneyCount);
        moneyData.playerMoney = moneyCount;
        MenuManager.Instance.DisplayMoneytText(moneyCount.ToString());

    }
    public void UpdatePlayerMoney(int value)
    {
        moneyCount += value;
      
        // Debug.Log("moneyCount" + moneyCount);
        MenuManager.Instance.DisplayMoneytText(moneyCount.ToString());
    }
    public void ResetPlayerPosition()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        player.transform.position = Vector3.zero;
        CameraController cam = FindAnyObjectByType<CameraController>();
        cam.transform.position = Vector3.zero;

    }
    public IEnumerator LoadSceneWhenLevelEnd()
    {
        Debug.Log("Load scene");
        yield return new WaitForSeconds(1);
        if (isBossDied && SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadSceneWhenLevelEnd();
            moneyData.playerMoney = moneyCount;
            Debug.Log("money count when level end:" + moneyCount);
        }
        else
        {
            Debug.Log(" scene loader not found");
        };
    }

    public void SetBossDeath(bool isFinalboss)
    {
        if (!isFinalboss)
        {
            isBossDied = true;

            StartCoroutine(LoadSceneWhenLevelEnd());
        }
        else
        {
            EnemyTriggerManager.Instance.HandleEnemyChangeWithCamera(false,true);
        }

    }
}
