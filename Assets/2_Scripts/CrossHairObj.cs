using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairObj : MonoBehaviour
{
    public enum _eSituateKind
    {
        Normal          = 0,
        Shot,
    }
    [SerializeField] float _durSpeed = 0.1f;
    [SerializeField] float _minSize = 0.4f;
    [SerializeField] float _maxSize = 0.3f;
    [SerializeField] Color[] _situateColor;
    [SerializeField] Sprite _gunCrossHair;

    Image _crossHair;
    float _timeStart;
    
    public bool _isGaze {
        get; set;
    }

    void Awake()
    {
        _crossHair = GetComponent<Image>();
        _timeStart = Time.time;
    }

    void Start()
    {
        transform.localScale = Vector3.one * _minSize;
        _crossHair.color = _situateColor[(int)_eSituateKind.Normal];
    }

    public void ChangeGunCrossHair()
    {
        _crossHair.sprite = _gunCrossHair;
    }

    void Update()
    {
        if(_isGaze)
        {
            float t = (Time.time - _timeStart) / _durSpeed;
            transform.localScale = Vector3.one * Mathf.Lerp(_minSize, _maxSize, t);
            _crossHair.color = _situateColor[(int)_eSituateKind.Shot];

        }
        else
        {
            transform.localScale = Vector3.one * _minSize;
            _timeStart = Time.time;
            _crossHair.color = _situateColor[(int)_eSituateKind.Normal];
        }
    }
}
