using ToDoApp.Data.Enums;

namespace ToDoApp.Data
{
    public class ServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _contextAccessor;

        public ServiceFactory(IServiceProvider serviceProvider, IHttpContextAccessor contextAccessor)
        {
            _serviceProvider = serviceProvider;
            _contextAccessor = contextAccessor;
        }

        public T GetService<T>()
        {
            StorageType storage = StorageType.Sql;

            if (_contextAccessor.HttpContext != null)
            {
                storage = (StorageType?)_contextAccessor.HttpContext.Session.GetInt32(nameof(StorageType)) ?? StorageType.Sql;
            }

            return _serviceProvider.GetKeyedService<T>(storage) ?? throw new KeyNotFoundException();
        }
    }
}
