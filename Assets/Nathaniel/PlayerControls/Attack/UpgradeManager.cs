using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SOUnlockedCOmbos unlockedCombos;
    [SerializeField] SOPlayerStats stats;
    [SerializeField] int[] ComboPrices;
    [SerializeField] int vitPrice;
    [SerializeField] int STRPrice;
    [SerializeField] int ExpPrice;

    [SerializeField] GameObject[] GreyOutOptions;
    void Start()
    {
        //for(int i = 0;  i < GreyOutOptions.Length; ++i)
        //{
        //    if (unlockedCombos.hasComboArray[i])
        //    {
        //        GreyOutOptions[i].SetActive(true);
        //    }
        //    else
        //    {
        //        GreyOutOptions[i].SetActive(false);
        //    }
        //}
    }
    public void UnlockCombo(int index)
    {
        if (unlockedCombos.hasComboArray[index] == false /* && comboPrices[index] <= money*/)
        {
            unlockedCombos.hasComboArray[index] = true;
         //   GreyOutOptions[index].SetActive(true);
            //money-=comboPrices[index];
        }
    }
    public void UpVitality()
    {
        //if ( vitPrive <= money){
        stats.vitalityLevel++;
        //money-= vitPrice;
    //}
    }
    public void UpStrength()
    {
        //if(STRPrice <= money){
        stats.strengthLevel++;
        //money-= STRPrice;
        //}
    }
    public void UpExpertise()
    {
        //if(ExpPrice <= money){
        stats.expertiseLevel++;
        //movey-=ExpPrice;
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
