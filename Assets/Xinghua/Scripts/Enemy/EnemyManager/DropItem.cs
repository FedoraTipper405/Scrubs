using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private float itemCount;

   private void Start()
   {
       itemCount = 0f;
   }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerHealth>() != null)
        {
            itemCount++;
            MenuManager.Instance.DisplayDropText(itemCount);

            Destroy(gameObject);
            //Health the player
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GainHealth(10);
            }
        }
    }
}
