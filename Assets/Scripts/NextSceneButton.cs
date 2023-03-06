using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneButton : MonoBehaviour
{
    public Button nextSceneButton;
    public Button Home;
    public Button tryAgain;
    


    void Start()
    {
        nextSceneButton.onClick.AddListener(LoadNextScene);
        Home.onClick.AddListener(HomeBtn);
        tryAgain.onClick.AddListener(TryAgain);
    }

    public void LoadNextScene()
    {
        int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        int nextSceneIndex = lastSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Level Select");
    }
}
