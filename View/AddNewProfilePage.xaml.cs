using PetManage.ViewModels;

namespace PetManage.View;

public partial class AddNewProfilePage : ContentPage
{
    private readonly AddNewProfilePageVM ViewModel;

    public AddNewProfilePage(AddNewProfilePageVM viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;
        BindingContext = ViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await ViewModel.LoadAnimalTypesAsync();
    }
}