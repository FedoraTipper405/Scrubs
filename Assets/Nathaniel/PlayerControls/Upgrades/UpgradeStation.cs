using UnityEngine;

public class UpgradeStation : MonoBehaviour
{
    [SerializeField] UpgradeMenuHandler handler;
     // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "operator")
        {
            handler.canUpgrade = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "operator")
        {
            handler.canUpgrade = false;
        }
    }
}
