using PetManage.ViewModels;

namespace PetManage.View;

public partial class AddHealthInfoPage : ContentPage
{
	public AddHealthInfoPage(AddHealthInfoVM VM)
	{
		InitializeComponent();
		BindingContext = VM;
	}
}