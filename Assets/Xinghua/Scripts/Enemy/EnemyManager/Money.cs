using UnityEngine;

public class Money : MonoBehaviour
{
    public GameObject moneyValue50;
    public GameObject moneyValue40;
    public GameObject moneyValue10;
  
    public int moneyValue50Percent;
    public int moneyValue40Percent;
    public int moneyValue10Percent;
    private SpriteRenderer spriteRenderer;
    private int moneyValue;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetRandomMoney();
     
    }
    private void GetRandomMoney()
    {
      
            int random = UnityEngine.Random.Range(1, 100);

            if (random >= 1 && random < 50)
            {
                moneyValue = moneyValue10Percent;
                SetSpawnCoinRender(moneyValue10);
            }
            else if (random >= 50 && random < 90)
            {
                moneyValue = moneyValue40Percent;
                SetSpawnCoinRender(moneyValue40);
            }
            else
            {
                moneyValue = moneyValue50Percent;
                SetSpawnCoinRender(moneyValue50);
            }
       

    }
    private void SetSpawnCoinRender(GameObject obj)
    {
        Sprite spriteToUse = obj.GetComponent<SpriteRenderer>().sprite;
        spriteRenderer.sprite = spriteToUse;
    }
  
    private void OnTriggerEnter2D(Collider2D other)
    {
         GameManager.Instance.UpdatePlayerMoney(moneyValue);
        if (other.gameObject.GetComponentInChildren<PlayerHealth>() != null)
        {
            SoundManager.Instance.PlaySFX("CoinCollect",1f);
            Destroy(gameObject);
        }
    }
}
