using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _player;
    [SerializeField] private SpriteRenderer _ai;
    [SerializeField] private float _speed = 5;

    private ScoreManager _scoreManager;
    private SpriteRenderer _ballRenderer;
    private Vector2 _velocity;
    private Vector2 _screenSize;
    private Bounds _ballBounds;

    private void Start()
    {
        // Calculate the viewport's limits in World Space
        _screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Cache items
        _scoreManager = FindObjectOfType<ScoreManager>();
        _ballRenderer = GetComponent<SpriteRenderer>();

        // Set initial velocity and clamp to +/-22.5 degrees
        float angle = Random.Range(-Mathf.PI / 8, Mathf.PI / 8);
        _velocity.x = _speed * Mathf.Cos(angle) * Mathf.Sign(-_velocity.x);
        _velocity.y = _speed * Mathf.Sin(angle);
    }

    private void Update()
    {
        Vector2 position = transform.localPosition;

        _ballBounds = _ballRenderer.bounds;
        if (_ballBounds.min.x < -_screenSize.x)
        {
            _scoreManager.AIScore++;
            position.x = -_screenSize.x + _ballBounds.extents.x;
            _velocity.x *= -1;
        }
        else if (_ballBounds.max.x > _screenSize.x)
        {
            _scoreManager.PlayerScore++;
            position.x = _screenSize.x - _ballBounds.extents.x;
            _velocity.x *= -1;
        }

        if (_ballBounds.min.y < -_screenSize.y)
        {
            position.y = -_screenSize.y + _ballBounds.extents.y;
            _velocity.y *= -1;
        }
        else if (_ballBounds.max.y > _screenSize.y)
        {
            position.y = _screenSize.y - _ballBounds.extents.y;
            _velocity.y *= -1;
        }

        if (_ballBounds.Intersects(_player.bounds))
        {
            position.x = _player.bounds.max.x + _ballBounds.extents.x;
            BounceOffPaddle(_player);
        }
        else if (_ballBounds.Intersects(_ai.bounds))
        {
            position.x = _ai.bounds.min.x - _ballBounds.extents.x;
            BounceOffPaddle(_ai);
        }

        transform.localPosition = (Vector3)(position + (_velocity * Time.deltaTime));
    }

    private void BounceOffPaddle(SpriteRenderer paddle)
    {
        float yDiff = transform.localPosition.y - paddle.gameObject.transform.localPosition.y;
        float normalizedYDiff = yDiff / (paddle.bounds.size.y / 2f);
        float angle = normalizedYDiff * Mathf.PI / 4f;

        // Add some randomness to the bounce angle
        float randomness = Random.Range(-0.1f, 0.1f);
        angle += randomness;

        // Go slightly faster, and update the velocity based on the new angle
        _speed += 0.1f;
        _velocity.x = _speed * Mathf.Cos(angle) * Mathf.Sign(-_velocity.x);
        _velocity.y = _speed * Mathf.Sin(angle);
    }
}