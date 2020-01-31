using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 0;

    private CharacterController _cc;
    private Camera _playerCam;

    [Header("Camera Settings")]
    public float minimumX = -90.0f;
    public float maximumX = 90.0f;
    public float cameraSmoothTime = 20.0f;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _playerCam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        RotateCamera();
        Move();
        Interact();
    }
    
    private void RotateCamera()
    {
        float horizontal = Input.GetAxis("Mouse X") * cameraSmoothTime * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * cameraSmoothTime * Time.deltaTime;

        transform.localRotation *= Quaternion.Euler(0.0f, horizontal, 0.0f);
        transform.localRotation *= Quaternion.Euler(-vertical, 0.0f, 0.0f);
        transform.localRotation = ClampRotationAroundXAxis(transform.localRotation);
    }
    
    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        movement *= speed * Time.deltaTime;

        _cc.SimpleMove(movement);
    }
    
    private void Interact()
    {
        
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, minimumX, maximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}