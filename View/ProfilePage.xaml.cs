using PetManage.ViewModels;

namespace PetManage.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfileVM VM)
	{
		InitializeComponent();
		BindingContext = VM;
	}
}