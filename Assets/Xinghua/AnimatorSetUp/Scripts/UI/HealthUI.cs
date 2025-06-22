using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]private Image hpBar;
    private void Start()
    {
        hpBar.fillAmount = 1f;
        UpdateHealthUI(1,1);
        gameObject.SetActive(true);
    }

    public void UpdateHealthUI(float current, float max)
    {
        hpBar.fillAmount = Mathf.Clamp(current / max, 0, 1);
    }

}