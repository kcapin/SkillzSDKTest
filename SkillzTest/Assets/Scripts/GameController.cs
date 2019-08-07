using UnityEngine.SceneManagement;
using SkillzSDK;

public class GameController : SkillzMatchDelegate
{
    public void OnMatchWillBegin(Match _matchInfo)
    {
        SceneManager.LoadScene(GameManager.Instance.GameSceneName);
    }

    public void OnSkillzWillExit()
    {
        SceneManager.LoadScene(GameManager.Instance.MenuSceneName);
    }
}
