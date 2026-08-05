using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Infrastructure;
using PetManage.Models;
using PetManage.Services;
using System.Collections.ObjectModel;

namespace PetManage.ViewModels
{
    public partial class AddInfoFoodVM : ObservableObject
    {
        private readonly DatabaseService _database;
        private FoodModel FoodModel;

        #region Observable Property

        [ObservableProperty]
        private NameOfCatFood selectedFood;

        [ObservableProperty]
        private double selectedWeight;

        [ObservableProperty]
        private string selectedNotes;

        [ObservableProperty]
        private TypeOfAppetite selectedAppetite;

        #endregion

        #region Observable Collection

        public ObservableCollection<NameOfCatFood> CatFoodPicker { get; }
        public ObservableCollection<TypeOfAppetite> TypeOfAppetitePicker { get; }

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

            CatFoodPicker = new ObservableCollection<NameOfCatFood>(Enum.GetValues<NameOfCatFood>());
            TypeOfAppetitePicker = new ObservableCollection<TypeOfAppetite>(Enum.GetValues<TypeOfAppetite>());
        }

        #endregion
    }
}
