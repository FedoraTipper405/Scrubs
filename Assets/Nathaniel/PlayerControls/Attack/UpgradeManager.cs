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
        }
    }
    public void UpStrength()
    {
        if(STRPrice <= gameManager.moneyCount){
        stats.strengthLevel++;
        gameManager.moneyCount-= STRPrice;
            gameManager.UpdateMoneyText();
        }
    }
    public void UpExpertise()
    {
        if(ExpPrice <= gameManager.moneyCount){
        stats.expertiseLevel++;
        gameManager.moneyCount-=ExpPrice;
            gameManager.UpdateMoneyText();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
