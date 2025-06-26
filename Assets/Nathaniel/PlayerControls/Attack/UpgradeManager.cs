using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SOUnlockedCOmbos unlockedCombos;
    [SerializeField] SOPlayerStats stats;
    [SerializeField] int[] comboPrices;
    [SerializeField] int vitPrice;
    [SerializeField] int STRPrice;
    [SerializeField] int ExpPrice;

    [SerializeField] TMP_Text strText;
    [SerializeField] TMP_Text vitText;
    [SerializeField] TMP_Text expText;

    [SerializeField] GameObject[] GreyOutOptions;
    [SerializeField] GameManager gameManager;
    void Start()
    {
        GrayOut();
    }
    public void GrayOut()
    {
        for (int i = 0; i < GreyOutOptions.Length; ++i)
        {
            if (unlockedCombos.hasComboArray[i])
            {
                GreyOutOptions[i].SetActive(true);
            }
            else
            {
                GreyOutOptions[i].SetActive(false);
            }
        }
    }
    public void UpdateLevelsText()
    {
        strText.SetText(stats.strengthLevel.ToString());
        vitText.SetText(stats.vitalityLevel.ToString());
        expText.SetText(stats.expertiseLevel.ToString());

    }
    public void UnlockCombo(int index)
    {
        if (unlockedCombos.hasComboArray[index] == false  && comboPrices[index] <= gameManager.moneyCount)
        {
            unlockedCombos.hasComboArray[index] = true;
            gameManager.moneyCount -= comboPrices[index];
            gameManager.UpdateMoneyText();
        //    GreyOutOptions[index].SetActive(true);
            GrayOut();
        }
    }
    public void UpVitality()
    {
        if ( vitPrice <= gameManager.moneyCount){
        stats.vitalityLevel++;
        gameManager.moneyCount-= vitPrice;
            gameManager.UpdateMoneyText();
            vitText.SetText(stats.vitalityLevel.ToString());
        }
    }
    public void UpStrength()
    {
        if(STRPrice <= gameManager.moneyCount){
        stats.strengthLevel++;
        gameManager.moneyCount-= STRPrice;
            gameManager.UpdateMoneyText();
            strText.SetText(stats.strengthLevel.ToString());
        }
    }
    public void UpExpertise()
    {
        if(ExpPrice <= gameManager.moneyCount){
        stats.expertiseLevel++;
        gameManager.moneyCount-=ExpPrice;
            gameManager.UpdateMoneyText();
            expText.SetText(stats.expertiseLevel.ToString());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
