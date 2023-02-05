using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AmibeCharacter : MonoBehaviour
{

    #region Fields

    [SerializeField] private Rigidbody2D _rb2D = null;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private Vector2 _upVelocity;
    [SerializeField] private bool _isStickable = false;
    [SerializeField] private bool _isCellingStickable = false;
    [SerializeField] private Estate _characterState = Estate.Amibe;
    [SerializeField] private SpriteRenderer _characterSprite = null;
    [SerializeField] private Animator _characterAnim = null;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _hoverTime = 2f;
    [SerializeField] private float _notHoverTime = 2f;
    [SerializeField] private Camera _mainCamera = null;
    private bool _isGrounded = false;
    private float _movementSpeed = 5f;
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private int _scoreDNA = 0;
    [SerializeField] private int _requireDNA = 1;
    [SerializeField] private GameObject _objInRange;
    [SerializeField] private BoxCollider2D _sticking2DColliderBox = null;
    [SerializeField] private PolygonCollider2D _characterPolyCollider2D = null;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    #region pausemenu
    public static Vector2 _lastCheckPointPos;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private GameObject _pauseMenu = null;
    #endregion

    #region Root
    [SerializeField] private PolygonCollider2D _rootPolyCollider2D = null;
    [SerializeField] private float _rootFOV = 2f;
    #endregion

    #region Amibe
    [Header("Amibe")]
    [SerializeField] private Sprite _amibeSprite = null;
    [SerializeField] private Animator _amibeAnim = null;
    [SerializeField] private float _amibeMass = 1f;
    [SerializeField] private float _amibeLinearDrag = 1f;
    [SerializeField] private float _amibeFOV = 2f;
    [SerializeField] private BoxCollider2D _amibeStickyCollider2D = null;
    [SerializeField] private PolygonCollider2D _amibePolyCollider2D = null;

    #endregion

    #region Batra
    [Header("Batra")]
    [SerializeField] private Sprite _batraSprite = null;
    [SerializeField] private Animator _batraAnim = null;
    [SerializeField] private float _batraJumpForce = 1f;
    [SerializeField] private float _batraMass = 1f;
    [SerializeField] private float _batraLinearDrag = 1f;
    [SerializeField] private float _batraFOV = 2f;
    [SerializeField] private BoxCollider2D _batraStickyCollider2D = null;
    [SerializeField] private PolygonCollider2D _batraPolyCollider2D = null;
    #endregion

    #region Avia
    [Header("Avia")]
    [SerializeField] private Sprite _aviaSprite = null;
    [SerializeField] private Animator _aviaAnim = null;
    [SerializeField] private float _aviaJumpForce = 1f;
    [SerializeField] private float _aviaMass = 1f;
    [SerializeField] private float _aviaLinearDrag = 1f;
    [SerializeField] private float _aviaHoverLinearDrag = 1f;
    [SerializeField] private float _aviaFOV = 2f;
    [SerializeField] private BoxCollider2D _aviaStickyCollider2D = null;
    [SerializeField] private PolygonCollider2D _aviaPolyCollider2D = null;
    #endregion

    #region Chimera
    [Header("Chimera")]
    [SerializeField] private Sprite _chimeraSprite = null;
    [SerializeField] private Animator _chimeraAnim = null;
    [SerializeField] private float _chimeraJumpForce = 1f;
    [SerializeField] private float _chimeraMass = 1f;
    [SerializeField] private float _chimeraLinearDrag = 1f;
    [SerializeField] private float _chimeraHoverLinearDrag = 1f;
    [SerializeField] private float _chimeraFOV = 2f;
    [SerializeField] private BoxCollider2D _chimeraStickyCollider2D = null;
    [SerializeField] private PolygonCollider2D _chimeraPolyCollider2D = null;
    #endregion

    #region Cthulhu
    [SerializeField] private GameObject _cthulhuObject = null;
    [SerializeField] private SpriteRenderer _cthulhuRenderer = null;
    [SerializeField] private GameObject _posCthulhu;
    [SerializeField] private Sprite _cthulhuFly = null;
    [SerializeField] private bool _isCthulhu = false;
    [SerializeField] private float _delay = 3f;
    [SerializeField] private float _endDelay = 13f;
    [SerializeField] private float _timeStamp = 0f;
    #endregion Cthulhu

    [SerializeField] private GameObject _rootObject = null;
    [SerializeField] private GameObject _amibeObject = null;
    [SerializeField] private GameObject _batraObject = null;
    [SerializeField] private GameObject _aviaObject = null;
    [SerializeField] private GameObject _chimeraObject = null;

    #region Sounds
    [Header("Character Sound")]
    [SerializeField] private float _amibeMovementSoundDelay = 0.3f;
    [SerializeField] private float _batraMovementSoundDelay = 0.3f;
    [SerializeField] private float _aviaMovementSoundDelay = 0.3f;
    [SerializeField] private float _chimeraMovementSoundDelay = 0.3f;

    private bool _strikeSoundSecurity = false;

    private float _movementSoundTimeStamp = 0.5f;
    private float _timeStampAttack = 0f;
    private bool _musicEndSecurity = false;

    #endregion Sounds

    #endregion Fields

    #region Properties
    public int ScoreDNA => _scoreDNA;
    public int RequireDNA => _requireDNA;
    #endregion Properties

    #region Methods


    void Awake()
    {
        transform.position = _lastCheckPointPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _spawnPoint.position;
        _pauseMenu.SetActive(false);
        _rootObject.SetActive(true);
        RootStatusUpdate();
        //_pauseMenu.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        // debug button
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

        if (Input.GetKey(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        if (_characterState == Estate.Root)
        {
            RootController();

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

    private void FixedUpdate()
    {
        if (_isCthulhu == true)
        {
            _timeStamp += Time.deltaTime;
            if (_timeStamp >= _delay)
            {
                _cthulhuRenderer.sprite = _cthulhuFly;
                float speed = (_timeStamp * 0.05f);
                transform.position += new Vector3(0, speed, 0);
                Debug.Log("Chtulluuuuuuu !!!!!");
                if(_musicEndSecurity == false)
                {
                    AudioManager.Instance.PlayMusic("M_5");
                    _musicEndSecurity = true;
                }

            }
            if(_timeStamp >= _endDelay)
            {
                SceneManager.LoadScene("Credits");
            }
        }
    }


    private void CheckEvolution()
    {

        switch(_characterState)
        {
            case Estate.Root:
                if (_scoreDNA == _requireDNA)
                {
                    _scoreDNA = 0;
                    _requireDNA = 2;
                    AmibeStatusUpdate();
                    AudioManager.Instance.StartSound("S_Evolution1");

                }
                break;

            case Estate.Amibe:
                if (_scoreDNA == _requireDNA)
                {
                    _scoreDNA = 0;
                    _requireDNA = 3;
                    BatraStatusUpdate();
                    AudioManager.Instance.StartSound("S_EvolutionBatra");

                }
                break;

            case Estate.Batra:
                if (_scoreDNA == _requireDNA)
                {
                    _scoreDNA = 0;
                    _requireDNA = 4;
                    AviaStatusUpdate();
                    AudioManager.Instance.StartSound("S_EvolutionBatra");


                }
                break;

            case Estate.Avia:
                if (_scoreDNA == _requireDNA)
                {
                    _scoreDNA = 0;
                    _requireDNA = 5;
                    ChimeraStatusUpdate();
                    AudioManager.Instance.StartSound("S_EvolutionAvia");


                }
                break;

            case Estate.Chimera:
                if (_scoreDNA == _requireDNA)
                {
                    _chimeraObject.SetActive(false);
                    _cthulhuObject.SetActive(true);
                    _rb2D.simulated = false;
                    _isCthulhu = true;
                    _timeStamp = 0;
                    transform.position = _posCthulhu.transform.position;
                    AudioManager.Instance.StartSound("S_EvolutionChimera");

                }

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
        else if (collision.gameObject.tag == "StickableCelling")
        {
            _isCellingStickable = true;
        }
        else if (collision.gameObject.tag == "DNA")
        {
            AudioManager.Instance.StartSound("S_ADN");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Destroyable")
        {
            _objInRange = collision.gameObject;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stickable")
        {
            _isStickable = false;
        }
        else if (collision.gameObject.tag == "StickableCelling")
        {
            _isCellingStickable = false;
            _rb2D.gravityScale = 1;
        }
        else if (collision != null && collision.gameObject.tag == "DNA")
        {
            _scoreDNA += 1;
            Debug.Log(_characterState + " " + _scoreDNA);
            CheckEvolution();
        }
        else if (collision.gameObject.tag == "Destroyable")
        {
            _objInRange = null;
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


    private void RootController()
    {

    }
    private void RootStatusUpdate()
    {
        _rootObject.SetActive(true);
        _characterState = Estate.Root;
        _mainCamera.orthographicSize = _rootFOV;
        _characterPolyCollider2D = _rootPolyCollider2D;
    }

    private void AmibeController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1)
        {
            _characterAnim.SetBool("isWalking", true);
            #region Amibe Move Sounds
            if (_movementSoundTimeStamp >= _amibeMovementSoundDelay)
            {
                AudioManager.Instance.StartSound("S_SlugMovement");
                _movementSoundTimeStamp = 0;
            }
            else
            {
                _movementSoundTimeStamp += Time.deltaTime;
            }
            #endregion Amibe Move Sounds
        }
        else
        {
            _characterAnim.SetBool("isWalking", false);
            _movementSoundTimeStamp = _amibeMovementSoundDelay;
        }
        if (Input.GetKey(KeyCode.RightArrow) && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);

        }
        else if (Input.GetKey(KeyCode.UpArrow) && _isCellingStickable == true)
        {
            _rb2D.gravityScale = 0;
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
        _rootObject.SetActive(false);
        _amibeObject.SetActive(true);
        _mainCamera.orthographicSize = _amibeFOV;
        _characterPolyCollider2D = _amibePolyCollider2D;
        _sticking2DColliderBox = _amibeStickyCollider2D;
        _characterState = Estate.Amibe;
    }

    private void BatraController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 && Mathf.Abs(_rb2D.velocity.y) < 0.001f)
        {
            _characterAnim.SetBool("isWalking", true);

            #region Batra Walk Sounds
            if (_movementSoundTimeStamp >= _batraMovementSoundDelay) 
            {
                AudioManager.Instance.StartSound("S_Walk");
                _movementSoundTimeStamp = 0;
            }
            else
            {
                _movementSoundTimeStamp += Time.deltaTime;
            }
            #endregion Batra Walk Sounds
        }
        else
        {
            _movementSoundTimeStamp = _batraMovementSoundDelay; //For the Sounds
            _characterAnim.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.RightArrow) && _isStickable == true && _isCellingStickable == false || Input.GetKey(KeyCode.LeftArrow) && _isStickable == true && _isCellingStickable == false)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);

        }
        else if (Input.GetKey(KeyCode.UpArrow) && _isCellingStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
            _rb2D.gravityScale = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && _isCellingStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isCellingStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
            _rb2D.gravityScale = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && _isCellingStickable == true && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isCellingStickable == true && _isStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
            _rb2D.gravityScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _rb2D.gravityScale = 1;
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

            AudioManager.Instance.StartSound("S_Jump");
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
        _characterPolyCollider2D = _batraPolyCollider2D;
        _sticking2DColliderBox = _batraStickyCollider2D;
        _characterAnim = _batraAnim;
        _mainCamera.orthographicSize = _batraFOV;
        _characterState = Estate.Batra;
    }

    private void AviaController()
    {
        #region Movement
        var movement = Input.GetAxis("Horizontal");
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 && Mathf.Abs(_rb2D.velocity.y) < 0.001f)
        {
  

            _characterAnim.SetBool("isWalking", true);

            #region Avia Walk Sounds
            if (_movementSoundTimeStamp >= _batraMovementSoundDelay)
            {
                AudioManager.Instance.StartSound("S_WalkAvia");
                _movementSoundTimeStamp = 0;
            }
            else
            {
                _movementSoundTimeStamp += Time.deltaTime;
            }
            #endregion Avia Walk Sounds
        }
        else
        {
            _movementSoundTimeStamp = _aviaMovementSoundDelay; //For the Sounds

            _characterAnim.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.RightArrow) && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);

        }
        else if (Input.GetKey(KeyCode.UpArrow) && _isCellingStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
            _rb2D.gravityScale = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && _isCellingStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isCellingStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
            _rb2D.gravityScale = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && _isCellingStickable == true && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isCellingStickable == true && _isStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
            _rb2D.gravityScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _rb2D.gravityScale = 1;
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

            AudioManager.Instance.StartSound("S_JumpAvia");

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

        #region Hover
        if (Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(_rb2D.velocity.y) > 0.001f)
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
        _characterPolyCollider2D = _aviaPolyCollider2D;
        _sticking2DColliderBox = _aviaStickyCollider2D;
        _characterAnim = _aviaAnim;
        _mainCamera.orthographicSize = _aviaFOV;
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
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 && Mathf.Abs(_rb2D.velocity.y) < 0.001f)
        {
            _characterAnim.SetBool("isWalking", true);

            #region Chimera Walk Sounds
            if (_movementSoundTimeStamp >= _batraMovementSoundDelay)
            {
                AudioManager.Instance.StartSound("S_WalkChimera");
                _movementSoundTimeStamp = 0;
            }
            else
            {
                _movementSoundTimeStamp += Time.deltaTime;
            }
            #endregion Chimera Walk Sounds
        }
        else
        {
            _movementSoundTimeStamp = _chimeraMovementSoundDelay; //For the Sounds

            _characterAnim.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.RightArrow) && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);

        }
        else if (Input.GetKey(KeyCode.UpArrow) && _isCellingStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
            _rb2D.gravityScale = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && _isCellingStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isCellingStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
            _rb2D.gravityScale = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && _isCellingStickable == true && _isStickable == true || Input.GetKey(KeyCode.LeftArrow) && _isCellingStickable == true && _isStickable == true)
        {
            _rb2D.MovePosition(_rb2D.position + _upVelocity * Time.fixedDeltaTime);
            _rb2D.gravityScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _rb2D.gravityScale = 1;
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

            AudioManager.Instance.StartSound("S_JumpChimera");

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

        #region Hover
        if (Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(_rb2D.velocity.y) > 0.001f)
        {
            ChimeraHoverStatusUpdate();
        }
        else
        {
            ChimeraStatusUpdate();
        }
        #endregion

        #region Strike
        if (Input.GetKey(KeyCode.C))
        {
            _characterAnim.SetBool("isStriking", true);
            Destroy(_objInRange.gameObject);

            if (_objInRange == null) 
            { 
                if(_strikeSoundSecurity == false)
                {
                    AudioManager.Instance.StartSound("S_Attack");
                    _strikeSoundSecurity = true;
                    _timeStampAttack = 0f;
                }
                else
                {
                    if (_timeStampAttack >= 0.1f)
                    {
                        _strikeSoundSecurity = false;
                    }
                    _timeStampAttack += Time.deltaTime;
                }

            } 
            else
            {
                if (_strikeSoundSecurity == false)
                {
                    AudioManager.Instance.StartSound("S_Destruction");
                    _strikeSoundSecurity = true;
                    _timeStampAttack = 0f;
                }
                else
                {
                    if (_timeStampAttack >= 0.1f)
                    {
                        _strikeSoundSecurity = false;
                    }
                    _timeStampAttack += Time.deltaTime;
                }
            }
        }
        else
        {
            _characterAnim.SetBool("isStriking", false);
        }
        #endregion
    }
    private void ChimeraStatusUpdate()
    {
        _aviaObject.SetActive(false);
        _chimeraObject.SetActive(true);
        _rb2D.mass = _chimeraMass;
        _rb2D.drag = _chimeraLinearDrag;
        _jumpForce = _chimeraJumpForce;
        _characterPolyCollider2D = _chimeraPolyCollider2D;
        _sticking2DColliderBox = _chimeraStickyCollider2D;
        _characterAnim = _chimeraAnim;
        _mainCamera.orthographicSize = _chimeraFOV;
        _characterState = Estate.Chimera;
    }

    private void ChimeraHoverStatusUpdate()
    {
        _rb2D.drag = _chimeraHoverLinearDrag;
    }
    #endregion Methods

}
