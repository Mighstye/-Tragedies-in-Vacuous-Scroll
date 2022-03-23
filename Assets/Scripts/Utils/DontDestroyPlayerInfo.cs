using UnityEngine;

public class DontDestroyPlayerInfo : MonoBehaviour
{
    public static DontDestroyPlayerInfo instance;
    public static GameObject playerInfo;

    private void Awake()
    {
        playerInfo = gameObject;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(playerInfo);
        }
    }
}