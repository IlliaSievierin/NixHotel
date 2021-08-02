using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.EF
{
    public class HotelInitializer : CreateDatabaseIfNotExists<HotelContext>
    {
        private void CategotyInitializer(HotelContext context)
        {
            var categoryList = new List<Category>()
            {
                new Category()
                {
                  CategoryName = "Studio",
                  Bed = 1,
                },
                new Category()
                {
                  CategoryName = "Business",
                  Bed = 2
                },
                new Category()
                {
                  CategoryName = "De luxe",
                  Bed = 3
                },
            };

            foreach (var category in categoryList)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();
        }
        private void ReservationInitializer(HotelContext context)
        {

            var reservationList = new List<Reservation>()
            {
                new Reservation()
                {
                   RoomId = 1,
                   CustomerId = 1,
                   ArrivalDate = new DateTime(2021,06,10),
                   DepartureDate = new DateTime(2021,06,17),
                   ReservationDate = new DateTime(2021,06,02),
                   CheckIn = true
                },
                new Reservation()
                {
                   RoomId = 2,
                   CustomerId = 2,
                   ArrivalDate = new DateTime(2021,06,10),
                   DepartureDate = new DateTime(2021,06,17),
                   ReservationDate = new DateTime(2021,06,02),
                   CheckIn = true
                }
            };

            foreach (var reservation in reservationList)
            {
                context.Reservations.Add(reservation);
            }
            context.SaveChanges();
        }
        private void PriceCategoryInitializer(HotelContext context)
        {
            var pricecategoryList = new List<PriceCategory>()
            {
                new PriceCategory()
                {
                   CategoryId = 1,
                   Price = 500,
                   StartDate = new DateTime(2021,6,1),
                   EndDate = new DateTime(2021,8,30)
                },
                new PriceCategory()
                {
                   CategoryId = 2,
                   Price = 800,
                   StartDate = new DateTime(2021,6,1),
                   EndDate = new DateTime(2021,8,30)
                },
                new PriceCategory()
                {
                   CategoryId = 3,
                   Price = 1250,
                   StartDate = new DateTime(2021,6,1),
                   EndDate = new DateTime(2021,8,30)
                }
            };

            foreach (var priceCategory in pricecategoryList)
            {
                context.PriceCategories.Add(priceCategory);
            }
            context.SaveChanges();
        }
        private void CustomerInitializer(HotelContext context)
        {
            var customerList = new List<Customer>()
            {
                new Customer()
                {
                    FirstName = "Max",
                    LastName = "Hunter",
                    MiddleName = "Henk",
                    DateOfBirth = new DateTime(1999,6,6),
                    Passport = "132947238"
                },
                new Customer()
                {
                    FirstName = "Leni",
                    LastName = "Johan",
                    MiddleName = "Ben",
                    DateOfBirth = new DateTime(1985,1,10),
                    Passport = "185947958"
                }
            };

            foreach (var customer in customerList)
            {
                context.Customers.Add(customer);
            }
            context.SaveChanges();
        }
        

        private void RoomInitializer(HotelContext context)
        {
            var roomList = new List<Room>()
            {
                new Room()
                {
                   RoomNumber = "101",
                   CategoryId = 1,
                   Active = true,
                },
                new Room()
                {
                   RoomNumber = "102",
                   CategoryId = 1,
                   Active = true,
                },
                new Room()
                {
                   RoomNumber = "201",
                   CategoryId = 2,
                   Active = true,
                },
                new Room()
                {
                   RoomNumber = "202",
                   CategoryId = 2,
                   Active = false,
                },
                new Room()
                {
                   RoomNumber = "301a",
                   CategoryId = 2,
                   Active = true,
                }
            };

            foreach (var room in roomList)
            {
                context.Rooms.Add(room);
            }
            context.SaveChanges();
        }
        private void EmployeeInitializer(HotelContext context)
        {
            var employeeList = new List<Employee>()
            {
                new Employee()
                {
                  Id = 1,
                  Login = "Test",
                  Password = "Test"
                }
            };

            foreach (var employee in employeeList)
            {
                context.Employees.Add(employee);
            }
            context.SaveChanges();
        }
        protected override void Seed(HotelContext context)
        {
            CategotyInitializer(context);
            CustomerInitializer(context);
            RoomInitializer(context);
            PriceCategoryInitializer(context);
            ReservationInitializer(context);
            EmployeeInitializer(context);
        }
    }
}
