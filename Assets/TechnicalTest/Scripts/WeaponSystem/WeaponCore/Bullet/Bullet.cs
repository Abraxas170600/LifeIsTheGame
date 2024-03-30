using UnityEngine;

namespace TechnicalTest.System.WeaponSystem.WeaponCore
{
    public abstract class Bullet : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void BulletAction();
    }
}
