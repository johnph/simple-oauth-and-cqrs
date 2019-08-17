namespace Resource.Infrastructure.Modules
{
    using Autofac;
    using MediatR;
    using Resource.Infrastructure.Domain;
    using Resource.Infrastructure.Repositories;

    public class InfrastructureModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public InfrastructureModule(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(
                typeof(DomainEventsDispatcherCommandHandlerDecorator<>),
                typeof(IRequestHandler<,>));

            builder.RegisterType<EmployeeRepository>()
               .As<IEmployeeRepository>()
               .InstancePerLifetimeScope();
        }
    }
}
