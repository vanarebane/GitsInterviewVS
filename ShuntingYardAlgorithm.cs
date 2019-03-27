using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GitsInterview
{
    internal class ShuntingYardAlgorithm
    {

        public static Double ProcessTheFormula(string userEnteredFormula)
        {

            List<string> operatorStack = new List<string>();
            List<string> outputStack = new List<string>();
            //string[] operatorStack, outputStack;
            Regex rgx = new Regex(@"^[\.+\-*/^() 0-9]*$");

            // Clean the input spaces
            string input = Regex.Replace(userEnteredFormula.Replace(" ", ""), @"[+\-*/^()]", " $& ");
            input = Regex.Replace(input, @"[ ]{2,}", " ");
            string[] inputStack = input.Split(' ');
            Console.WriteLine("Answer: " + input);

            Double d;

            foreach (string symbol in inputStack)
            {
                if (double.TryParse(symbol, out d))
                {
                    outputStack.Add(symbol); //.Add(symbol);
                }
                else
                {
                    // check here for brackets
                    while (operatorStack.Count > 0)
                    {
                        operatorStack.CopyTo(outputStack);
                    }


                    operatorStack.Add(symbol);
                }
            }

            return 2; //operands["^"].precence;
        }

    }
}