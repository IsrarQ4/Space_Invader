using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // call it by ScoreManager.Instance.AddScore(1) to add score
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text _scoreText;

    private int _score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateText();
    }
    //adds score basically the setter
    public void AddScore(int points)
    {
        _score += points;
        UpdateText();
    }
    //getter
    public int GetScore()
    {
        return _score;
    }

    private void UpdateText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _score;
        }
    }
}