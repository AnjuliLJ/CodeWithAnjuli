
using System.Diagnostics;

namespace PassXYZ.Vault;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

	protected override void OnStart()
	{
		Debug.WriteLine("PassXYZ.Vault.XYZ: OnStart");
		base.OnStart();
	}

	protected override void OnSleep()
	{
		Debug.WriteLine("PassXYZ.Vault.XYZ: OnSleep");
		base.OnSleep();
	}

	protected override void OnResume()
	{
		Debug.WriteLine("PassXYZ.Vault.XYZ: OnResume");
		base.OnResume();
	}
}
