using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Propiedades")]
    public string nombre;
    public int dmg;
    public int HP = 3;
    public int currentHP = 3;

    [Header("DescripcionUI")]
    public Image hpSprite;
    public TMP_Text description;
    public GameObject enemyDescription;

    private void Start()
    {
        description.text = nombre;
    }


    private void Update()
    {
        
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
        hpSprite.fillAmount = (float)currentHP / HP;
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyDescription.SetActive(true);  
    }

    void Die()
    {
        //destruir objeto
        VFXController.instance.EnemyDeath(transform.position);
        this.gameObject.SetActive(false);
    }
}
