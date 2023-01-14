using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndScale : MonoBehaviour
{
    //public GameObject _go;
    public Vector3 _vect;
    [SerializeField] Transform _transform;

    // Start is called before the first frame update
    //void Start()
    //{
    //    //_transform = _go.transform;
    //}

    // Update is called once per frame
    void Update()
    {
        _transform.Rotate(_vect);
    }
}
