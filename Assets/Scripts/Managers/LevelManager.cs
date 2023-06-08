using System.Collections.Generic;

public class LevelManager : MonoManager<LevelManager>
{
    public int fixedLevelStart = -1;
    public bool isFinishLevel = false;
    public int level;
    public LevelObject[] levelObjects;

    private Dictionary<int, LevelObject> levelObjectDict = new Dictionary<int, LevelObject>();

    public override void Create()
    {
        level = fixedLevelStart == -1 ? SaveLoadManager.instance.GetLevel : fixedLevelStart;
        GetLevel().SetLevel();
    }

    public LevelObject GetLevel()
    {
        if (!levelObjectDict.ContainsKey(level))
        {
            if (levelObjects.Length <= level - 1)
            {
                level = level - 1;
                return GetLevel();
            }
            levelObjectDict.Add(level, Instantiate(levelObjects[level - 1]));
        }
        return levelObjectDict[level];
    }
    public void FinishLevel()
    {
        isFinishLevel = true;

        FinishPanel.Instance.Open();

        AudioManager.Instance.PlayWin();
        PlayerManager.Instance.EndLevel();
        SaveLoadManager.Instance.SaveLevel();
    }
    public void NextLevel()
    {
        GetLevel().Close();
        level++;
        GetLevel().SetLevel();

        LeanTween.delayedCall(.1f, () =>
        {
            isFinishLevel = false;
        });

        LevelProgresPanel.Instance.Close();
        SplashPanel.Instance.Open();

        AudioManager.Instance.StopWin();
    }
    public void ResetLevel()
    {
        Destroy(GetLevel().gameObject);
        levelObjectDict.Remove(level);
        GetLevel().SetLevel();

        PlayerManager.Instance.playerObject.isDead = false;
        SplashPanel.Instance.Open();
    }
}