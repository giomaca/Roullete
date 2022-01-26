using System;
using System.Collections.Generic;

namespace Roulette
{
    class Program
    {
        static void Main(string[] args)
        {
            Roulette roulette = new Roulette();
            roulette.Play();
        }
    }

    class Roulette
    {
        private Dictionary<int, int> balance;
        private readonly Dictionary<int, string> numbers = new Dictionary<int, string>()
        {
            {1, "Red"}, {3, "Red"}, {5, "Red"}, {7, "Red"}, {9, "Red"}, {12, "Red"}, {14, "Red"}, {16, "Red"}, {18, "Red"},
            {19, "Red"}, {21, "Red"}, {23, "Red"}, {25, "Red"}, {27, "Red"}, {30, "Red"}, {32, "Red"}, {34, "Red"}, {36, "Red"},

            {2, "Black"}, {4, "Black"}, {6, "Black"}, {8, "Black"}, {10, "Black"}, {11, "Black"}, {13, "Black"}, {15, "Black"}, {17, "Black"},
            {20, "Black"}, {22, "Black"}, {24, "Black"}, {26, "Black"}, {28, "Black"}, {29, "Black"}, {31, "Black"}, {33, "Black"}, {35, "Black"},
        };
        private string lastWon = "The last won numbers:";
        private Dictionary<int, int> guessNumbers;
        private Dictionary<int, string> guessColors;
        private int wonNumber;
        private string wonColor;
        private Dictionary<int, int> bets;
        private int players;

        public void Play()
        {
        start:
            Console.WriteLine("****************************************");
            Console.WriteLine("*               ROULLETE               *");
            Console.WriteLine("****************************************");

            balance = new Dictionary<int, int>();


            Console.Write("How many player?\n=> ");
            players = int.Parse(Console.ReadLine());
            for (int i = 1; i <= players; i++)
                balance.Add(i, 1000);

            while (true)
            {
                guessNumbers = new Dictionary<int, int>();
                bets = new Dictionary<int, int>();
                guessColors = new Dictionary<int, string>();
                Console.WriteLine("----------------------------------------");
                for (int i = 1; i <= players; i++)
                    Console.WriteLine($"{i} PLAYER BALANCE: {balance[i]}");
                if (char.IsDigit(lastWon[lastWon.Length - 1]))
                    Console.WriteLine(lastWon);
                else
                    Console.WriteLine("There isn't last won numbers");

                //if (balance == 0 || balance < 0)
                //{
                //    Console.WriteLine("you lost all of your money.");
                //    return;
                //}

                try
                {
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("[1] Bet on Numbers");
                    Console.WriteLine("[2] Bet on Colors");
                    Console.WriteLine("[3] Change players amount");
                    Console.WriteLine("[4] Exit");
                    Console.Write("=> ");
                    int answer = int.Parse(Console.ReadLine());

                    for (int i = 1; i <= players; i++)
                    {
                        Console.WriteLine($"Bet {i} player");
                        if (answer == 1)
                            BetNumbers(i);
                        else if (answer == 2)
                            BetColor(i);
                        else if (answer == 3)
                            goto start;
                        else if (answer == 4)
                            return;
                    }
                    GenerateRandom();
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine($"Winning number is {wonNumber} {wonColor}");

                    for (int i = 1; i <= players; i++)
                    {
                        if (answer == 1)
                            PlaceNumber(i);
                        else if (answer == 2)
                            PlaceColor(i);
                    }
                }
                catch
                {
                    Console.WriteLine($"Invalid input");
                }
            
            }
        }

        private void BetNumbers(int id)
        {

            while (true)
            {
                try
                {
                    Console.Write("Enter numbers from 0 to 36\n=> ");
                    int guessNumber = int.Parse(Console.ReadLine());
                    if (guessNumber >= 0 && guessNumber <= 36)
                    {
                        guessNumbers.Add(id, guessNumber);
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                }
            }
            Betting(id);
        }

        private void BetColor(int id)
        {
            int color;
            while (true)
            {
                try
                {
                    Console.WriteLine("Choose color(1 or 2)");
                    Console.WriteLine("[1] - Red");
                    Console.WriteLine("[2] - Black");

                    Console.Write("=> ");
                    color = int.Parse(Console.ReadLine());
                    if (color == 1)
                        guessColors.Add(id, "Red");
                    else if (color == 2)
                        guessColors.Add(id, "Black");
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                }
            }
            Betting(id);
        
        }

        private void Betting(int id)
        {
            while (true)
            {
                try
                {
                    Console.Write("bet amount (Max: 60)\n=> ");
                    int bet = int.Parse(Console.ReadLine());
                    if (bet >= 1 && bet <= 60)
                    {
                        bets.Add(id, bet);
                        balance[id] -= bet;
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        private void PlaceNumber(int id)
        {
            if (wonNumber == guessNumbers[id])
            {
                Console.WriteLine($"{id} player Won, Placed number:{guessNumbers[id]}, Bet:{bets[id]}");
                bets[id] *= 2;
                balance[id] += bets[id];
                Console.WriteLine($"{id} balance is now: {balance[id]}");
            }
            else
            {
                Console.WriteLine($"{id} plyer lose. Placed number:{guessNumbers[id]}, Bet:{bets[id]}");
            }
        }

        private void PlaceColor(int id)
        {
            if (wonColor == guessColors[id])
            {
                Console.WriteLine($"{id} player Won, Placed color:{guessColors[id]}, Bet:{bets[id]}");
                bets[id] += (bets[id] * 20) / 100;
                balance[id] += bets[id];
                Console.WriteLine($"{id} balance is now:  {balance[id]}");
            }
            else
            {
                Console.WriteLine($"{id} plyer lose. Place color:{guessColors[id]}, Bet:{bets[id]}");
            }
        }

        private void GenerateRandom()
        {
            Random random = new Random();
            wonNumber = random.Next(0, 36);
            wonColor = numbers[wonNumber];
            lastWon += $" {wonNumber}";
        }
    }
}
