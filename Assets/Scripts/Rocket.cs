using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _rateOfChangeOfPosition = 10f;

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = FindAnyObjectByType<PlayerMove>().transform;
    }

    private void Update()
    {
        CheckRocketPosition();
        transform.position += transform.forward * Time.deltaTime * _speed;
        Vector3 toPlayer = _playerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }

    private void CheckRocketPosition()
    {
        if (transform.position.z != 0)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, 0f);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * _rateOfChangeOfPosition);
        }
    }
}
