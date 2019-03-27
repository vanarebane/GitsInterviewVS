using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// Shunting Yard demo for RubyNova and Nekotashi
// TASK: Write a basic calculator in an OO-focused fashion that leverages the shunting yard algorithm.

// by: vanarebane
// version: 0.1

// BUGS:
// * Regex needs to be tested and checked. Currently decimal places are not considered

// TODO:
// ProcessTheFormula is not finished
// Look up what "SOLID is part of OOP" is

// TO READ:
// https://en.wikipedia.org/wiki/Shunting-yard_algorithm
// http://www.oxfordmathcenter.com/drupal7/node/628


namespace GitsInterview
{
    class Program
    {
        static void Main(string[] args)
        {

            ConsoleUI console = new ConsoleUI();
            console.Start();
        }
    }
    public class ConsoleUI
    {

        private ShuntingYardAlgorithm SYA;
        public void Start()
        {

            // Welcome message & instructions
            Console.WriteLine(
                "Welcome to the Basic calculator, your operations are:\n" +
                "+  Add\n" +
                "-  Substract\n" +
                "/  Divide\n" +
                "*  Multiply\n" +
                "^  Exponent\n" +
                "() Brackets");

            // Ask the user for formula
            AskForUserFormula();
        }
        private static void AskForUserFormula()
        {

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
            Regex rgx = new Regex(@"^[\.+\-*/^() 0-9]*$");
            return rgx.IsMatch(userEnteredFormula);
        }
        private static void WriteFormulaProblem()
        {
            Console.Write("Sorry, bad formula, try using only numbers and operations, like so: ");
            Console.BackgroundColor = ConsoleColor.Black; // Make the sample formula different colour
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("12 ^ 2 + ( 3 / 4 ) * 1.5");
            Console.ResetColor();
        }
        private static void Exit()
        {
            // If any things are needed before exiting
        }
    }
    public class ShuntingYardAlgorithm
    {

        public static Double ProcessTheFormula(string userEnteredFormula)
        {

            List<string> operatorStack = new List<string>();
            List<string> outputStack = new List<string>();
            Regex rgx = new Regex(@"^[\.+\-*/^() 0-9]*$");

            // Clean the input spaces
            string input = Regex.Replace(userEnteredFormula.Replace(" ", ""), @"[+\-*/^()]", " $& ");
            input = Regex.Replace(input, @"[ ]{2,}", " ");
            string[] inputStack = input.Split(' ');
            Console.WriteLine("Answer: " + input);

            Double d;

            foreach (string symbol in inputStack)
            {
                if (double.TryParse(symbol, out d)){ 
                    outputStack.Add(symbol);
                }
                else
                {
                    operatorStack.Add(symbol);
                }
            }

            return 2; //operands["^"].precence;
        }

    }
    class Operand
    {
        public Double this[Double a, string symbol, Double b]
        {
            get
            {
                Double result;
                switch (symbol)
                {
                    case "+":
                        result = a + b;
                        break;
                    case "-":
                        result = a - b;
                        break;
                    case "*":
                        result = a * b;
                        break;
                    case "/":
                        result = a / b;
                        break;
                    case "^":
                        result = Math.Pow(a, b);
                        break;
                    default:
                        result = 0;
                        break;
                }
                return result;
            }
        }
    }
}
