using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private const float InitialSpeed = 10;

    private SpriteRenderer _ballRenderer;
    private SpriteRenderer _playerRenderer;
    private SpriteRenderer _aiRenderer;
    private Vector2 _velocity;
    private Vector2 _screenSize;
    private Bounds _ballBounds;
    private float _speed;

    private void Start()
    {
        // Cache items
        _ballRenderer = GetComponent<SpriteRenderer>();
        _playerRenderer = GameManager.Instance.Player.GetComponent<SpriteRenderer>();
        _aiRenderer = GameManager.Instance.Ai.GetComponent<SpriteRenderer>();
        _screenSize = GameManager.Instance.ScreenSizeInWorldSpace;

        Reset();
    }

    private void Reset()
    {
        transform.localPosition = Vector3.zero;
        _speed = InitialSpeed;

        // Set initial velocity and clamp to +/-22.5 degrees
        float angle = Random.Range(-Mathf.PI / 8, Mathf.PI / 8);
        _velocity.x = _speed * Mathf.Cos(angle);
        _velocity.y = _speed * Mathf.Sin(angle);
    }

    private void Update()
    {
        _ballBounds = _ballRenderer.bounds;

        Vector2 position = transform.localPosition;
        TestScreenEdgeCollision(ref position);
        TestPaddleCollision(ref position);

        transform.localPosition = (Vector3)(position + (_velocity * Time.deltaTime));
    }

    private void TestScreenEdgeCollision(ref Vector2 position)
    {
        if (_ballBounds.min.x < -_screenSize.x)
        {
            GameManager.Instance.AIScore++;
            position = Vector2.zero;
            Reset();
        }
        else if (_ballBounds.max.x > _screenSize.x)
        {
            GameManager.Instance.PlayerScore++;
            position = Vector2.zero;
            Reset();
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
    }

    private void TestPaddleCollision(ref Vector2 position)
    {
        if (_ballBounds.Intersects(_playerRenderer.bounds))
        {
            position.x = _playerRenderer.bounds.max.x + _ballBounds.extents.x;
            BounceOffPaddle(_playerRenderer);
        }
        else if (_ballBounds.Intersects(_aiRenderer.bounds))
        {
            position.x = _aiRenderer.bounds.min.x - _ballBounds.extents.x;
            BounceOffPaddle(_aiRenderer);
        }
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