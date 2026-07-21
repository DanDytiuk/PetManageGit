using PetManage.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
        }

        public async Task<SettingsModel> GetSettingsAsync()
        {
            await InitializeAsync();

            var settings =
                await _database.Table<SettingsModel>()
                    .FirstOrDefaultAsync();

            if (settings != null)
                return settings;

            settings = new SettingsModel();

            await _database.InsertAsync(settings);

            return settings;
        }

        public async Task SaveSettingsAsync(SettingsModel settings)
        {
            await InitializeAsync();

            await _database.InsertOrReplaceAsync(settings);
        }
    }
}
