using System.Text.Json.Serialization;

namespace GUI.Books;

public class Author
{
    [JsonInclude]
    public string FirstName { get; set; }

    [JsonInclude]
    public string MiddleName { get; set; }

    [JsonInclude]
    public string LastName { get; set; }

    public Author()
    {
        FirstName = "";
        MiddleName = "";
        LastName = "";
    }

    public string GetFullName()
    {
        return FirstName + (MiddleName == "" ? "" : " ") + MiddleName + (LastName == "" ? "" : " ") + LastName;
    }
}