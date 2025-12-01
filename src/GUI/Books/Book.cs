using System.Text.Json.Serialization;

namespace GUI.Books;

public class Book
{
    [JsonInclude]
    public string Title { get; set; }

    [JsonInclude]
    public string Description { get; set; }

    [JsonInclude] 
    public string Year { get; set; }

    [JsonInclude]
    public string Genre { get; set; }

    [JsonInclude]
    public Author Author { get; set; }

    public Book()
    {
        Title = "";
        Description = "";
        Year = "";
        Genre = "";
        Author = new();
    }
}