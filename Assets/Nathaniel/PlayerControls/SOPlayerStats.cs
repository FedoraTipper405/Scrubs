using UnityEngine;

[CreateAssetMenu(fileName = "SOPlayerStats", menuName = "Scriptable Objects/SOPlayerStats")]
public class SOPlayerStats : ScriptableObject
{
    public int strengthLevel;
    public float strengthMultPerLevel;

    public int vitalityLevel;
    public float vitalityMultPerLevel;

    public int expertiseLevel;
    public float expertiseMultPerLevel;
}
