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

    private void Start() {
        _colorMinimumHealth = Color.red;
        _colorMaximumHealth = Color.green;
    }

    private void Update() {
        _sliderHealthBar.value = Mathf.Lerp(_sliderHealthBar.value, _building.GetCurrentHealth() / _building.GetMaxHealth(), _lerpSpeed);
        _imgHealthBarFill.color = Color.Lerp(_colorMinimumHealth, _colorMaximumHealth, _sliderHealthBar.value);
    }

}
