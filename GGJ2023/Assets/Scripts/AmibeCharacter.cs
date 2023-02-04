using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmibeCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2D = null;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private Vector2 _upVelocity;
    [SerializeField] private bool _isStickable = false;
    [SerializeField] private Estate _characterState = Estate.Amibe;
    [SerializeField] private SpriteRenderer _characterSprite = null;
    [SerializeField] private Animator _characterAnim = null;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _hoverTime = 2f;
    [SerializeField] private float _notHoverTime = 2f;
    
    private bool _isGrounded = false;
    private float _movementSpeed = 5f;
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private int _scoreDNA = 0;
    [SerializeField] private int _requireDNA = 2;
    [SerializeField] private GameObject _objInRange;
    [SerializeField] private BoxCollider2D _sticking2DColliderBox = null;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    #region Amibe
    [SerializeField] private Sprite _amibeSprite = null;
    [SerializeField] private Animator _amibeAnim = null;
    [SerializeField] private float _amibeMass = 1f;
    [SerializeField] private float _amibeLinearDrag = 1f;
    [SerializeField] private PolygonCollider2D _amibeCollider2D = null;
    
    #endregion

    #region Batra
    [SerializeField] private Sprite _batraSprite = null;
    [SerializeField] private Animator _batraAnim = null;
    [SerializeField] private float _batraJumpForce = 1f;
    [SerializeField] private float _batraMass = 1f;
    [SerializeField] private float _batraLinearDrag = 1f;
    #endregion

    #region Avia
    [SerializeField] private Sprite _aviaSprite = null;
    [SerializeField] private Animator _aviaAnim = null;
    [SerializeField] private float _aviaJumpForce = 1f;
    [SerializeField] private float _aviaMass = 1f;
    [SerializeField] private float _aviaLinearDrag = 1f;
    [SerializeField] private float _aviaHoverLinearDrag = 1f;
    #endregion

    #region Chimera
    [Header("Chimera")]
    [SerializeField] private Sprite _chimeraSprite = null;
    [SerializeField] private Animator _chimeraAnim = null;
    [SerializeField] private float _chimeraJumpForce = 1f;
    [SerializeField] private float _chimeraMass = 1f;
    [SerializeField] private float _chimeraLinearDrag = 1f;
    [SerializeField] private float _chimeraHoverLinearDrag = 1f;
    [SerializeField] private PolygonCollider2D _chimeraCollider2D = null;
    #endregion
    [SerializeField] private GameObject _amibeObject = null;
    [SerializeField] private GameObject _batraObject = null;
    [SerializeField] private GameObject _aviaObject = null;
    [SerializeField] private GameObject _chimeraObject = null;

    public int ScoreDNA => _scoreDNA;
    public int RequireDNA => _requireDNA;

    // Start is called before the first frame update
    void Start()
    {
        _amibeObject.SetActive(true);
        AmibeStatusUpdate();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _amibeObject.SetActive(true);
            AmibeStatusUpdate();
        }
        if (Input.GetKey(KeyCode.Z))
        {
            _amibeObject.SetActive(false);
            _batraObject.SetActive(true);
            BatraStatusUpdate();
        }
        if (Input.GetKey(KeyCode.E))
        {
            _batraObject.SetActive(false);
            _aviaObject.SetActive(true);
            AviaStatusUpdate();
        }
        if (Input.GetKey(KeyCode.R))
        {
            _aviaObject.SetActive(false);
            _chimeraObject.SetActive(true);
            ChimeraStatusUpdate();
        }



        if (_characterState == Estate.Amibe)
        {
            AmibeController();
            
        }
        else if (_characterState == Estate.Batra)
        {
            BatraController();

        }
        else if (_characterState == Estate.Avia)
        {
            AviaController();

        }
        else if (_characterState == Estate.Chimera)
        {
            ChimeraController();

        }


      


    }


    private void CheckEvolution()
    {

        switch(_characterState)
        {
            case Estate.Amibe:
                if (_scoreDNA == _requireDNA)
                {
                    _scoreDNA = 0;
                    _requireDNA = 3;
                    BatraStatusUpdate();
                    
                }
                break;

            case Estate.Batra:
                if (_scoreDNA == _requireDNA)
                {
                    _scoreDNA = 0;
                    _requireDNA = 4;
                    AviaStatusUpdate();
                    
                }
                break;

            case Estate.Avia:
                if (_scoreDNA == _requireDNA)
                {
                    _scoreDNA = 0;
                    _requireDNA = 5;
                    ChimeraStatusUpdate();
                    
                }
                break;

            case Estate.Chimera:
                if (_scoreDNA == _requireDNA)
                {
                    Debug.Log("Chtulluuuuuuu !!!!!");
                }
                _scoreDNA = 0;
                _requireDNA = 6;
                break;

             default:
                Debug.LogError("CharacterState is non valid");
                break;
        }
    }


    #region Sticking and DNA Collect
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stickable")
        {
            _isStickable = true;
            
        }
        else if (collision.gameObject.tag == "DNA")
        {
            Destroy(collision.gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stickable")
        {
            _isStickable = false;
        }
        else if (collision != null && collision.gameObject.tag == "DNA")
        {
            _scoreDNA++;
            Debug.Log(_characterState + " " + _scoreDNA);
            CheckEvolution();
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            _isGrounded = false;
        }
    }

    private void AmibeController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1)
        {
            _characterAnim.SetBool("isWalking", true);
        }
        else
        {
            _characterAnim.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.RightArrow) && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isStickable == true)
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
        _amibeObject.SetActive(true);
        _characterState = Estate.Amibe;
    }

    private void BatraController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1)
        {
            _characterAnim.SetBool("isWalking", true);
        }
        else
        {
            _characterAnim.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.RightArrow) && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isStickable == true)
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
            _characterAnim.SetBool("isJumping", true);
        }
        else
        {
            _characterAnim.SetBool("isJumping", false);
        }
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
        #endregion
        
    }
    private void BatraStatusUpdate()
    {
        _amibeObject.SetActive(false);
        _batraObject.SetActive(true);
        _rb2D.mass = _batraMass;
        _rb2D.drag = _batraLinearDrag;
        _jumpForce = _batraJumpForce;
        _characterState = Estate.Batra;
    }

    private void AviaController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow) && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isStickable == true)
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
        _batraObject.SetActive(false);
        _aviaObject.SetActive(true);
        _rb2D.mass = _aviaMass;
        _rb2D.drag = _aviaLinearDrag;
        _jumpForce = _aviaJumpForce;
        _characterState = Estate.Avia;
    }
    private void AviaHoverStatusUpdate()
    {
        _rb2D.drag = _aviaHoverLinearDrag;
    }

    private void ChimeraController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow) && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isStickable == true)
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
        _aviaObject.SetActive(false);
        _chimeraObject.SetActive(true);
        _rb2D.mass = _chimeraMass;
        _rb2D.drag = _chimeraLinearDrag;
        _jumpForce = _chimeraJumpForce;
        _characterState = Estate.Chimera;
    }
    private void ChimeraHoverStatusUpdate()
    {
        _rb2D.drag = _chimeraHoverLinearDrag;
    }

    private void MoveForward()
    {
        if (_isStickable == true)
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
        if (_isStickable == true)
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
