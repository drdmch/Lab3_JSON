using System.Collections.Generic;
using GUI.Books;
using NJsonSchema;

namespace GUI;

public partial class MainPage : ContentPage
{
	private FileResult ChosenFile;

	private IFilePicker filePicker;

	private JsonSchema4 schema;

	private IList<Book> Books;
	public MainPage()
	{
		InitializeComponent();
		filePicker = FilePicker.Default;
	}
}
