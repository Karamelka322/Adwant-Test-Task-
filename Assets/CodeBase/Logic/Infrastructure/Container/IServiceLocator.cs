namespace CodeBase.Logic.Infrastructure.Container
{
    public interface IServiceLocator
    {
        void Register<TDependency>(object instance);
        void Register<TDependency_1, TDependency_2>(object instance);
        void Register(object instance);
        TDependency Get<TDependency>();
        TDependency[] GetAll<TDependency>();
    }
}