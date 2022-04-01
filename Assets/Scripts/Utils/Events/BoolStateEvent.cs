using UnityEngine;
namespace Utils.Events
{
    [CreateAssetMenu(fileName = "BoolState", menuName = "Events/Bool State Event (Bool->void)", order = 0)]
    public class BoolStateEvent: GenericEvent<bool>
    {
        
    }
}