using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageEffect : MonoBehaviour
{
    [Header("Pool")]
    public Transform pool;

    [Header("Components")]
    RectTransform _rectTransform;
    TextMeshProUGUI _damageText;

    [Header("Timer")]
    public float latency;
    float _time;

    [Header("Base color")]
    Color _baseColor;

    [Header("Direction")]
    Vector3 _worldPosition;
    Vector3 _basePosition;
    Vector3 _direction;

    public void Activation(Vector3 wordlPosition, int value, bool munitions = false)
    {
        if (!_damageText)
        {
            _rectTransform = transform.GetComponent<RectTransform>();
            _damageText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        
        if(!munitions)
            _damageText.text = "<color=red>" + value + "</color>";
        else
            _damageText.text = "<sprite=0> <color=green>+" + value + "</color>";

        _worldPosition = wordlPosition;
        transform.position = Camera.main.WorldToScreenPoint(_worldPosition);

        _baseColor = _damageText.color;
        _basePosition = new Vector3(Random.Range(-20f, 20f), 150, 0);
        _direction = new Vector3(Random.Range(-1f, 1f), 1, 0);

        gameObject.SetActive(true);
        _time = 0;

    }

    private void Update()
    {
        if(_time < latency)
        {
            _time += Time.deltaTime;
            float interpolation = 1 - (_time / latency);

            transform.position = Camera.main.WorldToScreenPoint(_worldPosition) + _basePosition + (-_direction * 50 * interpolation);

            _damageText.color = new Color(_baseColor.r, _baseColor.g, _baseColor.b, interpolation);

            if (_time >= latency)
                Desactivation();
        }
    }

    void Desactivation()
    {
        gameObject.SetActive(false);
        transform.SetParent(pool);
    }
}
