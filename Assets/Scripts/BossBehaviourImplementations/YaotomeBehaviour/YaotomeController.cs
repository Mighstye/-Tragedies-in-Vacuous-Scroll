using BossBehaviour;

namespace YaotomeBehaviour
{
    public class YaotomeController: BossController
    {
        protected override void AssignAnimationLib()
        {
            animationLib = new YaotomeAnimLib();
        }
    }
}