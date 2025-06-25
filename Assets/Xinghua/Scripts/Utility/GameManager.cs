using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerMovement player;
    public int dropItemCount;
    public int moneyCount;
    public int moneyValue = 0;


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
        moneyCount = 0;
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.DisplayMoneytText("0");
        }

    }
    //public void UpdateDropItem(int value)
    //{
    //    dropItemCount+= value;
    //    Debug.Log("game manager UpdateDropItem:" + dropItemCount);
    //}

    public void UpdateMoneyText()

    {

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
