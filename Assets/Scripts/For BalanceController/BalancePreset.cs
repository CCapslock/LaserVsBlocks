using UnityEngine;

[CreateAssetMenu(fileName = "BalanceController")]

public class BalancePreset: ScriptableObject
{
    public int DifferenceBetweenLvls;
    public float GlobalMaxHp;
    public LvlControllerForSpawner[] Lvls;
}
