using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public float healValue;
    private void Start()
    {
        healValue = 10f;
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
