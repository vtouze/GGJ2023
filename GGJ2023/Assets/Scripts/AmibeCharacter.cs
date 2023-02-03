using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmibeCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D = null;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private Vector2 _upVelocity;
    [SerializeField] private bool _isClimbable = false;
    [SerializeField] private Estate _characterState = Estate.Amibe;
    [SerializeField] private SpriteRenderer _characterSprite = null;
    [SerializeField] private Sprite _amibeSprite = null;
    [SerializeField] private Sprite _batraSprite = null;
    [SerializeField] private Sprite _aviaSprite = null;
    [SerializeField] private Sprite _chimeraSprite = null;

    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _distance = 1.0f;
    private bool _isGrounded = false;
    private float horizontal;
    private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float _hoverTime = 2f;
    [SerializeField] private float _notHoverTime = 2f;
    private bool isFacingRight = true;
    private float _movementSpeed = 5f;
    private float _jumpForce = 50f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _characterState = Estate.Amibe;
            _characterSprite.sprite = _amibeSprite;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            _characterState = Estate.Batra;
            _characterSprite.sprite = _batraSprite;
        }
        if (Input.GetKey(KeyCode.E))
        {
            _characterState = Estate.Avia;
            _characterSprite.sprite = _aviaSprite;
        }
        if (Input.GetKey(KeyCode.R))
        {
            _characterState = Estate.Chimera;
            _characterSprite.sprite = _chimeraSprite;
        }

        if (_characterState == Estate.Amibe)
        {
            AmibeController();
            
        }
        if (_characterState == Estate.Batra)
        {
            BatraController();

        }
        if (_characterState == Estate.Avia)
        {
            AviaController();

        }
        if (_characterState == Estate.Chimera)
        {
            ChimeraController();

        }


      


    }
    #region Sticking
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
    #endregion

    private void AmibeController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow) && _isClimbable == true || Input.GetKey(KeyCode.LeftArrow) && _isClimbable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);

        }
        else
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * _movementSpeed;
        }
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
        #endregion
    }
    private void BatraController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow) && _isClimbable == true || Input.GetKey(KeyCode.LeftArrow) && _isClimbable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);

        }
        else
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * _movementSpeed;
        }
        #endregion

        #region Jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rb2D.velocity.y) < 0.001f)
        {
            _rb2D.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
        #endregion
    }
    private void AviaController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow) && _isClimbable == true || Input.GetKey(KeyCode.LeftArrow) && _isClimbable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);

        }
        else
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * _movementSpeed;
        }
        #endregion

        #region Jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rb2D.velocity.y) < 0.001f)
        {
            _rb2D.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
        #endregion

        #region Hover

        #endregion
    }
    private void ChimeraController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow) && _isClimbable == true || Input.GetKey(KeyCode.LeftArrow) && _isClimbable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);

        }
        else
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * _movementSpeed;
        }
        #endregion

        #region Jump
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rb2D.velocity.y) < 0.001f)
        {
            _rb2D.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
        #endregion

        #region Strike

        #endregion
    }

    private void MoveForward()
    {
        if (_isClimbable == true)
        {
            
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
        }
        else
        {
            _rb2D.velocity += new Vector2 (_rb2D.velocity.y, _velocity.y);
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



    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
     
    private void HoverOn()
    {
        _rb2D.gravityScale = _hoverTime;
    }

    private void HoverOff()
    {
        _rb2D.gravityScale = _notHoverTime;
    }

    private void Strike()
    {

    }


    private void Flip()
    {
        
    }
    

}
