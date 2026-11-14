using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Infrastructure;
using PetManage.Models;
using PetManage.Models.ModelViewPicker;
using PetManage.Services;
using System.Collections.ObjectModel;

namespace PetManage.ViewModels
{
    public partial class AddHealthInfoVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region Observable Property

        public DateTime MinDate { get; } = new DateTime(2025, 1, 1);
        public DateTime MaxDate { get; } = new DateTime(2050, 12, 31);

        [ObservableProperty]
        public partial TypeHealthOperationModelPicker NewTypeHealthOperation { get; set; }

        [ObservableProperty]
        public partial NameHealthOperationModelPicker NewNameHealthOperations { get; set; }

        [ObservableProperty]
        public partial DateTime NewHealthOperationDate { get; set; } = DateTime.Today;

        [ObservableProperty]
        public partial TimeSpan NewHealthOperationTime { get; set; }

        [ObservableProperty]
        public partial string NewNotes { get; set; } = string.Empty;

        #endregion

        #region Observable Collection

        public ObservableCollection<TypeHealthOperationModelPicker> TypeHealthOperationPicker { get; } = [];
        public ObservableCollection<NameHealthOperationModelPicker> NameHealthOperationPicker { get; } = [];
        public ObservableCollection<HealthModel> HealthList { get; } = [];

        #endregion

        #region Functions

        public async Task LoadTypeHealthOperationAsync()
        {
            TypeHealthOperationPicker.Clear();

            var typeHealthOperations = await _database.GetTypeHealthOperationsAsync();

            foreach (var typeHealthOperation in typeHealthOperations)
            {
                TypeHealthOperationPicker.Add(new TypeHealthOperationModelPicker
                {
                    Id = typeHealthOperation.Id,
                    LocalizationKey = typeHealthOperation.LocalizationKey
                });
            }
        }

        public async Task LoadNameHealthOperationAsync()
        {
            NameHealthOperationPicker.Clear();

            var nameHealthOperations = await _database.GetNameHealthOperationsAsync(NewTypeHealthOperation?.Id ?? 0);

            foreach (var nameHealthOperation in nameHealthOperations)
            {
                NameHealthOperationPicker.Add(new NameHealthOperationModelPicker
                {
                    Id = nameHealthOperation.Id,
                    LocalizationKey = nameHealthOperation.LocalizationKey
                });
            }
        }

        #endregion

        #region Commands

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task SaveInfoHealth()
        {
            if (NewTypeHealthOperation == null
                || NewNameHealthOperations == null
                || NewHealthOperationDate == default)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Please fill in all required fields.", "OK");
                return;
            }

            HealthModel health = new()
            {
                NameOfHealthOperation = NewNameHealthOperations?.DisplayName ?? string.Empty,
                TypeHealthOperation = NewTypeHealthOperation?.DisplayName ?? string.Empty,
                Notes = NewNotes,
                HealthOperationDate = NewHealthOperationDate,
                HealthOperationTime = NewHealthOperationTime
            };

            await _database.SaveHealthAsync(health);
        }

        partial void OnNewTypeHealthOperationChanged(TypeHealthOperationModelPicker value)
        {
            if (value != null)
            {
                _ = LoadNameHealthOperationAsync();
            }
        }

        #endregion

        #region Constructor

        public AddHealthInfoVM(DatabaseService database)
        {
            _database = database;
            _ = LoadTypeHealthOperationAsync();
            
        }

        #endregion

    }
}
