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

        public T GetService<T>(StorageSource source) =>
            _serviceProvider.GetKeyedService<T>(GetStorageTypeOrDefault(source)) ?? throw new KeyNotFoundException();

        public StorageType GetStorageTypeOrDefault(StorageSource source)
        {
            StorageType storage = StorageType.Sql;

            if (_contextAccessor.HttpContext != null)
            {
                storage = (source) switch
                {
                    StorageSource.Session
                        => (StorageType?)_contextAccessor.HttpContext.Session.GetInt32(nameof(StorageType)) ?? storage,
                    
                    StorageSource.Header
                        => int.TryParse(_contextAccessor.HttpContext.Request.Headers["Storage"], out int type)
                            ? (StorageType?)type ?? storage
                            : storage,
                    
                    _ => storage
                };
            }
            return storage;
        }
}
}
