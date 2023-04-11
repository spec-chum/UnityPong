using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _playerScoreText;
    [SerializeField] private Text _aiScoreText;

    private int _playerScore;
    private int _aiScore;

    public int PlayerScore
    {
        get => _playerScore;
        set
        {
            _playerScore = value;
            _playerScoreText.text = _playerScore.ToString().PadLeft(5, ' ');
        }
    }

    public int AIScore
    {
        get => _aiScore;
        set
        {
            _aiScore = value;
            _aiScoreText.text = _aiScore.ToString().PadRight(5, ' ');
        }
    }
}