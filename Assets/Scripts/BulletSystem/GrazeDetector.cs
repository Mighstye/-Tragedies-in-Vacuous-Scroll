using UnityEngine;

namespace BulletSystem
{
    public class GrazeDetector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("EnemyBullet")) return;
            var bullet = col.gameObject.GetComponent<Bullet>();
            if (bullet == null)
            {
                Debug.LogWarning("Found bullet without Bullet script component!");
                return;
            }
        
            if (!bullet.grazeable) return;
            bullet.grazeable = false;
            //TODO: Call graze gauge increase method here
            //TODO: Call graze VFX here
        
        }
    }
}
