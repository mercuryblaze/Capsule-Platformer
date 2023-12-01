using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shotPeriod = 0.2f;
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private GameObject _flash;
    [SerializeField] private ParticleSystem _shotEffect;

    private float _timer;

    private void Update()
    {
        _timer += Time.unscaledDeltaTime;

        if (_timer > _shotPeriod)
        {
            if (Input.GetMouseButton(0))
            {
                _timer = 0;
                Shot();
            }
        }
    }

    protected internal virtual void Shot()
    {
        GameObject newBullet = Instantiate(_bulletPrefab, _spawn.position, _spawn.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = _spawn.forward * _bulletSpeed;
        _shotSound.Play();
        _flash.SetActive(true);
        Invoke(nameof(HideFlash), 0.12f);
        _shotEffect.Play();
    }

    private void HideFlash()
    {
        _flash.SetActive(false);
    }

    protected internal virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    protected internal virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    protected internal virtual void AddBullets(int numberOfBullets)
    {
    }
}
