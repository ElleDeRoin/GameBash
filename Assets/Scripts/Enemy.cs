using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;            // Movement speed towards player
    public string enemyType = "High";   // "High", "Medium", "Low"

    private void Update()
    {
        // Move left towards player
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy if goes off-screen
        if (transform.position.x < -10f)  // adjust to your level bounds
            Destroy(gameObject);
    }

    // Called when hit by player attack
    private void OnTriggerEnter2D(Collider2D collision)
{
    Attack attack = collision.GetComponent<Attack>();
    if (attack != null)
    {
        if (attack.attackType == AttackType.High && this.tag == "HighEnemy")
        {
            Destroy(gameObject);
        }
        else if (attack.attackType == AttackType.Medium && this.tag == "MediumEnemy")
        {
            Destroy(gameObject);
        }
        else if (attack.attackType == AttackType.Low && this.tag == "LowEnemy")
        {
            Destroy(gameObject);
        }
    }
}

}
