using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Models;
using PetManage.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PetManage.ViewModels
{
    public partial class HealthVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region ObservableProperty

        [ObservableProperty]
        public partial string TypeHealthOperation { get; set; } = string.Empty;
        
        [ObservableProperty]
        public partial string NameOfHealthOperation { get; set; } = string.Empty;
        
        [ObservableProperty]
        public partial DateTime HealthOperationDate { get; set; }
        
        [ObservableProperty]
        public partial string Notes { get; set; } = string.Empty;

        #endregion

        #region ObservableCollection

        public ObservableCollection<HealthModel> HealthList { get; } = [];

        #endregion

        #region Commands

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task OpenAddInfoHealthPage()
        {
            //await Shell.Current.GoToAsync(nameof(AddInfoHealthPage));
        }

        [RelayCommand]
        public async Task LoadInfoHealth()
        {
            HealthList.Clear();

            var healthList = await _database.GetHealthAsync();

            foreach (var health in healthList)
            {
                HealthList.Add(health);
            }
        }

        #endregion

        #region Constructor

        public HealthVM(DatabaseService database)
        {
            _database = database;
        }

        #endregion

    }
}
