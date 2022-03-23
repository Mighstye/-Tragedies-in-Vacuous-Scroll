namespace Utils
{
    public class ConstantRoot : Singleton<ConstantRoot>
    {
        protected override void Awake()
        {
            destructionProtection = true;
            base.Awake();
        }
    }
}