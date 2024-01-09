using UnityEngine;

public class Ubik : MonoBehaviour
{
    public enum GameStates { running, paused }
    public enum GameLanguages { english, spanish }

    public static Ubik Instance;

    [Header("Settings")]
    [SerializeField] private GameStates _gameState;
    public GameStates GameState { get { return _gameState; } }
    [SerializeField] private GameLanguages _gameLanguage;
    public GameLanguages GameLanguage { get { return _gameLanguage; } }
    [SerializeField] private float _zeroTimeFactor;
    public float ZeroTimeFactor { get { return _zeroTimeFactor; } }
    [SerializeField] private float _slowTimeFactor;
    public float SlowTimeFactor { get { return _slowTimeFactor; } }
    [SerializeField] private float _fastTimeFactor;
    public float FastTimeFactor { get { return _fastTimeFactor; } }
    [SerializeField] private float _veryFastTimeFactor;
    public float VeryFastTimeFactor { get { return _veryFastTimeFactor; } }


    [Header("Serialized Resources")]
    [SerializeField] private TileType[] _tileTypes;
    public TileType[] TileTypes { get { return _tileTypes; } }
    [SerializeField] private StuffType[] _stuffTypes;
    public StuffType[] StuffTypes { get { return _stuffTypes; } }

    [Header("Non-Serialized Resources")]
    [SerializeField] private GameObject _blockPrefab;
    public GameObject BlockPrefab { get { return _blockPrefab; } }
    [SerializeField] private GameObject _mainLayoutPrefab;
    public GameObject MainLayoutPrefab { get { return _mainLayoutPrefab; } }
    [SerializeField] private GameObject _stuffBoxPrefab;
    public GameObject StuffBoxPrefab { get { return _stuffBoxPrefab; } }
    [SerializeField] private GameObject _genericButtonPrefab;
    public GameObject GenericButtonPrefab { get { return _genericButtonPrefab; } }
    [SerializeField] private GameObject _settingWindowPrefab;
    public GameObject SettingsWindowPrefab { get { return _settingWindowPrefab;} }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadSerializedResources();
    }

    public void LoadSerializedResources()
    {
        _tileTypes = Resources.LoadAll<TileType>("TileTypes");
        _stuffTypes = Resources.LoadAll<StuffType>("StuffTypes");
    }

    public void SwitchLanguage()
    {
        switch (_gameLanguage)
        {
            case GameLanguages.english:
                _gameLanguage = GameLanguages.spanish;
                break;
            case GameLanguages.spanish:
                _gameLanguage = GameLanguages.english;
                break;
        }
    }
}
 
