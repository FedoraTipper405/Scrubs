using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Pacing,
        Attack,
    }
    public EnemyState currentState;
    public void SetEnemyState(EnemyState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
    }

}
