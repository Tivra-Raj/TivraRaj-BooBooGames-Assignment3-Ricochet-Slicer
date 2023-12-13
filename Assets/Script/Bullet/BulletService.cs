using UnityEngine;
using Utilities;

namespace Bullet
{
    public class BulletService : MonoGenericSingleton<BulletService>
    {
        [SerializeField] private BulletPool bulletPool;

        public int maxReflection = 5;
        public BulletController GetBullet(Vector3 bulletPosition, Quaternion bulletRotation)
        {
            BulletController bulletController = bulletPool.GetBullet( bulletPosition, bulletRotation);
            return bulletController;
        }

        public void ReturnBulletToPool(BulletController bulletToReturn) => bulletPool.ReturnItem(bulletToReturn);
    }
}