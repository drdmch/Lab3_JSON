namespace GUI.Books;

public struct Filters
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Author { get; set; }

    public string Genre { get; set; }

    public string Year { get; set; }
    public string MinYear { get; set; }
    public string MaxYear { get; set; }

    public Filters()
    {
        Title = "";
        Description = "";
        Author = "";
        Genre = "";
        Year = "";
        MinYear = "";
        MaxYear = "";   
    }

    public readonly bool ValidateBook(GUI.Books.Book book)
    {
        var title = book.Title.ToLower().Contains(Title.ToLower());
        var year = book.Year.ToLower().Contains(Year.ToLower());
        var description = book.Description.ToLower().Contains(Description.ToLower());
        var genre = book.Genre.ToLower().Contains(Genre.ToLower());
        var author = book.Author.GetFullName().ToLower().Contains(Author.ToLower());

        bool rangeOk = true;
        if (!string.IsNullOrWhiteSpace(MinYear))
        {
            if (!int.TryParse(MinYear, out int min)) return false;
            if (!int.TryParse(book.Year, out int by)) return false;
            if (by < min) rangeOk = false;
        }
        if (!string.IsNullOrWhiteSpace(MaxYear))
        {
            if (!int.TryParse(MaxYear, out int max)) return false;
            if (!int.TryParse(book.Year, out int by)) return false;
            if (by > max) rangeOk = false;
        }

        return title && year && description && genre && author && rangeOk;
    }
}
