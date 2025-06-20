using UnityEngine;

public class UpgradeMenuManager : MonoBehaviour
{
    public GameObject StatsMenu;
    public GameObject ComboMovesMenu;
    public GameObject UltimateMoveMenu;

    private void Start()
    {
        StatsMenu.SetActive(false);
        ComboMovesMenu.SetActive(false);
        UltimateMoveMenu.SetActive(false);
    }

    public void StatsMenuOpen()
    {
        StatsMenu.SetActive(true);
    }
    public void MovesMenuOpen()
    {
        ComboMovesMenu.SetActive(true);
    }
    public void UltimateMenuOpen()
    {
        UltimateMoveMenu.SetActive(true);
    }
    public void StatsMenuClose()
    {
        StatsMenu.SetActive(false);
    }
    public void MovesMenuClose()
    {
        ComboMovesMenu.SetActive(false);
    }
    public void UltimateMenuClose()
    {
        UltimateMoveMenu.SetActive(false);
    }

}
