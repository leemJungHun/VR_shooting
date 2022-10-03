using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] CrossHairObj _crossHairObj;
    [SerializeField] RectTransform _bulletImage;
    [SerializeField] Text _scoreText;
    [SerializeField] float _fireInterval = 0.2f;
    [Header("Test Parameta")]
    [SerializeField] bool _isFire = false;
    [SerializeField] float _firingRange = 100;

    AudioSource _audio;
    float _passTime = 0;
    Vector3 _originPos;
    float _maxBullet = 2;
    float _nowBullet = 2;
    float _reloadTime = 0.5f;
    bool _reloadBullet = false;
    int _score = 0;
    int _bulletWidth = 18;
    RaycastHit _rHit;
    Transform _camTF;
    int _lMask;

    public bool _fire
    {
        get; set;
    }

    public int _nowScore
    {
        get; set;
    }

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _originPos = transform.localPosition;

        _lMask = 1 << LayerMask.NameToLayer("Target");
    }
    // Start is called before the first frame update
    void Start()
    {
        _camTF = Camera.main.transform;
        _nowBullet = _maxBullet;
    }

    // Update is called once per frame
    void Update()
    {


        if (Physics.Raycast(_camTF.position, _camTF.forward, out _rHit, _firingRange, _lMask))
        {
            if (Physics.Raycast(_camTF.position, _camTF.forward, out _rHit, _firingRange))
            {
                Debug.Log(_rHit.collider.name);
                if (_rHit.collider.tag == "Target")
                {
                    switch (_rHit.collider.name)
                    {
                        case "TargetObject(Clone)":
                            _score++;
                            break;
                        case "MinusObject(Clone)":
                            _score--;
                            break;
                        case "UnbreakObject(Clone)":
                            break;
                    }

                    _isFire = true;
                    _scoreText.text = _score.ToString();
                    _nowScore = _score;
                    _rHit.collider.gameObject.GetComponent<TargetObj>().TargetShot();
                }
            }
        }
        else
        {
            _isFire = false;
            _crossHairObj._isGaze = false;
        }
        if (_reloadBullet)
        {
            _crossHairObj._isGaze = false;
            if (_nowBullet >= _maxBullet)
            {
                _nowBullet = _maxBullet;
                _bulletImage.sizeDelta = new Vector2(_bulletWidth* _nowBullet, _bulletImage.sizeDelta.y);
                _reloadBullet = false;
            }
            else
            {
                _nowBullet += _maxBullet * (Time.deltaTime / _reloadTime);
                _bulletImage.sizeDelta = new Vector2(_bulletWidth * _nowBullet, _bulletImage.sizeDelta.y);
            }
        }
        else
        {
            if (_isFire)
            {
                if (Time.time >= _passTime)
                {
                    _passTime = Time.time + _fireInterval;
                    _rHit.transform.SendMessage("Hitting", SendMessageOptions.DontRequireReceiver);
                    // 쏘는 코드 입력부분
                    StartCoroutine(FireEffect());
                }
                float z = Random.Range(1.0f, 6.0f);
            }
        }
        _fire = _isFire;
    }

    IEnumerator FireEffect()
    {
        _nowBullet--;
        if (_nowBullet == 0)
        {
            _reloadBullet = true;
        }
        _bulletImage.sizeDelta = new Vector2(_bulletWidth * _nowBullet, _bulletImage.sizeDelta.y);
        Vector2 v2Rand = Random.insideUnitCircle;
        transform.localPosition += new Vector3(0, v2Rand.x * 0.05f, v2Rand.y * 0.05f);/*
        _muzzleFlash.transform.Rotate(Vector3.forward, Random.Range(0, 360.0f));
        _muzzleFlash.enabled = true;
        _audio.PlayOneShot(_fireSFX);*/
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        //_muzzleFlash.enabled = false;
        transform.localPosition = _originPos;
    }
}
