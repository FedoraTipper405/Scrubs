using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    private float originalDrag;
    [SerializeField] private float linearDampingApplied;
    [SerializeField] private float strength = 1.0f;
    [SerializeField] private float delay = 0.2f;
    public UnityEvent OnBegin, OnDone;
    EnemyBaseController enemyBaseController;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyBaseController = GetComponent<EnemyBaseController>();
    }
    private void Start()
    {
        rb.gravityScale = 0;
    }
    private void OnEnable()
    {
        enemyBaseController.OnKnockBack += PlayKnockBackFeedBack;
    }
    private void OnDisable()
    {
        enemyBaseController.OnKnockBack -= PlayKnockBackFeedBack;
    }

    public void PlayKnockBackFeedBack(GameObject obj,float value)
    {
        StopAllCoroutines();
     
        //play the anima ：get hit

        Vector2 dir = (transform.position - obj.transform.position);
        dir.y = 0f;
        dir = dir.normalized;
        rb.AddForce(dir * strength * value, ForceMode2D.Impulse);
        originalDrag = rb.linearDamping;
        rb.linearDamping = 8f;
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector2.zero;
        //reset the animation

    }

}
