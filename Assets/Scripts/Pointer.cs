using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform _aim;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _playerTransform;
    //[SerializeField] private float _rotationSpeed = 15f;

    private float _yEuler;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        //Debug.Log(Input.mousePosition);

        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 50f, Color.yellow);
        Plane plane = new Plane(-Vector3.forward, Vector3.zero);
        float distance;
        plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance);
        _aim.position = point;

        Vector3 toAim = _aim.position - transform.position;
        transform.rotation = Quaternion.LookRotation(toAim);

        if (toAim.x < 0)
        {
            _yEuler = Mathf.Lerp(_yEuler, 45f, Time.deltaTime * 8f);
        }
        else
        {
            _yEuler = Mathf.Lerp(_yEuler, -45f, Time.deltaTime * 8f);
        }
        _playerTransform.localEulerAngles = new Vector3(0, _yEuler, 0);

        //_playerTransform.localRotation = Quaternion.Lerp(_playerTransform.localRotation,
        //    Quaternion.Euler(0f, _playerTransform.localRotation.x - Mathf.Sign(toAim.x) * 45f, 0f), _rotationSpeed * Time.deltaTime);
    }
}