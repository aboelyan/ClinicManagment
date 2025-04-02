using ClinicManagment.Data;
using System.Text.Json;

namespace ClinicManagment.Services
{
    public class LocalStorageService
    {
        private readonly string _basePath;
        
        public LocalStorageService()
        {
            _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OfflineStorage");
            Directory.CreateDirectory(_basePath);
        }

        public async Task SaveAsync<T>(string key, T data)
        {
            var filePath = Path.Combine(_basePath, $"{key}.json");
            var json = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<T> LoadAsync<T>(string key)
        {
            var filePath = Path.Combine(_basePath, $"{key}.json");
            if (!File.Exists(filePath))
                return default;

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task SyncWithDatabase(ClinicDbContext context)
        {
            // Implement sync logic here
        }
    }
}
