using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PetManage.Models;
using PetManage.Models.ModelViewPicker;
using PetManage.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PetManage.ViewModels;

public partial class AddNewProfilePageVM : ObservableObject
{
    private readonly DatabaseService _database;

    #region ObservableProperty


    [ObservableProperty]
    public partial string NewNamePet { get; set; } = string.Empty;

    [ObservableProperty]
    public partial BreedModelPicker? NewBreedPet { get; set; }

    [ObservableProperty]
    public partial AnimalModelPicker? NewAnimalPet { get; set; }

    [ObservableProperty]
    public partial GenderTypeModelPicker? NewGenderPet { get; set; }

    [ObservableProperty]
    public partial float NewWeightPet { get; set; }

    [ObservableProperty]
    public partial DateTime NewBirthPet { get; set; }

    [ObservableProperty]
    public partial string NotesPet { get; set; } = string.Empty;

    public DateTime MinDate { get; } = new DateTime(2010, 1, 1);
    public DateTime MaxDate { get; } = new DateTime(2099, 12, 31);

    #endregion

    #region ObservableCollection

    public ObservableCollection<AnimalModelPicker> AnimalTypes { get; } = new ObservableCollection<AnimalModelPicker>();

    public ObservableCollection<BreedModelPicker> BreedTypes { get; } = new ObservableCollection<BreedModelPicker>();

    public ObservableCollection<GenderTypeModelPicker> GenderTypes { get; } = new ObservableCollection<GenderTypeModelPicker>();

    #endregion

    #region Constructor

    public AddNewProfilePageVM(DatabaseService database)
    {
        _database = database;
        _ = LoadGenderASync();
    }

    #endregion

    #region PropertyChanged

    partial void OnNewAnimalPetChanged(AnimalModelPicker? value)
    {
        _ = LoadBreedsAsync(value);
    }

    #endregion

    #region Functions

    public async Task LoadAnimalTypesAsync()
    {
        AnimalTypes.Clear();

        var types = await _database.GetAnimalTypesAsync();

        foreach (var type in types)
        {
            AnimalTypes.Add(new AnimalModelPicker
            {
                Id = type.TypeID,
                LocalizationKey = type.LocalizationCode
            });
        }
    }

    private async Task LoadBreedsAsync(AnimalModelPicker? animalType)
    {
        BreedTypes.Clear();
        NewBreedPet = null;

        if (animalType == null)
            return;

        var breeds = await _database
            .GetBreedsByAnimalTypeAsync(animalType.Id);

        foreach (var breed in breeds)
        {
            BreedTypes.Add(new BreedModelPicker
            {
                Id = breed.Id,
                AnimalTypeId = breed.AnimalID,
                LocalizationKey = breed.LocalizationCode
            });
        }
    }

    private async Task LoadGenderASync()
    {
        GenderTypes.Clear();

        NewGenderPet = null;

        var gender = await _database.GetGenderTypesAsync();

        foreach (var g in gender)
        {
            GenderTypes.Add(new GenderTypeModelPicker
            {
                ID = g.Id,
                LocalizationKey = g.LocalizationName
            });

        }
    }

    #endregion

    #region RelayCommand

    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task SaveNewProfile()
    {
        if (NewWeightPet < 0)
        {
            await Shell.Current.DisplayAlert("Неправильна вага", "Введіть коректну вагу", "ОК");
            return;

        } else {

            ProfileModel profile = new()
            {
                Name = NewNamePet,
                Breed = NewBreedPet?.LocalizationKey ?? string.Empty,
                TypeOfPet = NewAnimalPet?.LocalizationKey ?? string.Empty,
                Gender = NewGenderPet?.LocalizationKey ?? string.Empty,
                Weight = NewWeightPet,
                Birth = NewBirthPet,
                Notes = NotesPet ?? string.Empty
            };

            await _database.SaveProfileAsync(profile);
        } 
        
        
    }

    #endregion

}