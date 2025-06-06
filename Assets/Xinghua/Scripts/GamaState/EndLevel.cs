using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class EndLevel : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;
    private bool isSpawned = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null && !isSpawned )
        {

            Instantiate(bossPrefab,transform.position,Quaternion.identity);
            isSpawned = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
       isSpawned = true;
    }
}
