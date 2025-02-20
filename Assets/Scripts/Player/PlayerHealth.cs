using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int hp;
    public int currentHealth;

    [Header("UI")]
    public Image fill;

    private bool isInvulnerable;
    private PlayerMove playerMove;

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        currentHealth = hp;
    }

    private void Update()
    {
        fill.fillAmount = (float)currentHealth / hp;
    }

    public void TakeDamage(int dmg)
    {
        if (playerMove.isInvulnerable || isInvulnerable)
            return;

        currentHealth -= dmg;

        StartCoroutine(StartInvulnreability());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    IEnumerator StartInvulnreability()
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(2f);

        isInvulnerable = false;
    }

    void Die()
    {
        VFXController.instance.PlayerDeath(transform.position);
        GameplayController.instance.PanelMuerte();
        playerMove.cart.m_Speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            TakeDamage(other.GetComponentInParent<Enemy>().dmg);
        }
    }
}
