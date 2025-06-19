using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerHealth>() != null)
        {
            //GameManager.Instance.UpdateDropItem(1);

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
