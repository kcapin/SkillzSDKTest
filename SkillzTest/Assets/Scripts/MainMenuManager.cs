using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void LaunchSkillz()
    {
        SkillzCrossPlatform.LaunchSkillz(new GameController());
    }

    public void Exit()
    {
        Application.Quit();
    }
}
