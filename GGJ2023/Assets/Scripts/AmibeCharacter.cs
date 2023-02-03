using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmibeCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D = null;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private Vector2 _upVelocity;
    [SerializeField] private bool _isClimbable = false;

    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _distance = 1.0f;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        /*if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && _rb2D.velocity.y > 0f)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, _rb2D.velocity.y * 0.5f);
        }*/

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveForward();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveBackward();
        }



        Flip();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climbing")
        {
            _isClimbable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Climbing")
        {
            _isClimbable = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }



    private void MoveForward()
    {
        if (_isClimbable == true)
        {
            
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
        }
        else
        {
            _rb2D.MovePosition(_rb2D.position + _velocity * Time.fixedDeltaTime);
        }


    }
    


        private void MoveBackward()
    {
        if (_isClimbable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
        }
        else
        {
            _rb2D.MovePosition(_rb2D.position - _velocity * Time.fixedDeltaTime);
        }
    }

    private void ClimbWall()
    {
        _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);

    }

}
