using UnityEngine;

namespace Bullet
{
    [RequireComponent (typeof (Rigidbody2D))]
    public class BulletView : MonoBehaviour
    {
        private Rigidbody2D bulletRigidbody;
        public LayerMask DestroyLayer;
        public BulletController BulletController { get; private set; }

        public void SetBulletController(BulletController bulletController)
        {
            BulletController = bulletController;
            bulletRigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() => BulletController?.ShootBullet();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            BulletController?.OnBulletCollidedWall(collision);
        }

        public Rigidbody2D GetBulletRigidbody() => bulletRigidbody;
    }

}