using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;

    [Header("State Vars")]
    [SerializeField] private Player _player;
    public Player Player { get { return _player; } }
    [SerializeField] private Block _selectedBlock;
    public Block SelectedBlock { get { return _selectedBlock; } }
    [SerializeField] private Block _pointedBlock;
    public Block PointedBlock { get { return _pointedBlock; } }

    [Header("Settings")]
    [SerializeField] private Vector2Int _gridSize;
    public Vector2Int GridSize { get { return _gridSize; } }
    [SerializeField] private TileType[] _defaultTileTypes;
    public TileType[] DefaultTileTypes { get { return _defaultTileTypes; } }
    [SerializeField] private Stuff[] _startingStuff;
    public Stuff[] StartingStuf { get { return _startingStuff; } }

    [Header("References")]
    [SerializeField] private GameObject _selectionBox;
    private Rig _rig;
    private TileDaimon _tileDaimon;
    public TileDaimon TileDaimon { get { return _tileDaimon; } }
    private TimeDaimon _timeDaimon;
    private BlockDaimon _blockDaimon;    
    public BlockDaimon BlockDaimon { get { return _blockDaimon; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    void Start()
    {
        _player = new Player(_startingStuff);
        _rig = FindObjectOfType<Rig>();
        _tileDaimon = GetComponent<TileDaimon>();
        _timeDaimon = GetComponent<TimeDaimon>();
        _blockDaimon = GetComponent<BlockDaimon>();        
        _tileDaimon.GenerateTiles(_gridSize, _defaultTileTypes);
        _blockDaimon.GenerateBlocks(_tileDaimon.Tiles);
        Stage.Instance.OpenUI(Ubik.Instance.MainLayoutPrefab, new UIData());
        _rig.SetConstraints(new Vector2(0, _gridSize.x), new Vector2(0, _gridSize.y));
    }

    private void Update()
    {
        switch (Ubik.Instance.GameState)
        {
            case Ubik.GameStates.paused:
                break;
            case Ubik.GameStates.running:
                ManageInput();
                break;
        }
    }

    public void DiscreteUpdate()
    {
        MainLayout mainLayout = FindObjectOfType<MainLayout>();
        if (mainLayout == null)
        {
            return;
        }
        string text = "";
        if (_selectedBlock != null)
        {
            text = "GridPos : " + _selectedBlock.Tile.GridPos.ToString();
        }
        else if (_pointedBlock != null)
        {
            text = "GridPos : " + _pointedBlock.Tile.GridPos.ToString();
        }
        UIData newData = new UIData();
        newData.TextSpa = text;
        newData.TextEng = text;
        mainLayout.DiscreteUpdate(newData);
        Stage.Instance.DiscreteUpdate();
    }

    public void ManageInput()
    {
        float rotate = Input.GetAxis("Rotate");
        if (Mathf.Abs(rotate) > 0.1f)
        {
            _rig.RotateCamera(rotate);
        }
        Vector2 cameraMovement = new Vector2();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            cameraMovement.x = horizontal;
        }
        if (Mathf.Abs(vertical) > 0.1f)
        {
            cameraMovement.y = vertical;
        }
        _rig.MoveCamera(cameraMovement);

        if (Input.GetMouseButtonDown(1))
        {
            _selectedBlock = null;
            DiscreteUpdate();
        }
        float zoom = Input.GetAxis("Zoom");
        if (zoom > 0.1f) { _rig.DecreaseOrthographicSize(Mathf.Abs(zoom)); }
        else if (zoom < -0.1f) { _rig.IncreaseOrthographicSize(Mathf.Abs(zoom)); }
    }

    public void Tick()
    {
        _tileDaimon.Tick();
        DiscreteUpdate();
    }

    public void MouseEnter(Block block)
    {
        _pointedBlock = block;
        if (_selectedBlock == null)
        {
            _selectionBox.transform.position = block.transform.position;
        }
        else
        {
            _selectionBox.transform.position = _selectedBlock.transform.position;
        }
        
        DiscreteUpdate();
    }
    public void MouseExit(Block block)
    {
        if (block == _pointedBlock) 
        {
            _pointedBlock = null; 
        }
        DiscreteUpdate();
    }
    public void MouseDown(Block block)
    {
        _selectedBlock = block;
        _selectionBox.transform.position = block.transform.position + new Vector3(0f, 0.25f, 0f);
        DiscreteUpdate();
    }

    public void OpenSettingsWindow()
    {
        if (Stage.Instance.ActiveWindow == null) 
        { 
            Stage.Instance.OpenUI(Ubik.Instance.SettingsWindowPrefab, new UIData());
            return;
        }
        if (Stage.Instance.ActiveWindow.GetComponent<SettingsWindow>() == null)
        {
            Stage.Instance.CloseUI(UI.Categories.window);
            return;
        }
    }

    public void SetTimeFactor(float factor)
    {
        _timeDaimon.Factor = factor;
    }

}
