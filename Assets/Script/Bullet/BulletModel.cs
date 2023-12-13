namespace Bullet
{
    public class BulletModel
    {
        public int damage;
        public float speed;
        public BulletController BulletController { get; private set; }

        public void SetBulletController(BulletController bulletController)
        {
            BulletController = bulletController;
        }

        public BulletModel(BulletScriptableObject bulletScriptableObject)
        {
            damage = bulletScriptableObject.damage;
            speed = bulletScriptableObject.speed;
        }
    }
}