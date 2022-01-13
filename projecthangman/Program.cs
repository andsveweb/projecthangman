namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            // clear the console and print the welcome message
            Console.Clear();
            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            Console.Clear();
            Console.Title = "art";
            string title = @"
            
 _    _ _____ _     _____ ________  ___ _____   _____ _____ 
| |  | |  ___| |   /  __ \  _  |  \/  ||  ___| |_   _|  _  |
| |  | | |__ | |   | /  \/ | | | .  . || |__     | | | | | |
| |/\| |  __|| |   | |   | | | | |\/| ||  __|    | | | | | |
\  /\  / |___| |___| \__/\ \_/ / |  | || |___    | | \ \_/ /
 \/  \/\____/\_____/\____/\___/\_|  |_/\____/    \_/  \___/ 
                                                            
      _   _    _    _   _  ____ __  __    _    _   _ 
     | | | |  / \  | \ | |/ ___|  \/  |  / \  | \ | |
     | |_| | / _ \ |  \| | |  _| |\/| | / _ \ |  \| |
     |  _  |/ ___ \| |\  | |_| | |  | |/ ___ \| |\  |
     |_| |_/_/   \_\_| \_|\____|_|  |_/_/   \_\_| \_|
            ";
            Console.WriteLine(title);
            Console.WriteLine("This hangman is hard, and you have to guess the word in less than 6 attempts!\nIf you guess right on a letter you won`t loose any tries");
            Console.WriteLine("Good luck!, you will need it!\nPress any key to start");
            Console.ReadKey();
            Console.Clear();


            // create a new game 
            var wordToGuess = PickWord();
            var guessedLetters = "";
            var guessesRemaining = 6;

            // as long as the tries are more or ecual to 0 and the word is not guessed continue
            while (guessesRemaining >= 0)
            {
                Console.WriteLine($"You have {guessesRemaining} guesses remaining");
                Console.WriteLine($"Available letters: {GetAvailableLetters(guessedLetters)}");
                DrawHangman(guessesRemaining);
                DisplayWord(wordToGuess, guessedLetters);

                Console.Write("Enter a letter: ");
                
                // string cant be null
                string? guess;

                // read the user input and check if it is a valid guess
                while (true)
                {
                    guess = Console.ReadLine()?.Trim().ToLower();
                    if (guess?.Length != 1)
                    {
                        Console.WriteLine("Please enter a single letter.");
                        continue;
                    }
                    if (guessedLetters.Contains(guess))
                    {
                        Console.WriteLine("You have already guessed that letter. Try again.");
                        continue;
                    }
                    if (!IsLetter(guess))
                    {
                        Console.WriteLine("Please enter a letter.");
                        continue;
                    }
                    guessedLetters += guess;
                    Console.Clear();
                    break;
                }

                // check if the guess is correct or not
                guessedLetters += guess;
                if (wordToGuess.Contains(guess))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(guess.ToUpper() + " is in the word! Good guess!");
                    Console.WriteLine("-------------------------------------");
                    Console.ResetColor();
                }
                else
                {
                    guessesRemaining--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That letter " + guess.ToUpper() + " is not in my word!");
                    Console.ResetColor();
                    Console.WriteLine("-------------------------------------");
                }

                // check if the user has won
                if (IsWordGuessed(wordToGuess, guessedLetters))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Congratulations, you won! The word is " + wordToGuess.Trim().ToUpper() + "!");
                    Console.ResetColor();
                    Console.WriteLine("Press 1 to play again or any other key to exit.");

                    // read the user input and check if continue or exit
                    if (Console.ReadKey().Key == ConsoleKey.D1)
                    {
                        Console.Clear();
                        wordToGuess = PickWord();
                        guessedLetters = "";
                        guessesRemaining = 6;
                    }
                    else
                    {
                        return;
                    }
                }
                // check if the user has lost
                if (guessesRemaining <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Sorry, you are DEAD. The word is " + wordToGuess.Trim().ToUpper() + "!");
                    Console.ResetColor();
                    Console.WriteLine("  _________     ");
                    Console.WriteLine("  |/       |    ");
                    Console.WriteLine("  |        O    ");
                    Console.WriteLine("  |       /|\\  ");
                    Console.WriteLine("  |       / \\  ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine(" _|_____          ");
                    Console.WriteLine("Press 1 to play again or any other key to exit.");
                    if (Console.ReadKey().Key == ConsoleKey.D1)
                    {
                        Console.Clear();
                        wordToGuess = PickWord();
                        guessedLetters = "";
                        guessesRemaining = 6;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        // pick a random word from the list of words from the webpage
        private static string PickWord()
        {
            using (HttpClient client = new HttpClient())
            {
                var dictionary = client.GetStringAsync("https://www.scrapmaker.com/data/wordlists/dictionaries/dictionary.txt").Result;
                var words = dictionary.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                var random = new Random();
                var randomWord = words[random.Next(0, words.Length)];
                return randomWord;
            }
        }


        // displays letter not guessed.
        private static string GetAvailableLetters(string guessedLetters)
        {
            return "abcdefghijklmnopqrstuvwxyz"
            .Where(letter => !guessedLetters.Contains(letter))
            .Aggregate("", (current, letter) => current + letter)
               ;
        }
        // Switch for stages in hangman 
        private static void DrawHangman(int guessesRemaining)
        {

            switch (guessesRemaining)
            {

                case 6:
                    Console.WriteLine("  _________     ");
                    Console.WriteLine("  |/       |    ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine(" _|______       ");
                    break;
                case 5:
                    Console.WriteLine("  _________     ");
                    Console.WriteLine("  |/       |    ");
                    Console.WriteLine("  |        O    ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine(" _|______          ");
                    break;
                case 4:
                    Console.WriteLine("  _________     ");
                    Console.WriteLine("  |/       |    ");
                    Console.WriteLine("  |        O    ");
                    Console.WriteLine("  |       /     ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine(" _|______          ");
                    break;
                case 3:
                    Console.WriteLine("  _________     ");
                    Console.WriteLine("  |/       |    ");
                    Console.WriteLine("  |        O    ");
                    Console.WriteLine("  |       /|  ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine(" _|_____          ");
                    break;
                case 2:
                    Console.WriteLine("  _________     ");
                    Console.WriteLine("  |/       |    ");
                    Console.WriteLine("  |        O    ");
                    Console.WriteLine("  |       /|\\   ");
                    Console.WriteLine("  |             ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine(" _|_____          ");
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("One wrong answer now and you are dead! ");
                    Console.ResetColor();
                    Console.WriteLine("  _________     ");
                    Console.WriteLine("  |/       |    ");
                    Console.WriteLine("  |        O    ");
                    Console.WriteLine("  |       /|\\  ");
                    Console.WriteLine("  |       /     ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine(" _|_____          ");
                    break;

                case 0:

                    Console.WriteLine("  _________     ");
                    Console.WriteLine("  |/       |    ");
                    Console.WriteLine("  |        O    ");
                    Console.WriteLine("  |       /|\\  ");
                    Console.WriteLine("  |       / \\  ");
                    Console.WriteLine("  |              ");
                    Console.WriteLine(" _|_____          ");
                    break;
            }
        }
        // Print blankspaces in word lenght and print if 
        private static void DisplayWord(string wordToGuess, string guessedLetters)
        {
            var wordToDisplay = wordToGuess
               .ToCharArray()
               .Select(letter => guessedLetters.Contains(letter) ? letter : '_')
               .Aggregate("", (current, letter) => current + letter);

            Console.WriteLine($"The word is: {wordToDisplay}");
        }
        // check if the guess is a letter
        private static bool IsLetter(string? guess)
        {
            return guess?.Length == 1 && char.IsLetter(guess[0]);
        }
        //  check if the letter is in the word to guess and if it is, add it to the guessed letters
        private static bool IsWordGuessed(string wordToGuess, string guessedLetters)
        {
            return wordToGuess
            .ToCharArray()
            .All(letter => guessedLetters.Contains(letter));
        }
    }
}
