
using System.Security.Cryptography.X509Certificates;
using test_create_password;
using test_create_password.eNum;

CreatePassword yen = new CreatePassword();
yen.CreateRandomIntInSelectedList();

// The following are extra  information about the selection of the user on the Console
CreatePassword.selectedList.ForEach(x =>Console.Write($"{x}, "));
Console.WriteLine($"PasswordLength: {yen.passwordLength}");




