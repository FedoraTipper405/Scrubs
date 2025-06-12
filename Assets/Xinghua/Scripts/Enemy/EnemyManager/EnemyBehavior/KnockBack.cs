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

        //play the anima ：get knock back
       // Debug.Log("play knock back sender:" + sender.name + value);
        Vector2 dir = (transform.position - sender.transform.position);
        dir.y = 0f;
        dir = dir.normalized;
        rb.AddForce(dir * strength * value, ForceMode2D.Impulse);
      
        originalDrag = rb.linearDamping;
        rb.linearDamping = 8f;
        StartCoroutine(FreezPosition());
    }
    private IEnumerator FreezPosition()
    {
        yield return new WaitForSeconds(0.4f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(Reset());

    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //reset the animation
    }

}
