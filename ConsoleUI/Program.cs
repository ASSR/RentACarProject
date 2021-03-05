using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //UsersAddTest();
            //CustomersTest();
            //ColorAddTest();
            //BrandAddTest();
            //CarAddTest();
            //RentalAddTest();
        }

        private static void RentalAddTest()
        {
            Dictionary<int, Car> cars = new Dictionary<int, Car>();
            CarManager carManager = new CarManager(new EfCarDal());
            var carList = carManager.GetAll();
            int i = 0;
            foreach (var car in carList.Data)
            {
                cars[i] = car;
                i++;
            }

            Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            var customerList = customerManager.GetAll();
            i = 0;
            foreach (var customer in customerList.Data)
            {
                customers[i] = customer;
                i++;
            }

            Dictionary<int, RentalDto> rentals = new Dictionary<int, RentalDto>();
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            Random rnd = new Random();

            rentals[0] = new RentalDto() {
                            CarId = cars[rnd.Next(0, (cars.Count() - 1))].CarId, 
                            CustomerId = customers[rnd.Next(0, (customers.Count() - 1))].CustomerId,
                            RentDate = Convert.ToDateTime("2021-04-12 10:15:00"),
                            ReturnDate = Convert.ToDateTime("2021-04-15 18:45:00")
                        };

            rentals[1] = new RentalDto() {
                            CarId = cars[rnd.Next(0, (cars.Count() - 1))].CarId,
                            CustomerId = customers[rnd.Next(0, (customers.Count() - 1))].CustomerId,
                            RentDate = Convert.ToDateTime("2021-02-10 10:15:00")
                        };

            rentals[2] = new RentalDto() {
                            CarId = cars[rnd.Next(0, (cars.Count() - 1))].CarId,
                            CustomerId = customers[rnd.Next(0, (customers.Count() - 1))].CustomerId,
                            RentDate = Convert.ToDateTime("2021-02-15 10:15:00"),
                            ReturnDate = Convert.ToDateTime("2021-02-15 18:45:00")
            };

            rentals[3] = new RentalDto() {
                            CarId = cars[rnd.Next(0, (cars.Count() - 1))].CarId,
                            CustomerId = customers[rnd.Next(0, (customers.Count() - 1))].CustomerId,
                            RentDate = Convert.ToDateTime("2021-02-15 10:15:00")
                        };

            rentals[4] = new RentalDto() {
                            CarId = cars[rnd.Next(0, (cars.Count() - 1))].CarId,
                            CustomerId = customers[rnd.Next(0, (customers.Count() - 1))].CustomerId,
                            RentDate = Convert.ToDateTime("2021-02-13 10:15:00")
                        };

            for (i = 0; i < rentals.Count(); i++)
            {
                var result = rentalManager.Add(rentals[i]);
                Console.WriteLine(result.Message);
            }
        }

        private static void CarAddTest()
        {
            Dictionary<int, Brand> brands = new Dictionary<int, Brand>();
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            var brandList = brandManager.GetAll();
            int i = 0;
            foreach (var brand in brandList.Data)
            {
                brands[i] = brand;
                i++;
            }

            Dictionary<int, Color> colors = new Dictionary<int, Color>();
            ColorManager colorManager = new ColorManager(new EfColorDal());
            var colorList = colorManager.GetAll();
            i = 0;
            foreach (var color in colorList.Data)
            {
                colors[i] = color;
                i++;
            }

            Dictionary<int, Car> cars = new Dictionary<int, Car>();
            CarManager carManager = new CarManager(new EfCarDal());
            cars[0] = new Car() { Brand = brands[0], Color = colors[0], DailyPrice = 152, ModelYear = 2015, Description = "Kamyon" };
            cars[1] = new Car() { Brand = brands[1], Color = colors[1], DailyPrice = 582, ModelYear = 2020, Description = "Otobüs" };
            cars[2] = new Car() { Brand = brands[2], Color = colors[2], DailyPrice = 529, ModelYear = 1990, Description = "Kamyonet" };
            cars[3] = new Car() { Brand = brands[3], Color = colors[3], DailyPrice = 79, ModelYear = 2008, Description = "Normal" };
            cars[4] = new Car() { Brand = brands[4], Color = colors[4], DailyPrice = 145, ModelYear = 2012, Description = "Motorsiklet" };

            for (i = 0; i < cars.Count(); i++)
            {
                var result = carManager.Add(cars[i]);
                Console.WriteLine(result.Message);
            }
        }

        private static void BrandAddTest()
        {
            List<string> brands = new List<string>
            {
                "Mercedes",
                "Pontiac",
                "BMW",
                "Ferrari",
                "McLaren",
                "Aston Martin"
            };
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            foreach (var brand in brands)
            {
                var result = brandManager.Add(new BrandAddDto() { BrandName = brand });
                Console.WriteLine(result.Message);
            } 
        }

        private static void ColorAddTest()
        {
            List<string> colors = new List<string>
            {
                "Kırmızı",
                "Siyah",
                "Mavi",
                "Yeşil",
                "Gri"
            };

            ColorManager colorManager = new ColorManager(new EfColorDal());

            foreach (var color in colors)
            {
                var result = colorManager.Add(new Color() { ColorName = color });
                Console.WriteLine(result.Message);
            }            
        }

        private static void CustomersTest()
        {
            Dictionary<int, User> users = new Dictionary<int, User>();
            /*UserManager userManager = new UserManager(new EfUserDal());
            var userList = userManager.GetAll();
            
            foreach (var user in userList.Data)
            {
                users[i] = user;
                i++;
            }*/     
            
            Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            customers[0] = new Customer() { CompanyName = "RentCar" };
            customers[1] = new Customer() { CompanyName = "N11" };
            customers[2] = new Customer() { CompanyName = "RentCar" };
            customers[3] = new Customer() { CompanyName = "PTT" };
            customers[4] = new Customer() { CompanyName = "Akakce" };

            for (int i = 0; i < customers.Count(); i++)
            {
                var result = customerManager.Add(customers[i]);
                Console.WriteLine(result.Message);
            }
        }

        private static void UsersAddTest()
        {
            Dictionary<int, User> users = new Dictionary<int, User>();
            UserManager userManager = new UserManager(new EfUserDal());
            users[0] = new User() { FirstName = "Aşır", LastName = "ÇALIŞIR", Email = "asircalisir@gmail.com" };
            users[1] = new User() { FirstName = "Aşır", LastName = "ÇALIŞIR", Email = "asircalisir@yahoo.com" };
            users[2] = new User() { FirstName = "Aşır", LastName = "ÇALIŞIR", Email = "asircalisir@hotmail.com" };
            users[3] = new User() { FirstName = "Ahmet", LastName = "YILDIRIM", Email = "ahmetyildirim@hotmail.com" };
            users[4] = new User() { FirstName = "Mehmet", LastName = "YILMAZ", Email = "mehmetyilmaz@hotmail.com" };


            /*for (int i = 0; i < users.Count(); i++)
            {
                var result = userManager.Add(users[i]);
                Console.WriteLine(result.Message);
            }*/
        }
    }
}