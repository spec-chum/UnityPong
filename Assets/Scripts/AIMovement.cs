using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private Transform _ball;
    private float _minY;
    private float _maxY;

    private void Start()
    {
        _ball = GameManager.Instance.Ball.transform;

        float cameraSize = GameManager.Instance.CameraSize;
        float halfPaddleHeight = transform.localScale.y / 2f;
        _minY = halfPaddleHeight - cameraSize;
        _maxY = cameraSize - halfPaddleHeight;
    }

    private void Update()
    {
        float direction = Mathf.Sign(_ball.position.y - transform.localPosition.y);
        float targetY = _ball.position.y + (direction * Random.Range(-0.5f, 0.5f));
        targetY = Mathf.Clamp(targetY, _minY, _maxY);
        float newY = Mathf.MoveTowards(transform.localPosition.y, targetY, _speed * Time.deltaTime);
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}