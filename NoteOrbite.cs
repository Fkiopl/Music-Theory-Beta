using UnityEngine;

public class NoteOrbiter : MonoBehaviour
{
    public float orbitRadius = 1.5f;
    public float orbitSpeed = 180f;
    public int damage = 1;
    public float damageCooldown = 0.5f;

    private Transform player;
    private float angle = 0f;
    private float damageTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;
        angle += orbitSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;
        transform.position = player.position + new Vector3(
            Mathf.Cos(rad), Mathf.Sin(rad), 0f) * orbitRadius;
        damageTimer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (damageTimer > 0f) return;
        if (other.CompareTag("Enemy"))
        {
            EnemyAI e = other.GetComponent<EnemyAI>();
            if (e != null)
            {
                e.TakeHit(damage);
                damageTimer = damageCooldown;
            }
        }
    }
}
