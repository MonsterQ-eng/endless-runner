using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using UnityEngine.SceneManagement;
using QuantumTek.EncryptedSave;

public class PlayerTut : MonoBehaviour
{

    [SerializeField] private LayerMask platformlayerMask;
    private SwipeGestureRecognizer swipeGestureDown;
    private TapGestureRecognizer tapGesture;
    private float fJumpPressedRemeber = 0;
    private float fJumpPressedRemeberTime = 0.2f;
    private float fGroundedRemeber = 0;
    private float fGroundedRemeberTime = 0.25f;
    [SerializeField] private bool doubleJump = true;
    [SerializeField]
    private GameObject cloud;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider;
    public float jumpVelocity;
    private bool OnceAnim;
    private float _speed;
    private bool DoOnce;

    public GameObject popUpCoin;
    public GameObject popUP;
    public GameObject enemyParticle;

    public GameObject deadZone;

    public GameObject canvasTuto;

    private bool tutON;

    private AudioSource audioSource;
    public AudioClip spikeClip;
    public AudioClip enemyClip;
    public AudioClip moneyClip;
    public AudioClip endGameClip;
    public AudioClip uiClip;

    private void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        _speed = 0;
        DoOnce = true;
        CreateSwipeGestureDown();
        CreateTapGesture();
        jumpVelocity = 20f;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Movement();

        if (IsGrounded())
        {
            GetComponent<Animator>().SetBool("IsGrounded", true);
            GetComponent<Animator>().SetBool("isJumping", false);
            rigidbody2d.gravityScale = 3;
            doubleJump = true;

        }
        else
        {
            GetComponent<Animator>().SetBool("IsGrounded", false);
            GetComponent<Animator>().SetBool("isJumping", true);
        }

        if (IsGrounded() && OnceAnim)
        {

            OnceAnim = false;
        }

        fGroundedRemeber -= Time.deltaTime;
        if (IsGrounded())
        {
            fGroundedRemeber = fGroundedRemeberTime;
        }

        fJumpPressedRemeber -= Time.deltaTime;




    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeathZone"))
        {
            audioSource.PlayOneShot(endGameClip);
            this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //GetComponent<BoxCollider2D>().enabled = false;
            _speed = 0f;
            deadZone.SetActive(true);
            tutON = false;
            ES_Save.Save(tutON, "tutON");
        }
    }

    private void Movement()
    {
        
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void TapGestureCallback(GestureRecognizer gesture)
    {

        if (gesture.State == GestureRecognizerState.Ended)
        {
            fJumpPressedRemeber = fJumpPressedRemeberTime;
        }

        if (gesture.State == GestureRecognizerState.Ended)
        {
            if ((fJumpPressedRemeber > 0) && (fGroundedRemeber > 0))
            {
                GetComponent<Animator>().SetTrigger("Jump");
                OnceAnim = true;
                rigidbody2d.velocity = Vector2.up * jumpVelocity;


            }
            else if (doubleJump)
            {
                Instantiate(cloud, transform.position, Quaternion.identity);
                rigidbody2d.velocity = Vector2.up * jumpVelocity;
                GetComponent<Animator>().SetTrigger("Jump");
                doubleJump = false;
            }
        }
        if (gesture.State == GestureRecognizerState.Ended && DoOnce)
        {
            Debug.Log("Start game!");
            _speed = 5f;
            DoOnce = false;
        }
    }

    private void CreateTapGesture()
    {
        tapGesture = new TapGestureRecognizer();
        tapGesture.StateUpdated += TapGestureCallback;
        FingersScript.Instance.AddGesture(tapGesture);
    }

    private void SwipeGestureCallbackDown(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            Debug.Log("Swipe!");

            rigidbody2d.gravityScale = 15;
        }
    }

    private void CreateSwipeGestureDown()
    {
        swipeGestureDown = new SwipeGestureRecognizer();
        swipeGestureDown.MinimumDistanceUnits = 0.3f;
        swipeGestureDown.MinimumSpeedUnits = 0.3f;
        swipeGestureDown.Direction = SwipeGestureRecognizerDirection.Down;
        swipeGestureDown.StateUpdated += SwipeGestureCallbackDown;
        swipeGestureDown.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
        FingersScript.Instance.AddGesture(swipeGestureDown);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, platformlayerMask);
        return raycastHit2D.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            audioSource.PlayOneShot(spikeClip);

            Destroy(collision.gameObject);
            StartCoroutine(ImTrying());
        }

        if (collision.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(moneyClip);

            Destroy(collision.gameObject);
            PopUpCoins();
        }

        if (collision.CompareTag("EnemyWhite"))
        {
            audioSource.PlayOneShot(enemyClip);

            Destroy(collision.gameObject);
            PopUpSpawn();
        }

        if (collision.CompareTag("EnemyDot"))
        {
            audioSource.PlayOneShot(enemyClip);

            Destroy(collision.gameObject);
            PopUpSpawn();
        }

        if (collision.CompareTag("EnemyCac"))
        {
            audioSource.PlayOneShot(enemyClip);

            StartCoroutine(ImTrying());
        }
    }

    private void PopUpSpawn()
    {
        Instantiate(popUP, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
        Instantiate(enemyParticle, new Vector2(transform.position.x + 1f, transform.position.y), Quaternion.identity);
    }

    private void PopUpCoins()
    {
        Instantiate(popUpCoin, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
    }

    IEnumerator ImTrying()
    {
        
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 155);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 155);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 155);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 155);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        
    }

    public void LoadMainMenu()
    {
        audioSource.PlayOneShot(uiClip);

        LeanTween.scale(canvasTuto, new Vector3(0, 0, 0), 0.6f).setOnComplete(() => SceneManager.LoadScene(0));
    }
    public void LoadGame()
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(uiClip);

        LeanTween.scale(canvasTuto, new Vector3(0, 0, 0), 0.6f).setOnComplete(() => SceneManager.LoadScene(2));
    }
}
