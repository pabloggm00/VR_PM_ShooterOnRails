using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Propiedades")]
    public string nombre;
    public int HP = 3;
    public int currentHP = 3;
    public int dmg;

    [Header("DescripcionUI")]
    public Image hpSprite;
    public TMP_Text description;
    public GameObject enemyDescription;

   public virtual void InitDescription()
    {
        if (enemyDescription != null)
        {
            description.text = nombre;
        }

    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    public virtual void UpdateHP()
    {
        if (hpSprite != null)
        {
            hpSprite.fillAmount = (float)currentHP / HP;
        }

    }


    public virtual void Die()
    {
        //destruir objeto
        
        this.gameObject.SetActive(false);
    }

    
}
