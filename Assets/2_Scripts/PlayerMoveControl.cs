using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    
    [SerializeField] float _speed = 1.5f;
    [SerializeField] float _damping = 3;
    [SerializeField] GameObject _myGun;
    [SerializeField] GameObject _tableGun;
    [SerializeField] GameObject _tableCilpBoard;
    [SerializeField] CrossHairObj _crossHairObj;
    [Header("Characer Parameter")]
    //[SerializeField] Animator _weaponAniCtrl;
    [SerializeField] Animator _clipAniCtrl;

    int _limitBulletCount = 99;
    int _currBulletCount = 99;

    CharacterController _controller;
    Camera _cam;

    List<Transform> _points = new List<Transform>();
    int _nextIndex = 0;

    public bool _isStopped
    {
        get; set;
    }

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cam = transform.GetChild(0).GetComponent<Camera>();

        _nextIndex = 0;
        _isStopped = false;
    }
    void Start()
    {
        Transform root = GameObject.Find("MoveRoot").transform;
        for (int n = 0; n < root.childCount; n++)
            _points.Add(root.GetChild(n));

    }

    void Update()
    {
        if (!_isStopped)
        {
            Vector3 direction = _points[_nextIndex].position - transform.position;
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * _damping);
            _controller.SimpleMove(transform.forward * _speed);
        }
        else
        {
            _clipAniCtrl.gameObject.SetActive(false);
            _tableGun.SetActive(false);
            _myGun.SetActive(true);
            _tableCilpBoard.SetActive(true);
            _crossHairObj.ChangeGunCrossHair();
        }
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MOVE_POINT"))
        {
            if (++_nextIndex >= _points.Count)
            {
                _nextIndex = 0;
                _isStopped = true;
            }
        }
    }
}
