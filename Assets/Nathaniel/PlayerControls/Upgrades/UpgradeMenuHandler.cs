using UnityEngine;

public class UpgradeMenuHandler : MonoBehaviour
{
   public bool canUpgrade = false;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAttacks playerAttacks;
    [SerializeField] GameObject upgradeCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void OpenUpgradeMenu()
    {
        upgradeCanvas.SetActive(true);
        playerMovement.canMove = false;
        playerAttacks.canInput = false;
    }
    public void CloseUpgradeMenu()
    {
        upgradeCanvas.SetActive(false);
        playerMovement.canMove = true;
        playerAttacks.canInput = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (canUpgrade && Input.GetKeyDown(KeyCode.E))
        {
            OpenUpgradeMenu();
        }
    }
}
