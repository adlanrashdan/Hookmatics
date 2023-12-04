using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject glowEffectPrefab;
    private Transform player;
    private bool isMoving = true;
    private float timer = 0f;
    private bool isGrounded = false;
    private Vector3 targetPosition;
    public Object Indicator;
    public float speed = 1.5f;
    private AimIndicator aimIndicator; // 新增 AimIndicator 引用

    void Start()
    {
        // 在 Start 方法中调用 SetRandomTarget，确保在开始时设置初始目标
        SetRandomTarget();
        SoundEffectManager.instance.PlaySoundEffect("FallingBlock");

        // 获取 AimIndicator 组件
        aimIndicator = GetComponentInChildren<AimIndicator>();

        AttachGlowEffect();
    }
    private void OnDestroy()
    {
        SoundEffectManager.instance.PlaySoundEffect("MissileDeath");
        Destroy(Indicator);
    }

    void Update()
    {
        if (isGrounded)
        {
            timer += Time.deltaTime;

            if (timer >= 10f)
            {
                DestroyProjectile();
            }
        }
        else
        {
            if (isMoving)
            {
                MoveProjectile();
                timer += Time.deltaTime;

                if (timer >= 10f)
                {
                    DestroyProjectile();
                }
            }
            else
            {
                HomingMovement();
            }
        }
    }

    public void SetTarget(Vector3 target)
    {
        player.position = target;
        isGrounded = false;
        timer = 0f;

        // 在设置目标时显示指示器
        ShowIndicator(target);
    }

    private void SetRandomTarget()
    {
        // 随机生成一个在玩家可移动范围内的位置
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        float randomX = Random.Range(player.position.x - 5f, player.position.x + 5f);
        float randomZ = Random.Range(player.position.z - 5f, player.position.z + 5f);

        targetPosition = new Vector3(randomX, player.position.y, randomZ);

        // 在降落之前显示一个指示器（你可以根据需要选择不同的指示器效果）
        ShowIndicator(targetPosition);
    }

    private void ShowIndicator(Vector3 targetPosition)
    {
        // 在这里实现显示指示器的逻辑，可以是UI元素、粒子效果等
        // 这里简单地使用 Debug.DrawLine 作为示例
        Debug.DrawLine(transform.position, targetPosition, Color.green, 3f);

        // 更新 AimIndicator 的关联炮弹位置
        if (aimIndicator != null)
        {
            aimIndicator.SetAssociatedProjectile(gameObject);
        }
        Indicator = Instantiate(Resources.Load("Prefabs/FX/CFX3_MagicAura_D_Runic"), targetPosition, Quaternion.Euler(90, 0, 0));


    }

    public bool IsMoving()
    {
        return isMoving;
    }

    private void MoveProjectile()
    {
        // 降低速度

        transform.position = Vector3.Lerp(transform.position, targetPosition, timer / speed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            DestroyProjectile();
        }
    }

    private void HomingMovement()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * 5f, Space.World);
            transform.LookAt(player);
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacle"))
        {
            DestroyProjectile();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void DestroyProjectile()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void AttachGlowEffect()
    {
        if (glowEffectPrefab != null)
        {
            Instantiate(glowEffectPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}