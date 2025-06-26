using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSOs : MonoBehaviour
{
    [SerializeField] SOPlayerStats soStats;
    [SerializeField] SOUnlockedCOmbos soUnComb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "XHGym")
        {
            ResetSO();
        }
    }
    public void ResetSO()
    {
soStats.strengthLevel = 1;
soStats.vitalityLevel = 1;
soStats.expertiseLevel = 1;
        for(int i = 0; i < soUnComb.hasComboArray.Length; i++)
        {
            if(i == 4)
            {
                soUnComb.hasComboArray[i] = true;
            }
            else
            {
                soUnComb.hasComboArray[i] = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
