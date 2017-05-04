using SugarFactory.Data;

namespace SugarFactory.Services
{
    public class Service
    {
        protected Service()
        {
            this.Context = new SugarFactoryContext();
        }

        protected SugarFactoryContext Context { get; private set; }
    }
}
