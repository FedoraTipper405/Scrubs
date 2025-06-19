using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SOUnlockedCOmbos unlockedCombos;
    [SerializeField] SOPlayerStats stats;

    void Start()
    {
        
    }
    public void UnlockCombo(int index)
    {
        if (unlockedCombos.hasComboArray[index] == false)
        {
            unlockedCombos.hasComboArray[index] = true;
        }
    }
    public void UpVitality()
    {
        stats.vitalityLevel++;
    }
    public void UpStrength()
    {
        stats.strengthLevel++;
    }
    public void UpExpertise()
    {
        stats.expertiseLevel++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
