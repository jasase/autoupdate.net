using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUpdate.TestCore
{
    [TestClass]
    public abstract class Specification
    {
        [TestInitialize]
        public void Setup()
        {
            EstablishContext();
            Because();
        }

        public virtual void EstablishContext()
        { }

        public virtual void Because()
        { }
    }
}
