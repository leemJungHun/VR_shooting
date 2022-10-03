using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObj : MonoBehaviour
{
    [SerializeField] GameObject _effect;
    float _force;
    float _lifeTime = 20;

    Rigidbody rgbd3D;

    bool _destory = false;

    void Awake()
    {
        _force = Random.Range(1000, 1500);
        rgbd3D = GetComponent<Rigidbody>();
        rgbd3D.AddForce(transform.forward * _force);

        Destroy(gameObject, _lifeTime);

    }

    public void TargetShot()
    {
        if (!_destory)
        {
            if (gameObject.name != "UnbreakObject(Clone)")
            {
                Destroy(gameObject, 0.1f);
            }
            Instantiate(_effect, transform);
        }

        _destory = true;
    }

}
