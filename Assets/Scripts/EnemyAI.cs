using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Rigidbody2D rb;

    [Header("Patrol Settings")]
    public float patrolSpeed = 2f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool movingRight = true;

    [Header("Vision Cone")]
    public float detectionRange = 6f;
    public float visionAngle = 45f;
    public LayerMask visionObstacles;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private float fireTimer;
    private bool seesPlayer;

    void Start()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        seesPlayer = PlayerInVisionCone();

        if (seesPlayer)
        {
            StopAndShoot();
        }
    }

    void FixedUpdate()
    {
        if (!seesPlayer)
        {
            Patrol();
        }
    }

    // =========================
    // PATROL 
    // =========================
    void Patrol()
    {
        rb.linearVelocity = new Vector2(
            (movingRight ? 1 : -1) * patrolSpeed,
            rb.linearVelocity.y
        );

        RaycastHit2D groundInfo = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            1f,
            groundLayer
        );

        if (!groundInfo.collider)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Flip();
        }
    }

    // =========================
    // SHOOTING
    // =========================
    void StopAndShoot()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        FacePlayer();

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 dir = (player.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<bullet>().Setup(dir);
    }

    // =========================
    // VISION CONE
    // =========================
    bool PlayerInVisionCone()
    {
        Vector2 dirToPlayer = (player.position - transform.position).normalized;

        float facingDir = movingRight ? 1f : -1f;
        float angle = Vector2.Angle(Vector2.right * facingDir, dirToPlayer);

        if (angle > visionAngle) return false;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > detectionRange) return false;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            dirToPlayer,
            distance,
            visionObstacles
        );

        if (hit && hit.collider.transform != player)
            return false;

        return true;
    }

    // =========================
    // UTILS
    // =========================
    void FacePlayer()
    {
        bool shouldFaceRight = player.position.x > transform.position.x;
        if (shouldFaceRight != movingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        float facingDir = movingRight ? 1f : -1f;
        Vector3 right = Quaternion.Euler(0, 0, visionAngle) * Vector3.right * facingDir;
        Vector3 left = Quaternion.Euler(0, 0, -visionAngle) * Vector3.right * facingDir;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + right * detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + left * detectionRange);
    }
}
