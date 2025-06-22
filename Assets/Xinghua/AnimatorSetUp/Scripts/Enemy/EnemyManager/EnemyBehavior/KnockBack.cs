using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    private float originalDrag;
    [SerializeField] private float linearDampingApplied;
    [SerializeField] private float delayFreezeAll = 0.2f;
    public UnityEvent OnBegin, OnDone;
    EnemyBaseController enemyBaseController;
    EnemyAI enemyAI;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyBaseController = GetComponent<EnemyBaseController>();
        enemyAI = GetComponentInParent<EnemyAI>();

    }
    private void Start()
    {
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnEnable()
    {
        enemyBaseController.OnKnockBack += PlayKnockBackFeedBack;
    }
    private void OnDisable()
    {
        enemyBaseController.OnKnockBack -= PlayKnockBackFeedBack;
    }

    public void PlayKnockBackFeedBack(GameObject sender,float value)
    {
        StopAllCoroutines();
        Vector2 dir = (transform.position - sender.transform.position);
        dir.y = 0f;
     
        dir = dir.normalized;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        float strength = enemyBaseController.enemyData.knockBackStrength;
        rb.AddForce(dir * strength * value, ForceMode2D.Impulse);
        originalDrag = rb.linearDamping;
        rb.linearDamping = linearDampingApplied;
        StartCoroutine(FreezPosition());
    }

    private IEnumerator FreezPosition()
    {
        yield return new WaitForSeconds(delayFreezeAll);
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
    }
}
