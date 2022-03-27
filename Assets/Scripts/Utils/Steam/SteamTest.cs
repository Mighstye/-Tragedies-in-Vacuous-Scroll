using Steamworks;
using UnityEngine;

namespace Utils
{
    public class SteamTest : MonoBehaviour
    {
        private void Start()
            {
                if (!SteamManager.Initialized) return;
                var personaName = SteamFriends.GetPersonaName();
                Debug.Log(personaName);
            }
        
    }
}