using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpRate;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _lerpRate * Time.deltaTime);
    }
}
