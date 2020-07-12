using System.CodeDom;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private Camera _camera;

    [SerializeField]
    private float _turnSmoothTime = 0.1f;
    [SerializeField]
    private float _speed = 10f;
    private float _turnSmoothVelocity;
    private float _horizontal;
    private float _vertical;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        Move(_horizontal, Time.deltaTime);
    }

    private void Move(float horizontal, float vertical)
    {
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > Mathf.Epsilon)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, directionAngle, ref _turnSmoothVelocity, _turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, directionAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDirection.normalized * _speed * Time.deltaTime);
        }
    }

    public void GetAxes(InputAction.CallbackContext context)
    {
        Vector2 contextV2 = context.ReadValue<Vector2>();
        _horizontal = contextV2.x;
        _vertical = contextV2.y;
    }

    public void Jump(InputAction.CallbackContext context)
    {

    }
}
