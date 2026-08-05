using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Services;

namespace PetManage.ViewModels
{
    public partial class ProfileVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region Observable Property

        [ObservableProperty]
        private int profilePetID;

        [ObservableProperty]
        private string profileNamePet;

        [ObservableProperty]
        private string profileBreedPet;

        [ObservableProperty]
        private string profileTypeOfPet;

        [ObservableProperty]
        private string profileGenderPet;

        [ObservableProperty]
        private float profileWeightPet;

        [ObservableProperty]
        private float profileAgePet;

        [ObservableProperty]
        private string profileNotesPet;

        #endregion

        #region Observable Collection



        #endregion

        #region Commands

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        #endregion

        #region Constructor

        public ProfileVM(DatabaseService database)
        {
            _database = database;
        }

        #endregion
    }
}
