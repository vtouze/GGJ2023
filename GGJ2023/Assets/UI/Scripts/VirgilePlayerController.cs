using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirgilePlayerController : MonoBehaviour
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

    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public static Vector2 _lastCheckPointPos;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private GameObject _pauseMenu = null;

    void Awake()
    {
        transform.position = _lastCheckPointPos;
    }

    void Start()
    {
        _pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _characterState = Estate.Amibe;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            _characterState = Estate.Batra;
        }
        if (Input.GetKey(KeyCode.E))
        {
            _characterState = Estate.Avia;
        }
        if (Input.GetKey(KeyCode.R))
        {
            _characterState = Estate.Chimera;
        }

        if (_characterState == Estate.Amibe)
        {
            _characterSprite.sprite = _amibeSprite;
            
        }
        if (_characterState == Estate.Batra)
        {
            _characterSprite.sprite = _batraSprite;

        }
        if (_characterState == Estate.Avia)
        {
            _characterSprite.sprite = _aviaSprite;

        }
        if (_characterState == Estate.Chimera)
        {
            _characterSprite.sprite = _chimeraSprite;
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveForward();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveBackward();
        }
        Flip();

        if (_characterState == Estate.Batra)
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, jumpingPower);
            }

            if (Input.GetKeyUp(KeyCode.Space) && _rb2D.velocity.y > 0f)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, _rb2D.velocity.y * 0.5f);
            }
        }

        if(_characterState == Estate.Avia)
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, jumpingPower);
            }

            if (Input.GetKeyUp(KeyCode.Space) && _rb2D.velocity.y > 0f)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, _rb2D.velocity.y * 0.5f);
            }
            HoverOn();
        }

        if (_characterState == Estate.Chimera)
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, jumpingPower);
            }

            if (Input.GetKeyUp(KeyCode.Space) && _rb2D.velocity.y > 0f)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, _rb2D.velocity.y * 0.5f);
            }
            HoverOn();
            Strike();
        }
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

    private void MoveForward()
    {
        if (_isClimbable == true)
        {
            
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
        }
        else
        {
            _rb2D.velocity = new Vector2 (_rb2D.velocity.y, _velocity.y);
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
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    

}
