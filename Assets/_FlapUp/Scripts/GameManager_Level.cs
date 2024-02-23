using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using SgLib;

public enum GameStateLevel
{
    Prepare,
    Playing,
    Paused,
    PreGameOver,
    GameOver
}

public class GameManager_Level : MonoBehaviour
{
    public static GameManager_Level Instance { get; private set; }

    public static event System.Action<GameState, GameState> GameStateChanged = delegate { };

    public GameStateLevel _gameStateLevel
    {
        get
        {
            return _gameStateLevel;
        }
        private set
        {
            if (value != _gameStateLevel)
            {
                GameState oldState = _gameState;
                _gameStateLevel = value;

                GameStateChanged(_gameState, oldState);
            }
        }
    }

    private GameState _gameState = GameState.Prepare;

    public static int GameCount
    {
        get { return _gameCount; }
        private set { _gameCount = value; }
    }

    private static int _gameCount = 0;

    [Header("Set the target frame rate for this game")]
    public int targetFrameRate = 60;

    [Header("Gameplay Preferences")]
    public UIManager uIManager;
    public GameObject goldPrefab;
    public GameObject parentPlayer;
    public GameObject theGround;
    

    [Header("Gameplay Config")]
    public int initialObstacle;
    //How many obstacle you create when the game start
    public int space;
    //Space between 2 obstacle

    /*when the score higher than this value, 
    in this case is 5, that mean player jump over 5 obstacle, 
    the first obstacle will be destroyed (the obstacle you create for the first time) 
    and the ground will be moved to the position of the obstacle that you destroyed. 
    After that, this value will automatically counting. */
    public int obstacleCounter = 4;

    public float maxObstacleFluctuationRange = 4;
    //Max moving flutuation range of obstacle
    public float minObstacleFluctuationRange = 3;
    //Min moving flutuation range of obstacle
    public int scoreToUpdateValue = 10;
    //When you reached this score, onstacle speed will be decrease
    public float decreaseObstacleSpeedValue = 0.05f;
    //Obstacle speed will be minus by this value
    public float minObstacleSpeedFactor = 1f;
    // Min obstacle speed factor
    public float maxObstacleSpeedFactor = 1.5f;
    //Max obstacle speed factor
    public float minimumMinObstacleSpeedFactor = 0.4f;
    //Limited of min obstacle speed factor
    public float minimumMaxObstacleSpeedFactor = 0.7f;
    //Limited of max ofstacle speed factor
    [Range(0f, 1f)]
    public float goldFrequecy;

    private List<GameObject> listObstacle = new List<GameObject>();
    private GameObject obstaclePrefab;
    private GameObject currentObstacle;
    private Vector3 obstaclePosition;
    private Vector3 addedPosition;
    private bool hasCheckedScore = false;
    private int listIndex = 0;
    private int listDestroyIndex = 0;
    float randomizeX;
    void OnEnable()
    {
        PlayerController.PlayerDied += PlayerController_PlayerDied;
    }

    void OnDisable()
    {
        PlayerController.PlayerDied -= PlayerController_PlayerDied;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(Instance.gameObject);
            Instance = this;
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void PlayerController_PlayerDied()
    {
        GameOver();
    }

    void Start()
    {
        _gameStateLevel = GameStateLevel.Prepare;
        Application.targetFrameRate = targetFrameRate;
        ScoreManager.Instance.Reset();

        
    }
    private void Update()
    {
      
    }
    public void StartGame()
    {
        _gameStateLevel = GameStateLevel.Playing;

        if (SoundManager.Instance.background != null)
            SoundManager.Instance.PlayMusic(SoundManager.Instance.background);
    }

    public void GameOver()
    {
        _gameState = GameState.GameOver;
        GameCount++;

        if (SoundManager.Instance.background != null)
            SoundManager.Instance.StopMusic();
    }

    public void RestartGame(float delay = 0)
    {
        StartCoroutine(CRRestartGame(delay));
    }

    IEnumerator CRRestartGame(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
