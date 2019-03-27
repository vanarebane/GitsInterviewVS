using System;
using System.Text.RegularExpressions;

namespace GitsInterview
{
    internal class ConsoleUI
    {
        public void Start()
        {
            // Welcome message & instructions
            Console.WriteLine(
                "Welcome to the Basic calculator, your operations are:\n" +
                "+  Add\n" +
                "-  Substract\n" +
                "/  Divide\n" +
                "*  Multiply\n"
                +"^  Exponent\n" 
                +"() Brackets"
                );

            // Ask the user for formula
            AskForUserFormula();
        }
        private static void AskForUserFormula()
        {

            Console.ResetColor();
            Console.WriteLine("\nWrite out your formula or type exit:");

            string userEnteredFormula = Console.ReadLine();

            // Check if user want's to exit
            if (userEnteredFormula == "exit")
            {
                Exit();
            }
            else
            {
                // Check for valid formula & process the formula OR send the user explanation that the formula was not valid
                if (ValidateTheFormula(userEnteredFormula))
                {
                    Console.WriteLine("Answer: " +
                        GitsInterview.ShuntingYardAlgorithm.ProcessTheFormula(userEnteredFormula));
                }
                else
                {
                    WriteFormulaProblem();
                }
                // Loop to ask new formula
                AskForUserFormula();
            }
        }
        private static bool ValidateTheFormula(string userEnteredFormula)
        {
            // Check if right symbols are used in the formula
            return new Regex(@"^[,+\-*/^() 0-9]*$").IsMatch(userEnteredFormula);
        }
        private static void WriteFormulaProblem()
        {
            Console.Write("Sorry, bad formula, try using only numbers and operations, like so: ");
            Console.BackgroundColor = ConsoleColor.Black; // Make the sample formula different colour
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("12^2 + ( 3 / 4 ) * -1.5");
            Console.ResetColor();
        }
        private static void Exit()
        {
            // If any things are needed before exiting
        }
    }
}