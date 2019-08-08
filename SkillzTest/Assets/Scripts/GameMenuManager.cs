using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    [SerializeField]
    private TextMeshProUGUI gameOverScoreText;

    private int score;
    private bool finished = false;

    public void AddScore(int _score)
    {
        if (finished) return;

        score += _score;
        currentScoreText.text = "Score: " + score.ToString();

        if(score >= 10)
        {
            Finish();
        }
    }

    public void Forfeit()
    {
        Finish();
    }

    public void Finish()
    {
        if (finished) return;
        finished = !finished;
        gameOverScoreText.text = "YOUR SCORE" + "\r\n" + score.ToString();
        gameOverScoreText.gameObject.SetActive(true);

        Invoke("end", 3f);
    }

    private void end()
    {
        SceneManager.LoadScene(GameManager.Instance.MenuSceneName);
    }
}
