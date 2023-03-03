using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public int numberOfLevels = 5;
    public string levelSceneNamePrefix = "Level";
    public Button[] levelButtons;

    private void Start()
    {       
        for (int i = 1; i < numberOfLevels; i++)
        {
            if (!IsLevelUnlocked(i + 1))
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void SelectLevel(int levelNumber)
    {
        if (levelNumber == 1)
        {
            SceneManager.LoadScene(levelSceneNamePrefix + " 1");
        }
        else
        {
            bool isUnlocked = IsLevelUnlocked(levelNumber);
            if (isUnlocked)
            {
                SceneManager.LoadScene(levelSceneNamePrefix + levelNumber);
            }
            else
            {
                Debug.Log("Level " + levelNumber + " is locked!");
            }
        }
    }
    private bool IsLevelUnlocked(int levelNumber)
    {
        return (levelNumber == 1 || PlayerPrefs.GetInt("Level" + levelNumber.ToString()) == 1);
    }
}

