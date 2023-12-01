using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _friction;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Transform _colliderTransform;

    private int _jumpFrameCounter;

    public bool IsGrounded => _isGrounded;

    private void Update()
    {
        bool isDuck = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.S) || _isGrounded == false;
        
        _colliderTransform.localScale = Vector3.Lerp(_colliderTransform.localScale, new Vector3(1f, isDuck ? 0.5f : 1f, 1f), Time.deltaTime * 15f);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    public void Jump()
    {
        _rigidbody.AddForce(0f, _jumpSpeed, 0f, ForceMode.VelocityChange);
        _jumpFrameCounter = 0;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(0f, 0f, 0f, ForceMode.VelocityChange);

        float speedMulitplier = 1f;

        if (_isGrounded == false)
        {
            speedMulitplier = 0.2f;

            if (_rigidbody.velocity.x > _maxSpeed && Input.GetAxis("Horizontal") > 0)
            {
                speedMulitplier = 0f;
            }
            if (_rigidbody.velocity.x < -_maxSpeed && Input.GetAxis("Horizontal") < 0)
            {
                speedMulitplier = 0f;
            }
        }

        _rigidbody.AddForce(Input.GetAxis("Horizontal") * _moveSpeed * speedMulitplier, 0f, 0f, ForceMode.VelocityChange);

        if (_isGrounded)
        {
            _rigidbody.AddForce(-_rigidbody.velocity.x * _friction, 0f, 0f, ForceMode.VelocityChange);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 15f);
        }

        _jumpFrameCounter += 1;

        if (_jumpFrameCounter == 2)
        {
            _rigidbody.freezeRotation = false;
            _rigidbody.AddRelativeTorque(0f, 0f, 7f, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);
            if (angle < 45)
            {
                _isGrounded = true;
                _rigidbody.freezeRotation = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }
}