using PetManage.ViewModels;

namespace PetManage.View;

public partial class HealthPage : ContentPage
{
    private readonly HealthVM _healthVM;

    public HealthPage(HealthVM healthVM)
    {
        InitializeComponent();

        _healthVM = healthVM;
        BindingContext = _healthVM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _healthVM.LoadInfoHealth();
    }
}