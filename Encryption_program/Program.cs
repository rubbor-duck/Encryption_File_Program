using System.Collections;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.Reflection;
using System;
using System.IO;

/*  TODO: Add a way to retry inputing a destination file location after encryption/decrytpion
*/

int option = -1;
string aesKey = "1234567890123456";
string aesIV = "6543210987654321";

string originalText = "";
string encryptedAES = "";

const string projectFolder = @"C:\Users\nelso\Documents\College\Fall 2026\CSE 310\Encryption_File_Program\Encryption_program";

while (option != 0)
{
    Console.WriteLine("|| Encrypt Your Files ||");
    Console.WriteLine("1. Encrypt File");
    Console.WriteLine("2. Decrypt File");
    Console.WriteLine("3. Compare Files");
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
        Console.WriteLine("\nWhat File do you want to read from?");
        Console.Write("Selection: ");

        string originalfile = Console.ReadLine();

            if (File.Exists(Path.Combine(projectFolder, originalfile)))
            {
                originalText = ReadFile(Path.Combine(projectFolder, originalfile));

                encryptedAES = Encrypt(originalText, aesKey, aesIV);
                Console.WriteLine($"\n AES Encrypted:\n {encryptedAES}");


                Console.WriteLine("\nWhat File do you want to write to?");
                Console.Write("Selection: ");

                string encryptedFileLocation = Console.ReadLine();

                if (File.Exists(Path.Combine(projectFolder, encryptedFileLocation)))
                {
                    WriteToFile(Path.Combine(projectFolder, "test_encrypted.txt"), encryptedAES);
                }

                else
                {
                    Console.WriteLine("Error: Encrypted file Does Not Exist");
                }
            }

            else
            {
                Console.WriteLine("Error: File Does Not Exist");
            }
        
        break;

    case 2:
        Console.WriteLine("What File do you want to read from?");
        Console.Write("Selection: ");

        string encryptedfile = Console.ReadLine();

            if (File.Exists(Path.Combine(projectFolder, encryptedfile)))
            {
                encryptedAES = ReadFile(Path.Combine(projectFolder, encryptedfile));

                string decryptedAES = Decrypt(encryptedAES, aesKey, aesIV);
                Console.WriteLine($"\n AES Decrypted\n {decryptedAES}");

                Console.WriteLine("\nWhat File do you want to write to?");
                Console.Write("Selection: ");

                string decryptedFileLocation = Console.ReadLine();

                if (File.Exists(Path.Combine(projectFolder, decryptedFileLocation)))
                {
                    WriteToFile(Path.Combine(projectFolder,"test_decrypted.txt"), decryptedAES);
                }

                else
                {
                    Console.WriteLine("Error: Decrypted file does not exist");
                }
            }

            else
            {
                Console.WriteLine("Error: Encrypted file Does Not Exist");
            }
        break;

    case 3:
        string filePath1;
        string filePath2;
        string file1 = "";
        string file2 = "";

        Console.WriteLine("What two files do you want to compare?\n");

        Console.Write("File 1: ");
        filePath1 = Console.ReadLine();

        if (File.Exists(Path.Combine(projectFolder, filePath1)))
            {
                file1 = ReadFile(Path.Combine(projectFolder, filePath1));
            }

        else
            {
                Console.WriteLine("Error: File does not exist");
            }
    
        Console.Write("\nFile 2: ");
        filePath2 = Console.ReadLine();

        if (File.Exists(Path.Combine(projectFolder, filePath2)))
            {
                file2 = ReadFile(Path.Combine(projectFolder, filePath2));
            }

        else
            {
                Console.WriteLine("Error: File does not exist");
            }

        if (file1 == file2)
            {
                Console.WriteLine("\nThe files are the same");
            }

        else
            {
                Console.WriteLine("\nThe files contain different data");
            }
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

string ReadFile(string fileLocation)
{
    Console.WriteLine($"Reading from {fileLocation}");

    string contents = File.ReadAllText(fileLocation);

    return contents;
}

void WriteToFile(string fileLocation, string contents)
{
    Console.WriteLine($"Writing to {fileLocation}");

    File.WriteAllText(fileLocation, contents);
}