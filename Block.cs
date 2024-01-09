using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Tile _tile;
    public Tile Tile { get { return _tile; } }

    [SerializeField] private Maquette _maquette;
    public Maquette Maquette { get {  return _maquette; } }

    [SerializeField] private MeshRenderer _meshRenderer;

    public void Setup(Tile tile)
    {
        _tile = tile;
        DiscreteUpdate();
    }

    public void DiscreteUpdate()
    {
        if (_maquette == null)
        {
            OpenMaquette(_tile.TileType.MaquettePrefab);
        }
        if (_maquette.Invoke != _tile.TileType.Invoke)
        {
            CloseMaquette();
        }
        if (_maquette == null)
        {
            OpenMaquette(_tile.TileType.MaquettePrefab);
        }

    }

    public void OpenMaquette(GameObject prefab)
    {
        GameObject maquetteObj = Instantiate(prefab, transform);
        Maquette maquette = maquetteObj.GetComponent<Maquette>();
        if (maquette == null) 
        {
            Destroy(maquetteObj);
            Debug.LogWarning("Maquette prefab contains no suitable script");
            return;
        }
        _maquette = maquette;
        _maquette.Open();
    }

    public void CloseMaquette()
    {
        if (_maquette != null) { _maquette.Close(); }
        _maquette = null;
    }

    private void OnMouseEnter()
    {
        Main.Instance.MouseEnter(this);
    }
    private void OnMouseExit()
    {
        Main.Instance.MouseExit(this);
    }
    private void OnMouseDown()
    {
        Main.Instance.MouseDown(this);
    }
}
