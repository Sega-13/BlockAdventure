using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class SquareTextureData : ScriptableObject
{
    [System.Serializable]
    public class TextureData
    {
        public Sprite texture;
        public Config.SquareColor squareColor;
    }
    public int thresholdVal = 10;
    public const int StartThresholdVal = 10;
    public List<TextureData> activeSquareTextures;

    public Config.SquareColor currentColor;
    private Config.SquareColor nextColor;

    public int GetCurrentColorIndex()
    {
        var currentIndex = 0;
        for(int index =0; index<activeSquareTextures.Count; index++)
        {
            if (activeSquareTextures[index].squareColor == currentColor)
            {
                currentIndex = index;
            }
        }
        return currentIndex;

    }
    public void UpdateColor(int currentScore)
    {
        currentColor = nextColor;
        var currentColorIndex = GetCurrentColorIndex();
        if(currentColorIndex == activeSquareTextures.Count-1) 
        {
            nextColor = activeSquareTextures[0].squareColor;
        }
        else
        {
            nextColor = activeSquareTextures[currentColorIndex+1].squareColor;
        }
        thresholdVal = StartThresholdVal + currentScore;
    }
    public void SetStartColor()
    {
        thresholdVal = StartThresholdVal;
        if (activeSquareTextures != null && activeSquareTextures.Count >= 2)
        {
            currentColor = activeSquareTextures[0].squareColor;
            nextColor = activeSquareTextures[1].squareColor;
        }
    }
    
    private void OnEnable()
    {
        SetStartColor();
    }
}
