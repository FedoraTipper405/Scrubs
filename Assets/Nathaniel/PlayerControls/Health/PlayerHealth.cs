using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
   // [SerializeField] float maxPlayerHealth;
    public float maxPlayerHealth;//xh change this for access in dropItem
    float currentPlayerHealth;

    public float FullSpecial;
    public float currentSpecial;

    [SerializeField]GameObject healthBlackBar;

    [SerializeField] GameObject specialBlackBar;
    [SerializeField] GameObject fullBlackSpecial;

    [SerializeField] SOPlayerStats stats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
        currentSpecial = 0;
        HealthIncrease();   
    }
    public void HealthIncrease()
    {
        maxPlayerHealth = maxPlayerHealth * (stats.vitalityMultPerLevel * stats.vitalityLevel - stats.vitalityMultPerLevel + 1);
        currentPlayerHealth = maxPlayerHealth;
    }
    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        currentPlayerHealth -= damage;
        healthBlackBar.transform.localScale = new Vector3(1 - ( currentPlayerHealth / maxPlayerHealth),healthBlackBar.transform.localScale.y,healthBlackBar.transform.localScale.z);
        if(currentPlayerHealth < 0)
        {
            //  Debug.Log("Dead");

            SceneManager.LoadScene("MainMenu");
        }
    }
    public void GainSpecial(float specialVal)
    {
        currentSpecial += specialVal * (stats.expertiseMultPerLevel  * stats.expertiseLevel - stats.expertiseMultPerLevel + 1);
        if(currentSpecial > FullSpecial)
        {
            currentSpecial = FullSpecial;
        }
        specialBlackBar.transform.localScale = new Vector3(1-currentSpecial/FullSpecial, specialBlackBar.transform.localScale.y, specialBlackBar.transform.localScale.z);
        if (currentSpecial == 0)
        {
            fullBlackSpecial.SetActive(true);
        }
        else
        {
            fullBlackSpecial.SetActive(false);
        }
    }
    public void UsedSpecial()
    {
        currentSpecial = 0;
        specialBlackBar.transform.localScale = new Vector3(1-currentSpecial / FullSpecial, specialBlackBar.transform.localScale.y, specialBlackBar.transform.localScale.z);
        if(currentSpecial == 0)
        {
            fullBlackSpecial.SetActive(true);
        }
    }
    public void GainHealth(float healthVal)
    {
        currentPlayerHealth += healthVal;
        healthBlackBar.transform.localScale = new Vector3(1 - (currentPlayerHealth / maxPlayerHealth), healthBlackBar.transform.localScale.y, healthBlackBar.transform.localScale.z);
        if (currentPlayerHealth >  maxPlayerHealth)
        {
            currentPlayerHealth=maxPlayerHealth;
        }
    }
}
