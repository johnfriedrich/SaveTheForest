using Enums;
using Interact;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")] public float speed = 0;
    public float pickUpRange;

    [SerializeField] private Grabable _carryingObject;

    private bool CanCarry => _carryingObject.Type == InteractableEnum.Hands;

    [SerializeField] private Transform _grabableHook;

    [SerializeField] private Grabable _hands;

    [Header("Movement")] [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxVelocityChange = 10f;

    [Header("CameraRotation")] [SerializeField]
    private float _lookSensitivity = 10f;

    [SerializeField] public float CameraRotationLimit = 85f;

    private Vector3 _rotation = Vector3.zero;
    private float _cameraRotationX;
    private float _currentCameraRotationX;
    private Rigidbody _rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ShootRay();
        if (Input.GetKey("w")) transform.Translate(Vector3.forward * Time.deltaTime * 3);
        if (Input.GetKey("s")) transform.Translate(Vector3.back * Time.deltaTime * 3);
        if (Input.GetKey("a")) transform.Translate(Vector3.left * Time.deltaTime * 3);
        if (Input.GetKey("d")) transform.Translate(Vector3.right * Time.deltaTime * 3);
        
        Rotate();

        PerformRotation();
    }

    private void Rotate()
    {
        float yRot = Input.GetAxisRaw("Mouse X");
        _rotation = new Vector3(0f, yRot, 0f) * _lookSensitivity;
        float xRot = Input.GetAxisRaw("Mouse Y");
        _cameraRotationX = xRot * _lookSensitivity;
    }

    public void PerformRotation()
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotation));
        _currentCameraRotationX -= _cameraRotationX;
        //Set and Clamp Camera Rotation
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -CameraRotationLimit, CameraRotationLimit);
        Camera.main.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
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
        _carryingObject = grabable;
        //Fuck off gravity
        grabable.GetComponent<Rigidbody>().isKinematic = true;
        grabable.gameObject.transform.SetParent(_grabableHook, true);
    }

    private void Drop()
    {
        //Make sure gravity applies to this object
        _carryingObject.GetComponent<Rigidbody>().isKinematic = false;
        //unparent object
        _carryingObject.transform.SetParent(null);
        _carryingObject = _hands;
    }

}