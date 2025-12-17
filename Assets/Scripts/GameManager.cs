
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int winningScore = 50;

    private int _currentScore = 0;
    #endregion

    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Unity Methods
    private void Start()
    {
        UpdateScoreUI();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Adds points to the current score and checks for win condition.
    /// </summary>
    /// <param name="points">The amount of points to add.</param>
    public void AddScore(int points)
    {
        _currentScore += points;
        UpdateScoreUI();

        if (_currentScore >= winningScore)
        {
            Debug.Log("Level Passed!");
            // Burada bir sonraki seviyeye geçiş veya oyun sonu ekranı gösterilebilir.
        }
    }
    #endregion

    #region Private Methods
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + _currentScore;
        }
    }
    #endregion
}
