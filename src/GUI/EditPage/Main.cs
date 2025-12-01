using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using GUI.Books;

namespace GUI;

public delegate void MyCallback();

public partial class EditPage : ContentPage
{
	private IList<GUI.Books.Book> books;
	private int index;

	private readonly NavigationBar navigationBar;

	private readonly MyCallback cb;
	public EditPage(IList<GUI.Books.Book> books, int index, MyCallback callback)
	{
		InitializeComponent();
		this.books = books;
		this.index = index;
		cb = callback;
		TitleEntry.Text = books[index].Title;
		GenreEntry.Text = books[index].Genre;
		YearEntry.Text = books[index].Year;
		AuthorEntry.Text = books[index].Author.GetFullName();
		DescriptionEntry.Text = books[index].Description;

		navigationBar = new NavigationBar();

		navigationBar.BackButton.Text = "Save";
		navigationBar.RightButton.Text = "Delete";
		navigationBar.TitleLabel.Text = "Edit: " + books[index].Title;

		navigationBar.BackButton.Command = new Command(() => OnBackButtonPressed());
		navigationBar.RightButton.Command = new Command(() => Delete());

		Grid.SetRow(navigationBar, 0);
		Grid.SetColumn(navigationBar, 0);
		Grid.SetColumnSpan(navigationBar, 2);

		grid.Children.Add(navigationBar);
	}

	public void ApplyChanges()
	{
		string[] AuthorContent = (AuthorEntry.Text ?? "").Split(" ");
		StringBuilder builder = new();

		int ind = AuthorContent.Length > 2 ? 1 : 0;
		books[index].Author.FirstName = AuthorContent[0];
		books[index].Author.MiddleName = index > 0 ? AuthorContent[ind] : "";

		while (++ind < AuthorContent.Length)
		{
			builder.Append(AuthorContent[ind]);
			if (ind + 1 != AuthorContent.Length)
			{
				builder.Append(" ");
			}
		}

		books[index].Author.LastName = builder.ToString();
		books[index].Title = TitleEntry.Text ?? "";
		books[index].Genre = GenreEntry.Text ?? "";
		books[index].Year = YearEntry.Text ?? "";
		books[index].Description = DescriptionEntry.Text ?? "";
	}

	protected override bool OnBackButtonPressed()
	{
		ApplyChanges();
		cb();
		return base.OnBackButtonPressed();
	}

	private void OnExit(object? _, EventArgs a)
	{
		OnBackButtonPressed();
	}

	private void Delete()
	{
		books.RemoveAt(index);
		cb();
		base.OnBackButtonPressed();
	}

	private void OnDelete(object? _, EventArgs args)
	{
		Delete();
	}
}
