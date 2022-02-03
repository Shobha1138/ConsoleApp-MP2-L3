using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp_MP2_L3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ITAsset3> assets = new List<ITAsset3>();
            ITAsset3Context db1 = new ITAsset3Context();

            while (true)
            {


                Console.Write("Enter type, (C)omputer or (P)hone or (R)ead or (U)pdate or (D)elete: ");
                ConsoleKeyInfo choice = Console.ReadKey();
                Console.WriteLine();


                if (choice.Key == ConsoleKey.C || choice.Key == ConsoleKey.P)
                {


                    List<ITAsset3> sorteddt = db1.ITAssets3.OrderBy(data => data.Office).ThenBy(data => data.PurchaseDate).ToList();
                    Console.WriteLine("Type".PadRight(15) + "Office".PadRight(8) + "Brand".PadRight(15) + "Model".PadRight(15) + "Purchasedate".PadRight(28) + "Price".PadRight(10));

                    foreach (ITAsset3 data in sorteddt)
                    {
                        DateTime dtnow = DateTime.Now;
                        TimeSpan diff1 = dtnow - data.PurchaseDate;

                        int alarmDays1 = 1004;
                        int alarmDays2 = 913;
                        int itemDays1 = diff1.Days;

                        if (itemDays1 > alarmDays1)


                        {

                            Console.ForegroundColor = ConsoleColor.Red;
                            if (data.Office == "SE")
                            {

                                Console.WriteLine(data.GetType().Name.PadRight(15) + data.Office.PadRight(8) + data.Brand.PadRight(15) + data.Model.PadRight(15) + data.PurchaseDate.ToString().PadRight(28) + $"SEK{(data.Price) * 8}".ToString().PadRight(10));
                                Console.ResetColor();

                            }
                            else if (data.Office == "FR")
                            {

                                Console.WriteLine(data.GetType().Name.PadRight(15) + data.Office.PadRight(8) + data.Brand.PadRight(15) + data.Model.PadRight(15) + data.PurchaseDate.ToString().PadRight(28) + $"EUR{(data.Price) * 1.5}".ToString().PadRight(10));
                                Console.ResetColor();

                            }
                            else
                            {

                                Console.WriteLine(data.GetType().Name.PadRight(15) + data.Office.PadRight(8) + data.Brand.PadRight(15) + data.Model.PadRight(15) + data.PurchaseDate.ToString().PadRight(28) + $"USD{data.Price}".ToString().PadRight(10));
                                Console.ResetColor();

                            }

                        }

                        if (itemDays1 > alarmDays2 && itemDays1 < alarmDays1)

                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;

                            if (data.Office == "SE")
                            {

                                Console.WriteLine(data.GetType().Name.PadRight(15) + data.Office.PadRight(8) + data.Brand.PadRight(15) + data.Model.PadRight(15) + data.PurchaseDate.ToString().PadRight(28) + $"SEK{(data.Price) * 8}".ToString().PadRight(10));
                                Console.ResetColor();

                            }
                            else if (data.Office == "FR")
                            {
                                data.Price = data.Price * 1.5;
                                Console.WriteLine(data.GetType().Name.PadRight(15) + data.Office.PadRight(8) + data.Brand.PadRight(15) + data.Model.PadRight(15) + data.PurchaseDate.ToString().PadRight(28) + $"EUR{(data.Price) * 1.5}".ToString().PadRight(10));
                                Console.ResetColor();

                            }
                            else
                            {

                                Console.WriteLine(data.GetType().Name.PadRight(15) + data.Office.PadRight(8) + data.Brand.PadRight(15) + data.Model.PadRight(15) + data.PurchaseDate.ToString().PadRight(28) + $"USD{data.Price}".ToString().PadRight(10));
                                Console.ResetColor();

                            }


                        }

                        if (itemDays1 < alarmDays2)
                        {
                            if (data.Office == "SE")
                            {


                                Console.WriteLine(data.GetType().Name.PadRight(15) + data.Office.PadRight(8) + data.Brand.PadRight(15) + data.Model.PadRight(15) + data.PurchaseDate.ToString().PadRight(28) + $"SEK{(data.Price) * 8}".ToString().PadRight(10));

                            }
                            else if (data.Office == "FR")
                            {

                                Console.WriteLine(data.GetType().Name.PadRight(15) + data.Office.PadRight(8) + data.Brand.PadRight(15) + data.Model.PadRight(15) + data.PurchaseDate.ToString().PadRight(28) + $"EUR{(data.Price) * 1.5}".ToString().PadRight(10));

                            }
                            else
                            {


                                Console.WriteLine(data.GetType().Name.PadRight(15) + data.Office.PadRight(8) + data.Brand.PadRight(15) + data.Model.PadRight(15) + data.PurchaseDate.ToString().PadRight(28) + $"USD{data.Price}".ToString().PadRight(10));

                            }

                        }




                    }



                    Console.WriteLine("Enter Office(SE/FR/US): ");
                    string Office = Console.ReadLine();
                    Console.WriteLine("Brand: ");
                    string Brand = Console.ReadLine();
                    Console.WriteLine("Model: ");
                    string Model = Console.ReadLine();
                    Console.WriteLine("Computer ParchaseDate(YYYY-MM-DD): ");
                    DateTime PurchaseDate;
                    DateTime.TryParse(Console.ReadLine(), out PurchaseDate);
                    Console.WriteLine("Computer Price: ");
                    int Price = int.Parse(Console.ReadLine());


                    if (choice.Key == ConsoleKey.C)
                    {
                        db1.ITAssets3.Add(new Computer(Brand, Model, PurchaseDate, Price, Office));
                        db1.SaveChanges();
                    }
                    if (choice.Key == ConsoleKey.P)
                    {
                        db1.ITAssets3.Add(new Phone(Brand, Model, PurchaseDate, Price, Office));
                        db1.SaveChanges();
                    }



                }




                if (choice.Key == ConsoleKey.U)
                {
                    UpdateInDatabase();
                    Console.WriteLine("DATA Updated in DATABASE: ");
                    Console.WriteLine();
                    //Console.WriteLine("DATA IN ASSET DATABASE: ");
                    //Console.WriteLine("Id".PadRight(5) + "Type".PadRight(15) + "Office".PadRight(8) + "Brand".PadRight(15) + "Model".PadRight(15) + "Purchasedate".PadRight(28) + "Price".PadRight(10));
                    //db1.ITAssets3.ToList().ForEach(assetdb => Console.WriteLine(assetdb.Id.ToString().PadRight(5) + assetdb.GetType().Name.PadRight(15) + assetdb.Office.PadRight(8) + assetdb.Brand.PadRight(15) + assetdb.Model.PadRight(15) + assetdb.PurchaseDate.ToString().PadRight(28) + assetdb.Price.ToString().PadRight(10)));
                    //Console.WriteLine("Number of Assets: " + db1.ITAssets3.Count());
                    ReadFromDatabase();
                    Console.WriteLine("Number of Assets: " + db1.ITAssets3.Count());
                }



                if (choice.Key == ConsoleKey.D)
                {
                    DeleteFromDatabase();
                    Console.WriteLine("DATA Daleted in DATABASE: ");
                    Console.WriteLine();
                    Console.WriteLine("DATA IN ASSET DATABASE: ");
                    Console.WriteLine("Id".PadRight(5) + "Type".PadRight(15) + "Office".PadRight(8) + "Brand".PadRight(15) + "Model".PadRight(15) + "Purchasedate".PadRight(28) + "Price".PadRight(10));
                    db1.ITAssets3.ToList().ForEach(assetdb => Console.WriteLine(assetdb.Id.ToString().PadRight(5) + assetdb.GetType().Name.PadRight(15) + assetdb.Office.PadRight(8) + assetdb.Brand.PadRight(15) + assetdb.Model.PadRight(15) + assetdb.PurchaseDate.ToString().PadRight(28) + assetdb.Price.ToString().PadRight(10)));
                    Console.WriteLine("Number of Assets: " + db1.ITAssets3.Count());

                }


                if (choice.Key == ConsoleKey.R)
                {
                    ReadFromDatabase();
                    Console.WriteLine("Number of Assets: " + db1.ITAssets3.Count());

                }




            }

        }


        private static void ReadFromDatabase()
        {
            Console.WriteLine("Data in ITASSET database");
            Console.WriteLine("Id".PadRight(5) + "Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(15) + "Purchasedate".PadRight(28) + "Office".PadRight(15) + "Price".PadRight(10));
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ITAsset3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new
                    SqlCommand("SELECT * FROM ITAssets3 ", connection))
                using (SqlDataReader reader = command.ExecuteReader())

                    while (reader.Read())
                    {

                        int id = reader.GetInt32(0);
                        string brand = reader.GetString(1);
                        string model = reader.GetString(2);
                        DateTime purchasedate = reader.GetDateTime(3);
                        double price = reader.GetDouble(4);
                        string type = reader.GetString(6);
                        string office = reader.GetString(5);

                        Console.WriteLine(id.ToString().PadRight(5) + type.PadRight(15) + brand.PadRight(15) + model.PadRight(15) + purchasedate.ToString().PadRight(28) + office.PadRight(15) + price);


                    }


            }

        }
        private static void UpdateInDatabase()
        {

            int updatedRows;
            Console.WriteLine("Update data in Asset database");
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ITAsset3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new
                    SqlCommand("UPDATE ITAssets3 SET Brand = @brand WHERE Id = @id", connection))

                {
                    Console.WriteLine("SELECT ASSET TO UPDATE: ");
                    int value = int.Parse(Console.ReadLine());
                    command.Parameters.AddWithValue("@id", value);
                    Console.WriteLine("ENTER ASSET NEW BRAND NAME: ");
                    string newbrand = Console.ReadLine();
                    command.Parameters.AddWithValue("@brand", newbrand);
                    updatedRows = command.ExecuteNonQuery();


                }

            }



        }

        private static void DeleteFromDatabase()
        {
            {

                Console.WriteLine("Delete data in Asset database");
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ITAsset3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM ITAssets3 WHERE Id=@id", connection))
                    {
                        Console.WriteLine("SELECT ASSET TO DELETE: ");
                        int value = int.Parse(Console.ReadLine());
                        command.Parameters.AddWithValue("@id", value);
                        command.ExecuteNonQuery();
                    }

                }
            }
        }
    }
    class ITAsset3
    {


        public ITAsset3(string brand, string model, DateTime purchaseDate, double price, string office)
        {
            Brand = brand;
            Model = model;
            PurchaseDate = purchaseDate;
            Price = price;
            Office = office;

        }

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Price { get; set; }
        public string Office { get; set; }

    }

    class Computer : ITAsset3
    {
        public Computer(string brand, string model, DateTime purchaseDate, double price, string office) : base(brand, model, purchaseDate, price, office)
        {
        }
    }
    class Phone : ITAsset3
    {
        public Phone(string brand, string model, DateTime purchaseDate, double price, string office) : base(brand, model, purchaseDate, price, office)
        {
        }
    }

    class ITAsset3Context : DbContext
    {
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<ITAsset3> ITAssets3 { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ITAsset3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}

