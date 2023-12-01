using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Automat : Gun
{
    [Header("Automat")]
    [SerializeField] private int _numberOfBullets;
    [SerializeField] private Text _bulletsText;
    [SerializeField] private PlayerArmory _playerArmory;

    protected internal override void Shot()
    {
        base.Shot();

        _numberOfBullets--;
        UpdateText();

        if (_numberOfBullets == 0)
        {
            _playerArmory.TakeGunByIndex(0);
        }
    }

    protected internal override void Activate()
    {
        base.Activate();
        _bulletsText.enabled = true;
        UpdateText();
    }

    protected internal override void Deactivate()
    {
        base.Deactivate();
        _bulletsText.enabled = false;
    }

    private void UpdateText()
    {
        _bulletsText.text = "����: " + _numberOfBullets.ToString();
    }

    protected internal override void AddBullets(int numberOfBullets)
    {
        base.AddBullets(numberOfBullets);
        _numberOfBullets += numberOfBullets;
        UpdateText();
        _playerArmory.TakeGunByIndex(1);
    }
}
