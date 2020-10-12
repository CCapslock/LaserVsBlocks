using UnityEngine;

public class ArtController 
{
    public ArtPreset[] ArtPresets;
    public SpriteRenderer BackGroundGlowRenderer;
    private Gradient _currentGradient;
    private Color _currentColor;
    private int _currentPresetNum;

    public void SelectPreset()
    {
        int num = Random.Range(0, ArtPresets.Length);
        if (num != PlayerPrefs.GetInt("LastArtPreset"))
        {
            _currentPresetNum = num;
            PlayerPrefs.SetInt("LastArtPreset", num);
        }
        else
        {
            SelectPreset();
        }
    }
    public Gradient GetCurrentGradient()
    {
        _currentGradient = ArtPresets[_currentPresetNum].GradintForBlocks;
        return _currentGradient;
    }
    public Color GetCurrentColor()
    {
        _currentColor = ArtPresets[_currentPresetNum].ColorForBackGround;
        return _currentColor;
    }
}
