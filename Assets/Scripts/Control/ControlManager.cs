using UnityEngine.InputSystem;
using Utils;

public class ControlManager : Singleton<ControlManager>
{
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void SwitchMap(string mapName)
    {
        playerInput.SwitchCurrentActionMap(mapName);
    }

    public void SwitchToPlayer()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }
}