using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 75;
    private float _speed = 5.0f;
    private float _jumpForce = 4.0f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] public BoxCollider myCollider;
    private bool _isGrounded;
    private bool _isDoubleJumpAvailable;
    [SerializeField] private float _blinkCooldown = 5.0f;
    private float _nextBlink = -1.0f;
    private float _blinkDistance = 5.0f;
    [SerializeField] private GameObject _orbPrefab;
    [SerializeField] public GameObject attackSpawner;
    [SerializeField] private bool _facingRight = true;
    [SerializeField] public Wand equippedWand;

    // Start is called before the first frame update
    void Start()
    {
        _isGrounded = false;
        _isDoubleJumpAvailable = false;

        equippedWand.SetAbilityDefaults();          //resets values in Ability ScriptableObjects to default values
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Movement();

        //turn towards mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePosition.x < playerPosition.x && _facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _facingRight = false;
        }
        else if (mousePosition.x > playerPosition.x && !_facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _facingRight = true;
        }

        //jump
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

        //utility attack
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            equippedWand.UseUAttack(this, attackSpawner);
            //Blink();
        }

        //basic attack
        if (Input.GetMouseButtonDown(0))
        {
            equippedWand.UseLAttack(this, attackSpawner);
        }
        
        //Lock movement on the Z axis to 0
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public int IsFacingRight()
    {
        if (_facingRight)
        {
            return 1;
        }
        else
        {
            return -1;
        }
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
        bool raycastHit = Physics.Raycast(myCollider.bounds.center, Vector3.down, myCollider.bounds.extents.y + extraHeight);
        Debug.DrawRay(myCollider.bounds.center, Vector3.down * (myCollider.bounds.extents.y + extraHeight));
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

    private void BasicAttack()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        GameObject orb = Instantiate(_orbPrefab, attackSpawner.transform.position, Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        //player dies
    }
}
