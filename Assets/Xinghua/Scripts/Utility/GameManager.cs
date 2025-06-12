using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerMovement player;
    public bool isWin = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }


    public void ResetPlayerPosition()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        player.transform.position = Vector3.zero;
        CameraController cam = FindAnyObjectByType<CameraController>();
        cam.transform.position = Vector3.zero;

    }
    public void CheckLevelState()
    {

        //check the boss and enemy amout ,now just check boss
        isWin = true;
    }
}
