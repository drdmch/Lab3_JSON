using System.Text;
using System.Text.Json;
using System.Diagnostics;
using NJsonSchema;

namespace GUI;

public partial class MainPage : ContentPage
{
	private void SaveData() {
		File.WriteAllText(ChosenFile.FullPath, string.Empty);
		JsonSerializerOptions options= new() { WriteIndented = true };
		File.WriteAllText(ChosenFile.FullPath, JsonSerializer.Serialize(Books, options));
	}

	private async void ExitButton_Clicked(object? _, EventArgs e)
	{
		var option = await DisplayAlert("Confirm exit", "Are you sure tou want to exit the program ?", "Yes", "No");
		if (option)
		{
			System.Environment.Exit(0);
		}
	}

	private async void OpenButton_Clicked(object? _, EventArgs e)
	{
		if (await Permissions.CheckStatusAsync<Permissions.StorageRead>() != PermissionStatus.Granted
		&& await Permissions.RequestAsync<Permissions.StorageRead>() != PermissionStatus.Granted)
		{
			return;
		}

		if (await Permissions.CheckStatusAsync<Permissions.StorageRead>() != PermissionStatus.Granted
		&& await Permissions.RequestAsync<Permissions.StorageRead>() != PermissionStatus.Granted)
		{
			return;
		}

		if (schema is null)
		{
            schema = await JsonSchema4.FromTypeAsync<List<GUI.Books.Book>>();
        }

        var customFileType = new FilePickerFileType(
				new Dictionary<DevicePlatform, IEnumerable<string>>
				{
					{ DevicePlatform.Android, new[] { "application/json" } },
					{ DevicePlatform.WinUI, new[] {".json"} }
				});
		var options = new PickOptions() { PickerTitle = "Select json file with books", FileTypes = customFileType };
		ChosenFile = await filePicker.PickAsync(options);

		if (ChosenFile == null)
		{
			return;
		}
        StringBuilder builder = new();

		using (var stream = await ChosenFile.OpenReadAsync())
		{
			int b = stream.ReadByte();
			while (b != -1)
			{
				builder.Append((char)b);
				b = stream.ReadByte();
			}
		}
		var errors = schema.Validate(builder.ToString());
		if (errors.Count > 0)
		{
			foreach (var err in errors) {
				Debug.WriteLine(err);
			}
			await DisplayAlert("Error", "The file violates standard format", "Ok");
			return;
		}
		Books = JsonSerializer.Deserialize<List<GUI.Books.Book>>(builder.ToString());
		Title = "JsonEditor - " + ChosenFile.FileName;
		ClearResults();
		DisplayResults(Books);
	}

	private async void FindButton_Clicked(object? _, EventArgs e)
	{
		if (ChosenFile == null)
		{
			await DisplayAlert("Error", "Input file is not chosen", "Ok");
			return;
		}

		Find();
	}

	private void ClearButton_Clicked(object? _, EventArgs e)
	{
		ClearFilters();
		DisplayResults(Books);
	}

	private async void ShowInfo(object? _, EventArgs args) {
		await DisplayAlert("Information", "Author: Demchik Daria, 2 course, K-26\nJsonEditor allows the user to view and edit json files associated with books", "Ok");
	}

	private async void AddButton_Clicked(object? _, EventArgs args) {
		Books.Add(new GUI.Books.Book());
		await Navigation.PushModalAsync(new EditPage(Books, Books.Count - 1, () => { SaveData(); Find(); }));
	}
}
