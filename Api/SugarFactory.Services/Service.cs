using SugarFactory.Data;

namespace SugarFactory.Services
{
    public class Service
    {
        protected SugarFactoryContext Context { get; private set; }

        protected Service()
        {
            this.Context = new SugarFactoryContext();
        }

       
    }
}
