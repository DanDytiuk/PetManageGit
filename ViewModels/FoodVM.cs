using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Models;
using PetManage.Services;
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

        #endregion

        #region ObservableCollection

        public ObservableCollection<FoodModel> FoodList { get; }

        #endregion

        #region Commands

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
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
