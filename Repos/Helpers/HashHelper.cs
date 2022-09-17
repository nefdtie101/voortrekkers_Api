using System.Security.Cryptography;
using System.Text;
using Repos.Interface;
using Microsoft.Extensions.Configuration;

namespace Repos.Helpers;

public class HashHelper : IHashHelper
{
    private readonly IConfiguration _config;

    public HashHelper(IConfiguration configuration)
    {
        _config = configuration;
    }
    public string ToSHA256(string s)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s + _config.GetValue<string>("JWT:Key")));
        return Convert.ToBase64String(bytes);
    }
}