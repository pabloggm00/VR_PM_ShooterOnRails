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

    [Header("Blink Settings")]
    public Material damageMaterial;
    public Renderer shipRenderer;
    public float blinkDuration = 0.2f;
    public int blinkCount = 5;
    private Material originalMaterial;

    private bool isInvulnerable;
    private PlayerMove playerMove;

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        currentHealth = hp;

        if (shipRenderer != null)
            originalMaterial = shipRenderer.material;
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
        StartCoroutine(BlinkEffect());

        yield return new WaitForSeconds(blinkCount * blinkDuration);

        isInvulnerable = false;
    }

    IEnumerator BlinkEffect()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            shipRenderer.material = damageMaterial;
            yield return new WaitForSeconds(blinkDuration);
            shipRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkDuration);
        }
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
