using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform playerTransform;
    [SerializeField] float cameraFollowSpeed;
    [SerializeField] bool canMove;
    [SerializeField] GameObject Enemyholder;
    float timeFrozen = 0;
    void Start()
    {
        
    }
    //should be called by event when combat starts
    public void StartEncounter()
    {
        canMove = false;
    }
    //called by event when combat is done
    public void StopEncounter()
    {
        canMove=true;
    }
    // Update is called once per frame
    void Update()
    {
        //detects if the player is further right than the camera, and if its allowed to move
        if(playerTransform.position.x > transform.position.x && canMove)
        {
            transform.position += new Vector3(cameraFollowSpeed * Time.deltaTime,0,0);
        }

        if(canMove == false)
        {
            timeFrozen += Time.deltaTime;
            if(timeFrozen > 10 && Enemyholder.transform.childCount == 0) { }
            {
                canMove = true;
                timeFrozen = 0;
            }
        }
    }
}
