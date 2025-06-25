using UnityEngine;

public class UpgradeStation : MonoBehaviour
{
    [SerializeField] UpgradeMenuHandler handler;
    [SerializeField] GameObject promptText;
     // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "operator")
        {
            handler.canUpgrade = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "operator")
        {
           promptText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "operator")
        {
            handler.canUpgrade = false;
            promptText.SetActive(false);
        }
    }
}
