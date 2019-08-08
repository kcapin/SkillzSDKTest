using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LaunchSkillz()
    {
        SceneManager.LoadScene(GameManager.Instance.GameSceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
