using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hangman
{
    public class Program
    {
        public static void Main()
        {                  // The list of words and control parameter ''game'' are created. 
            bool game = true;
            string[] words;
            words = new string[10] { "flower", "cat", "computer", "treasure", "town", "planet", "horse", "luggage", "forest", "island" };
            while (game == true)
            {
                Console.WriteLine("Welcome to hangman. \r\n To play, press 1. To exit, press 0.");
                bool valid = int.TryParse(Console.ReadLine(), out int Choice);

                if (valid == false)
                {
                    Console.WriteLine("Invalid type of input. Press either the numbers 0, or 1");
                }
                else
                    switch (Choice) // The program is on untill it shut down below upon user request. 
                    {
                        case 0:
                            Console.WriteLine("Closes the program");
                            game = false;
                            break;
                        case 1: // The word for the gameround is randomly chosen from the ''words'' array and utility parameters are set.

                            Random random = new Random();
                            int dice = random.Next(1, words.Length + 1);
                            bool isright = false;
                            int attempts = 10;
                            string hidden_word = words[dice - 1];
                            int length = hidden_word.Length;
                            int revealed = 0;
                            Char[] hidden_word_arr = new Char[length];
                            hidden_word_arr = hidden_word.ToCharArray(0, length);
                            StringBuilder guessed_characters_sb = new StringBuilder(null, length);
                            Char[] displayed_word = new Char[2 * length];// The word with characters replaced by dashes is build.
                            for (int i = 0; i < (2 * length);)
                            {
                                displayed_word[i] = '_';
                                displayed_word[i + 1] = ' ';
                                i = i + 2;
                            }
                            while (attempts > 0 && isright == false) // The game is on as long as attempts are left, or untill the word is revealed (isright=true).
                            {
                                Console.WriteLine($"You have { attempts } attempts left.");
                                Console.WriteLine(displayed_word);
                                if (attempts != 10)
                                {// This message is not shown the first time.
                                    Console.WriteLine("You have alrady guessed the following wrong characters");
                                    Console.WriteLine(guessed_characters_sb.ToString());
                                }
                                string guess = null;
                                Console.WriteLine("Guess either a letter, or write the whole word");
                                try // Ensures the game does not chrash by invalid user input.
                                {
                                    guess = Console.ReadLine().ToLower();
                                }
                                catch (IOException)
                                {
                                    Console.WriteLine("The program does not recognise your input. Please give a letter.");
                                }
                                catch (OutOfMemoryException)
                                {
                                    Console.WriteLine("Your computer has run out of memory");
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    Console.WriteLine("Your input argument is too big");
                                }
                                if (guess != null) // Game proceeds if user input was recognised.
                                {
                                    int guesslength = guess.Length;
                                    if (guesslength > 1)
                                    {
                                        if (guesslength != length) // The guess is repeated if user guesses more than one character, but the string has not the same length as the word.
                                        {
                                            Console.WriteLine("Bad guess, try again!");
                                        }
                                        else if (string.Equals(hidden_word, guess)) // Checks if user guessed whole word is a match. 
                                        {
                                            Console.WriteLine(hidden_word);
                                            Console.WriteLine("You guessed correct!");
                                            isright = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("you guessed wrong!");// Attempts remaining are decreased upon wrong guess.
                                            attempts--;
                                        }
                                    }
                                    else
                                    {
                                        if (guessed_characters_sb.ToString().Contains(guess) || Array.IndexOf(displayed_word, Convert.ToChar(guess)) > -1)// Checks if letter is already guessed.
                                        {
                                            Console.WriteLine("you have already guessed that letter!");
                                        }
                                        else
                                        {
                                            int flag = 0;

                                            char guess_letter = Convert.ToChar(guess);
                                            for (int i = 0; i < length; i++)
                                            {
                                                if (hidden_word_arr[i] == guess_letter)
                                                {
                                                    displayed_word[2 * i] = guess_letter;
                                                    revealed++;
                                                    flag = 1;
                                                }
                                            }
                                            if (revealed == length)// If all characters are revealed, the game stops.
                                            {
                                                Console.WriteLine(hidden_word);
                                                Console.WriteLine("you have unraveled the word!");
                                                isright = true;
                                            }
                                            else
                                            {
                                                if (flag == 0)
                                                {
                                                    guessed_characters_sb.Append(guess + ",");// Displays guessed wrong characters.
                                                }
                                                attempts--; // Attempts remaining are decreased i whole word is not yet revealed.
                                            }
                                        }
                                    }
                                }
                            }
                            if (attempts == 0)
                                Console.WriteLine("You are hanged!"); // When attempts are spent up, user gets this message.
                            break;
                        default:
                            Console.WriteLine("The program did not understand your input. Press either 0 or 1");
                            break;
                    }
            }
        }
    }
}


