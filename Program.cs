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
   
}
