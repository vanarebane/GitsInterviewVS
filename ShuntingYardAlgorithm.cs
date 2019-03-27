using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GitsInterview
{
    internal class ShuntingYardAlgorithm
    {

        public static string ProcessTheFormula(string userEnteredFormula)
        {

            List<string> operatorStack = new List<string>();
            List<string> outputStack = new List<string>();
            //string[] operatorStack, outputStack;
            Regex rgx = new Regex(@"^[\.+\-*/^() 0-9]*$");

            Dictionary<string, int> precedence = 
            new Dictionary<string, int>(){
                {"^", 4},
                {"*", 3},
                {"/", 3},
                {"+", 2},
                {"-", 2},
                {"(", -1}
            };

            Console.ForegroundColor = ConsoleColor.Red;
            // Check if two forbidden operations are next to each other
            if (new Regex(@"[+*/-][+*/]").IsMatch(userEnteredFormula))
            {
                return "There are two operations next to each other that break the formula";
            }

            // If formula ends with operation
            if (!new Regex(@"[0-9]$").IsMatch(userEnteredFormula))
            {
                return "Formula ends with an operation";
            }

            // Clean the input spaces
            string input = Regex.Replace(userEnteredFormula.Replace(" ", ""), @"[+\-*/^()]", " $& ");
            input = Regex.Replace(input, @"[ ]{2,}", " ");
            string[] inputStack = input.Split(' ');
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Clean input: "+input);

            // Set the console colour red for all future errors
            Console.ForegroundColor = ConsoleColor.Red;

            // The Shunting Yard Algorithm
            Double d;
            bool makeNextNumberNegative = false;
            string lastSymbol = "";

            foreach (string symbol in inputStack)
            {
                
                if (double.TryParse(symbol, out d))
                {
                    if (makeNextNumberNegative)
                    {
                        outputStack.Add("-" + symbol);
                        makeNextNumberNegative = false;
                    }
                    else { outputStack.Add(symbol); }

                    lastSymbol = "";
                }
                else
                {
                    // If operation is minus, make next number negative or stop the process
                    if (outputStack.Count == 0)
                    {
                        if (symbol == "-") makeNextNumberNegative = true;
                        else return "Formula starting with " + symbol + " operation witch is bad";
                    }
                    // If next operation is after an operation make the next number negative
                    else if (lastSymbol != "" && symbol == "-")
                    {
                        makeNextNumberNegative = true;
                    }
                    // If brackets end after operation
                    else if (lastSymbol != "" && symbol == ")")
                    {
                        return "Operation symbol at the end of brackets should follow with number";
                    }
                    // If start of brackets, just push to the operatorStack
                    else if (symbol == "(")
                    {
                        operatorStack.Add(symbol);
                    }
                    // If end of brackets
                    else if (symbol == ")")
                    {
                        while (operatorStack.Count > 0 && operatorStack[operatorStack.Count - 1] != "(")
                        {
                            outputStack.Add(operatorStack[operatorStack.Count - 1]);
                            operatorStack.RemoveAt(operatorStack.Count - 1);
                        }
                        operatorStack.RemoveAt(operatorStack.Count - 1); // Should be the (, so just discard it
                    }
                    // Add to the stack by precedence
                    else
                    {
                        while(operatorStack.Count > 0)
                        {
                            int compare = precedence[symbol] - precedence[operatorStack[operatorStack.Count - 1]];
                            if (compare < 0 || symbol != "^" && compare <= 0)
                            {
                                outputStack.Add(operatorStack[operatorStack.Count - 1]);
                                operatorStack.RemoveAt(operatorStack.Count - 1);
                            }
                            else break;
                        }
                        operatorStack.Add(symbol);
                    }

                    lastSymbol = symbol;
                }
                // Console.WriteLine("   " + symbol + "  |  " + String.Join(" ", operatorStack.ToArray()) + "  |  " + String.Join(" ", outputStack.ToArray()));

            }


            while (operatorStack.Count > 0)
            {
                outputStack.Add(operatorStack[operatorStack.Count - 1]);
                operatorStack.RemoveAt(operatorStack.Count - 1);
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Shunted: " + String.Join(" ", outputStack.ToArray()));
            Console.ResetColor();

            List<Double> numbers = new List<Double>();
            Double result = 0;

            if (outputStack.Count == 1) {
                return outputStack[0];
            }

            while (outputStack.Count > 0)
            {
                string symbol = outputStack[0];
                outputStack.RemoveAt(0);

                if (double.TryParse(symbol, out d))
                {
                    numbers.Add(d);
                }
                else
                {
                    if (numbers.Count < 2) {
                        return "Too many operators";
                    }
                    if (symbol == "+")
                    {
                        result = numbers[numbers.Count-2] + numbers[numbers.Count-1];
                    }
                    else if(symbol == "-")
                    {
                        result = numbers[numbers.Count-2] - numbers[numbers.Count-1];
                    }
                    else if (symbol == "*")
                    {
                        result = numbers[numbers.Count-2] * numbers[numbers.Count-1];
                    }
                    else if (symbol == "/")
                    {
                        result = numbers[numbers.Count-2] / numbers[numbers.Count-1];
                    }
                    else if (symbol == "^")
                    {
                        result = Math.Pow(numbers[numbers.Count-2], numbers[numbers.Count-1]);
                    }
                    numbers[numbers.Count - 2] = result;
                    numbers.RemoveAt(numbers.Count - 1);
                }
            }

            return result.ToString();
        }

    }
}