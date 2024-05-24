using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using кркр.Models;

namespace кркр.Infrastructure
{
    public class DatabaseControl
    {
        public static void AddUser(Users user)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }
        public static void AddBooking(Bookings bookings)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Bookings.Add(bookings);
                ctx.SaveChanges();
            }
        }
        public static void AddRoom(Rooms room)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Rooms.Add(room);
                ctx.SaveChanges();
            }
        }
        public static void AddViolation(Violations violation)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Violations.Add(violation);
                ctx.SaveChanges();
            }
        }
        public static bool CheckUser(Users user)
        {
            bool exist = false;
            using (DbAppContext ctx = new ())
            {
                List<Users> users = ctx.Users
                    .Where(p => p.Login == user.Login && p.Password == user.Password)
                    .ToList();

                if (users.Count != 0)
                    exist = true;
            }
            return exist;
        }
        public static ObservableCollection<Rooms> GetRooms()
        {
            using (DbAppContext ctx = new ())
            {
                ObservableCollection<Rooms> rooms = new(ctx.Rooms
                    .Include(p => p.RoomTypesEntity)
                    .Include(p => p.RoomStatesEntity)
                    .OrderBy(p => p.Id)
                );
                return rooms;
            }
        }
        public static ObservableCollection<Rooms> GetFreeRooms(DateTime dateA, DateTime dateD)
        {
            using (DbAppContext ctx = new ())
            {
                string DateA = dateA.ToString("yyyy-MM-dd HH:mm:ss");
                string DateD = dateD.ToString("yyyy-MM-dd HH:mm:ss");

                string query = $@"SELECT DISTINCT * FROM ""Rooms""
                                 WHERE ""Id"" NOT IN 
                                 (
	                                 SELECT ""Room_id"" 
 	                                 FROM ""Bookings"" 
 	                                 WHERE (""DateOfArrival"" BETWEEN '{DateA}' AND '{DateD}') 
 	                                 OR (""DateOfDeparture"" BETWEEN '{DateA}' AND '{DateD}')
                                 )";
                ObservableCollection<Rooms> rooms = new (
                    ctx.Rooms
                    .FromSqlRaw(query)
                    .Include(p => p.RoomTypesEntity)
                    .Include(p => p.RoomStatesEntity)
                    .OrderBy(p => p.Id)
                );

                if (rooms.Count == 0)
                {
                    rooms = GetRooms();
                }
                return rooms;
            }
        }
        public static ObservableCollection<Bookings> GetBookingsByClientName(string clientName)
        {
            using (DbAppContext ctx = new())
            {
                string query = $@"SELECT ""Bookings"".""Id"", 
                                         ""Bookings"".""DateOfArrival"", 
	                                     ""Bookings"".""DateOfDeparture"",
	                                     ""Bookings"".""Room_id"", 
                                         ""Bookings"".""User_id"", 
	                                     ""Bookings"".""Status"", 
                                         ""Bookings"".""Violation_id""
                                  FROM ""Bookings""
                                  JOIN ""Users"" ON ""Bookings"".""User_id"" = ""Users"".""Id""
                                  WHERE LOWER(""Users"".""FIO"") LIKE LOWER('%{clientName}%')";

                ObservableCollection<Bookings> bookings = new(ctx.Bookings
                    .FromSqlRaw(query)
                    .Where(p => p.Status == "Не заселен")
                    .Include(p => p.UsersEntity)
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomStatesEntity)
                    .OrderBy(p => p.Id));

                if (bookings.Count == 0) 
                { 
                    bookings = GetBookings();
                }
                return bookings;
            }
        }
        public static ObservableCollection<Bookings> GetBookingsByClientNameSelected(string clientName)
        {
            using (DbAppContext ctx = new ())
            {
                string query = @$"SELECT ""Bookings"".""Id"", ""Bookings"".""DateOfArrival"", 
	                                     ""Bookings"".""DateOfDeparture"",
	                                     ""Bookings"".""Room_id"", ""Bookings"".""User_id"", 
	                                     ""Bookings"".""Status"", ""Bookings"".""Violation_id""
                                  FROM ""Bookings""
                                  JOIN ""Users"" ON ""Bookings"".""User_id"" = ""Users"".""Id""
                                  WHERE LOWER(""Users"".""FIO"") LIKE LOWER('%{clientName}%')";

                ObservableCollection<Bookings> bookings = new (ctx.Bookings
                    .FromSqlRaw(query)
                    .Where(p => p.Status == "Заселен")
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.UsersEntity)
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomStatesEntity)
                    .OrderBy(p => p.Id));

                if (bookings.Count == 0)
                {
                    bookings = GetBookings();
                }
                return bookings;
            }
        }
        public static ObservableCollection<Bookings> GetBookings()
        {
            using (DbAppContext ctx = new ())
            {
                ObservableCollection<Bookings> bookings = new (ctx.Bookings
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomStatesEntity)
                    .Include(p => p.UsersEntity)
                    .OrderByDescending(p => p.DateOfArrival));

                return bookings;
            }
        }
        public static ObservableCollection<Bookings> GetBookingsSelected()
        {
            using (DbAppContext ctx = new ())
            {
                ObservableCollection<Bookings> bookings = new (ctx.Bookings
                    .Where(p => p.Status == "Заселен")
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomStatesEntity)
                    .Include(p => p.UsersEntity)
                    .OrderByDescending(p => p.DateOfArrival));
                return bookings;
            }
        }

        public static ObservableCollection<Bookings> GetBookingsNotSelected()
        {
            using (DbAppContext ctx = new ())
            {
                ObservableCollection<Bookings> bookings = new (ctx.Bookings
                    .Where(p => p.Status == "Не заселен")
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomStatesEntity)
                    .Include(p => p.UsersEntity)
                    .OrderByDescending(p => p.DateOfArrival));

                return bookings;
            }
        }

        public static Users GetUser(Users users)
        {
            using (DbAppContext ctx = new ())
            {
                Users user = ctx.Users.FirstOrDefault(p => p.Login == users.Login);
                return user;
            }
        }
        public static Users GetClientById(int user_id)
        {
            using (DbAppContext ctx = new ())
            {
                Users user = ctx.Users.FirstOrDefault(p => p.Id == user_id);
                return user;
            }
        }
        public static ObservableCollection<Bookings> GetClientBooking(int userId)
        {
            using (DbAppContext ctx = new ())
            {
                ObservableCollection<Bookings> bookings = new (ctx.Bookings
                    .Where(p => p.User_id == userId)
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomStatesEntity)
                    .Include(p => p.UsersEntity)
                );

                return bookings;
            }
        }
        public static Users GetClient(Users clients)
        {
            using (DbAppContext ctx = new ())
            {
                Users client = ctx.Users.First(p => p.FIO == clients.FIO);
                return client;
            }
        }
        public static ObservableCollection<Users> GetFreeClients()
        {
            using (DbAppContext ctx = new ())
            {
                string query = @"SELECT * 
                                 FROM ""Users""
                                 WHERE ""Role_id"" = 2 
                                 AND ""Id"" NOT IN 
                                 (
	                                 SELECT ""User_id"" 
	                                 FROM ""Bookings"" 
	                                 WHERE ""Status"" = 'Заселен'
                                 )";
                ObservableCollection<Users> clients = new (ctx.Users.FromSqlRaw(query));
                return clients;
            }
        }
        public static Users GetAdmin(int id)
        {
            using (DbAppContext ctx = new ())
            {
                Users admin = ctx.Users.First(p => p.Id == id);
                return admin;
            }
        }
        public static RoomTypes GetRoomTypeById(int Id)
        {
            using (DbAppContext ctx = new ())
            {
                RoomTypes roomType = ctx.RoomTypes.First(p => p.Id == Id);
                return roomType;
            }
        }
        public static ObservableCollection<Users> GetAllClients()
        {
            using (DbAppContext ctx = new ())
            {
                string query = @"SELECT * 
                                 FROM ""Users""
                                 WHERE ""Role_id"" = 2";
                ObservableCollection<Users> clients = new(ctx.Users.FromSqlRaw(query));
                return clients;
            }
        }
        public static ObservableCollection<RoomTypes> GetRoomTypes()
        {
            using (DbAppContext ctx = new ())
            {
                ObservableCollection<RoomTypes> room = new ObservableCollection<RoomTypes>(ctx.RoomTypes);
                return room;
            }
        }
        public static ObservableCollection<Violations> GetViolations()
        {
            using (DbAppContext ctx = new ())
            {
                ObservableCollection<Violations> violations = new ObservableCollection<Violations>(ctx.Violations);
                return violations;
            }
        }
        public static ObservableCollection<Violations> GetViolationsForPage()
        {
            using (DbAppContext ctx = new ())
            {
                ObservableCollection<Violations> violations = new ObservableCollection<Violations>(ctx.Violations
                    .Where(p => p.Id != 5)
                );

                return violations;
            }
        }
        public static void UpdateClient(Users client)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Users.Update(client);
                ctx.SaveChanges();
            }
        }
        public static void UpdateRoom(Rooms room)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Rooms.Update(room);
                ctx.SaveChanges();
            }
        }
        public static void UpdateViolation(Violations violation)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Violations.Update(violation);
                ctx.SaveChanges();
            }
        }
        public static void UpdateBooking(Bookings booking)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Bookings.Update(booking);
                ctx.SaveChanges();
            }
        }
        public static void DeleteUser(Users client)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Users.Remove(client);
                ctx.SaveChanges();
            }
        }
        public static void DeleteRoom(Rooms room)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Rooms.Remove(room);
                ctx.SaveChanges();
            }
        }
        public static void DeleteViolation(Violations violation)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Violations.Remove(violation);
                ctx.SaveChanges();
            }
        }
        public static void DeleteBooking(Bookings booking)
        {
            using (DbAppContext ctx = new ())
            {
                ctx.Bookings.Remove(booking);
                ctx.SaveChanges();
            }
        }
    }
}
