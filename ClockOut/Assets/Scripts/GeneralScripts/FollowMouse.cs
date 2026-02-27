using UnityEngine;
using UnityEngine.InputSystem;
public class FollowMouse : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private GameObject centerPoint;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = GetMouseDir();
        RotateInDir();
    }

    Vector3 GetMousePos()
    {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f; // important for 2D
        return mouseWorldPos;
    }

    public Vector2 GetMouseDir()
    {
        Vector2 direction = (GetMousePos() - centerPoint.transform.position).normalized;
        return direction * distance;
    }

    void RotateInDir()
    {
        Vector2 dir = GetMouseDir();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
