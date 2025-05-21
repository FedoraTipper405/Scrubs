using UnityEngine;
using UnityEngine.Rendering;

public class EndLevel : MonoBehaviour
{
    [SerializeField] GameObject winMenu;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null )
        {
         
            if (winMenu != null)
            {
 
                winMenu.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("winMenu null");
            }
           
        }
    }
}
