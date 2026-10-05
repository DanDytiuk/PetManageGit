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
        private async Task DeleteProfile()
        {

        }

        [RelayCommand]
        private async Task EditProfile()
        {
            string request = await Shell.Current.DisplayActionSheet("Зміна даних", "Відміна", null, "Ім'я", "День народження", "Порода", "Тип пітомця", "Стать улюбленця", "Вага", "Замітки");

            if (string.IsNullOrEmpty(request) || request == "Відміна")
                return;

            switch (request)
            {
                case "Ім'я":
                {
                    string newName = await Shell.Current.DisplayPromptAsync("Зміна імені", "Введіть нове ім'я", initialValue: ProfileNamePet, accept: "ОК", cancel: "Відміна");
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        ProfileNamePet = newName;
                        await _database.UpdateProfileAsync(ProfilePetID, "Name", newName);
                    }

                    break;
                }

                case "День народження":
                {
                    string initial = ProfileBirthPet != default ? ProfileBirthPet.ToString("yyyy-MM-dd") : string.Empty;
                    string dateInput = await Shell.Current.DisplayPromptAsync("Зміна дати народження", "Введіть дату у форматі yyyy-MM-dd", initialValue: initial, accept: "ОК", cancel: "Відміна");
                    if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out var newDate))
                    {
                        ProfileBirthPet = newDate;
                        await _database.UpdateProfileAsync(ProfilePetID, "Birth", newDate);
                    }

                    break;
                }

                case "Порода":
                {
                    string newBreed = await Shell.Current.DisplayPromptAsync("Зміна породи", "Введіть породу", initialValue: ProfileBreedPet, accept: "ОК", cancel: "Відміна");
                    if (!string.IsNullOrWhiteSpace(newBreed))
                    {
                        ProfileBreedPet = newBreed;
                        await _database.UpdateProfileAsync(ProfilePetID, "Breed", newBreed);
                    }

                    break;
                }

                case "Тип пітомця":
                {
                    string newType = await Shell.Current.DisplayPromptAsync("Зміна типу", "Введіть тип пiтомця", initialValue: ProfileTypeOfPet, accept: "ОК", cancel: "Відміна");
                    if (!string.IsNullOrWhiteSpace(newType))
                    {
                        ProfileTypeOfPet = newType;
                        await _database.UpdateProfileAsync(ProfilePetID, "TypeOfPet", newType);
                    }

                    break;
                }

                case "Стать улюбленця":
                {
                    string newGender = await Shell.Current.DisplayPromptAsync("Зміна статі", "Введіть стать улюбленця", initialValue: ProfileGenderPet, accept: "ОК", cancel: "Відміна");
                    if (!string.IsNullOrWhiteSpace(newGender))
                    {
                        ProfileGenderPet = newGender;
                        await _database.UpdateProfileAsync(ProfilePetID, "Gender", newGender);
                    }

                    break;
                }

                case "Вага":
                {
                    string initialWeight = ProfileWeightPet > 0 ? ProfileWeightPet.ToString() : string.Empty;
                    string weightInput = await Shell.Current.DisplayPromptAsync("Зміна ваги", "Введіть вагу (ціле або дробне число)", initialValue: initialWeight, accept: "ОК", cancel: "Відміна");
                    if (!string.IsNullOrWhiteSpace(weightInput) && double.TryParse(weightInput, out var newWeight))
                    {
                        ProfileWeightPet = newWeight;
                        await _database.UpdateProfileAsync(ProfilePetID, "Weight", newWeight);
                    }

                    break;
                }

                case "Замітки":
                {
                    string newNotes = await Shell.Current.DisplayPromptAsync("Зміна заміток", "Введіть замітки", initialValue: ProfileNotesPet, accept: "ОК", cancel: "Відміна");
                    if (newNotes != null)
                    {
                        ProfileNotesPet = newNotes;
                        await _database.UpdateProfileAsync(ProfilePetID, "Notes", newNotes);
                    }

                    break;
                }
            }
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
