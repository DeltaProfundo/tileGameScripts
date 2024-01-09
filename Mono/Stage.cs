using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static Stage Instance;

    [Header("References")]
    [SerializeField] private Transform _elementContainer;
    [SerializeField] private Transform _layoutContainer;
    [SerializeField] private Transform _windowContainer;
    [SerializeField] private Transform _overlayContainer;

    [SerializeField] private List<UI> _activeElements = new List<UI>();
    public List<UI> ActiveElements { get { return _activeElements; } }
    [SerializeField] private UI _activeLayout;
    public UI ActiveLayout { get { return _activeLayout; } }    
    [SerializeField] private UI _activeWindow;
    public UI ActiveWindow { get { return _activeWindow; } }   
    [SerializeField] private UI _activeOverlay;
    public UI ActiveOverlay { get { return _activeOverlay; } } 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void DiscreteUpdate()
    {
        for (int i = 0; i < _activeElements.Count; i++)
        {
            _activeElements[i].DiscreteUpdate();
        }
        if (_activeLayout != null) { _activeLayout.DiscreteUpdate(); }
        if (_activeWindow != null) { _activeWindow.DiscreteUpdate(); }
        if (_activeOverlay != null) { _activeOverlay.DiscreteUpdate(); }
    }
    public void OpenUI(GameObject prefab, UIData data)
    {
        GameObject uiObj = Instantiate(prefab, transform);
        UI ui = uiObj.GetComponent<UI>();
        if (ui == null)
        {
            Destroy(uiObj);
            Debug.LogWarning("Stage : OpenUI : UI script not found");
            return;
        }
        switch (ui.Category)
        {
            case UI.Categories.element:
                _activeElements.Add(ui);
                break;
            case UI.Categories.layout:
                if (_activeLayout != null) { CloseUI(_activeLayout); }
                _activeLayout = ui;
                break;
            case UI.Categories.window:
                if (_activeWindow != null) { CloseUI(_activeWindow); }
                _activeWindow = ui;
                break;
            case UI.Categories.overlay:
                if (_activeOverlay != null) { CloseUI(_activeOverlay); }
                _activeOverlay = ui;
                break;
        }
        ui.Setup(data);
        ui.Open();
    }
    public void CloseUI(UI.Categories category)
    {
        switch (category)
        {
            case UI.Categories.element:
                for (int i = 0; i < _activeElements.Count; i++)
                {
                    UI element = _activeElements[i];
                    element.Close();
                }
                _activeElements.Clear();
                break;
            case UI.Categories.layout:
                if (_activeLayout != null) { _activeLayout.Close(); }
                _activeLayout = null;
                break;
            case UI.Categories.window:
                if (_activeWindow != null) { _activeWindow.Close(); }
                _activeWindow = null;
                break;
            case UI.Categories.overlay:
                if (_activeOverlay != null) { _activeOverlay.Close(); }
                _activeOverlay = null;
                break;
        }
    }

    public void CloseUI(UI ui)
    {
        switch(ui.Category)
        {
            case UI.Categories.element:
                if (!_activeElements.Contains(ui)) { return; }
                _activeElements.Remove(ui);                
                break;
            case UI.Categories.layout:
                if (_activeLayout == null) { return; }
                _activeLayout = null;
                break;
            case UI.Categories.window:
                if (_activeWindow == null) { return; }
                _activeWindow = null;
                break;
            case UI.Categories.overlay:
                if (_activeOverlay == null) { return; }
                _activeOverlay = null;
                break;
        }
        ui.Close();
    }
}
