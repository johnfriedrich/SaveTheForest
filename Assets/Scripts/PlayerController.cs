using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    
    [Header("Camera Settings")]
    public float MinimumX = -90.0f;
    public float MaximumX = 90.0f;
    public float CameraSmoothTime = 20.0f;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void RotateCamera() //Mouse rotation is used for first-person view only
    {
        float horizontal = Input.GetAxis("Mouse X") * CameraSmoothTime * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * CameraSmoothTime * Time.deltaTime;

        transform.localRotation *= Quaternion.Euler(0.0f, horizontal, 0.0f);
        FirstPerson.transform.localRotation *= Quaternion.Euler(-vertical, 0.0f, 0.0f);
        FirstPerson.transform.localRotation = ClampRotationAroundXAxis(FirstPerson.transform.localRotation);
    }
}