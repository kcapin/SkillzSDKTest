using UnityEngine;
using TMPro;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    [SerializeField]
    private TextMeshProUGUI gameOverScoreText;

    private int score;

    public void AddScore(int _score)
    {
        if (score >= 10) return;

        score += _score;
        currentScoreText.text = "Score: " + score.ToString();

        if(score >= 10)
        {
            Finish();
        }
    }

    public void Forfeit()
    {
        SkillzCrossPlatform.AbortMatch();
    }

    public void Finish()
    {
        gameOverScoreText.text = "YOUR SCORE" + "\r\n" + score.ToString();
        gameOverScoreText.gameObject.SetActive(true);

        Invoke("reportScore", 3f);
    }

    private void reportScore()
    {
        if (SkillzCrossPlatform.IsMatchInProgress())
        {
            SkillzCrossPlatform.ReportFinalScore(score.ToString());
        }
    }
}
