using UnityEngine;

namespace Bullet
{
    public class BulletController
    {
        public BulletModel BulletModel { get; private set; }
        public BulletView BulletView { get; private set; }

        private int refelctions = 0;

        public BulletController(BulletModel _bulletModel, BulletView _bulletView, Vector3 bulletPosition, Quaternion bulletRotation)
        {
            BulletModel = _bulletModel;
            BulletView = GameObject.Instantiate<BulletView>(_bulletView, bulletPosition, bulletRotation);

            BulletModel.SetBulletController(this);
            BulletView.SetBulletController(this);
        }

        public void SetPosition(Vector3 position, Quaternion rotation)
        {
            BulletView.gameObject.transform.SetPositionAndRotation(position, rotation);
        }

        public void ShootBullet()
        {
            BulletView.transform.Translate(Vector2.up * BulletModel.speed * Time.deltaTime);          
        }

        public void OnBulletCollidedWall(Collision2D collision)
        {
            if (refelctions < BulletService.Instance.maxReflection && !collision.gameObject.CompareTag("destroy"))
            {
                Vector2 reflection = Vector2.Reflect(BulletView.transform.up, collision.contacts[0].normal);
                BulletView.transform.up = reflection;
                refelctions++;
            }
            else
            {
                BulletView.gameObject.SetActive(false);
                BulletService.Instance.ReturnBulletToPool(this);
                refelctions = 0;
            }
        }
    }
}