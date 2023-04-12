using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _minY;
    private float _maxY;

    private void Start()
    {
        float cameraSize = GameManager.Instance.Camera.orthographicSize;
        float halfPaddleHeight = transform.localScale.y / 2f;

        _minY = halfPaddleHeight - cameraSize;
        _maxY = cameraSize - halfPaddleHeight;
    }

    private void Update()
    {
        var newPos = transform.localPosition;
        var mousePos = GameManager.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
        newPos.y = Mathf.Clamp(mousePos.y, _minY, _maxY);
        transform.localPosition = newPos;
    }
}