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
        private int passwordLength = 0;
        private string numberAllowed= string.Empty;
        private string upperCaseAllowed = string.Empty;
        private string lowerCaseAllowed = string.Empty;
        private string specialCharacterAllowed = string.Empty;
        private bool anyValidInput = false;
        private List<string> password = new List<string>();
        private string finalPassword = string.Empty;

        private static List<QuestionType> selectedList = new List<QuestionType>();
        private static string questionNumber = "Do you want to include numbers?";
        private static string questionLowerCase = "How about lowercase characters?";
        private static string questionUpperCase = "OK! How about uppercase characters?";
        private static string questionLength = "Great! Lastly. How long do you want to keep your password length?";
        private static string questionSpecialCharacter = "All right! We are almost done. Would you also want to add special characters?";
        private static string validityConsoleMessage = "Please enter a valid input. One of the following characters 'Y', 'y', 'n', 'N' are considered valid only.";
        private static string validityLengthConsoleMessage = "Please enter a valid input. The length should be greater than 1.Please try Again.";
        private static string wellcomeTitle = "Wellcome to the BESTPASSWORDMANAGER";
        private static Random random = new Random();
        private static List<string> numberList = new List<string>() { "0","1","2","3","4","5","6","7","8","9"};
        private static List<string> lowerCaseList = new List<string>(){ "a", "b", "c", "d","e","f","g","h", "i", "j",  "k", "l", "m","n","o","p", "q","r", "s","t", "u", "v","w","x","y","z"};
        private static List<string> upperCaseList = new List<string>(){ "A", "B", "C", "D","F","G","H","I", "J",  "K", "L", "M","N","O","P", "Q","R", "S","T", "U", "V","W","X","Y","Z"};
        private static List<string> specialCharacterList = new List<string>() { "#", "$", ".", ",", "/",":", ";", "%","?"};

        public List<QuestionType> getSelectedList ()
        {
            return selectedList;
        }

        public int getPasswordLength()
        {
            return passwordLength;
        }

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
                    CallQuestionAndReadFromConsole(sentValue, questionNumber, numberAllowed);
                    break;
                case QuestionType.LOWERCASE:
                    CallQuestionAndReadFromConsole(sentValue, questionLowerCase, lowerCaseAllowed);
                    break;
                case QuestionType.UPPERCASE:
                    CallQuestionAndReadFromConsole(sentValue, questionUpperCase, upperCaseAllowed);
                    break;
                case QuestionType.SPECIALCHARACTER:
                    CallQuestionAndReadFromConsole(sentValue, questionSpecialCharacter, specialCharacterAllowed);
                    break;
                case QuestionType.LENGTH:
                    ReadLengthFromConsole(questionLength);
                    break;
                default:
                    Console.WriteLine("Invalid Type");
                    break;
            }
        }
          
        public void CallQuestionAndReadFromConsole(QuestionType value,string question, string typeAllowed)
        {
            Console.WriteLine(question);
            typeAllowed = Console.ReadLine();
            bool validType = CheckValidityOfInputs(typeAllowed);
          

            if (!validType)
            {
                Console.WriteLine(validityConsoleMessage);
                CallQuestionAndReadFromConsole(value, question, typeAllowed);
            } 
            else
            {
                if (IsAllowed(typeAllowed)) 
                {
                    selectedList.Add(value);
                    anyValidInput= true;
                }
                
            }
            
        }

        public void ReadLengthFromConsole(string question)
        {
            int number = 0;
            Console.WriteLine(question);
            var temp = Console.ReadLine();

            if (int.TryParse(temp, out number))
            {
               bool validType = CheckValidityOfIntegerInput(number);
                if (!validType)
                {
                    Console.WriteLine(validityLengthConsoleMessage);
                    ReadLengthFromConsole(question);
                }
                else
                {
                    passwordLength = number;
                }
            }
            else
            {
                Console.WriteLine("Please Enter a number");
                ReadLengthFromConsole(question);
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

            if (anyValidInput)
            {
                 for (int i=0; i<passwordLength ; i++) 
                 {
                 int tempRandomValue = random.Next(1, selectedList.Count+1);

                 AddCharacterToPassword(selectedList[tempRandomValue - 1]);
                 }

                 finalPassword = string.Join("", password);
                 WriteFinalPasswordToConsole();
            }
            else
            {
                Console.WriteLine("You should select at least one of avaliable the options.");
            }
           

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
