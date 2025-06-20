using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject dropTextCanves;
    [SerializeField] GameObject goCanves;
    [SerializeField] TMP_Text moneyText;
    public UnityEvent PlayerWin;

    private void Start()
    {
        winMenu.SetActive(false);
        goCanves.SetActive(false);
    }

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

    public void ShowWinMenu()
    {
        if (winMenu != null)
        {
            winMenu.SetActive(true);
        }
    }

    public void DisplayMoneytText(string value)
    {
        if (moneyText != null)
        {
            dropTextCanves.SetActive(true);
            moneyText.text = value;
        }
        else
        {
            Debug.Log("dropText is null");
        }
    }
    public void ShowGo()  
    {
        if(goCanves != null)
        {
            goCanves.SetActive(true);
        }
        Invoke("HideGo", 1f);
    }
    public void HideGo()
    {
        if (goCanves != null)
        {
            goCanves.SetActive(false);
        }
    }

}
