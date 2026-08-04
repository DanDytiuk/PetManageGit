using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Models;
using PetManage.Services;
using PetManage.View;
using System.Collections.ObjectModel;


namespace PetManage.ViewModels
{
    public partial class FoodVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region ObservableProperty

        [ObservableProperty]
        private string foodName; 

        [ObservableProperty]
        private string typeOfFood;

        [ObservableProperty]
        private double weight;

        [ObservableProperty]
        private DateTime foodDate;

        #endregion

        #region ObservableCollection

        public ObservableCollection<FoodModel> FoodList { get; } = new();

        #endregion

        #region Commands

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task OpenAddInfoFoodPage()
        {
            await Shell.Current.GoToAsync(nameof(AddInfoFoodPage));
        }

        [RelayCommand]
        public async Task LoadInfoFood()
        {
            FoodList.Clear();

            var foodList = await _database.GetFoodAsync();

            foreach (var food in foodList)
            {
                FoodList.Add(food);
            }
        }

        #endregion

        #region Constructor

        public FoodVM(DatabaseService database)
        {
            _database = database;

        }

        #endregion



    }
}
