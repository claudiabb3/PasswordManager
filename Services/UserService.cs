
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace PasswordManager.Services
{

        public static class UserService
        {
            private static readonly string FilePath = "passwords.json";

            public static List<UserData> LoadUsers()
            {
                if (!File.Exists(FilePath))
                    return new List<UserData>();

                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<UserData>>(json) ?? new List<UserData>();
            }

            public static void SaveUsers(List<UserData> users)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(users, options);
                File.WriteAllText(FilePath, json);
            }

            public static void UpdateUser(List<UserData> allUsers, UserData updatedUser)
            {
                var existingUsers = allUsers.Where(u => u.User != updatedUser.User).ToList();
                existingUsers.Add(updatedUser);
                SaveUsers(existingUsers);
            }
        }
    }



