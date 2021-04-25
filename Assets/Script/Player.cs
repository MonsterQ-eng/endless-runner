using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.EncryptedSave;
using DigitalRubyShared;
using UnityEngine.UI;
using System;
using GooglePlayGames;

public class Player : MonoBehaviour
{
    public Image doubleCoinUpgradeImage;
    public Image hearthUpgradeButtonImage;

    public CoinShower coinShower;

    public AchivmentyGoogle achivmentyGoogle;

    [SerializeField] private LayerMask platformlayerMask;

    [SerializeField] private float _speed = 0f;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider;
    [SerializeField] private int _lives = 3;
    public float jumpVelocity;
    private float coin;
    private float money;
    Vector2 startPoint;
    float distance;
    int intDistance;
    public float timeFaster = 30f;
    GameManager gameManager;
    UIManager uiManager;
    private bool DoOnce;
    int finalScore;

    private SwipeGestureRecognizer swipeGestureDown;
    private SwipeGestureRecognizer swipeGestureUp;
    private TapGestureRecognizer tapGesture;
    private float fJumpPressedRemeber = 0;
    private float fJumpPressedRemeberTime = 0.2f;
    private float fGroundedRemeber = 0;
    private float fGroundedRemeberTime = 0.25f;
    [SerializeField] private bool doubleJump = true;
    [SerializeField]
    private GameObject cloud;
    private bool swipeUp;
    public bool noDeath;
    public Sprite[] skinPrevieSprite;
    private int selected;
    public Text startText;
    public GameObject hearthUpgradeButton;
    public GameObject doubleCoinUpgrade;
    public GameObject upgradePanel;
    public Admob admob;
    public bool doubleCoin;
    public Text coinText;
    public bool fasterStart;
    public bool doubleScore;
    [SerializeField] private float timeScoreBoost;
    private float jumpBoostTime;
    private float ubreakableTime;
    private float coinMagnetTime;

    public GameObject fireCoin;
    public GameObject fireScore;

    bool OnceAnim;

    public GameObject fallingStrokes;

    // PROGRESS BAR

    public ProgressBar pbJumpBoost;
    bool doubleJumpTimeActive = false;
    float remainTime;
    public GameObject jumpBoostProgressBar;

    public ProgressBar pbUbreacableBoost;
    bool ubreacableBoostTimeActive = false;
    float ubreacableBoostremainTime;
    public GameObject ubreacableBoostProgressBar;

    public ProgressBar pbCoinMagnet;
    bool coinMagnetTimeActive = false;
    float coinMagnetRemainTime;
    public GameObject coinMagnetProgressBar;

    // END PROGRESS BAR

    //PopUP

    public GameObject popUP;
    public GameObject popUpCoin;

    //ParticleEnemy
    public GameObject particleEnemy;

    //ParticlePlayer
    public GameObject particleDooubleScore;

    public GameObject particleCoinMagnet;


    //achivmenty
    int enemyCounter;
    int boosterCounter;


    //trailrenderer
    public TrailRenderer tr;
    Gradient gradientRainbow;
    Gradient gradientWhite;
    GradientColorKey[] colorKeyRainbow;
    GradientColorKey[] colorKeyWhite;
    GradientAlphaKey[] alphaKeyRainbow;
    GradientAlphaKey[] alphaKeyWhite;

    //CoinMagnet
    public GameObject coinMagnetCollider;


    private IEnumerator doubleScoreTimeC;
    private IEnumerator jumpBoostTimeC;
    private IEnumerator UnbreakableC;
    private IEnumerator CoinMagnetC;


    public Sprite[] backgroundPrevieSprite;
    public SpriteRenderer background;
    private int selectedBackground;

    private bool beatHighscore;
    private int gamesPlayed;


    public MissionHandler msHandler;
    private int playerLevel;

    //Sound effect
    public AudioClip loseLife;
    public AudioClip collectBooster;
    public AudioClip gameOver;
    public AudioClip collectCoin;
    public AudioClip enemyKill;
    private AudioSource audiosource;


    //Shaders
    public Material[] skinsShader;

    public RuntimeAnimatorController unicornControl;
    public RuntimeAnimatorController defaultControl;

    // Start is called before the first frame update
    void Start()
    {
        msHandler = GameObject.Find("MissionHandler").GetComponent<MissionHandler>();
        enemyCounter = 0;
        boosterCounter = 0;
        OnceAnim = false;
        admob = GameObject.Find("AdmobManager").GetComponent<Admob>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        startPoint.x = -6;
        distance = 0f;
        _speed = 0;
        DoOnce = true;
        timeFaster = 30f;
        coin = 0f;
        money = ES_Save.Load<float>("money");
        selected = ES_Save.Load<int>("selectedSkinNEW");
        gameObject.GetComponent<SpriteRenderer>().sprite = skinPrevieSprite[selected];
        CreateSwipeGestureDown();
        doubleJump = true;
        upgradePanel.gameObject.SetActive(true);
        doubleCoin = false;
        fasterStart = false;
        doubleScore = false;
        swipeUp = ES_Save.Load<bool>("swipeup");
        if (swipeUp)
        {
            CreateSwipeGestureUp();
            startText.text = "Swipe to start!";
        }
        else
        {
            CreateTapGesture();
            startText.text = "Tap to start";
        }
        if (ES_Save.Exists("timeScoreBooster"))
        {
            Debug.Log("Finded timeScoreBooster");
            timeScoreBoost = ES_Save.Load<float>("timeScoreBooster");
        }
        else
        {
            Debug.Log("Create a timeScoreBooster");
            timeScoreBoost = 10f;
            ES_Save.Save(timeScoreBoost, "timeScoreBooster");
        }
        if (ES_Save.Exists("jumpBoosterTime2"))
        {
            Debug.Log("Finded jumpBoosterTime");
            jumpBoostTime = ES_Save.Load<float>("jumpBoosterTime2");
        }
        else
        {
            Debug.Log("Create a jumpBoosterTime");
            jumpBoostTime = 10f;
            ES_Save.Save(jumpBoostTime, "jumpBoosterTime2");
        }
        if (ES_Save.Exists("timeUbreakableBooster"))
        {
            Debug.Log("Finded timeUbreakableBooster");
            ubreakableTime = ES_Save.Load<float>("timeUbreakableBooster");
        }
        else
        {
            Debug.Log("Create a timeUbreakableBooster");
            ubreakableTime = 10f;
            ES_Save.Save(ubreakableTime, "timeUbreakableBooster");
        }

        if (ES_Save.Exists("timeCoinMagnet"))
        {
            Debug.Log("Finded timeCoinMagnet");
            coinMagnetTime = ES_Save.Load<float>("timeCoinMagnet");
        }
        else
        {
            Debug.Log("Create a timeCoinMagnet");
            coinMagnetTime = 10f;
            ES_Save.Save(coinMagnetTime, "timeCoinMagnet");
        }
        jumpVelocity = 20f;
        noDeath = false;



        gradientRainbow = new Gradient();
        colorKeyRainbow = new GradientColorKey[6];
        colorKeyRainbow[0].color = Color.red;
        colorKeyRainbow[0].time = 0.0f;
        colorKeyRainbow[1].color = Color.yellow;
        colorKeyRainbow[1].time = 0.20f;
        colorKeyRainbow[2].color = Color.green;
        colorKeyRainbow[2].time = 0.40f;
        colorKeyRainbow[3].color = Color.cyan;
        colorKeyRainbow[3].time = 0.60f;
        colorKeyRainbow[4].color = Color.blue;
        colorKeyRainbow[4].time = 0.80f;
        colorKeyRainbow[5].color = new Color32(255, 0, 255, 255);
        colorKeyRainbow[5].time = 1.0f;
        alphaKeyRainbow = new GradientAlphaKey[2];
        alphaKeyRainbow[0].alpha = 1.0f;
        alphaKeyRainbow[0].time = 0.0f;
        alphaKeyRainbow[1].alpha = 0.40f;
        alphaKeyRainbow[1].time = 0.80f;



        gradientWhite = new Gradient();
        colorKeyWhite = new GradientColorKey[2];
        colorKeyWhite[0].color = Color.white;
        colorKeyWhite[0].time = 0.0f;
        colorKeyWhite[1].color = Color.white;
        colorKeyWhite[1].time = 1.0f;
        alphaKeyWhite = new GradientAlphaKey[2];
        alphaKeyWhite[0].alpha = 1.0f;
        alphaKeyWhite[0].time = 0.0f;
        alphaKeyWhite[1].alpha = 0.40f;
        alphaKeyWhite[1].time = 0.80f;

        gradientWhite.SetKeys(colorKeyWhite, alphaKeyWhite);

        tr.colorGradient = gradientWhite;

        selectedBackground = ES_Save.Load<int>("selectedBackground");
        background.sprite = backgroundPrevieSprite[selectedBackground];

        beatHighscore = false;

        gamesPlayed = PlayerPrefs.GetInt("gamePlayed", 0);
        // MissionLoad();

        playerLevel = ES_Save.Load<int>("playerLevel");

        //doubleScoreTimeC = DoubleScoreTime(timeScoreBoost);
        //jumpBoostTimeC = JumpBoostTime(jumpBoostTime);
        //UnbreakableC = Unbreakable(ubreakableTime);
        //CoinMagnetC = CoinMagnet(coinMagnetTime);

        audiosource = GetComponent<AudioSource>();

        gameObject.GetComponent<SpriteRenderer>().material = skinsShader[selected];

        if(selected == 12)
        {
            gameObject.GetComponent<Animator>().runtimeAnimatorController = unicornControl;
        }
        else
        {
            gameObject.GetComponent<Animator>().runtimeAnimatorController = defaultControl;
        }

    }

    // Update is called once per frame
    private void Update()
    {


        coinMagnetCollider.transform.position = new Vector2(transform.position.x, transform.position.y);

        Movement();
        Distances();
        if (fasterStart)
        {
            Faster();
        }
        else
        {
            timeFaster += 1 * Time.deltaTime;
        }

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



        if (doubleJumpTimeActive)
        {
            remainTime -= Time.deltaTime;
            float percent = (remainTime / jumpBoostTime) * 100;
            pbJumpBoost.BarValue = percent;
        }
        if (ubreacableBoostTimeActive)
        {
            ubreacableBoostremainTime -= Time.deltaTime;
            float percent = (ubreacableBoostremainTime / ubreakableTime) * 100;
            pbUbreacableBoost.BarValue = percent;
        }
        if (coinMagnetTimeActive)
        {
            coinMagnetRemainTime -= Time.deltaTime;
            float percent = (coinMagnetRemainTime / coinMagnetTime) * 100;
            pbCoinMagnet.BarValue = percent;
        }



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
            _speed = 7f;
            DoOnce = false;
            upgradePanel.gameObject.SetActive(false);
            fasterStart = true;
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
    private void SwipeGestureCallbackUp(GestureRecognizer gesture)
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
            _speed = 7f;
            DoOnce = false;
            upgradePanel.gameObject.SetActive(false);
            fasterStart = true;
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

    private void CreateSwipeGestureUp()
    {
        swipeGestureUp = new SwipeGestureRecognizer();
        swipeGestureUp.MinimumDistanceUnits = 0.3f;
        swipeGestureUp.MinimumSpeedUnits = 0.3f;
        swipeGestureUp.Direction = SwipeGestureRecognizerDirection.Up;
        swipeGestureUp.StateUpdated += SwipeGestureCallbackUp;
        swipeGestureUp.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
        FingersScript.Instance.AddGesture(swipeGestureUp);
    }

    public void Faster()
    {

        if (_lives >= 1)
        {
            if (Time.timeSinceLevelLoad >= timeFaster)
            {
                _speed += .1f;
                timeFaster += 5;
            }
        }

    }
    private void Movement()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, platformlayerMask);
        return raycastHit2D.collider != null;
    }

    public void Damage(int type)
    {


        if (_lives > 0 && noDeath == false)
        {
            _lives -= 1;
            uiManager.UpdateLives(_lives);
            StartCoroutine(ImTrying());
            audiosource.PlayOneShot(loseLife);
        }
        else
        {
            if (noDeath && type == 0)
            {
                if (doubleCoin)
                {
                    coin += 20;
                    PopUpSpawn();
                    enemyCounter += 1;

                }
                else
                {
                    coin += 10;
                    PopUpSpawn();
                    enemyCounter += 1;
                }
                msHandler.Mission8();
                audiosource.PlayOneShot(enemyKill);
            }
        }
        if (_lives == 0)
        {
            _speed = 0;
            LoadPlayerData();
            finalScore = ES_Save.Load<int>("finalscore");
            if (finalScore < intDistance)
            {
                finalScore = intDistance;
                ES_Save.Save<int>(finalScore, "finalscore");
                uiManager.NewBest();
                beatHighscore = true;
                msHandler.Mission6();
            }
            long scoreGP = Convert.ToInt64(intDistance);
            GooglePlayScript.PostScore(scoreGP);
            ScoreAchiv(intDistance);
            timeFaster = 30;

            gamesPlayed += 1;
            PlayerPrefs.SetInt("gamePlayed", gamesPlayed);
            PlayerPrefs.SetInt("ChallengeItems", PlayerPrefs.GetInt("ChallengeItems", 0) + 1);

            audiosource.PlayOneShot(gameOver);

            msHandler.Mission7();
            
            StopCoroutine(ImTrying());

            jumpBoostProgressBar.SetActive(false);
            ubreacableBoostProgressBar.SetActive(false);
            coinMagnetProgressBar.SetActive(false);

            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
            StartCoroutine(WiatforMoney());
            uiManager.DeadUI();
            gameManager.OnDeath();
            StartCoroutine(ADWaiter());
            SavePlayerData();
            MoneyAciv(money);
            tr.gameObject.SetActive(false);
            particleDooubleScore.SetActive(false);
            achivmentyGoogle.EnemyAchiv(enemyCounter);
            achivmentyGoogle.BoosterAchiv(boosterCounter);
            msHandler.MissionDoIt();
            msHandler.CheckAllMission();
            //MissionDoIt();
            //noDead = true;
        }
    }

    IEnumerator ADWaiter()
    {
        yield return new WaitForSeconds(0.65f);
        admob.ShowAd();
    }


    IEnumerator ImTrying()
    {
        noDeath = true;
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
        noDeath = false;
    }

    IEnumerator WiatforMoney()
    {
        while (true)
        {
            money += coin;
            yield return new WaitForSeconds(10);
        }
    }

    private void MoneyAciv(float money)
    {
        if (Social.localUser.authenticated)
        {
            if (money >= 100)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQCA", 100.0f, (bool success) => { Debug.Log(success); });
            }
            if (money >= 1000)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQCQ", 100.0f, (bool success) => { Debug.Log(success); });
            }

            if (money >= 10000)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQCg", 100.0f, (bool success) => { Debug.Log(success); });
            }
            if (money >= 100000)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQCw", 100.0f, (bool success) => { Debug.Log(success); });
            }
            if (money >= 1000000)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQDA", 100.0f, (bool success) => { Debug.Log(success); });
            }
        }
    }

    private void ScoreAchiv(int score)
    {
        if (Social.localUser.authenticated)
        {
            if (score >= 100)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQDQ", 100.0f, (bool success) => { Debug.Log(success); });
            }
            if (score >= 1000)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQDg", 100.0f, (bool success) => { Debug.Log(success); });
            }
            if (score >= 2000)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQDw", 100.0f, (bool success) => { Debug.Log(success); });
            }
            if (score >= 5000)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQEA", 100.0f, (bool success) => { Debug.Log(success); });
            }
            if (score >= 10000)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQEQ", 100.0f, (bool success) => { Debug.Log(success); });
            }
            if (score >= 50000)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI9IfZjt8OEAIQEg", 100.0f, (bool success) => { Debug.Log(success); });
            }
        }

    }


    public void SavePlayerData()
    {
        ES_Save.Save<float>(money, "money");
    }
    public void LoadPlayerData()
    {
        finalScore = ES_Save.Load<int>("finalscore");
        money = ES_Save.Load<float>("money");
    }

    public float AllMoney()
    {
        return money;
    }

    public void Money(int mo)
    {
        if (doubleCoin)
        {
            coin += mo * 2;         
        }
        else
        {
            coin += mo;
        }
        if(mo == 1)
        {
            PopUpCoins();
        }
        else
        {
            PopUpCoinsPlat();
        }
        
        audiosource.PlayOneShot(collectCoin);
        //msHandler.Mission2(doubleCoin, 0);
    }
    public void MoneyEnemy()
    {
        if (doubleCoin)
        {
            coin += 20;
            PopUpSpawn();
            enemyCounter += 1;

        }
        else
        {
            coin += 10;
            PopUpSpawn();
            enemyCounter += 1;
        }        
        msHandler.Mission8();
        audiosource.PlayOneShot(enemyKill);
    }

    public GameObject popUpCoinPlat;

    private void PopUpCoins()
    {
        Instantiate(popUpCoin, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
    }
    private void PopUpCoinsPlat()
    {
        Instantiate(popUpCoinPlat, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
    }

    private void PopUpSpawn()
    {
        Instantiate(popUP, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
        Instantiate(particleEnemy, new Vector2(transform.position.x + 1f, transform.position.y), Quaternion.identity);
    }


    public float HowMoney()
    {
        return coin;
    }

    public int HowBoosterCounter()
    {
        return boosterCounter;
    }

    public int HowEnemyCounter()
    {
        return enemyCounter;
    }

    public float Live()
    {
        return _lives;
    }

    public void RefreshLevel()
    {
        playerLevel = ES_Save.Load<int>("playerLevel");
    }


    public void Distances()
    {
        Vector3 myPosition = transform.position;
        myPosition.y = 0;
        startPoint.y = 0;
        if (doubleScore)
        {
            distance += _speed * Time.deltaTime * 2 * playerLevel;
        }
        else
        {
            distance += _speed * Time.deltaTime * playerLevel;         
        }
        intDistance = Mathf.RoundToInt(distance);
        //msHandler.Mission0(doubleScore);
    }
    public float Distance()
    {
        return intDistance;
    }

    public float Speed()
    {
        return _speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("doubleScore"))
        {
            Debug.Log("Get DoubleScore");
            boosterCounter += 1;
            Destroy(other.gameObject);
            audiosource.PlayOneShot(collectBooster);
            if (doubleScoreTimeC != null)
            {
                StopCoroutine(doubleScoreTimeC);
                doubleScoreTimeC = null;
                fireScore.SetActive(false);
                doubleScore = false;
                particleDooubleScore.SetActive(false);
            }
           
                doubleScoreTimeC = DoubleScoreTime(timeScoreBoost);
                StartCoroutine(doubleScoreTimeC);
                particleDooubleScore.SetActive(true);
                      
            msHandler.Mission4();
            
        }
        if (other.gameObject.CompareTag("jumpBoost"))
        {
            Debug.Log("Get jumpBoost");
            boosterCounter += 1;
            Destroy(other.gameObject);
            audiosource.PlayOneShot(collectBooster);
            if (jumpBoostTimeC != null)
            {
                StopCoroutine(jumpBoostTimeC);
                jumpBoostTimeC = null;
                jumpVelocity = 20f;
                doubleJumpTimeActive = false;
                jumpBoostProgressBar.SetActive(false);
            }
            jumpBoostTimeC = JumpBoostTime(jumpBoostTime);
            StartCoroutine(jumpBoostTimeC);
            doubleJumpTimeActive = true;
            remainTime = jumpBoostTime;
            jumpBoostProgressBar.SetActive(true);
            msHandler.Mission4();
        }
        if (other.gameObject.CompareTag("UnbreakableBooster"))
        {
            Debug.Log("Get UnbreakableBooster");
            boosterCounter += 1;
            Destroy(other.gameObject);
            audiosource.PlayOneShot(collectBooster);
            if (UnbreakableC != null)
            {
                StopCoroutine(UnbreakableC);
                UnbreakableC = null;
                noDeath = false;
                ubreacableBoostTimeActive = false;
                ubreacableBoostProgressBar.SetActive(false);
                tr.material = new Material(Shader.Find("Sprites/Default"));
                tr.colorGradient = gradientWhite;
                tr.time = 0.6f;
            }
            UnbreakableC = Unbreakable(ubreakableTime);
            StartCoroutine(UnbreakableC);
            ubreacableBoostTimeActive = true;
            ubreacableBoostremainTime = ubreakableTime;
            ubreacableBoostProgressBar.SetActive(true);

            gradientRainbow.SetKeys(colorKeyRainbow, alphaKeyRainbow);
            tr.material = new Material(Shader.Find("Sprites/Default"));
            tr.colorGradient = gradientRainbow;
            tr.time = 0.8f;

            msHandler.Mission4();
        }
        if (other.gameObject.CompareTag("coinMagnet"))
        {
            Debug.Log("Get coin magnet!");
            boosterCounter += 1;
            Destroy(other.gameObject);
            audiosource.PlayOneShot(collectBooster);
            if (CoinMagnetC != null)
            {
                StopCoroutine(CoinMagnetC);
                CoinMagnetC = null;
                coinMagnetCollider.SetActive(false);
                coinMagnetTimeActive = false;
                coinMagnetProgressBar.SetActive(false);
                particleCoinMagnet.SetActive(false);
            }
            CoinMagnetC = CoinMagnet(coinMagnetTime);
            StartCoroutine(CoinMagnetC);
            particleCoinMagnet.SetActive(true);
            coinMagnetTimeActive = true;
            coinMagnetRemainTime = coinMagnetTime;
            coinMagnetProgressBar.SetActive(true);

            msHandler.Mission4();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeathZone"))
        {
            _lives = 0;
            this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            gameManager.OnDeath();
            this.Damage(2);
            GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(this.gameObject, 1);
        }
    }


    public void HeartUpgrade()
    {
        if (money > 250)
        {
            money -= 250;
            SavePlayerData();
            _lives = 4;
            uiManager.UpdateLives(_lives);
            hearthUpgradeButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
            hearthUpgradeButtonImage.color = new Color32(255, 255, 255, 155);

        }
        else
        {
            StartCoroutine(NotMoneyText());
        }
    }

    IEnumerator NotMoneyText()
    {
        startText.text = "Not enought money!";
        startText.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(3);
        if (swipeUp)
        {
            startText.text = "Swipe to start";
        }
        else
        {
            startText.text = "Tap to start";
        }
        
        startText.color = new Color32(255, 255, 255, 255);
    }


    public void AdNotLoad()
    {
        StartCoroutine(AdNotLoadC());
    }

    IEnumerator AdNotLoadC()
    {
        startText.text = "Ad not loaded yet!";
        startText.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(3);
        if (swipeUp)
        {
            startText.text = "Swipe to start";
        }
        else
        {
            startText.text = "Tap to start";
        }
        startText.color = new Color32(255, 255, 255, 255);
    }

    public void FailToLoad()
    {
        StartCoroutine(FaildLoadAds());
    }

    IEnumerator FaildLoadAds()
    {
        startText.text = "Ad's faild to load!";
        startText.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(3);
        if (swipeUp)
        {
            startText.text = "Swipe to start";
        }
        else
        {
            startText.text = "Tap to start";
        }
        startText.color = new Color32(255, 255, 255, 255);
    }


    public void RewardText()
    {
        StartCoroutine(Rewarded());
        //coinText.color = new Color32(255, 0, 0, 255);
        doubleCoinUpgrade.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        doubleCoinUpgradeImage.color = new Color32(255, 255, 255, 155);
        doubleCoin = true;
        fireCoin.SetActive(true);
    }

    IEnumerator Rewarded()
    {
        startText.text = "Succes! Reward Applied!";
        startText.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(3);
        if (swipeUp)
        {
            startText.text = "Swipe to start";
        }
        else
        {
            startText.text = "Tap to start";
        }
        startText.color = new Color32(255, 255, 255, 255);
    }


    public void AdClosed()
    {
        doubleCoinUpgrade.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        doubleCoinUpgradeImage.color = new Color32(255, 255, 255, 155);
    }


    IEnumerator DoubleScoreTime(float time)
    {
        doubleScore = true;
        fireScore.SetActive(true);
        yield return new WaitForSeconds(time);
        fireScore.SetActive(false);
        doubleScore = false;
        particleDooubleScore.SetActive(false);
        
    }

    IEnumerator JumpBoostTime(float time)
    {
        jumpVelocity = 30f;
        yield return new WaitForSeconds(time);
        jumpVelocity = 20f;
        doubleJumpTimeActive = false;
        jumpBoostProgressBar.SetActive(false);
    }

    IEnumerator Unbreakable(float time)
    {
        noDeath = true;
        yield return new WaitForSeconds(time);
        noDeath = false;
        ubreacableBoostTimeActive = false;
        ubreacableBoostProgressBar.SetActive(false);
        tr.material = new Material(Shader.Find("Sprites/Default"));
        tr.colorGradient = gradientWhite;
        tr.time = 0.6f;
    }

    IEnumerator CoinMagnet(float time)
    {
        coinMagnetCollider.SetActive(true);
        yield return new WaitForSeconds(time);
        coinMagnetCollider.SetActive(false);
        coinMagnetTimeActive = false;
        coinMagnetProgressBar.SetActive(false);
        particleCoinMagnet.SetActive(false);
    }

    public GameObject popUpMission;
    public Text popUpMissionText;

    public void MissionCompleted(int number)
    {
        popUpMission.SetActive(true);
        popUpMission.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(popUpMission, new Vector3(1, 1, 1), 0.3f).setEaseInCubic();
        popUpMissionText.text = "Mission " + number + " completed!";
        LeanTween.scale(popUpMission, new Vector3(0, 0, 0), 0.3f).setEaseInOutCubic().setDelay(2).setOnComplete(() => popUpMission.SetActive(false));
    }

    public void LevelUP()
    {
        popUpMission.SetActive(true);
        popUpMission.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(popUpMission, new Vector3(1, 1, 1), 0.3f).setEaseInCubic();
        popUpMissionText.text = "Level UP " + playerLevel;
        LeanTween.scale(popUpMission, new Vector3(0, 0, 0), 0.3f).setEaseInOutCubic().setDelay(2).setOnComplete(() => popUpMission.SetActive(false));

    }



}
