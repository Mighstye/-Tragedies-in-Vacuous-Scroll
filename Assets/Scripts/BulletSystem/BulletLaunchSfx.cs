using System.Collections;
using UnityEngine;
using Utils;

namespace BulletSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class BulletLaunchSfx : Singleton<BulletLaunchSfx>
    {
        [SerializeField]private AudioSource audioSource;
        [SerializeField] private float minimumCallInterval = 0.15f;
        private bool spammingLock = false;
        private void Start()
        {
            audioSource ??= GetComponent<AudioSource>();
        }

        public void Play()
        {
            if (spammingLock) return;
            StartCoroutine(PlayAndLock());
        }

        private IEnumerator PlayAndLock()
        {
            spammingLock = true;
            audioSource.Play();
            yield return new WaitForSeconds(minimumCallInterval);
            spammingLock = false;
        }

        #region spamTest
        private void SpammingTest()
        {
            StartCoroutine(Spam());
        }

        private IEnumerator Spam()
        {
            for (var i = 0; i < 20; i++)
            {
                Play();
                yield return new WaitForSeconds(0.1f);
            }
        }
        #endregion
        
    }
}