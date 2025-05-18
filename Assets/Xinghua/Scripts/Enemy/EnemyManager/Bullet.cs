using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float speed = 10f;
    private Vector3 moveDirection;


    public void Shoot(Vector3 direction)
    {
        direction.z = 0;
        moveDirection = direction;
      
        Destroy(gameObject,3f);
    }
    private void FixedUpdate()
    {
        transform.position += moveDirection * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
           // Debug.Log("bullet shoot the player");
            //take damage of the player
            Destroy(gameObject);
        }
    }
}
