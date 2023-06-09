﻿using System;
using System.Text.Json;

namespace InventoryManagementSystem.Data
{
    
        public static class UsersService
        {
            public const string SeedUsername = "admin";
            public const string SeedPassword = "shreya";

            private static void SaveAll(List<Users> users)
            {
                string appDataDirectoryPath = Utils.GetAppDirectoryPath();
                string appUsersFilePath = Utils.GetUsersFilePath();

                if (!Directory.Exists(appDataDirectoryPath))
                {
                    Directory.CreateDirectory(appDataDirectoryPath);
                }

                var json = JsonSerializer.Serialize(users);
                File.WriteAllText(appUsersFilePath, json);
            }

            public static List<Users> WriteData(Guid userId, string username, string password, Role role, Guid createdBy, DateTime createdAt)
            {

                List<Users> users = ReadData();
                users.Add(
                    new Users
                    {
                        Username = username,
                        PasswordHash = password,
                        Role = role,
                        CreatedBy = createdBy,
                        CreatedAt = createdAt,

                    }
                );
                SaveAll(users);
                return users;
            }

            public static List<Users> ReadData()
            {
                string usersFilePath = Utils.GetUsersFilePath();

                if (!File.Exists(usersFilePath))
                {
                    return new List<Users>();
                }
                var json = File.ReadAllText(usersFilePath);
                if (json == "")
                {
                    return new List<Users>();
                }
                return JsonSerializer.Deserialize<List<Users>>(json);
            }


            public static Users GetById(Guid Id)
            {
                string _filePath = "D/Users/shreyarai/Downloads/IMS/RequestedInventory.json";

                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
                List<Users> inventoryReqs = ReadData();
                return inventoryReqs.FirstOrDefault(x => x.Id == Id);
            }


            public static void DeleteByUserId(Guid userId)
            {
                string _filePath = "D/Users/shreyarai/Downloads/IMS/RequestedInventory.json";

                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
            }


            public static List<Users> GetAll()
            {
                string appUsersFilePath = Utils.GetUsersFilePath();
                if (!File.Exists(appUsersFilePath))
                {
                    return new List<Users>();
                }

                var json = File.ReadAllText(appUsersFilePath);

                return JsonSerializer.Deserialize<List<Users>>(json);
            }

            public static List<Users> Create(Guid userId, string username, string password, Role role)
            {
                List<Users> users = GetAll();
                bool usernameExists = users.Any(x => x.Username == username);

                if (usernameExists)
                {
                    throw new Exception("Username already exists.");
                }

                users.Add(
                    new Users
                    {
                        Username = username,
                        PasswordHash = Utils.HashSecret(password),
                        Role = role,
                        CreatedBy = userId
                    }
                );
                SaveAll(users);
                return users;
            }

            public static void SeedUsers()
            {
                var users = GetAll().FirstOrDefault(x => x.Role == Role.Admin);

                if (users == null)
                {
                    Create(Guid.Empty, SeedUsername, SeedPassword, Role.Admin);
                }
            }



            public static List<Users> Delete(Guid id)
            {

                List<Users> users = GetAll();
                Users user = users.FirstOrDefault(x => x.Id == id);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }


                users.Remove(user);
                SaveAll(users);

                return users;
            }

            public static Users Login(string username, string password)
            {
                var loginErrorMessage = "Invalid username or password.";
                List<Users> users = GetAll();
                Users user = users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                {
                    throw new Exception(loginErrorMessage);
                }

                bool passwordIsValid = Utils.VerifyHash(password, user.PasswordHash);

                if (!passwordIsValid)
                {
                    throw new Exception(loginErrorMessage);
                }

                return user;
            }

            public static Users ChangePassword(Guid id, string currentPassword, string newPassword)
            {
                if (currentPassword == newPassword)
                {
                    throw new Exception("New password must be different from current password.");
                }

                List<Users> users = GetAll();
                Users user = users.FirstOrDefault(x => x.Id == id);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                bool passwordIsValid = Utils.VerifyHash(currentPassword, user.PasswordHash);

                if (!passwordIsValid)
                {
                    throw new Exception("Incorrect current password.");
                }

                user.PasswordHash = Utils.HashSecret(newPassword);
                user.HasInitialPassword = false;
                SaveAll(users);

                return user;
            }
        }

    }
