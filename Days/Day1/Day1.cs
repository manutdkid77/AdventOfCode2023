using adventofcode_2023.Services;

namespace adventofcode_2023.Days.Day1
{
    public class Day1
    {
        public int SolvePartOne()
        {
            FileReader fileReader = new FileReader();
            var lines = fileReader.ReadAllLines(@"./Days/Day1/input.txt");

            if (lines == null || lines.Count() == 0)
                return 0;

            var calibrationLines = new List<int>();

            for (var i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];

                var numberAtStart = SearchFromStringStart(line);
                var numberAtEnd = SearchFromStringEnd(line);

                if (string.IsNullOrWhiteSpace(numberAtStart) || string.IsNullOrWhiteSpace(numberAtEnd))
                    continue;

                var calibrationLine = Convert.ToInt32(numberAtStart + numberAtEnd);

                calibrationLines.Add(calibrationLine);
            }

            return calibrationLines.Sum();
        }

        public string SearchFromStringStart(string line)
        {
            for (var j = 0; j < line.Count(); j++)
            {
                if (Char.IsDigit(line[j]))
                    return line[j].ToString();
            }

            return null;
        }

        public string SearchFromStringEnd(string line)
        {
            for (var j = line.Count() - 1; j >= 0; j--)
            {
                if (Char.IsDigit(line[j]))
                    return line[j].ToString();
            }

            return null;
        }

        Dictionary<string, int> WordNumberMap = new Dictionary<string, int>() {
            { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 },
            { "eight", 8 }, { "nine", 9 }};

        private string SearchWordNumberMap(string word)
        {
            if (WordNumberMap.TryGetValue(word, out var numberFound))
                return numberFound.ToString();

            return null;
        }

        public int SolvePartTwo()
        {
            FileReader fileReader = new FileReader();
            var lines = fileReader.ReadAllLines(@"./Days/Day1/input.txt");

            if (lines == null || lines.Count() == 0)
                return 0;

            var calibrationLines = new List<int>();

            for (var i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];

                var numberAtStart = SearchFromStringStartPartTwo(line);
                var numberAtEnd = SearchFromStringEndPartTwo(line);

                if (string.IsNullOrWhiteSpace(numberAtStart) || string.IsNullOrWhiteSpace(numberAtEnd))
                    continue;

                var calibrationLine = Convert.ToInt32(numberAtStart + numberAtEnd);

                calibrationLines.Add(calibrationLine);
            }

            return calibrationLines.Sum();
        }

        private string SearchFromStringStartPartTwo(string line)
        {
            for (var j = 0; j < line.Count(); j++)
            {
                if (Char.IsDigit(line[j]))
                    return line[j].ToString();

                //check for numbers with 3 characters
                if (j >= 2)
                {
                    var word = $"{line[j - 2]}{line[j - 1]}{line[j]}";

                    var foundNumber = SearchWordNumberMap(word);

                    if (!string.IsNullOrWhiteSpace(foundNumber))
                        return foundNumber;

                    //check for numbers with 4 characters
                    if (j >= 3)
                    {
                        word = $"{line[j - 3]}{word}";

                        foundNumber = SearchWordNumberMap(word);

                        if (!string.IsNullOrWhiteSpace(foundNumber))
                            return foundNumber;
                    }

                    //check for numbers with 5 characters
                    if (j >= 4)
                    {
                        word = $"{line[j - 4]}{word}";

                        foundNumber = SearchWordNumberMap(word);

                        if (!string.IsNullOrWhiteSpace(foundNumber))
                            return foundNumber;
                    }
                }

            }

            return null;
        }

        private string SearchFromStringEndPartTwo(string line)
        {
            for (var j = line.Count() - 1; j >= 0; j--)
            {
                if (Char.IsDigit(line[j]))
                    return line[j].ToString();


                //check for numbers with 3 characters
                if (line.Count() - j > 2)
                {
                    var word = $"{line[j]}{line[j + 1]}{line[j + 2]}";

                    var foundNumber = SearchWordNumberMap(word);

                    if (!string.IsNullOrWhiteSpace(foundNumber))
                        return foundNumber;

                    //check for numbers with 4 characters
                    if (line.Count() - j > 3)
                    {
                        word = $"{word}{line[j + 3]}";

                        foundNumber = SearchWordNumberMap(word);

                        if (!string.IsNullOrWhiteSpace(foundNumber))
                            return foundNumber;
                    }

                    //check for numbers with 5 characters
                    if (line.Count() - j > 4)
                    {
                        word = $"{word}{line[j + 4]}";

                        foundNumber = SearchWordNumberMap(word);

                        if (!string.IsNullOrWhiteSpace(foundNumber))
                            return foundNumber;
                    }
                }

            }

            return null;
        }

    }
}
