namespace GUI;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Maui.Graphics;

public partial class MainPage : ContentPage
{
	private GUI.Books.Filters CollectFilters()
	{
		var filters = new GUI.Books.Filters();
		if (TitleCheckbox.IsChecked)
		{
			filters.Title = TitleEntry.Text ?? "";
		}

		if (DescriptionCheckbox.IsChecked)
		{
			filters.Description = DescriptionEntry.Text ?? "";
		}

		if (AuthorCheckbox.IsChecked)
		{
			filters.Author = AuthorEntry.Text ?? "";
		}

		if (GenreCheckbox.IsChecked)
		{
			filters.Genre = GenreEntry.Text ?? "";
		}

		if (YearCheckbox.IsChecked)
		{
			filters.Year = YearEntry.Text ?? "";
		}

		return filters;
	}

	private void ClearFilters()
	{
		TitleEntry.Text = "";
		TitleCheckbox.IsChecked = false;
		AuthorEntry.Text = "";
		AuthorCheckbox.IsChecked = false;
		GenreEntry.Text = "";
		GenreCheckbox.IsChecked = false;
		YearEntry.Text = "";
		YearCheckbox.IsChecked = false;
		DescriptionEntry.Text = "";
		DescriptionCheckbox.IsChecked = false;
	}

	private void ClearResults()
	{
		while (ResultsTable.Children.Count > 5)
		{
			ResultsTable.Children.RemoveAt(5);
		}
		while (ResultsTable.RowDefinitions.Count > 1)
		{
			ResultsTable.RowDefinitions.RemoveAt(1);
		}
	}

	private void CreateLabel(int row, int column, string text)
	{
		var label = new Label
		{
			Text = text,
			VerticalOptions = LayoutOptions.Center,
			HorizontalOptions = LayoutOptions.Center,
		};
		ResultsTable.SetRow(label, row);
		ResultsTable.SetColumn(label, column);
		ResultsTable.Children.Add(label);
	}

	private void CreateDescriptionButton(int row, GUI.Books.Book b)
	{
		var button = new Button
		{
			Text = "View",
			VerticalOptions = LayoutOptions.Center,
			HorizontalOptions = LayoutOptions.Center,
			BackgroundColor = Colors.Wheat,
			TextColor = Colors.Black,
		};
		button.Clicked += async (object? _, EventArgs _) => await DisplayAlert("Description", b.Description, "Ok");
		ResultsTable.SetRow(button, row);
		ResultsTable.SetColumn(button, 4);
		ResultsTable.Children.Add(button);
	}

	private void CreateEditButton(int row, GUI.Books.Book b)
	{
		var button = new Button
		{
			Text = "Edit",
			VerticalOptions = LayoutOptions.Center,
			HorizontalOptions = LayoutOptions.Center,
			BackgroundColor = Colors.Wheat,
			TextColor = Colors.Black,
		};

		button.Clicked += async (object? _, EventArgs _) =>
		{
			var page = new EditPage(Books, Books.IndexOf(b), () => { SaveData(); Find(); });
			await Navigation.PushModalAsync(page);
		};

		ResultsTable.SetRow(button, row);
		ResultsTable.SetColumn(button, 5);
		ResultsTable.Children.Add(button);
	}

	private void Find()
	{
		var filterOptions = CollectFilters();
		ClearResults();

		var results = (from book in Books where filterOptions.ValidateBook(book) select book).ToList();
		DisplayResults(results);
	}

	private void DisplayResult(int row, GUI.Books.Book book)
	{
		ResultsTable.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		CreateLabel(row, 0, book.Title);
		CreateLabel(row, 1, book.Author.GetFullName());
		CreateLabel(row, 2, book.Year);
		CreateLabel(row, 3, book.Genre);
		CreateDescriptionButton(row, book);
		CreateEditButton(row, book);
	}

	private void DisplayResults(IList<GUI.Books.Book> books)
	{
		for (int i = 1; i <= books.Count; ++i)
		{
			DisplayResult(i, books[i - 1]);
		}
	}
}
