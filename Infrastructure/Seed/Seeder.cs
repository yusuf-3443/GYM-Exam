using System.Security.Cryptography;
using System.Text;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed;

public class Seeder(DataContext context,UserManager<IdentityUser> userManager)
{

    public async Task<bool> SeedUser()
    {
        var existing = await userManager.FindByNameAsync("admin");
        if (existing != null) return false;
        
        var identity = new IdentityUser()
        {
            UserName = "admin",
            PhoneNumber = "13456777",
            Email = "admin@gmail.com"
        };

        var result = await userManager.CreateAsync(identity, "hello123");
        await userManager.AddToRoleAsync(identity, Roles.Admin);
        return result.Succeeded;
    }


    private static string ConvertToHash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }


}
