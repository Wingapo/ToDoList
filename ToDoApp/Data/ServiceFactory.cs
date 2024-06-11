using ToDoApp.Data.Enums;

namespace ToDoApp.Data
{
    public class ServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _contextAccessor;

        public StorageTypeLocation Location { get; set; }

        public ServiceFactory(IServiceProvider serviceProvider, IHttpContextAccessor contextAccessor)
        {
            _serviceProvider = serviceProvider;
            _contextAccessor = contextAccessor;

            Location = StorageTypeLocation.Session;
        }

        public T GetService<T>() =>
            _serviceProvider.GetKeyedService<T>(GetStorageTypeOrDefault()) ?? throw new KeyNotFoundException();

        public StorageType GetStorageTypeOrDefault()
        {
            StorageType storage = StorageType.Sql;

            if (_contextAccessor.HttpContext != null)
            {
                storage = (Location) switch
                {
                    StorageTypeLocation.Session
                        => (StorageType?)_contextAccessor.HttpContext.Session.GetInt32(nameof(StorageType)) ?? storage,
                    
                    StorageTypeLocation.Header
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
