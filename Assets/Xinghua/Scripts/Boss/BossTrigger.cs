using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class BossTrigger : MonoBehaviour
{
    //spawn final boss; if all boss and enemy clear end level to selecl level menu
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnXOffset = 6f;

    private bool isSpawned = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyTriggerManager.Instance.enemiesKilled = 0;
        if (other.GetComponent<PlayerMovement>() != null && !isSpawned)
        {
            
            GameObject boss = Instantiate(bossPrefab, spawnPoint.position + new Vector3(spawnXOffset, 0, 0), Quaternion.identity);
            EnemyTriggerManager.Instance.taskEnemieCount++;
            EnemyTriggerManager.Instance.HandleEnemyChangeWithCamera(true,false);
            
            isSpawned = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isSpawned = true;
    }
}
