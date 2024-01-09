using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsWindow : UI
{
    [Header("Settings")]
    [SerializeField] private string _traxTextEng;
    [SerializeField] private string _traxTextSpa;
    [SerializeField] private string _fxTextEng;
    [SerializeField] private string _fxTextSpa;

    [Header("References")]
    [SerializeField] private GenericButton[] _buttons;
    [SerializeField] private TextMeshProUGUI _traxLabel;
    [SerializeField] private TextMeshProUGUI _fxLabel;
    [SerializeField] private Slider _traxSlider;
    [SerializeField] private Slider _fxSlider;

    public override void Setup(UIData data)
    {
        base.Setup(data);
        Audio audio = Audio.Instance;
        _traxSlider.value = audio.TraxVolume;
        _fxSlider.value = audio.FXVolume;        
    }

    public override void DiscreteUpdate()
    {
        base.DiscreteUpdate();
        _traxLabel.SetText(Parsing.Text(_traxTextEng, _traxTextSpa));
        _fxLabel.SetText(Parsing.Text(_fxTextEng, _fxTextSpa));
        for (int i = 0; i < _buttons.Length; i++)
        {
            GenericButton selButton = _buttons[i];
            selButton.DiscreteUpdate();
        }
    }

    private void Update()
    {
        Audio audio = Audio.Instance;
        audio.DiscreteUpdate(_traxSlider.value, _fxSlider.value);
    }
}
