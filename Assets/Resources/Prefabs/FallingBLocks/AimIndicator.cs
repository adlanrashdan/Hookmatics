using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    private GameObject associatedProjectile;

    void Update()
    {
        // 检查关联的炮弹是否存在，不存在则销毁准星
        if (associatedProjectile == null)
        {
            Destroy(gameObject);
            return;
        }

        // 更新准星位置为关联炮弹的当前位置，稍微抬高一些以避免与地形相撞
        transform.position = associatedProjectile.transform.position + Vector3.up * 0.5f;
    }

    public void SetAssociatedProjectile(GameObject projectile)
    {
        associatedProjectile = projectile;
    }
}