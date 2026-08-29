using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Services;
using PetManage.View;

namespace PetManage.ViewModels
{
    public partial class ProfileVM : ObservableObject
    {
        private readonly DatabaseService _database;

        #region Observable Property

        [ObservableProperty]
        public partial int ProfilePetID { get; set; }

        [ObservableProperty]
        public partial string ProfileNamePet { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ProfileBreedPet { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ProfileTypeOfPet { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ProfileGenderPet { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double ProfileWeightPet { get; set; }

        [ObservableProperty]
        public partial double ProfileAgePet { get; set; }

        [ObservableProperty]
        public partial string ProfileNotesPet { get; set; } = string.Empty;

        #endregion

        #region Observable Collection



        #endregion

        #region Functions

        private async Task LoadProfileAsync()
        {
            var profile = await _database.GetProfileAsync();

            if (profile != null) 
            {
                ProfilePetID = profile.PetID;
                ProfileNamePet = profile.Name;
                ProfileBreedPet = profile.Breed;
                ProfileTypeOfPet = profile.TypeOfPet;
                ProfileGenderPet = profile.Gender;
                ProfileWeightPet = profile.Weight;
                ProfileAgePet = profile.Age;
                ProfileNotesPet = profile.Notes;
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
        private async Task AddNewProfile()
        {
            await Shell.Current.GoToAsync(nameof(AddNewProfilePage));
        }

        [RelayCommand]
        private async Task EditProfile()
        {
            await Shell.Current.GoToAsync(nameof(ChangeInfoProfilePage));
        }

        #endregion

        #region Constructor

        public ProfileVM(DatabaseService database)
        {
            _database = database;

            _ = LoadProfileAsync();
        }

        #endregion
    }
}
