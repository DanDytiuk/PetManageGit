using PetManage.ViewModels;

namespace PetManage.View;

public partial class HealthPage : ContentPage
{
	public HealthPage(HealthVM healthVM)
	{
		BindingContext = healthVM;
		InitializeComponent();
	}
}