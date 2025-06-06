using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject winMenu;
    public UnityEvent PlayerWin;

    private void Start()
    {
        winMenu.SetActive(false);
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
}
