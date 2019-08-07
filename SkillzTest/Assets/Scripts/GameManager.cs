using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string MenuSceneName;
    public string GameSceneName;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
