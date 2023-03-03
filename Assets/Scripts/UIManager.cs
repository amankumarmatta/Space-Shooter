using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public string firstLevelName;
    private int lastSceneIndex;

    void Start()
    {
        lastSceneIndex = PlayerPrefs.GetInt("LastSceneIndex");
    }

    public void RestartLastScene()
    {
        SceneManager.LoadScene(lastSceneIndex);
    }

    public void OnHomeClicked()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
