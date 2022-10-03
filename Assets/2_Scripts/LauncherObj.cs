using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherObj : MonoBehaviour
{
    [SerializeField] GameObject[] _targetPrefabs;
    [SerializeField] CountDown _countDown;
    public Transform _leftLuncher;
    public Transform _rightLuncher;
    float _delayTime = 5f;
    float _time = 5f;
    float _timeCheck = 0f;
    float _limitTime = 10f;

    bool _isStart = false;
    // Update is called once per frame

    public void IsStart()
    {
        _isStart = true;
    }

    public bool GetIsStart()
    {
        return _isStart;
    }

    void Update()
    {
        if (_isStart)
        {
            _time += Time.deltaTime;
            _timeCheck += Time.deltaTime;
            if (_time >= _delayTime)
            {
                Instantiate(_targetPrefabs[Random.Range(0, _targetPrefabs.Length)], _leftLuncher.position, _leftLuncher.rotation);
                Instantiate(_targetPrefabs[Random.Range(0, _targetPrefabs.Length)], _rightLuncher.position, _rightLuncher.rotation);
                _time = 0;
            }
            if (_timeCheck>_limitTime)
            {
                _isStart = false;
                _countDown.ResultScore();
            }
        }
    }
}
