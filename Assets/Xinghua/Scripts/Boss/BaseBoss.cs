using System;
using System.Collections;
using UnityEngine;

public class BaseBoss : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float stopDistance = 1f;
    public float currentHealth;
    [SerializeField] protected float maxHealth;
    protected SpriteRenderer bossRenderer;
    public float damage;
    public event Action OnDeath;
    protected bool isFinalBoss =false;
   
    public virtual void TakeDamage(float amount)
    {

    }


    protected virtual void FlipTowardsPlayer()
    {

    }
    
    protected void Die()
    {

        if (this.gameObject.GetComponent<Brute>() != null)
        {
            SoundManager.Instance.PlaySFX("BruteDeathSound", 1f);
        }
        else if(this.gameObject.GetComponent<FlyBoss>() != null)
        {
            SoundManager.Instance.PlaySFX("BruteSlamAttackSound", 1f);
        }
        Destroy(gameObject);
        //sound
    
      
      
        GameManager.Instance.LoadSceneWhenLevelEnd();
        OnDeath?.Invoke();

    }
    protected void GetHitFlash()
    {
        bossRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (bossRenderer != null)
        {
            bossRenderer.color = new Color(1f, 0f, 0f, 1f);//red
            StartCoroutine(EndFlash());
        }
    }

    protected IEnumerator EndFlash()
    {
        yield return new WaitForSeconds(0.25f);

        bossRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
    protected void OnDeathAnimEnd()
    {

        Destroy(gameObject);
        if (GameManager.Instance!= null)
        {
            GameManager.Instance.SetBossDeath(this.isFinalBoss);
        }
        else
        {
            Debug.Log("game manager is null");
        }
       

    }
}
