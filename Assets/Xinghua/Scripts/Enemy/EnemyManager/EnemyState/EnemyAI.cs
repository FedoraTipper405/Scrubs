using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Pacing,
        MovingToPlayer,
        Attack,
    }
    public EnemyState currentState;
    public void SetEnemyState(EnemyState newState)
    {
        currentState = newState;
    }





}
