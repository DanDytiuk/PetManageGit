using PetManage.ViewModels;

namespace PetManage.View;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}