using PetManage.Models;
using SQLite;
using System.Diagnostics;

namespace PetManage.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public async Task InitializeAsync()
        {
            if (_database != null)
                return;

            string path = Path.Combine(
                FileSystem.AppDataDirectory,
                "PetManage.db");

            _database = new SQLiteAsyncConnection(path);

            await _database.CreateTableAsync<SettingsModel>();
            await _database.CreateTableAsync<ProfileModel>();
            await _database.CreateTableAsync<FoodModel>();
        }

        #region GetDataAsync

        public async Task<SettingsModel> GetSettingsAsync()
        {
            await InitializeAsync();

            var settings = await _database.Table<SettingsModel>()
                    .FirstOrDefaultAsync();

            if (settings != null) return settings;

            settings = new SettingsModel();

            await _database.InsertAsync(settings);

            return settings;
        }

        public async Task<List<FoodModel>> GetFoodAsync()
        {
            await InitializeAsync();

            return await _database.Table<FoodModel>()
                                  .OrderByDescending(x => x.DateOfEat)
                                  .ToListAsync();
        }

        #endregion



        public async Task SaveSettingsAsync(SettingsModel settings)
        {
            await InitializeAsync();

            await _database.InsertOrReplaceAsync(settings);
        }

        public async Task<int> SaveFoodAsync(FoodModel food)
        {
            await InitializeAsync();
            return await _database.InsertAsync(food);
        }
    }
}
