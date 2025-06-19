using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject dropTextCanves;
    [SerializeField] TMP_Text dropText;
    public UnityEvent PlayerWin;

    private void Start()
    {
        winMenu.SetActive(false);
        DisplayDropText(0);
    }

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

    private void Update()
    {
        if (GameManager.Instance.isWin == true)
        {
            ShowWinMenu();
        }
    }
    private void ShowWinMenu()
    {
        if (winMenu != null)
        {
            winMenu.SetActive(true);
        }
    }

    public void DisplayDropText(float value)
    {
      
        if (dropText != null)
        {
            dropTextCanves.SetActive(true);

            dropText.text = "value";
            Debug.Log("display drop item");
        }
        else
        {
            Debug.Log("dropText is null");
        }

    }
}
