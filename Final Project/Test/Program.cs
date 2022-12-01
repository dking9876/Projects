namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestDbConnection();
        }
        static public void  TestDbConnection()
        {
            var db = new DataLayer.DbConnection();
            db.Connect("https://booksstore.documents.azure.com:443/", "9hRgHS70P6B2vo4KrK34Os75nFNDrp3N8eE8w6YRH0MYppNhjnUlGptXbEMKAC5EPe5ENVryQnucACDbWJyClw==");
        }
    }
}
