namespace AutoUpdate.TestCore
{
    public interface ITestModule
    {
        void Setup(Specification specification);

        void CleanUp();
    }
}
