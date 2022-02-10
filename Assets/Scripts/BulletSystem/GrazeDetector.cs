using UnityEngine;

namespace BulletSystem
{
    public class GrazeDetector : MonoBehaviour
    {
        ///private int grazeValue=0;
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

            if (Graze.instance.AddGraze())
            {
                ///Debug.Log("Bullet Entered Graze area.");
                ///Debug.Log(Graze.instance.get());
                
                //TODO: Call graze VFX here
            }

            /**if (grazeValue < 100)
            {
                Debug.Log("Bullet Entered Graze area.");
                grazeValue += 1;
                Debug.Log(grazeValue);
            }**/
        }
    }
}
