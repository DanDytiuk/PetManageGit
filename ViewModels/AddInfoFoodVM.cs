using CommunityToolkit.Mvvm.ComponentModel;
using PetManage.Infrastructure;
using PetManage.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PetManage.ViewModels
{
    public partial class AddInfoFoodVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region Observable Property

        [ObservableProperty]
        private double selectedWeight;

        [ObservableProperty]
        private string selectedNotes;

        [ObservableProperty]
        private string selectedAppetite;

        #endregion

        #region Observable Collection

        public ObservableCollection<NameOfCatFood> CatFoodPicker { get; }
        public ObservableCollection<TypeOfAppetite> TypeOfAppetitePicker { get; }

        #endregion

        public AddInfoFoodVM(DatabaseService database)
        {
            _database = database;

            CatFoodPicker = new ObservableCollection<NameOfCatFood>(Enum.GetValues<NameOfCatFood>());
            TypeOfAppetitePicker = new ObservableCollection<TypeOfAppetite>(Enum.GetValues<TypeOfAppetite>());
        }

    }
}
