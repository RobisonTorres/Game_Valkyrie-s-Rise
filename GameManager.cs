using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score;

    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BuscarScoreText();
    }

    private void Start()
    {
        BuscarScoreText();
    }

    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void BuscarScoreText()
    {
        GameObject obj = GameObject.Find("CollectableCount");

        if (obj != null)
        {
            scoreText = obj.GetComponent<TextMeshProUGUI>();
        }
    }
}