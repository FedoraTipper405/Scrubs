using UnityEngine;

public class Attack : MonoBehaviour
{
    private TargetTest player;
    private float faceToFaceDistance = 0.2f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject.GetComponent<TargetTest>();
        if (player != null)
        {
            if (Mathf.Abs(player.transform.position.y - transform.position.y) <= 0.2f)
            {
                player.TakeDamage();
            }
        }
    }
}
