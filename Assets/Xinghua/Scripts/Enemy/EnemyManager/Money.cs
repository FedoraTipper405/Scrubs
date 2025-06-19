using UnityEngine;

public class Money : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.gameObject.GetComponentInChildren<PlayerHealth>() != null)
        {
            Debug.Log("player get money");
    
            GameManager.Instance.UpdatePlayerMoney();

            Destroy(gameObject);

          
        }
    }
}
