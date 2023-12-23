using System.Text.RegularExpressions;
using adventofcode_2023.Services;

namespace adventofcode_2023.Days
{
    //https://adventofcode.com/2023/day/3
    public class Day3
    {
        public int SolvePartOne()
        {
            FileReader fileReader = new FileReader();
            var lines = fileReader.ReadAllLines($"./Days/{nameof(Day3)}/input.txt");
            var sumOfNumbersFound = 0;

            if (lines == null || lines.Count() == 0)
                return 0;

            for (var i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];

                var specialCharacterspattern = @"[^.^\d]";
                var numberOnlypattern = @"\d+";

                foreach (Match match in Regex.Matches(line, numberOnlypattern))
                {
                    int index = match.Index;
                    int length = match.Length;

                    //search to the immediate left for any special character
                    if (index != 0 && Regex.IsMatch(line[index - 1].ToString(), specialCharacterspattern))
                    {
                        sumOfNumbersFound += Convert.ToInt32(match.Value);
                        continue;
                    }

                    //search to the immediate right for any special character
                    if ((index + length < line.Count()) && Regex.IsMatch(line[index + length].ToString(), specialCharacterspattern))
                    {
                        sumOfNumbersFound += Convert.ToInt32(match.Value);
                        continue;
                    }

                    //search whether its adjacent to the bottom line
                    //and diagonally with the bottom line
                    if (i + 1 < lines.Count())
                    {
                        var theLineBelow = lines[i + 1];
                        foreach (Match specialCharMatch in Regex.Matches(theLineBelow, specialCharacterspattern))
                        {
                            var specialCharMatchIndex = specialCharMatch.Index;
                            if (specialCharMatchIndex >= (index - 1) && specialCharMatchIndex <= (index + length))
                            {
                                sumOfNumbersFound += Convert.ToInt32(match.Value);
                                continue;
                            }
                        }
                    }

                    //search whether its adjacent to the upper line
                    //and diagonally with the upper line
                    if (i != 0)
                    {
                        var theLineBelow = lines[i - 1];
                        foreach (Match specialCharMatch in Regex.Matches(theLineBelow, specialCharacterspattern))
                        {
                            var specialCharMatchIndex = specialCharMatch.Index;
                            if (specialCharMatchIndex >= (index - 1) && specialCharMatchIndex <= (index + length))
                            {
                                sumOfNumbersFound += Convert.ToInt32(match.Value);
                                continue;
                            }
                        }
                    }
                }
            }

            return sumOfNumbersFound;
        }

        public int SolvePartTwo()
        {

            FileReader fileReader = new FileReader();
            var lines = fileReader.ReadAllLines($"./Days/{nameof(Day3)}/input.txt");
            var sumOfGearRatios = 0;

            if (lines == null || lines.Count() == 0)
                return 0;

            for (var i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];

                var numbersOnlyPattern = @"\d+";
                var numberToEndOfStringPattern = @"\d+$";
                var numberToStartOfStringPattern = @"^\d+";
                var gearPattern = @"[*]";

                foreach (Match match in Regex.Matches(line, gearPattern))
                {
                    int index = match.Index;
                    int length = match.Length;
                    var findCount = 0;
                    var gearRatio = 1;

                    //search to the left of the special character
                    //the substring to search starts from start of the line till the special character
                    //use a regex which searches for a number at the end of substring
                    var matchAtLeft = Regex.Match(line.Substring(0, index), numberToEndOfStringPattern);
                    if (index != 0 && matchAtLeft.Success)
                    {
                        gearRatio *= Convert.ToInt32(matchAtLeft.Value);
                        findCount++;
                    }

                    //search to the right of the special character
                    //the substring to search starts from a place after the special character till end of the line
                    //use a regex which searches for a number at the start of substring
                    var matchAtRight = Regex.Match(line.Substring(index + 1), numberToStartOfStringPattern);
                    if ((index + length < line.Count()) && matchAtRight.Success)
                    {
                        gearRatio *= Convert.ToInt32(matchAtRight.Value);
                        findCount++;
                    }

                    if (findCount == 2)
                    {
                        sumOfGearRatios += gearRatio;
                        continue;
                    }

                    //search whether its adjacent to the bottom line
                    //and diagonally with the bottom line
                    if (i + 1 < lines.Count())
                    {
                        var theLineBelow = lines[i + 1];
                        foreach (Match numberMatch in Regex.Matches(theLineBelow, numbersOnlyPattern))
                        {
                            var numberMatchIndex = numberMatch.Index;
                            var numberMatchLength = numberMatch.Length;
                            if (numberMatchIndex - 1 <= index && (numberMatchIndex + numberMatchLength) >= index)
                            {
                                findCount++;
                                gearRatio *= Convert.ToInt32(numberMatch.Value);
                                if (findCount == 2)
                                {
                                    sumOfGearRatios += gearRatio;
                                    continue;
                                }
                            }
                        }
                    }

                    //search whether its adjacent to the upper line
                    //and diagonally with the upper line
                    if (i != 0)
                    {
                        var theLineBelow = lines[i - 1];
                        foreach (Match numberMatch in Regex.Matches(theLineBelow, numbersOnlyPattern))
                        {
                            var numberMatchIndex = numberMatch.Index;
                            var numberMatchLength = numberMatch.Length;
                            if (numberMatchIndex - 1 <= index && (numberMatchIndex + numberMatchLength) >= index)
                            {
                                findCount++;
                                gearRatio *= Convert.ToInt32(numberMatch.Value);
                                if (findCount == 2)
                                {
                                    sumOfGearRatios += gearRatio;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }

            return sumOfGearRatios;
        }
    }
}