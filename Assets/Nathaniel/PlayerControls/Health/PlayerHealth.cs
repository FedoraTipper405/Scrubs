using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxPlayerHealth;
    float currentPlayerHealth;

    public float FullSpecial;
    public float currentSpecial;

    [SerializeField]GameObject healthBlackBar;

    [SerializeField] GameObject specialBlackBar;
    [SerializeField] GameObject fullBlackSpecial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
        currentSpecial = 0;
       
    }
    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GainSpecial(10);
        }
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
        currentSpecial += specialVal;
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
