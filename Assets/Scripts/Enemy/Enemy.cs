using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int HP = 3;
    public int currentHP = 3;
    public Image hpSprite;

    private void Update()
    {
        hpSprite.fillAmount = (float)currentHP / HP;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP < 0)
        {
            currentHP = 0;
        }
    }

    void Die()
    {
        //destruir objeto
        //instanciar vfx muerte
    }
}
