using PetManage.ViewModels;

namespace PetManage.View;

public partial class AddNewProfilePage : ContentPage
{
	public AddNewProfilePage(AddNewProfilePageVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}