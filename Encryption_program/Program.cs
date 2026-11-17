using System.Collections;
using System.Runtime.InteropServices;

int option = -1;

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
        Console.WriteLine("Encryption Placeholder");
        break;

    case 2:
        Console.WriteLine("Decryption Placeholder");
        break;

    case 0:
        Console.WriteLine("Have a wonderful day!");
        break;
    default:
        Console.WriteLine("Please Select A Valid Option");
        break;
    }
}



