using UnityEngine;
using UnityEngine.InputSystem;
public class CameraMouseOffset : MonoBehaviour
{
    public Transform player;
    public float maxOffset = 2f;
    public float smoothSpeed = 10f;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        Vector3 direction = mouseWorld - player.position;
        Vector3 offset = Vector3.ClampMagnitude(direction, maxOffset);

        Vector3 targetPos = player.position + offset;
        
        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * smoothSpeed
        );
    }
}
