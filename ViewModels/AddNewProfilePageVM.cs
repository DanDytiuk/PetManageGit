using CommunityToolkit.Mvvm.ComponentModel;
using PetManage.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PetManage.ViewModels
{
    public partial class AddNewProfilePageVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region ObservableProperty

        [ObservableProperty]
        private string newPetname;



        [ObservableProperty]
        private string newBreedPet;

        #endregion

        #region ObsevableCollection

        

        #endregion
    }
}
