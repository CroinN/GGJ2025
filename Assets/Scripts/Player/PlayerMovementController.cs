using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private LayerMask _groundCheckLayerMask;
    [SerializeField] private Transform _groundCheckPosition;
    [SerializeField] private float _groundCheckRadius;
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
        if (velocity.x != 0 && velocity.z != 0)
        {
            _rigidbody.velocity = velocity;
        }
    }

    public void Jump()
    {
        Collider[] colliders = Physics.OverlapSphere(_groundCheckPosition.position, _groundCheckRadius, _groundCheckLayerMask);

        if (colliders.Length > 0)
        {
            _rigidbody.AddForce(new Vector3(0f, _jumpForce, 0f), ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_groundCheckPosition.position, _groundCheckRadius);
    }
}
