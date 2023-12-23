using System.Text.RegularExpressions;
using adventofcode_2023.Services;

namespace adventofcode_2023.Days
{
    //https://adventofcode.com/2023/day/4
    public class Day4
    {
        public double SolvePartOne()
        {
            FileReader fileReader = new FileReader();
            var cards = fileReader.ReadAllLines($"./Days/{nameof(Day4)}/input.txt");

            if (cards == null || cards.Count() == 0)
                return 0;

            var numberOnlypattern = @"\d+";
            double sumOfAllCardPoints = 0;

            foreach (var card in cards)
            {
                //split the string into two halves, one half has the winning numbers, the other half is just a set of numbers
                var parts = card.Split("|");

                if (parts == null || parts.Count() != 2)
                    continue;

                var winningNumbers = Regex.Matches(parts[0], numberOnlypattern);

                var myNumbers = Regex.Matches(parts[1], numberOnlypattern);

                var matchesFound = FindMatches(winningNumbers, myNumbers);

                if (matchesFound == 0)
                    continue;

                //The first match makes the card worth one point
                //and each match after the first doubles the point value of that card.
                //so every match is basically a power of 2ᵐᵃᵗᶜʰᵉˢᶠᵒᵘⁿᵈ⁻¹
                var cardWorth = Math.Pow(2, matchesFound - 1);
                sumOfAllCardPoints += cardWorth;
            }

            return sumOfAllCardPoints;
        }

        private int FindMatches(MatchCollection winningNumbers, MatchCollection myNumbers)
        {
            var matchesFound = 0;
            //loop between both set of numbers to find how many times winning numbers appear in the set of numbers
            for (var i = 0; i < winningNumbers.Count(); i++)
            {
                //skip the first number from the winning numbers, since it has the card number
                //Ex Card 1
                if (i == 0)
                    continue;

                for (var j = 0; j < myNumbers.Count(); j++)
                {
                    if (winningNumbers[i].Value == myNumbers[j].Value)
                        matchesFound++;
                }
            }
            return matchesFound;
        }

        public int SolvePartTwo()
        {
            FileReader fileReader = new FileReader();
            var cards = fileReader.ReadAllLines($"./Days/{nameof(Day4)}/input.txt");

            if (cards == null || cards.Count() == 0)
                return 0;

            var numberOnlypattern = @"\d+";

            //dictionary to hold the original and copies of a card for every round.
            Dictionary<int, int> scratchCards = new Dictionary<int, int>();

            for (var i = 0; i < cards.Length; i++)
            {
                var cardNo = i + 1;
                var card = cards[i];

                //split the string into two halves, one half has the winning numbers, the other half is just a set of numbers
                var parts = card.Split("|");

                if (parts == null || parts.Count() != 2)
                    continue;

                var winningNumbers = Regex.Matches(parts[0], numberOnlypattern);

                var myNumbers = Regex.Matches(parts[1], numberOnlypattern);

                var matchesFound = FindMatches(winningNumbers, myNumbers);

                ProcessCards(scratchCards, cardNo, matchesFound);
            }

            Console.Write("Hello");

            return scratchCards.Sum(x => x.Value);
        }

        private void ProcessCards(Dictionary<int, int> scratchCards, int cardNo, int matchesFound)
        {
            //add the current original card to the dictionary
            AddOrUpdateCardCount(scratchCards, cardNo);

            //get the total cards of a particular round (original and copies)
            var cardsToAdd = GetCardCount(scratchCards, cardNo);

            var nextCard = cardNo + 1;

            //add or update the count of the other cards found
            //make sure to increase the count of the other cards based on the value of cardsToAdd
            for (var i = 0; i < matchesFound; i++)
            {
                AddOrUpdateCardCount(scratchCards, nextCard, cardsToAdd);
                nextCard = nextCard + 1;
            }
        }

        private void AddOrUpdateCardCount(Dictionary<int, int> scratchCards, int cardNo, int cardsToAdd = 1)
        {
            if (scratchCards.ContainsKey(cardNo))
            {
                scratchCards[cardNo] = scratchCards[cardNo] + (1 * cardsToAdd);
            }
            else
            {
                scratchCards.Add(cardNo, (1 * cardsToAdd));
            }
        }

        private int GetCardCount(Dictionary<int, int> scratchCards, int cardNo)
        {
            int cardCount = 0;
            scratchCards.TryGetValue(cardNo, out cardCount);
            return cardCount;
        }
    }
}