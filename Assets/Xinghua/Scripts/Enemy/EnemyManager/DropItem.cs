using UnityEngine;

public class DropItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Destroy(gameObject);
            //add the money to player pocket
        }
    }
}
