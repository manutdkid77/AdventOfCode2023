using System.Text.RegularExpressions;
using adventofcode_2023.Services;

namespace adventofcode_2023.Days
{
    //https://adventofcode.com/2023/day/2
    public class Day2
    {
        public int SolvePartOne()
        {
            FileReader fileReader = new FileReader();
            var games = fileReader.ReadAllLines($"./Days/{nameof(Day2)}/input.txt");

            if (games == null || games.Count() == 0)
                return 0;

            var redCubePattern = @"\d+ red";
            var totalRedCubesInBag = 12;

            var greenCubePattern = @"\d+ green";
            var totalGreenCubesInBag = 13;

            var blueCubePattern = @"\d+ blue";
            var totalBlueCubesInBag = 14;

            var sumOfGameIDs = 0;

            //each game has a round.
            //A game is valid if the cubes of a particular colour picked in each round should not be greater than the cubes of that colour in the bag
            //if the above condition is valid, then add the game ids of all valid game

            foreach (var game in games)
            {
                if (!IsValidPick(game, redCubePattern, totalRedCubesInBag))
                    continue;


                if (!IsValidPick(game, greenCubePattern, totalGreenCubesInBag))
                    continue;


                if (!IsValidPick(game, blueCubePattern, totalBlueCubesInBag))
                    continue;

                sumOfGameIDs += SearchForDigit(game);
            }

            return sumOfGameIDs;
        }

        public int SearchForDigit(string input)
        {
            string pattern = @"\d+";

            Match match = Regex.Match(input, pattern);
            if (match.Success && Int32.TryParse(match.Value, out var digit))
                return digit;

            return 0;
        }

        public bool IsValidPick(string input, string pattern, int maxCount)
        {
            var matches = Regex.Matches(input, pattern);

            foreach (Match match in Regex.Matches(input, pattern))
            {
                var digitFound = SearchForDigit(match.Value);

                //if the cubes of a colour picked for the round is greater than total cubes of the colour in the bag
                //then it is not a valid pick
                if (digitFound > maxCount)
                    return false;
            }

            return true;
        }

        public int SolvePartTwo()
        {
            FileReader fileReader = new FileReader();
            var games = fileReader.ReadAllLines($"./Days/{nameof(Day2)}/input.txt");

            if (games == null || games.Count() == 0)
                return 0;

            var redCubePattern = @"\d+ red";
            var greenCubePattern = @"\d+ green";
            var blueCubePattern = @"\d+ blue";

            var sumOfPowers = 0;

            //we need to find the max value of each coloured cube for a particular game
            //multiply these values together
            //then find the sum of these multiplied values

            foreach (var game in games)
            {
                var redCubes = GetMaxCubesOfAColour(game, redCubePattern);
                var greenCubes = GetMaxCubesOfAColour(game, greenCubePattern);
                var blueCubes = GetMaxCubesOfAColour(game, blueCubePattern);

                var powerOfCubeSet = redCubes * greenCubes * blueCubes;
                sumOfPowers += powerOfCubeSet;
            }

            return sumOfPowers;
        }

        public int GetMaxCubesOfAColour(string input, string pattern)
        {
            var matches = Regex.Matches(input, pattern);
            var highest = 0;

            foreach (Match match in Regex.Matches(input, pattern))
            {
                var digitFound = SearchForDigit(match.Value);

                if (digitFound > highest)
                    highest = digitFound;
            }

            return highest;
        }
    }
}