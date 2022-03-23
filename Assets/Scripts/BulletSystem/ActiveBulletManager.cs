using Control;
using Utils;

namespace BulletSystem
{
    public class ActiveBulletManager : Singleton<ActiveBulletManager>
    {
        private void Start()
        {
            YoumuController.instance.onYoumuHit += Wipe;
            //Spell.instance.onSpellUse += Wipe;
        }


        public void Wipe()
        {
            /* LEGACY
            var nbActiveBullets = this.transform.childCount;
            for(var i = 0; i < nbActiveBullets; i++)
            {
                var bullet = transform.GetChild(0).gameObject.GetComponent<Bullet>();
                if (bullet.gameObject.CompareTag("FriendlyBullet")) return;
                bullet.InvokeBulletDeath();
            } */

            foreach (var bullet in GetComponentsInChildren<Bullet>())
            {
                if (bullet.gameObject.CompareTag("FriendlyBullet")) return;
                bullet.InvokeBulletDeath();
            }
        }
    }
}