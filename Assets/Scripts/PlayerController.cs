using Interact;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")] public float speed = 0;
    public float pickUpRange;

    private CharacterController _cc;
    private bool _canCarry = true;

    [Header("Camera Settings")] public float minimumX = -90.0f;
    public float maximumX = 90.0f;
    public float cameraSmoothTime = 20.0f;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
        Move();

        if (Input.GetKeyDown(KeyCode.E))
            ShootRay();
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

    private void ShootRay()
    {
        RaycastHit target;

        if (Physics.Raycast(transform.position, transform.forward, out target, pickUpRange))
        {
            if (target.transform.GetComponent<Grabable>())
            {
                PickUp(target.transform.gameObject);
            }
        }
    }

    private void PickUp(GameObject grabableObject)
    {
        grabableObject.transform.parent = this.transform;
        grabableObject.transform.localPosition += Vector3.up;
        _canCarry = false;
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