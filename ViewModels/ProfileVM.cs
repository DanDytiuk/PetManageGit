using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Services;
using PetManage.View;

namespace PetManage.ViewModels
{
    public partial class ProfileVM : ObservableObject
    {
        private readonly DatabaseService _database;
        private readonly LocalizationManager _loc = LocalizationManager.Instance;

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
        public partial DateTime ProfileBirthPet { get; set; }

        [ObservableProperty]
        public partial string ProfileNotesPet { get; set; } = string.Empty;

        public string BreedDisplayName => LocalizationManager.Instance[ProfileBreedPet];
        public string AnimalTypeDisplayName => LocalizationManager.Instance[ProfileTypeOfPet];
        public string GenderDisplayName => LocalizationManager.Instance[ProfileGenderPet];

        public DateTime MinDate { get; } = new DateTime(2020, 1, 1);
        public DateTime MaxDate { get; } = new DateTime(2099, 12, 31);

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
                ProfileBirthPet = profile.Birth;
                ProfileNotesPet = profile.Notes;
                OnPropertyChanged(nameof(BreedDisplayName));
                OnPropertyChanged(nameof(AnimalTypeDisplayName));
                OnPropertyChanged(nameof(GenderDisplayName));
            }

        }

        private async Task DeleteProfileAsync()
        {
            
        }

        private async Task ChangeProfileAsync()
        {
             
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

            _loc.PropertyChanged += OnLocalizationChanged;

            _ = LoadProfileAsync();
        }

        private void OnLocalizationChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Item[]")
            {
                OnPropertyChanged(nameof(BreedDisplayName));
                OnPropertyChanged(nameof(AnimalTypeDisplayName));
                OnPropertyChanged(nameof(GenderDisplayName));
            }
        }

        #endregion
    }
}
