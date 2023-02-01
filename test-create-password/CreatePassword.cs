using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using test_create_password.eNum;

namespace test_create_password

{
    public class CreatePassword
    {
        public int passwordLength = 0;
        public string numberAllowed= string.Empty;
        public string upperCaseAllowed = string.Empty;
        public string lowerCaseAllowed = string.Empty;
        public string specialCharacterAllowed = string.Empty;
        public bool validNumber;
        public bool validUpperCase;
        public bool validLowerCase;
        public bool validSpecialCharacterAllowed;
        public bool validLength;
        public List<string> password = new List<string>();
        public string finalPassword = string.Empty;

        public static List<QuestionType> selectedList = new List<QuestionType>();
        public static string questionNumber = "Do you want to include numbers?";
        public static string questionLowerCase = "How about lowercase characters?";
        public static string questionUpperCase = "OK! How about uppercase characters?";
        public static string questionLength = "Great! Lastly. How long do you want to keep your password length?";
        public static string questionSpecialCharacter = "All right! We are almost done. Would you also want to add special characters?";
        public static string validityConsoleMessage = "Please enter a valid input. One of the following characters 'Y', 'y', 'n', 'N' are considered valid only.";
        public static string validityLengthConsoleMessage = "Please enter a valid input. The length should be greater than 1.Please try Again.";
        public static string wellcomeTitle = "Wellcome to the BESTPASSWORDMANAGER";
        public static Random random = new Random();
        public static List<string> numberList = new List<string>() { "0","1","2","3","4","5","6","7","8","9"};
        public static List<string> lowerCaseList = new List<string>(){ "a", "b", "c", "d","e","f","g","h", "i", "j",  "k", "l", "m","n","o","p", "q","r", "s","t", "u", "v","w","x","y","z"};
        public static List<string> upperCaseList = new List<string>(){ "A", "B", "C", "D","F","G","H","I", "J",  "K", "L", "M","N","O","P", "Q","R", "S","T", "U", "V","W","X","Y","Z"};
        public static List<string> specialCharacterList = new List<string>() { "#", "$", ".", ",", "/",":", ";", "%","?"};
       
       public CreatePassword () 
            {
       
            WellcomeScreen();
            ComposeQuestion(QuestionType.NUMBER);
            ComposeQuestion(QuestionType.LOWERCASE);
            ComposeQuestion(QuestionType.UPPERCASE);
            ComposeQuestion(QuestionType.SPECIALCHARACTER);
            ComposeQuestion(QuestionType.LENGTH);
        }


        public void WellcomeScreen ()
        {
            CreateStars();
            Console.WriteLine(wellcomeTitle);
            CreateStars();
        }
        
       public void CreateStars ()
        {
            Console.WriteLine("******************************************************");
        }

        public void ComposeQuestion(QuestionType sentValue)
        {

            switch (sentValue)
            {
                case QuestionType.NUMBER: 
                    CallQuestionAndReadFromConsole(sentValue, questionNumber, numberAllowed, validNumber);
                    break;
                case QuestionType.LOWERCASE:
                    CallQuestionAndReadFromConsole(sentValue, questionLowerCase, lowerCaseAllowed, validLowerCase);
                    break;
                case QuestionType.UPPERCASE:
                    CallQuestionAndReadFromConsole(sentValue, questionUpperCase, upperCaseAllowed, validUpperCase);
                    break;
                case QuestionType.SPECIALCHARACTER:
                    CallQuestionAndReadFromConsole(sentValue, questionSpecialCharacter, specialCharacterAllowed, validSpecialCharacterAllowed);
                    break;
                case QuestionType.LENGTH:
                    ReadLengthFromConsole(questionLength, validLength);
                    break;
                default:
                    Console.WriteLine("Invalid Type");
                    break;
            }
        }
          
        public void CallQuestionAndReadFromConsole(QuestionType value,string question, string typeAllowed, bool validType)
        {
            Console.WriteLine(question);
            typeAllowed = Console.ReadLine();
            validType = CheckValidityOfInputs(typeAllowed);
          

            if (!validType)
            {
                Console.WriteLine(validityConsoleMessage);
                CallQuestionAndReadFromConsole(value, question, typeAllowed, validType);
            } 
            else
            {
                if (IsAllowed(typeAllowed)) selectedList.Add(value);
            }
            
        }

        public void ReadLengthFromConsole(string question, bool validType)
        {
            int number = 0;
            Console.WriteLine(question);
            var temp = Console.ReadLine();

            if (int.TryParse(temp, out number))
            {
                validType = CheckValidityOfIntegerInput(number);
                if (!validType)
                {
                    Console.WriteLine(validityLengthConsoleMessage);
                    ReadLengthFromConsole(question, validType);
                }
                else
                {
                    passwordLength = number;
                }
            }
            else
            {
                Console.WriteLine("Please Enter a number");
                ReadLengthFromConsole(question, validType);
            }
        }

        public bool CheckValidityOfInputs(string consoleInput)
        {
            var temp = (consoleInput.Equals("y") || consoleInput.Equals("Y") || consoleInput.Equals("N") || consoleInput.Equals("n")) ?  true :  false;
            return temp;
        }

        public bool CheckValidityOfIntegerInput(int input)
        {
           bool result = (input >= 1) ? true : false;
            return result;
        }

        public bool IsAllowed(string consoleInput)
        {
            bool result =  consoleInput.Equals("y") || consoleInput.Equals("Y") ? true : false;
            return result;
        }

        public void CreateRandomIntInSelectedList ()
        {

            for (int i=0; i<passwordLength ; i++) 
            {
                int tempRandomValue = random.Next(1, selectedList.Count+1);
 
                AddCharacterToPassword(selectedList[tempRandomValue - 1]);
            }
            finalPassword = string.Join("", password);
            WriteFinalPasswordToConsole();

        }

        public void AddCharacterToPassword(QuestionType name)
        {
            switch (name)
            {
                case QuestionType.LOWERCASE:
                    SelectFromCurrentList(lowerCaseList);
                    break;
                case QuestionType.NUMBER:
                    SelectFromCurrentList(numberList);
                    break;
                case QuestionType.UPPERCASE:
                    SelectFromCurrentList(upperCaseList);
                    break;
                case QuestionType.SPECIALCHARACTER:
                    SelectFromCurrentList(specialCharacterList);
                    break;
                default: throw new ArgumentException("Invalid Selection");
            }


        }

        public void WriteFinalPasswordToConsole ()
        {
         Console.WriteLine("**************************************************");
         Console.WriteLine(finalPassword);
         Console.WriteLine("**************************************************");
        }

        public void SelectFromCurrentList(List<string> listName)
        {
            var tempNumber = random.Next(1, listName.Count);
            password.Add(listName[tempNumber- 1]);
        }
        
    }

}
