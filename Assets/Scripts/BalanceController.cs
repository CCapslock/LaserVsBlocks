public class BalanceController 
{
    public BalancePreset CurrentBalancePreset;
    public MainGameController GameController;
    private int _maxScore = 0;
    private int _currentLvlNum = 0;

    public void SetCorrectBalance(int CurrentScore)
    {
        if(CurrentScore > _maxScore)
        {
            _currentLvlNum++;
            _maxScore = CurrentBalancePreset.Lvls[_currentLvlNum].MaxScore;
            SetCorrectBalance(CurrentScore);
        }
        else
        {
            GameController.SetCorrectBalance();
        }
    }
    public FigureWithWeightForSpawner[] GetFigures()
    {
        return CurrentBalancePreset.Lvls[_currentLvlNum].Figures;
    }
    public float GetMaxHp()
    {
        return CurrentBalancePreset.Lvls[_currentLvlNum].MaxHp;
    }
    public float GetMinHp()
    {
        return CurrentBalancePreset.Lvls[_currentLvlNum].MinHp;
    }
}
