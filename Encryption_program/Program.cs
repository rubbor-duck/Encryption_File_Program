using System.Collections;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;

//TODO: Make the key and IV randomized

int option = -1;
string aesKey = "1234567890123456";
string aesIV = "6543210987654321";

string originalText = "";
string encryptedAES = "";

while (option != 0)
{
    Console.WriteLine("|| Encrypt Your Files ||");
    Console.WriteLine("1. Encrypt File");
    Console.WriteLine("2. Decrypt File");
    Console.WriteLine("0. Exit Program");
    Console.WriteLine();
    Console.Write("Selection: ");

    if (int.TryParse(Console.ReadLine(), out int input))
    {
        option = input;
    }
    else
    {
        Console.WriteLine("Error - Input wasn't an integer");
        continue;
    }


    switch (option)
    {
    case 1:
        Console.Write("Text to encrypt: ");

        originalText = Console.ReadLine();

        encryptedAES = Encrypt(originalText, aesKey, aesIV);
        Console.WriteLine($"\n AES Encrypted:\n {encryptedAES}");
        break;
    case 2:
        string decryptedAES = Decrypt(encryptedAES, aesKey, aesIV);
        Console.WriteLine($"\n AES Decrypted\n {decryptedAES}");
        break;
    case 0:
        Console.WriteLine("Have a wonderful day!");
        break;
    default:
        Console.WriteLine("Please Select A Valid Option");
        break;
    }
}

string Encrypt(string plainText, string key, string iv)
{
    using Aes aes = Aes.Create();
    aes.Key = Encoding.UTF8.GetBytes(key);
    aes.IV = Encoding.UTF8.GetBytes(iv);

    using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
    using var ms = new MemoryStream();
    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
    using (var sw = new StreamWriter(cs))
        sw.Write(plainText);

    return Convert.ToBase64String(ms.ToArray());
}

string Decrypt(string cipherText, string key, string iv)
{
    byte[] cipherBytes = Convert.FromBase64String(cipherText);
    using Aes aes = Aes.Create();
    aes.Key = Encoding.UTF8.GetBytes(key);
    aes.IV = Encoding.UTF8.GetBytes(iv);

    using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
    using var ms = new MemoryStream(cipherBytes);
    using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
    using var sr = new StreamReader(cs);

    return sr.ReadToEnd();
}