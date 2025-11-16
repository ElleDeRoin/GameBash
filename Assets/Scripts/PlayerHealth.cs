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

    [Header("Animation")]
    [Tooltip("Animator on the child VFX with the SpriteRenderer")]
    public Animator anim;

    private bool isDead = false;

    private void Start()
    {
        // Automatically find Animator in child if not assigned
        if (anim == null)
            anim = GetComponentInChildren<Animator>();

        currentHealth = maxHealth;
        UpdateHeartsUI();
    }

    /// <summary>
    /// Call this to reduce player health
    /// </summary>
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        // Play damage animation
        if(anim != null)
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
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player died!");

        // Trigger death animation
        if(anim != null)
            anim.SetTrigger("Die");

        // Delay scene load until animation finishes
        StartCoroutine(LoadGameOverAfterDelay());
    }

    private System.Collections.IEnumerator LoadGameOverAfterDelay()
    {
        // Wait for animation length or a fixed delay (adjust 1.2f if your animation is longer)
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("GameOver");
    }
}
