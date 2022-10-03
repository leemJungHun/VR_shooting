using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Transform _camTf;

    void Start()
    {
        _camTf = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(_camTf);
    }
}

