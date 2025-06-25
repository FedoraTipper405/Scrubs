using UnityEngine;

[CreateAssetMenu(fileName = "MoneyData", menuName = "Money/MoneyData")]
public class MoneyData : ScriptableObject
{
    public int playerMoney;
    public bool hasPlayed;
}
