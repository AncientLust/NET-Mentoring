using OOP.DocumentTypes;

namespace OOP
{
    internal class DataSource
    {
        public Book Book { get; set; }
        public LocalizedBook LocalizedBook { get; set; }
        public Patent Patent { get; set; }
        public Magazine Magazine { get; set; }

        public DataSource()
        {
            Book = new Book(
                1,
                1,
                "Book Title",
                "BookAuthor1, BookAuthor2",
                new DateTime(2001, 1, 1),
                50,
                "BookPublisher"
            );

            LocalizedBook = new LocalizedBook(
                2,
                2,
                "Localized Book Title",
                "LocalizedBookAuthor1, LocalizedBookAuthorAuthor2",
                new DateTime(2001, 1, 1),
                50,
                "LocalPublisher",
                "OriginalPublisher",
                "LocalizationCountry"
            );

            Patent = new Patent(
                3,
                "Patent Title",
                "PatentAuthor1, PatentAuthorAuthor2",
                new DateTime(2003, 3, 3),
                new DateTime(2023, 3, 3),
                12345678
            );

            Magazine = new Magazine(
                4,
                "Magazine Title",
                "MagazinePublisher",
                1,
                new DateTime(2004, 4, 4)
            );
        }
    }
}