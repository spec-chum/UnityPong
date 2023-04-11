using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _minY;
    private float _maxY;

    private void Start()
    {
        float cameraSize = Camera.main.orthographicSize;
        float halfPaddleHeight = transform.localScale.y / 2f;

        _minY = halfPaddleHeight - cameraSize;
        _maxY = cameraSize - halfPaddleHeight;
    }

    private void Update()
    {
        var newPosition = transform.localPosition;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.y = Mathf.Clamp(mousePos.y, _minY, _maxY);

        transform.localPosition = newPosition;
    }
}