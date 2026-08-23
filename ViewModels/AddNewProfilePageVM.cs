using CommunityToolkit.Mvvm.ComponentModel;
using PetManage.Models;
using PetManage.Models.ModelViewPicker;
using PetManage.Services;
using System.Collections.ObjectModel;

namespace PetManage.ViewModels;

public partial class AddNewProfilePageVM : ObservableObject
{
    private readonly DatabaseService _database;

    #region ObservableProperty

    [ObservableProperty]
    public partial BreedModelPicker? NewBreedPet { get; set; }

    [ObservableProperty]
    public partial AnimalModelPicker? NewAnimalPet { get; set; }

    #endregion

    #region ObservableCollection

    public ObservableCollection<AnimalModelPicker> AnimalTypes { get; } = [];

    public ObservableCollection<BreedModelPicker> BreedTypes { get; } = [];

    #endregion

    #region Constructor

    public AddNewProfilePageVM(DatabaseService database)
    {
        _database = database;
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

    #endregion
}