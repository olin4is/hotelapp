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
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }
        public static void AddClient(Clients client)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Clients.Add(client);
                ctx.SaveChanges();
            }
        }
        public static void AddBooking(Bookings bookings)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Bookings.Add(bookings);
                ctx.SaveChanges();
            }
        }
        public static void AddRoom(Rooms room)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Rooms.Add(room);
                ctx.SaveChanges();
            }
        }
        public static void AddViolation(Violations violation)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Violations.Add(violation);
                ctx.SaveChanges();
            }
        }
        public static bool CheckUser(Users user)
        {
            bool exist = false;
            using (DbAppContext ctx = new DbAppContext())
            {
                List<Users> users = ctx.Users
                    .Where(p => p.Login == user.Login && p.Password == user.Password)
                    .ToList();

                if (users.Count != 0)
                {
                    exist = true;
                }
            }
            return exist;
        }
        public static ObservableCollection<Rooms> GetRooms()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<Rooms> rooms = new ObservableCollection<Rooms>(ctx.Rooms
                    .Include(p => p.RoomTypesEntity)
                    .OrderBy(p => p.Id)
                );
                return rooms;
            }
        }
        public static ObservableCollection<Rooms> GetFreeRooms(DateTime dateA, DateTime dateD)
        {
            using (DbAppContext ctx = new DbAppContext())
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

                ObservableCollection<Rooms> rooms = new ObservableCollection<Rooms>(
                    ctx.Rooms
                    .FromSqlRaw(query)
                    .Include(p => p.RoomTypesEntity)
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
            using (DbAppContext ctx = new DbAppContext())
            {
                string query = $@"SELECT ""Bookings"".""Id"", ""Bookings"".""DateOfArrival"", 
	                                     ""Bookings"".""DateOfDeparture"",
	                                     ""Bookings"".""Room_id"", ""Bookings"".""User_id"", 
	                                     ""Bookings"".""Status"", ""Bookings"".""Violation_id""
                                  FROM ""Bookings""
                                  JOIN ""Clients"" ON ""Bookings"".""User_id"" = ""Clients"".""User_id""
                                  WHERE LOWER(""Clients"".""FIO"") LIKE LOWER('%{clientName}%')";

                ObservableCollection<Bookings> bookings = new ObservableCollection<Bookings>(ctx.Bookings
                    .FromSqlRaw(query)
                    .Where(p => p.Status == "Не заселен")
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.UsersEntity)
                    .ThenInclude(p => p.ClientsEntity)
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
            using (DbAppContext ctx = new DbAppContext())
            {
                string query = @$"SELECT ""Bookings"".""Id"", ""Bookings"".""DateOfArrival"", 
	                                     ""Bookings"".""DateOfDeparture"",
	                                     ""Bookings"".""Room_id"", ""Bookings"".""User_id"", 
	                                     ""Bookings"".""Status"", ""Bookings"".""Violation_id""
                                  FROM ""Bookings""
                                  JOIN ""Clients"" ON ""Bookings"".""User_id"" = ""Clients"".""User_id""
                                  WHERE LOWER(""Clients"".""FIO"") LIKE LOWER('%{clientName}%')";

                ObservableCollection<Bookings> bookings = new ObservableCollection<Bookings>(ctx.Bookings
                    .FromSqlRaw(query)
                    .Where(p => p.Status == "Заселен")
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.UsersEntity)
                    .ThenInclude(p => p.ClientsEntity)
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
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<Bookings> bookings = new ObservableCollection<Bookings>(ctx.Bookings
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.UsersEntity)
                    .ThenInclude(p => p.ClientsEntity)
                    .OrderByDescending(p => p.DateOfArrival));

                return bookings;
            }
        }
        public static ObservableCollection<Bookings> GetBookingsSelected()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<Bookings> bookings = new ObservableCollection<Bookings>(ctx.Bookings.
                    Where(p => p.Status == "Заселен").
                    Include(p => p.RoomsEntity).ThenInclude(p => p.RoomTypesEntity).
                    Include(p => p.UsersEntity).ThenInclude(p => p.ClientsEntity).ToList().OrderByDescending(p => p.DateOfArrival));
                return bookings;
            }
        }

        public static ObservableCollection<Bookings> GetBookingsNotSelected()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<Bookings> bookings = new ObservableCollection<Bookings>(ctx.Bookings
                    .Where(p => p.Status == "Не заселен")
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.UsersEntity)
                    .ThenInclude(p => p.ClientsEntity)
                    .OrderByDescending(p => p.DateOfArrival));

                return bookings;
            }
        }

        public static Users GetUser(Users users)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Users user = ctx.Users.First(p => p.Login == users.Login);
                return user;
            }
        }
        public static Clients GetClientById(int Id)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Clients client = ctx.Clients.First(p => p.User_id == Id);
                return client;
            }
        }
        public static ObservableCollection<Bookings> GetClientBooking(int userId)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<Bookings> bookings = new ObservableCollection<Bookings>(ctx.Bookings
                    .Where(p => p.User_id == userId)
                    .Include(p => p.RoomsEntity)
                    .ThenInclude(p => p.RoomTypesEntity)
                    .Include(p => p.UsersEntity)
                    .ThenInclude(p => p.ClientsEntity)
                );

                return bookings;
            }
        }
        public static Clients GetClient(Clients clients)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Clients client = ctx.Clients.First(p => p.FIO == clients.FIO);
                return client;
            }
        }
        public static Admins GetAdmin(int Id)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                Admins admin = ctx.Admins.First(p => p.User_id == Id);
                return admin;
            }
        }
        public static RoomTypes GetRoomTypeById(int Id)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                RoomTypes roomType = ctx.RoomTypes.First(p => p.Id == Id);
                return roomType;
            }
        }
        public static ObservableCollection<Clients> GetAllClients()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<Clients> clients = new ObservableCollection<Clients>(ctx.Clients);
                return clients;
            }
        }
        public static ObservableCollection<Clients> GetFreeClients()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<Clients> clients = new ObservableCollection<Clients>(ctx.Clients);
                return clients;
            }
        }
        public static ObservableCollection<RoomTypes> GetRoomTypes()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<RoomTypes> room = new ObservableCollection<RoomTypes>(ctx.RoomTypes);
                return room;
            }
        }
        public static ObservableCollection<Violations> GetViolations()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<Violations> violations = new ObservableCollection<Violations>(ctx.Violations);
                return violations;
            }
        }
        public static ObservableCollection<Violations> GetViolationsForPage()
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ObservableCollection<Violations> violations = new ObservableCollection<Violations>(ctx.Violations
                    .Where(p => p.Id != 5)
                );

                return violations;
            }
        }
        public static void UpdateClient(Clients client)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Clients.Update(client);
                ctx.SaveChanges();
            }
        }
        public static void UpdateRoom(Rooms room)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Rooms.Update(room);
                ctx.SaveChanges();
            }
        }
        public static void UpdateViolation(Violations violation)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Violations.Update(violation);
                ctx.SaveChanges();
            }
        }
        public static void UpdateBooking(Bookings booking)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Bookings.Update(booking);
                ctx.SaveChanges();
            }
        }
        public static void DeleteClient(Clients client)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Clients.Remove(client);
                ctx.SaveChanges();
            }
        }
        public static void DeleteRoom(Rooms room)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Rooms.Remove(room);
                ctx.SaveChanges();
            }
        }
        public static void DeleteViolation(Violations violation)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Violations.Remove(violation);
                ctx.SaveChanges();
            }
        }
        public static void DeleteBooking(Bookings booking)
        {
            using (DbAppContext ctx = new DbAppContext())
            {
                ctx.Bookings.Remove(booking);
                ctx.SaveChanges();
            }
        }
    }
}
