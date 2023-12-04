using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject launchEffectPrefab;
    public GameObject aimIndicatorPrefab; // 新增瞄准星Prefab

    private void Start()
    {
        InvokeRepeating("FireProjectile", 0f, 10f);
    }

    private void FireProjectile()
    {
        // 实例化发射特效
        if (launchEffectPrefab != null)
        {
            Instantiate(launchEffectPrefab, firePoint.position, Quaternion.identity);
        }

        // 实例化炮弹
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        ProjectileController projectile = projectileObject.GetComponent<ProjectileController>();

        if (projectile != null)
        {
            // 设置炮弹属性
            projectile.SetTarget(firePoint.position);

            // 实例化瞄准星
            GameObject aimIndicator = Instantiate(aimIndicatorPrefab, projectileObject.transform.position, Quaternion.identity);
            AimIndicator aimIndicatorScript = aimIndicator.GetComponent<AimIndicator>();

            if (aimIndicatorScript != null)
            {
                // 设置瞄准星关联的炮弹
                aimIndicatorScript.SetAssociatedProjectile(projectileObject);
            }
        }
    }
}