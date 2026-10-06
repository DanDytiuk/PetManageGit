using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Infrastructure;
using PetManage.Models;
using PetManage.Models.ModelViewPicker;
using PetManage.Services;
using System.Collections.ObjectModel;

namespace PetManage.ViewModels
{
    public partial class AddInfoFoodVM : ObservableObject
    {
        private readonly DatabaseService _database;
        
        #region Observable Property

        [ObservableProperty]
        public partial FoodNames SelectedNameOfFood { get; set; }

        [ObservableProperty]
        public partial FoodSeriesName SelectedFoodSeries { get; set; }

        [ObservableProperty]
        public partial double SelectedWeight { get; set; }

        [ObservableProperty]
        public partial string SelectedNotes { get; set; }

        [ObservableProperty]
        public partial TypeOfAppetite SelectedAppetite { get; set; }

        #endregion

        #region Observable Collection

        public ObservableCollection<FoodNamesPicker> FoodNamesPicker { get; } = new ObservableCollection<FoodNamesPicker>();
        public ObservableCollection<TypeOfAppetite> TypeOfAppetitePicker { get; } = new ObservableCollection<TypeOfAppetite>();
        public ObservableCollection<FoodSeriesPicker> FoodSeriesPicker { get; } = new ObservableCollection<FoodSeriesPicker>();

        #endregion

        #region Functions

        public async Task LoadNameFoodAsync()
        {
            FoodNamesPicker.Clear();

            var foodNames = await _database.GetNameFoodsAsync();

            foreach (var foodName in foodNames)
            {
                FoodNamesPicker.Add(new FoodNamesPicker
                {
                    ID = foodName.FoodID,
                    LocalizationKey = foodName.LocalizationCode
                });
            }
        }

        public async Task LoadFoodSeriesAsync()
        {
            FoodSeriesPicker.Clear();

            var foodSeries = await _database.GetFoodSeriesAsync(SelectedNameOfFood.FoodID);

            foreach (var series in foodSeries)
            {
                FoodSeriesPicker.Add(new FoodSeriesPicker
                {
                    ID = series.FoodID,
                    LocalizationKey = series.LocalizationCode
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
        private async Task SaveInfoFood()
        {
            if (SelectedNameOfFood == null || SelectedFoodSeries == null || SelectedWeight <= 0 || SelectedAppetite == null)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Please fill in all required fields.", "OK");
                return;
            }   

            FoodModel food = new()
            {
                PetID = 0,
                FoodName = SelectedNameOfFood.ToString(),
                FoodSeries = SelectedFoodSeries.ToString(),
                Weight = SelectedWeight,
                Notes = SelectedNotes,
                Appetite = SelectedAppetite.ToString(),
                DateOfEat = DateTime.Now
            };

            await _database.SaveFoodAsync(food);
        }

        #endregion

        #region Constructor

        public AddInfoFoodVM(DatabaseService database)
        {
            _database = database;

            _ = LoadNameFoodAsync();
            _ = LoadFoodSeriesAsync();
            TypeOfAppetitePicker = new ObservableCollection<TypeOfAppetite>(Enum.GetValues<TypeOfAppetite>());
        }

        #endregion
    }
}
