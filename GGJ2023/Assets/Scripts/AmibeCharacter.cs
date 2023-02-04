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
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _distance = 1.0f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float _hoverTime = 2f;
    [SerializeField] private float _notHoverTime = 2f;
    private float _movementSpeed = 5f;
    private float _jumpForce = 50f;
    [SerializeField] private BoxCollider2D _sticking2DColliderBox = null;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    #region Amibe
    [SerializeField] private Sprite _amibeSprite = null;
    [SerializeField] private float _amibeMass = 1f;
    [SerializeField] private float _amibeLinearDrag = 1f;
    [SerializeField] private PolygonCollider2D _amibeCollider2D = null;
    
    #endregion

    #region Batra
    [SerializeField] private Sprite _batraSprite = null;
    [SerializeField] private float _batraJumpForce = 1f;
    [SerializeField] private float _batraMass = 1f;
    [SerializeField] private float _batraLinearDrag = 1f;
    [SerializeField] private PolygonCollider2D _batraCollider2D = null;
    [SerializeField] private Vector2 _batraStickyBoxOffset = Vector2.zero;
    #endregion

    #region Avia
    [SerializeField] private Sprite _aviaSprite = null;
    [SerializeField] private float _aviaJumpForce = 1f;
    [SerializeField] private float _aviaMass = 1f;
    [SerializeField] private float _aviaLinearDrag = 1f;
    [SerializeField] private float _aviaHoverLinearDrag = 1f;
    [SerializeField] private PolygonCollider2D _aviaCollider2D = null;
    [SerializeField] private Vector2 _aviaStickyBoxOffset = Vector2.zero;
    #endregion

    #region Chimera
    [SerializeField] private Sprite _chimeraSprite = null;
    [SerializeField] private float _chimeraJumpForce = 1f;
    [SerializeField] private float _chimeraMass = 1f;
    [SerializeField] private float _chimeraLinearDrag = 1f;
    [SerializeField] private float _chimeraHoverLinearDrag = 1f;
    [SerializeField] private PolygonCollider2D _chimeraCollider2D = null;
    [SerializeField] private Vector2 _chimeraStickyBoxOffset = Vector2.zero;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            AmibeStatusUpdate();
        }
        if (Input.GetKey(KeyCode.Z))
        {
            BatraStatusUpdate();
        }
        if (Input.GetKey(KeyCode.E))
        {
            AviaStatusUpdate();
        }
        if (Input.GetKey(KeyCode.R))
        {
            ChimeraStatusUpdate();
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
    private void AmibeStatusUpdate()
    {
        _characterState = Estate.Amibe;
        _characterSprite.sprite = _amibeSprite;
        _rb2D.mass = _amibeMass;
        _rb2D.drag = _amibeLinearDrag;
        _amibeCollider2D.enabled = true;

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
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
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
    private void BatraStatusUpdate()
    {
        _characterState = Estate.Batra;
        _characterSprite.sprite = _batraSprite;
        _jumpForce = _batraJumpForce;
        _rb2D.mass = _batraMass;
        _rb2D.drag = _batraLinearDrag;
        _amibeCollider2D.enabled = false;
        _batraCollider2D.enabled = true;
        _sticking2DColliderBox.offset = _batraStickyBoxOffset;
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
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
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
        if(Input.GetButton("Jump") && Mathf.Abs(_rb2D.velocity.y) > 0.001f)
        {
            AviaHoverStatusUpdate();
        }
        else
        {
            AviaStatusUpdate();
        }
        #endregion
    }
    private void AviaStatusUpdate()
    {
        _characterState = Estate.Avia;
        _characterSprite.sprite = _aviaSprite;
        _jumpForce = _aviaJumpForce;
        _rb2D.mass = _aviaMass;
        _rb2D.drag = _aviaLinearDrag;
        _batraCollider2D.enabled = false;
        _aviaCollider2D.enabled = true;
        _sticking2DColliderBox.offset = _aviaStickyBoxOffset;
    }
    private void AviaHoverStatusUpdate()
    {
        _rb2D.drag = _aviaHoverLinearDrag;
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
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
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
        if (Input.GetButton("Jump") && Mathf.Abs(_rb2D.velocity.y) > 0.001f)
        {
            ChimeraHoverStatusUpdate();
        }
        else
        {
            ChimeraStatusUpdate();
        }
        #endregion

        #region Strike

        #endregion
    }
    private void ChimeraStatusUpdate()
    {
        _characterState = Estate.Chimera;
        _characterSprite.sprite = _chimeraSprite;
        _jumpForce = _chimeraJumpForce;
        _rb2D.mass = _chimeraMass;
        _rb2D.drag = _chimeraLinearDrag;
        _aviaCollider2D.enabled = false;
        _chimeraCollider2D.enabled = true;
        _sticking2DColliderBox.offset = _chimeraStickyBoxOffset;
    }
    private void ChimeraHoverStatusUpdate()
    {
        _rb2D.drag = _chimeraHoverLinearDrag;
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
