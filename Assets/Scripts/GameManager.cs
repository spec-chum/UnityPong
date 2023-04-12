using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera Camera;
    public GameObject Ball;
    public GameObject Player;
    public GameObject Ai;

    [SerializeField] private Text _playerScoreText;
    [SerializeField] private Text _aiScoreText;

    private int _playerScore;
    private int _aiScore;

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


    public float CameraSize => Camera.orthographicSize;
    public Vector2 ScreenSizeInWorldSpace => Camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

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
