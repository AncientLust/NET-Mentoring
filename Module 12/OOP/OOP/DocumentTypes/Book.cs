namespace OOP.DocumentTypes;

internal class Book 
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public DateTime PublishDate { get; set; }
    public int NumberOfPages { get; set; }
    public string Publisher { get; set; }


    //public Book(ulong cardNumber, string isbn, string title, string authors,
    //    DateTime publishDate, int numberOfPages, string publisher)
    //{
    //    Type = nameof(Book);
    //    CardNumber = cardNumber;

    //    ISBN = isbn;
    //    Title = title;
    //    Authors = authors;
    //    PublishDate = publishDate;
    //    NumberOfPages = numberOfPages;
    //    Publisher = publisher;
    //}
}

