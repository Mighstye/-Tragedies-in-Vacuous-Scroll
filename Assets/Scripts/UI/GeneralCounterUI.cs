using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class GeneralCounterUI : MonoBehaviour
    {
        [SerializeField] private GameObject iconPrefab;
        private List<GameObject> icons;

        private void Start()
        {
            Init(0);
        }

        public void Init(int initialCount)
        {
            icons ??= new List<GameObject>(GetComponentsInChildren<Transform>().Select(t => t.gameObject));
            icons.Remove(gameObject);
            if (icons.Count >= initialCount) return;
            for (var i = icons.Count; i < initialCount; i++)
            {
                var icon = Instantiate(iconPrefab, transform);
                icons.Add(icon);
            }
        }

        public void UpdateIcons(int activeCount)
        {
            for (var i = 0; i < activeCount; i++) icons[i].SetActive(true);

            for (var i = activeCount; i < icons.Count; i++) icons[i].SetActive(false);
        }
    }
}