using UnityEngine;

public class DrawDivider : MonoBehaviour
{
    [SerializeField] private GameObject _divider;

    private void Start()
    {
        float cameraSize = GameManager.Instance.CameraSize;

        var position = Vector3.zero;
        for (float yPos = -cameraSize + 0.2f; yPos < cameraSize; yPos += 0.4f)
        {
            position.y = yPos;
            Instantiate(_divider, position, Quaternion.identity, transform);
        }
    }
}