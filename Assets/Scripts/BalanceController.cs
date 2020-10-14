public class BalanceController 
{
    public BalancePreset CurrentBalancePreset;
    public MainGameController GameController;
    public int СurrentLvlNum = 0;
    private int _maxScore = 0;

    public void SetCorrectBalance(int CurrentScore)
    {
        if(CurrentScore > _maxScore)
        {
            СurrentLvlNum++;
            _maxScore = CurrentBalancePreset.Lvls[СurrentLvlNum].MaxScore;
            SetCorrectBalance(CurrentScore);
        }
        else
        {
            GameController.SetCorrectBalance();
        }
    }
    public FigureWithWeightForSpawner[] GetFigures()
    {
        return CurrentBalancePreset.Lvls[СurrentLvlNum].Figures;
    }
    public float GetMaxHp()
    {
        return CurrentBalancePreset.Lvls[СurrentLvlNum].MaxHp;
    }
    public float GetMinHp()
    {
        return CurrentBalancePreset.Lvls[СurrentLvlNum].MinHp;
    }
    public int GetCurrentLvlNum()
    {
        return СurrentLvlNum;
    }
}
