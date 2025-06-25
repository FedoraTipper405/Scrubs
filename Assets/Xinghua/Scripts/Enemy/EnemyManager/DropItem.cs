using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
   private int  healValue;
    [SerializeField]private float healPercent = 0.1f;
    private void Start()
    {
       
        PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
        healValue = (int)(playerHealth.maxPlayerHealth * healPercent);
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerHealth>() != null)
        {

            Destroy(gameObject);

            //Health the player
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GainHealth(healValue);
            }
        }
    }
}
