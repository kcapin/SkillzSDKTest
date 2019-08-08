using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void LaunchSkillz()
    {
        var _controller = new GameController();
        SkillzCrossPlatform.LaunchSkillz(_controller);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
