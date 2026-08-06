using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public int hp = 3;

    private Transform player;
    private float attackTimer = 0f;
    private EnemyHealthBar healthBar;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        // Setup health bar
        healthBar = gameObject.AddComponent<EnemyHealthBar>();
        healthBar.Setup(hp);
    }

    void Update()
    {
        if (player == null) return;
        Vector2 dir = (player.position - transform.position).normalized;
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
        attackTimer -= Time.deltaTime;
    }

    public void TakeHit(int dmg)
    {
        hp -= dmg;
        if (healthBar != null) healthBar.UpdateBar(hp);
        if (hp <= 0)
        {
            GameManager.instance.EnemyDied();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && attackTimer <= 0f)
        {
            GameManager.instance.PlayerTakeDamage();
            attackTimer = 1f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && attackTimer <= 0f)
        {
            GameManager.instance.PlayerTakeDamage();
            attackTimer = 1f;
        }
    }
}