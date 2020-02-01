using Interact;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")] public float speed = 0;
    public float pickUpRange;

    [SerializeField]
    private Grabable _carryingObject;

    private bool CanCarry => _carryingObject == null;

    [Header("Camera Settings")] public float minimumX = -90.0f;
    public float maximumX = 90.0f;
    public float cameraSmoothTime = 20.0f;

    [SerializeField] private Transform _grabableHook;
    [SerializeField] private CharacterController _cc;

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
        var horizontal = Input.GetAxis("Mouse X") * cameraSmoothTime * Time.deltaTime;
        var vertical = Input.GetAxis("Mouse Y") * cameraSmoothTime * Time.deltaTime;

        transform.localRotation *= Quaternion.Euler(0.0f, horizontal, 0.0f);
        transform.localRotation *= Quaternion.Euler(-vertical, 0.0f, 0.0f);
        transform.localRotation = ClampRotationAroundXAxis(transform.localRotation);
    }

    private void Move()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var movement = new Vector3(horizontal, 0.0f, vertical);
        movement *= speed * Time.deltaTime;

        _cc.SimpleMove(movement);
    }

    private void ShootRay()
    {
        if (!CanCarry)
        {
            Drop();
            return;
        }

        RaycastHit target;
        if (Physics.Raycast(transform.position, transform.forward, out target, pickUpRange))
        {
            var grabable = target.transform.GetComponent<Grabable>();
            if (grabable)
            {
                PickUp(grabable);
                return;
            }
            
            var usableObject = target.transform.GetComponent<UsableObject>();
            
            if (usableObject)
            {
               Use(usableObject);
               return;
            }
        }
    }

    private void Use(UsableObject usableObject)
    {
        var returnObject = usableObject.TryHelp(_carryingObject);
        if (returnObject)
        {
            _carryingObject.UsableAction.Help();
            Debug.Log($"{_carryingObject.Type} tries to help {usableObject.Type}");
        }
    }

    private void PickUp(Grabable grabable)
    {
        //Fuck off gravity
        grabable.GetComponent<Rigidbody>().isKinematic = true;
        grabable.gameObject.transform.SetParent(_grabableHook, false);
        _carryingObject = grabable;
    }

    private void Drop()
    {
        //Make sure gravity applies to this object
        _carryingObject.GetComponent<Rigidbody>().isKinematic = false;
        //unparent object
        _carryingObject.transform.SetParent(null);
        _carryingObject = null;
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion q)
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