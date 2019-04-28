using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour {

    [SerializeField]
    private Building _building;
    [SerializeField]
    private Slider _sliderHealthBar;
    [SerializeField]
    private Image _imgHealthBarFill;
    [SerializeField]
    private float _lerpSpeed = 1f;

    private Color _colorMinimumHealth;
    private Color _colorMaximumHealth;

    private void Awake() {
        _colorMinimumHealth = new Color(255, 96, 0, 255);
        _colorMaximumHealth = new Color(100, 233, 36, 255);
    }

    private void Update() {
        _sliderHealthBar.value = Mathf.Lerp(_sliderHealthBar.value, _building.GetCurrentHealth() / _building.GetMaxHealth(), _lerpSpeed);
        _imgHealthBarFill.color = Color.Lerp(_colorMinimumHealth, _colorMinimumHealth, _sliderHealthBar.value);
    }

}
