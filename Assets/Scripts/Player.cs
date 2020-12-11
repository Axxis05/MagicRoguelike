using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 5.0f;
    private float _jumpForce = 4.0f;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private BoxCollider _bc;
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private bool _isDoubleJumpAvailable;
    [SerializeField]
    private float _blinkCooldown = 5.0f;
    private float _nextBlink = -1.0f;
    private float _blinkDistance = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _isGrounded = false;
        _isDoubleJumpAvailable = false;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && (_isGrounded || _isDoubleJumpAvailable))
        {
            if (_isGrounded)
            {
                Jump();
            }
            else if (_isDoubleJumpAvailable)
            {
                _isDoubleJumpAvailable = false;
                Jump();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > _nextBlink)
        {
            Blink();
        }


        //Lock movement on the Z axis to 0
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0f, 0f);
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    void Jump()
    {
        Vector3 jumpVector = new Vector3(0f, _jumpForce, 0f);
        _rb.velocity = new Vector3(_rb.velocity.x, 0, 0);
        _rb.AddForce(jumpVector, ForceMode.Impulse);
    }

    void Blink()
    {
        int direction = 0;
        Vector3 endLocation = transform.position;

        _nextBlink = Time.time + _blinkCooldown;

        if (Input.GetAxis("Horizontal") >= 0)
        {
            direction = 1;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            direction = -1;
        }
        else
        {
            Debug.LogError("Input.GetAxis('Horizontal' is undefined.");

        }

        float newX = transform.position.x + (_blinkDistance * direction);
        endLocation.x = newX;

        transform.position = endLocation;
    }

    private void IsGrounded()
    {
        float extraHeight = 0.01f;
        bool raycastHit = Physics.Raycast(_bc.bounds.center, Vector3.down, _bc.bounds.extents.y + extraHeight);
        Debug.DrawRay(_bc.bounds.center, Vector3.down * (_bc.bounds.extents.y + extraHeight));
        Color rayColor;
        if (raycastHit)
        {
            rayColor = Color.green;
            _isGrounded = true;
            _isDoubleJumpAvailable = true;
        }
        else
        {
            rayColor = Color.red;
            _isGrounded = false;
        }
    }

}
