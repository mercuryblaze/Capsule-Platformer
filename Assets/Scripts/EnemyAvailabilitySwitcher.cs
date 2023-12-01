using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyAvailabilitySwitcher : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private GameObject[] _enemies;

    private void Start()
    {
        _enemies = FindObjectsOfType<EnemyHealth>().Select(EnemyHealth => EnemyHealth.gameObject).ToArray();
    }

    private void Update()
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_enemies[i] != null)
            {
                Vector3 distance = _player.position - _enemies[i].GetComponent<Transform>().position;

                if (distance.magnitude > 20f)
                {
                    _enemies[i].SetActive(false);
                }
                else
                {
                    _enemies[i].SetActive(true);
                }
            }
            else
            {
                _enemies = _enemies.Where((val, idx) => idx != i).ToArray();
            }
        }

        Debug.Log(_enemies.Length);
    }
}
