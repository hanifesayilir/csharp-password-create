
using System.Security.Cryptography.X509Certificates;
using test_create_password;
using test_create_password.eNum;

CreatePassword yen = new CreatePassword();
yen.CreateRandomIntInSelectedList();

if (yen.anyValidInput)
{
    yen.getSelectedList().ForEach(x => Console.Write($"{x}, "));
    Console.WriteLine($"PasswordLength: {yen.getPasswordLength()}");
}



