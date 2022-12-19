using MauiMvvmTest.Pages;
using MauiMvvmTest.ViewModels;

namespace MauiMvvmTest;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();

		builder.Services.AddSingleton<Page1>();
        builder.Services.AddSingleton<Page1ViewModel>();

        builder.Services.AddSingleton<Pages.Page2>();
        builder.Services.AddSingleton<Page2ViewModel>();

        return builder.Build();
	}
}
