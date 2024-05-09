using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace word_guessing_game
{
    class Program
    {
        static string preAnswer = "";
        static void Main(string[] args)
        {
            Console.ResetColor();
            string theWord = "";
            
            string filePath = "words.txt"; 
            List<string> wordsList = File.ReadAllText(filePath)
                                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(word => word.Trim())
                                        .ToList();

            bool correct = false;

            Random rnd = new Random();

            List<string> results = new List<string>();

            theWord = wordsList[rnd.Next(0, wordsList.Count)];

            while (!correct)
            {
                correct = false;
                results.Clear();
                Console.Clear();
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Please enter your guess: ");
                        preAnswer = Console.ReadLine().ToLower();
                        if (preAnswer == "")
                        {
                            throw new Exception("Cannot be blank");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: " + ex.Message);
                        continue;
                    }
                    break;
                }

                char[] theGuessInChars = preAnswer.ToArray();
                char[] theWordInChars = theWord.ToArray();         

                if (theWord.Length > preAnswer.Length)
                {
                    for (int i = 0; i < preAnswer.Length; i++)
                    {
                        if (theGuessInChars[i] == theWordInChars[i])
                        {
                            results.Add("Position " + (i + 1) + " is fully correct");
                        }
                        else if (theWord.Contains(theGuessInChars[i]))
                        {
                            results.Add("Position " + (i + 1) + " is partially correct");
                        }
                        else if (theGuessInChars[i] != theWordInChars[i])
                        {
                            results.Add("Position " + (i + 1) + " is not correct");
                        }                   
                    }
                    results.Add("Guess is too small");
                }
                else
                {
                    for (int i = 0; i < theWord.Length; i++)
                    {
                        if (theGuessInChars[i] == theWordInChars[i])
                        {
                            results.Add("Position " + (i + 1) + " is fully correct");
                        }
                        else if (theWord.Contains(theGuessInChars[i]))
                        {
                            results.Add("Position " + (i + 1) + " is partially correct");
                        }
                        else if (theGuessInChars[i] != theWordInChars[i])
                        {
                            results.Add("Position " + (i + 1) + " is not correct");
                        }
                    }
                    if (preAnswer.Length != theWord.Length)
                    {
                        results.Add("Guess is too big");
                    }
                }

                Console.Clear();

                Console.WriteLine("A full match is when the character and the position match");
                Console.WriteLine("A partial match is when the character is contained within the word you are trying to guess");
                Console.WriteLine("A non match is when the character and the position both do not match");
                Console.WriteLine("");

                for (int i = 0; i < preAnswer.Length; i++)
                {
                    if (i < results.Count)
                    {
                        if (results[i].EndsWith("is fully correct"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        if (results[i].EndsWith("is partially correct"))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        if (results[i].EndsWith("is not correct"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                    }
                    if (i > theWord.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    if (i < theGuessInChars.Length)
                    {
                        Console.Write(theGuessInChars[i] + " ");
                    }                  
                }

                Console.WriteLine("\n");

                for (int i = 0; i < preAnswer.Length; i++)
                {
                    if (i < results.Count && (!results[i].EndsWith("is too big") && !results[i].EndsWith("is too small")))
                    {
                        if (results[i].EndsWith("is fully correct"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if (results[i].EndsWith("is partially correct"))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else if (results[i].EndsWith("is not correct"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.WriteLine(results[i]);
                    }
                    if (i >= results.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Position not there");
                    }
                }
                Console.WriteLine("");
                if (results.Any(str => str.EndsWith("too big")))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Guess too big");
                }
                if (results.Any(str => str.EndsWith("too small")))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Guess too small");
                }

                Console.ResetColor();

                Console.WriteLine("");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                correct = results.All(str => str.EndsWith("is fully correct"));

            }
        }
    }
}
