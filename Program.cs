using System;
using System.Linq;

namespace EFStudiiDeCaz
{
    class Program
    {
        static void Main(string[] args)
        {
            // SeedDatabase();
            Console.ReadLine();
        }

        private void SeedDatabase()
        {
            CreateEmployees();
            CreatePhotograph();
            CreateBusiness();
            CreateProduct();
        }

        private void CreateEmployees()
        {
            using (var context = new EmployeeContext())
            {
                Model.FullTimeEmployee fullTimeEmployee;

                fullTimeEmployee = new Model.FullTimeEmployee
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Salary = 3200
                };
                context.Employees.Add(fullTimeEmployee);

                fullTimeEmployee = new Model.FullTimeEmployee {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Salary = 3000
                };
                context.Employees.Add(fullTimeEmployee);

                var hourlyPayedmployee = new Model.HourlyEmployee {
                    FirstName = "Tom",
                    LastName = "Jones",
                    Wage = 12
                };
                context.Employees.Add(hourlyPayedmployee);
                
                context.SaveChanges();
            }

            using (var context = new EmployeeContext())
            {
                Console.WriteLine("---All Employees ---");
                foreach (var emp in context.Employees) {
                    bool fullTime = emp is Model.HourlyEmployee ? false : true;
                    Console.WriteLine("{0} {1} ({2})", emp.FirstName, emp.LastName, fullTime ? "Full Time" : "Hourly");
                }
                Console.WriteLine("---Full Time ---");
                foreach (var fte in context.Employees.OfType<Model.FullTimeEmployee>()) { Console.WriteLine("{0} {1}", fte.FirstName, fte.LastName); }
                Console.WriteLine("---Hourly ---");
                foreach (var hourly in context.Employees.OfType<Model.HourlyEmployee>()) { Console.WriteLine("{0} {1}", hourly.FirstName, hourly.LastName); }
            }
        }

        private void CreatePhotograph()
        {
            byte[] thumbBits = new byte[100];
            byte[] fullBits = new byte[2000];

            using (var context = new PhotographContext())
            {
                var photograph = new Model.Photograph {
                    Title = "Bird",
                    ThumbnailBits = thumbBits
                };

                var fullImage = new Model.PhotographFullImage {
                    HighResolutionBits = fullBits
                };

                photograph.PhotographFullImage = fullImage;
                
                context.Photographs.Add(photograph);
                
                context.SaveChanges();
            }

            using (var context = new PhotographContext())
            {
                foreach (var photo in context.Photographs)
                {
                    Console.WriteLine("Photo: {0}, ThumbnailSize {1} bytes", photo.Title, photo.ThumbnailBits.Length);

                    context
                        .Entry(photo)
                        .Reference(p => p.PhotographFullImage)
                        .Load();
                    
                    Console.WriteLine("Full Image Size: {0} bytes", photo.PhotographFullImage.HighResolutionBits.Length);
                }
            }
        }

        private void CreateBusiness()
        {
            using (var context = new BusinessContext()){
                var business = new Model.Business{
                    Name = "GPS Security",
                    LicenseNumber = "LN555333"
                };
                context.Businesses.Add(business);

                var retail = new Model.Retail {
                    Name = "Foodpanda",
                    LicenseNumber = "LN252550",
                    Address = "str Street 1",
                    City = "Iasi",
                    State = "IS",
                    ZIPCode = "700300"
                };
                context.Businesses.Add(retail);

                var web = new Model.ECommerce {
                    Name = "Examle.com",
                    LicenseNumber = "LN890210",
                    URL = "www.example.com"
                };
                context.Businesses.Add(web);

                context.SaveChanges();
            }

            using (var context = new BusinessContext()){ Console.WriteLine("\n---All Businesses ---");
                foreach (var b in context.Businesses) 
                {

                    Console.WriteLine("{0} (#{1})", b.Name, b.LicenseNumber);
                }
                Console.WriteLine("\n---Retail Businesses ---"); 
                foreach (var r in context.Businesses.OfType<Model.Retail>()) 
                {
                    Console.WriteLine("{0} (#{1})", r.Name, r.LicenseNumber);
                    Console.WriteLine("{0}", r.Address); 
                    Console.WriteLine("{0}, {1} {2}", r.City, r.State, r.ZIPCode);
                } Console.WriteLine("\n---eCommerce Businesses ---"); 
                foreach (var e in context.Businesses.OfType<Model.ECommerce>())
                {
                    Console.WriteLine("{0} (#{1})", e.Name, e.LicenseNumber);
                    Console.WriteLine("Online address is: {0}", e.URL); }
            }
        }

        private void CreateProduct()
        {
            using (var context = new ProductContext())
            {
                var product = new Model.Product {
                    SKU = 1,
                    Description = "VS Code",
                    Price = 0,
                    ImageURL = "vscode.png"
                };
                context.Products.Add(product); product = new Model.Product

                {
                    SKU = 2,
                    Description = "GSuite",
                    Price = 50,
                    ImageURL = "gsuite.png"
                };
                context.Products.Add(product);
                
                product = new Model.Product {
                    SKU = 3,
                    Description = "Azure",
                    Price = 12,
                    ImageURL = "azure.png"
                };
                context.Products.Add(product);
                
                product = new Model.Product {
                    SKU = 4,
                    Description = "MBA Course",
                    Price = 399,
                    ImageURL = "mba-course.png"
                };
                context.Products.Add(product);
                
                context.SaveChanges();
            }

            using (var context = new ProductContext()) {
                foreach (var p in context.Products) {
                    Console.WriteLine("{0} {1} {2} {3}", p.SKU, p.Description, p.Price.ToString("C"), p.ImageURL);
                }
            }
        }
    }
}
