using UnityEditor;
using UnityEngine;

public class BaseBoss : MonoBehaviour
{
    [SerializeField]protected float speed;
    [SerializeField] protected float stopDistance = 1f;
    protected float currentHealth;
    [SerializeField] protected float maxHealth;
    public virtual void TakeDamage(float amount)
    {

    }
}
