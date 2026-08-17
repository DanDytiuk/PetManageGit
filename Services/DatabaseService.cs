using Android.Net.Wifi.Aware;
using PetManage.Models;
using SQLite;
using System.Diagnostics;
using System.Net.WebSockets;

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
            await _database.CreateTableAsync<AnimalTypeModel>();
            await _database.CreateTableAsync<BreedModel>();
            await _database.CreateTableAsync<AppetiteModel>();
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

        public async Task InitializeAnimalTypesAsync()
        {
            var count = await _database.Table<AnimalTypeModel>().CountAsync();

            if (count > 0)
                return;

            var AnimalTypes = new List<AnimalTypeModel>
            {
                new() {LocalizationCode = "AnimalType_Cat"},
                new() {LocalizationCode = "AnimalType_Dog"},
                new() {LocalizationCode = "AnimalType_Hamster"},
                new() {LocalizationCode = "AnimalType_Rabbit"},
                new() {LocalizationCode = "AnimalType_Parrot"},
                new() {LocalizationCode = "AnimalType_Turtle"},
                new() {LocalizationCode = "AnimalType_Fox"},
                new() {LocalizationCode = "AnimalType_Lizard" },
                new() {LocalizationCode = "AnimalType_Spyder"},
                new() {LocalizationCode = "AnimalType_Snake" },
                new() {LocalizationCode = "AnimalType_Other" }
            };

            await _database.InsertAllAsync(AnimalTypes);
        }

        public async Task InitializeAppetiteTypesAsync()
        {

            var count = await _database.Table<AppetiteModel>().CountAsync();

            if (count > 0)
                return;

            var appetiteTypes = new List<AppetiteModel>
            {   
                new() {LocalizationCode = "Appetite_Beautiful"},
                new() {LocalizationCode = "Appetite_Normal"},
                new() {LocalizationCode = "Appetite_Good"},
                new() {LocalizationCode = "Appetite_Bad"},
                new() {LocalizationCode = "Appetite_VeryBad"}
            };

            await _database.InsertAllAsync(appetiteTypes);
        }

        public async Task InitializeBreedsAsync()
        {
            var count = await _database.Table<BreedModel>().CountAsync();

            if (count > 0) return;

            var breeds = new List<BreedModel>
            {
                new() {AnimalID = 1, LocalizationCode = }
            }
        }

        #endregion

        #region SaveAsync

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

        #endregion
    }
}
