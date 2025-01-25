using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direciton)
    {
        direciton = transform.right * direciton.x + transform.forward * direciton.z;
        
        Vector3 velocity = direciton * _speed;
        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }

    public void Jump()
    {
        _rigidbody.AddForce(new Vector3(0f, _jumpForce, 0f), ForceMode.Impulse);
    }
}
