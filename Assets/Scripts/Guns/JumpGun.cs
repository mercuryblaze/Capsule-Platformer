using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGun : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _spawn;
    [SerializeField] private Gun _pistol;
    [SerializeField] private float _maxCharge = 3f;
    [SerializeField] private ChargeIcon _chargeIcon;

    private float _currentCharge;
    private bool _isCharged;

    private void Update()
    {
        if (_isCharged)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _playerRigidbody.AddForce(-_spawn.forward * _speed, ForceMode.VelocityChange);
                _pistol.Shot();
                _isCharged = false;
                _currentCharge = 0f;
                _chargeIcon.StartCharge();
            }
        }
        else
        {
            _currentCharge += Time.unscaledDeltaTime;
            _chargeIcon.SetChargeValue(_currentCharge, _maxCharge);

            if (_currentCharge >= _maxCharge)
            {
                _isCharged = true;
                _chargeIcon.StopCharge();
            }
        }
    }
}
