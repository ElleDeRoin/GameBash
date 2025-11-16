using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("UI")]
    public Image[] hearts; // Assign in Inspector
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Animator anim;
    private bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>(); // Grab animator
        currentHealth = maxHealth;
        UpdateHeartsUI();
    }

    // Call this to reduce player health
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        // Play damage animation
        anim.SetTrigger("Damage");

        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player died!");

        // Trigger death animation if available
        anim.SetTrigger("Die");

        // Delay scene load until animation finishes
        StartCoroutine(LoadGameOverAfterDelay());
    }

    private System.Collections.IEnumerator LoadGameOverAfterDelay()
    {
        // Wait for animation length or use fixed delay
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("GameOver");
    }
}
