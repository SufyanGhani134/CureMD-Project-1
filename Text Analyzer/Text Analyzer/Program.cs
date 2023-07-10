using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Text_Analyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a sentence:");

            //change the all letters of the string to lower case and remove leading and ending spaces 
            string sentence = Console.ReadLine().ToLower().Trim(); 

            if(string.IsNullOrWhiteSpace(sentence)) //Check if the string is not empty
            {
                Console.WriteLine("Please Enter something!");
                Main(args);
            }
            else
            {
                int count = WordCount(sentence); //returns an integer indicating number of words in the given sentence

                if(count == 0) //Check if the entered string have any word with letters or not
                {
                    Console.WriteLine("No word Found");
                }
                else
                {
                    string[] words = WordArray(sentence, count); //Generate an array of words from the given sentence
                    foreach (string word in words)
                    {
                        Console.WriteLine($"{word}");
                    }
                    ConsoleKeyInfo keyInfo; //storing key info with keyInfo variable in ConsoleKeyInfo
                    Console.WriteLine("Press Spacebar to count words frequency");
                    keyInfo = Console.ReadKey(); //store pressed key in keyInfo
                    if (keyInfo.Key == ConsoleKey.Spacebar) //Check if pressed key in Space bar
                    {
                        WordFrequency(words); //calculate frequency of each word in the word array
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press Spacebar to generate sentences with random words from the given sentence");
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        SentenceMaker(words);//Generate sentences with random words from array of words
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press Spacebar to find the longest and the shortes word(s) from given sentence");
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        LongestWordFinder(words); //Find Longest Word(s) in words array
                        ShortestWordFinder(words);//Find shortest Word(s) in words array
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press Spacebar to run word finder");
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        WordFinder(words); //Find a given word in the words Array
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press Spacebar to find Palindrome word(s) from the given sentence");
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        PalindromeCheck(words); //Find Palindrome word in words array
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press Spacebar to count Vowels and Consonants in the words of given sentence");
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        VowelConsonantCounter(words); //Calculate vowels and consonants in a word from words array
                    }
                }
            }

            Console.WriteLine() ;
            Console.WriteLine("Press Enter to close program!") ;
            Console.ReadLine() ;

        }

        static int WordCount(string sentence)
        {
            int count = 0; //initializing an integer count for counting words and assigning its initial value to zero
            for (int i = 0; i < sentence.Length; i++) //iterating through all characters of the string in the sentence
            {
                if (i == sentence.Length - 1) //check if the character is at last index of given string(sentence) 
                {
                    if (Char.IsLetter(sentence[i])) //check if the character is a letter
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
                else //for the characters from start to second last character of the sentence
                {
                    //word start from a letter and ends with a character which is not a letter
                    if (Char.IsLetter(sentence[i]) && !Char.IsLetter(sentence[i + 1]))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        static string[] WordArray(string sentence, int count) 
        {
            //initalizing an array of strings with size of count i.e., number of words in the sentence
            string[] words = new string[count]; 
            string word = ""; //initializing an empty string which will concatenate with the letters of each word of the string
            int wordIndex = 0; //wordIndex refers to the index of each word in the words array

            //Iterating through each character of the sentence
            for (int i = 0; i < sentence.Length; i++)
            {
                //Check for the last character of the sentence
                if (i == sentence.Length - 1)
                {
                    if (Char.IsLetter(sentence[i])) //check if the sentence is ending with a letter
                    {
                        word += sentence[i]; //concatenating the last letter with the previous value
                        words[wordIndex] = word; //placing the word at wordIndex in words array 
                    }
                    else
                    {
                        //if sentence is ending with a character which is not a letter, place the word without any concatenation
                        //with current value of iteration
                        words[wordIndex] = word; 
                    }
                }
                //remaining conditions for the starting to the second last character of the sentence
                else if (Char.IsLetter(sentence[i])) 
                {
                    //if character is a letter, concatenate it with the previous value of the word(string)
                    //this will be for the starting of a word till the ending of the word
                    word += sentence[i];
                }
                //check the ending of a word i.e., each word ends with a non-letter character followed by a letter
                //e.g., 'hello world' hello starts with 'h' and ends with the space just before 'w' 
                else if (!Char.IsLetter(sentence[i]) && Char.IsLetter(sentence[i + 1])) 
                {
                    if (count != 1 && !String.IsNullOrEmpty(word)) //check if the sentence have more than one word
                    {
                        words[wordIndex] = word; //Place the word at the wordIndex of the words array
                        wordIndex++; //incrementing wordIndex for the next word
                        word = ""; //clearing word string to start next word
                    } 
                    //else if sentence have just one word then the ending will be with the last character of the sentence
                    //describe in the first block of conditions
                }
            }
            return words;
        }

        static void WordFrequency(string[] words)
        {
            Console.WriteLine("Words Frequency :");
            for(int i=0; i<words.Length; i++) //loop for each word from words array
            {
                int count = 0; //initializing the count of each word
                for(int j = 0; j<words.Length; j++) //loop to compare within the words array
                {
                    if (words[i] == words[j] && j<i) //break the loop if ith word == jth word and the jth word lie befor ith word
                                                     //to avoid repitition of comparison
                    {
                        break;
                    }
                    if (words[i] == words[j])
                    {
                        count++;
                    }
                }
                if(count > 0) //check if the sentence is not empty string
                {
                    Console.WriteLine($"\'{words[i]}\' : {count}");
                }
            }
            Console.WriteLine();
        }

        static void SentenceMaker(string[] words)
        {
            Console.WriteLine("Enter the number of sentences:");   
            int n = int.Parse(Console.ReadLine());
            int len = words.Length;
            Console.WriteLine("Generated Sentences: ");
            Random random = new Random(); //creating new instance from a built-in class Random to generate random integers
            if (n > 0)
            {
                for (int i = 1; i <= n; i++) //iteration for each sentence upto n sentences 
                {
                    Console.Write(i + ": ");
                    for (int j = 1; j <= 5; j++) //iteraion for each word in the sentence upto 5 words
                    {
                        //random.Next[int] generate a random number upto the number of words in wordsArray
                        Console.Write(words[random.Next(len - 1)] + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Invalid Number!");
                Console.WriteLine();
                SentenceMaker(words);
            }
            
            Console.WriteLine();
        }

        static void LongestWordFinder(string[] words)
        {
            Console.WriteLine("Longest word(s) : ");
            for(int i=0; i<words.Length ; i++)
            {
                bool isLongest = true; //flag that remains true if the current iteration(word) has length greater than all iterations(words)
                for (int j=0; j<words.Length ; j++)
                {
                    if (words[i].Length < words[j].Length) //check if the iteration(word) has length shorter than any iteration(word)
                    {
                        isLongest = false;
                    }
                }

                if (isLongest) //check if the flag remains true for any word which will be the longest
                {
                    Console.WriteLine(words[i]);
                }
            }
            Console.WriteLine();
        }

        static void ShortestWordFinder(string[] words)
        {
            Console.WriteLine("Shortest word(s) : ");
            for (int i = 0; i < words.Length; i++)
            {
                bool isShortest = true; //flag that remains true if the current iteration(word) has length shorter than all iterations(words)
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[i].Length > words[j].Length)//check if the iteration(word) has length greater than any iteration(word)
                    {
                        isShortest = false;
                    }
                }

                if (isShortest)//check if the flag remains true for any word which will be the shortest
                {
                    Console.WriteLine(words[i]);
                }
            }
            Console.WriteLine();
        }

        static void WordFinder(string[] words)
        {
            Console.WriteLine("Enter a word :");
            string word = Console.ReadLine();
            int count = 0; //intializing count to calculate number of times the given word occur in a sentence.
            for(int i =0;  i < words.Length ; i++) //iteration to check if the given word present in words array
            {
                if(word.ToLower().Trim() == words[i].ToLower().Trim())
                {
                    count++; //increment in count if given word matches with any word in words array
                }
            }

            if(count > 0) //Check if count >0 means if the given word is present in words array
            {
                Console.WriteLine($"The given word \'{word}\' appears {count} time(s) in the given sentence.");
            }
            else
            {
                Console.WriteLine("Word not Found!");
            }
            Console.WriteLine();
        }

        static void PalindromeCheck(string[] words)
        {
            Console.WriteLine("Palindrome words in given sentence are:");
            int count = 0; //check number of palindrome words in words array
            for(int i=0 ; i<words.Length; i++)
            {
                bool isPalindrome = true; //flag that remains true if the word in iteration is a palindrome
                int wordLength = words[i].Length;
                for(int j=0; j < wordLength; j++)
                {
                    //check if the cooresponding letter from start and end of the word same or not
                    if (words[i][j] != words[i][wordLength - 1 - j]) 
                    {
                        isPalindrome = false;
                    }
                }

                if (isPalindrome)//if flag remains true for palindrome
                {
                    Console.WriteLine(words[i]);
                    count++;
                }
            }
            if(count == 0) //Check if there is no palindrome word in words array
            {
                Console.WriteLine("No Palindrome Word Found!");
            }
            Console.WriteLine();
        }

        static void VowelConsonantCounter(string[] words)
        {
            string[] vowels = { "a", "e", "i", "o", "u"}; //vowels array
            for(int i =0;  i<words.Length; i++) //loop for each word in words array
            {
                int vowelCounter = 0; //initializing vowel counter to calculate number of vowels in each word from words array
                for (int j = 0; j < words[i].Length; j++) //loop for each character in each word in words array
                {
                    for(int k=0; k<vowels.Length; k++) //loop for each letter in vowels array
                    {
                        //converts each character in each word to string to compare it with vowels 
                        bool isVowel = vowels[k] == words[i][j].ToString(); 
                        if (isVowel)
                        {
                            vowelCounter ++;
                        }
                    }
                }

                Console.WriteLine($"\'{words[i]}\' : {vowelCounter} vowels and {words[i].Length-vowelCounter} Consonants");
            }
        } 

    }
    }
