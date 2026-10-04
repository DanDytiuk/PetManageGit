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
        public partial NameOfCatFood SelectedFood { get; set; }

        [ObservableProperty]
        public partial double SelectedWeight { get; set; }

        [ObservableProperty]
        public partial string SelectedNotes { get; set; }

        [ObservableProperty]
        public partial TypeOfAppetite SelectedAppetite { get; set; }

        #endregion

        #region Observable Collection

        public ObservableCollection<FoodNamesPicker> FoodNamesPicker { get; } = new ObservableCollection<FoodNamesPicker>();
        public ObservableCollection<TypeOfAppetite> TypeOfAppetitePicker { get; }
        public ObservableCollection<FoodSeriesPicker> FoodSeriesPicker { get; }

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
            if (SelectedFood == null || SelectedWeight <= 0 || SelectedAppetite == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }   

            FoodModel food = new()
            {
                PetID = 0,
                FoodName = SelectedFood.ToString(),
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
            TypeOfAppetitePicker = new ObservableCollection<TypeOfAppetite>(Enum.GetValues<TypeOfAppetite>());
        }

        #endregion
    }
}
