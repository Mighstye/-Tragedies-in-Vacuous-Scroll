using Utils;

namespace BossBehaviour
{
    public class BossBehaviourSystemProxy : Singleton<BossBehaviourSystemProxy>
    {
        public BossController bossController;
        public void ReassignController(BossController controller)
        {
            bossController = controller;
        }
    }
    
    
}