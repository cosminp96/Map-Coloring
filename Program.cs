using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapColoring
{
    public class Program
    {
        #region Initialisation Methods
        /// <summary>
        /// Function that initializes the state for the 3 countries problem
        /// </summary>
        /// <returns>Created list of countries</returns>
        public List<Country> InitializationForThreeCountries()
        {
            List<Country> countries = new List<Country>();

            Country WA = new Country() { ID = 0, Name="WA", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.RED), new Color(ThreeColors.GREEN), new Color(ThreeColors.BLUE) } };
            Country SA = new Country() { ID = 1, Name = "SA", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.RED), new Color(ThreeColors.GREEN) } };
            Country NT = new Country() { ID = 2, Name = "NT", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.GREEN) } };
            WA.Neighbours.Add(SA); WA.Neighbours.Add(NT);
            SA.Neighbours.Add(WA); SA.Neighbours.Add(NT);
            NT.Neighbours.Add(WA); NT.Neighbours.Add(SA);

            countries.Add(WA); countries.Add(SA); countries.Add(NT);

            return countries;
        }
        /// <summary>
        /// Function that initializes the state for the 7 countries problem
        /// </summary>
        /// <returns>Created list of countries</returns>
        public List<Country> InitializationForSevenCountries()
        {
            List<Country> countries = new List<Country>();

            Country T = new Country() { ID = 0, Name = "T", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.RED), new Color(ThreeColors.BLUE), new Color(ThreeColors.GREEN) } };
            Country WA = new Country() { ID = 1, Name = "WA", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.RED) } };
            Country NT = new Country() { ID = 2, Name = "NT", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.RED), new Color(ThreeColors.BLUE), new Color(ThreeColors.GREEN) } };
            Country SA = new Country() { ID = 3, Name = "SA", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.RED), new Color(ThreeColors.BLUE), new Color(ThreeColors.GREEN) } };
            Country Q = new Country() { ID = 4, Name = "Q", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.GREEN) } };
            Country NSW = new Country() { ID = 5, Name = "NSW", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.RED), new Color(ThreeColors.BLUE), new Color(ThreeColors.GREEN) } };
            Country V = new Country() { ID = 6, Name = "V", Color = new Color(), AvailableColors = new List<Color>() { new Color(ThreeColors.RED), new Color(ThreeColors.BLUE), new Color(ThreeColors.GREEN) } };

            T.Neighbours.Add(V);
            WA.Neighbours.Add(NT); WA.Neighbours.Add(SA);
            NT.Neighbours.Add(WA); NT.Neighbours.Add(Q); NT.Neighbours.Add(SA);
            SA.Neighbours.Add(WA); SA.Neighbours.Add(NT); SA.Neighbours.Add(Q); SA.Neighbours.Add(NSW); SA.Neighbours.Add(V);
            Q.Neighbours.Add(NT); Q.Neighbours.Add(SA); Q.Neighbours.Add(NSW);
            NSW.Neighbours.Add(Q); NSW.Neighbours.Add(SA); NSW.Neighbours.Add(V);
            V.Neighbours.Add(SA); V.Neighbours.Add(NSW); V.Neighbours.Add(T);

            countries.Add(T); countries.Add(WA); countries.Add(NT); countries.Add(SA); countries.Add(Q); countries.Add(NSW); countries.Add(V);

            return countries;
        }
        #endregion

        #region Printing Methods
        /// <summary>
        /// Function that prints all the countries within a delimited square
        /// </summary>
        /// <param name="countries">List of countries</param>
        /// <param name="title">Not mandatory. Title of the countries section</param>
        public void PrintCountries(List<Country> countries, string title = "Countries/Variables")
        {
            Console.WriteLine("+------------------------------------------------+");
            string titleLine = "|";
            var titleSpaces = 48 - title.Length;
            if (titleSpaces % 2 != 0)
                titleLine += string.Concat(Enumerable.Repeat(" ", (titleSpaces / 2) + 1));
            else
                titleLine += string.Concat(Enumerable.Repeat(" ", titleSpaces / 2));
            titleLine += title;
            titleLine += string.Concat(Enumerable.Repeat(" ", titleSpaces / 2)) + "|";
            Console.WriteLine(titleLine);
            Console.WriteLine("+------------------------------------------------+");
            foreach (var country in countries)
            {
                string line = "|";
                var spaces = 48 - country.Name.Length;
                if (spaces % 2 != 0)
                    line += string.Concat(Enumerable.Repeat(" ", (spaces / 2) + 1));
                else
                    line += string.Concat(Enumerable.Repeat(" ", spaces / 2));
                line += country.Name;
                line += string.Concat(Enumerable.Repeat(" ", spaces / 2)) + "|";
                Console.WriteLine(line);
            }
            Console.WriteLine("+------------------------------------------------+");
        }
        /// <summary>
        /// Function that prints all the constraints on neighbours for each country
        /// </summary>
        /// <param name="countries">List of countries</param>
        public void PrintNeighboursConstraints(List<Country> countries)
        {
            //Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|             Neighbours/Constraints             |");
            Console.WriteLine("+------------------------------------------------+");
            foreach (var country in countries)
            {
                string line = "|";
                string info = country.Name + " | ";
                foreach (var neighbour in country.Neighbours)
                {
                    info += neighbour.Name + " ";
                }
                var spaces = 48 - info.Length;
                if (spaces % 2 != 0)
                    line += string.Concat(Enumerable.Repeat(" ", (spaces / 2) + 1));
                else
                    line += string.Concat(Enumerable.Repeat(" ", spaces / 2));
                line += info;
                line += string.Concat(Enumerable.Repeat(" ",spaces / 2)) + "|";
                Console.WriteLine(line);
            }
            Console.WriteLine("+------------------------------------------------+");
        }
        /// <summary>
        /// Function that prints all the constraints on colors for each country
        /// </summary>
        /// <param name="countries">List of countries</param>
        /// <param name="title">Not mandatory. Title of the colors constraints section</param>
        public void PrintColorsConstraints(List<Country> countries, string title = "Colors/Constraints/Initial State")
        {
            //Console.WriteLine("+------------------------------------------------+");
            string titleLine = "|";
            var titleSpaces = 48 - title.Length;
            if (titleSpaces % 2 != 0)
                titleLine += string.Concat(Enumerable.Repeat(" ", (titleSpaces / 2) + 1));
            else
                titleLine += string.Concat(Enumerable.Repeat(" ", titleSpaces / 2));
            titleLine += title;
            titleLine += string.Concat(Enumerable.Repeat(" ", titleSpaces / 2)) + "|";
            Console.WriteLine(titleLine);
            Console.WriteLine("+------------------------------------------------+");
            foreach (var country in countries)
            {
                string line = "|";
                string info = country.Name + " | ";
                foreach (var color in country.AvailableColors)
                {
                    info += color.SelectedColor.ToString() + " ";
                }
                var spaces = 48 - info.Length;
                if (spaces % 2 != 0)
                    line += string.Concat(Enumerable.Repeat(" ", (spaces / 2) + 1));
                else
                    line += string.Concat(Enumerable.Repeat(" ", spaces / 2));
                line += info;
                line += string.Concat(Enumerable.Repeat(" ", spaces / 2)) + "|";
                Console.WriteLine(line);
            }
            Console.WriteLine("+------------------------------------------------+");
        }
        /// <summary>
        /// Function that prints all the colors  from the domain
        /// </summary>
        public void PrintThreeColorDomain()
        {
            //Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|                  Colors/Domain                 |");
            Console.WriteLine("+------------------------------------------------+");
            foreach (var color in (ThreeColors[])Enum.GetValues(typeof(ThreeColors)))
            {
                if (color == ThreeColors.NONE)
                    continue;
                string line = "|";
                var spaces = 48 - color.ToString().Length;
                if (spaces % 2 != 0)
                    line += string.Concat(Enumerable.Repeat(" ", (spaces / 2) + 1));
                else
                    line += string.Concat(Enumerable.Repeat(" ", spaces / 2));
                line += color.ToString();
                line += string.Concat(Enumerable.Repeat(" ", spaces / 2)) + "|";
                Console.WriteLine(line);
            }
            Console.WriteLine("+------------------------------------------------+");
        }
        /// <summary>
        /// Function that prints the current color for each country
        /// </summary>
        /// <param name="countries">List of countries</param>
        public void PrintCountryColors(List<Country> countries)
        {
            bool isSolution = IsSolution(countries);
            //Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|              Country Colors/State              |");
            Console.WriteLine("+------------------------------------------------+");
            foreach (var country in countries)
            {
                string line = "|";
                string info = country.Name + " - " + country.Color.SelectedColor.ToString();

                var spaces = 48 - info.Length;
                if (spaces % 2 != 0)
                    line += string.Concat(Enumerable.Repeat(" ", (spaces / 2) + 1));
                else
                    line += string.Concat(Enumerable.Repeat(" ", spaces / 2));
                line += info;
                line += string.Concat(Enumerable.Repeat(" ", spaces / 2)) + "|";
                Console.WriteLine(line);
            }
            Console.WriteLine("+------------------------------------------------+");
            string resultLine = "|";
            string resultText = "IS SOLUTION? - " + isSolution.ToString();

            var resultSpaces = 48 - resultText.Length;
            if (resultSpaces % 2 != 0)
                resultLine += string.Concat(Enumerable.Repeat(" ", (resultSpaces / 2) + 1));
            else
                resultLine += string.Concat(Enumerable.Repeat(" ", resultSpaces / 2));
            resultLine += resultText;
            resultLine += string.Concat(Enumerable.Repeat(" ", resultSpaces / 2)) + "|";
            Console.WriteLine(resultLine);
            Console.WriteLine("+------------------------------------------------+");
        }
        #endregion

        #region Functionalities
        /// <summary>
        /// Function that checks if the current state of the list is the solution state
        /// </summary>
        /// <param name="countries">List of countries</param>
        /// <returns>True if the list represents a solution, false otherwise</returns>
        public bool IsSolution(List<Country> countries)
        {
            foreach (var country in countries)
            {
                if (country == null || country.Color == null || country.Color.SelectedColor == ThreeColors.NONE || country.Neighbours.Count == 0)
                    return false;
                foreach (var neighbour in country.Neighbours)
                {
                    if (neighbour.Color.SelectedColor == country.Color.SelectedColor)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Function that checks if the current list of countries can have a coloring solution
        /// </summary>
        /// <param name="countries">List of countries</param>
        /// <returns></returns>
        public int HasNoSolution(List<Country> countries)
        {
            var country = countries.FirstOrDefault(x => x.Color.SelectedColor == ThreeColors.NONE && x.AvailableColors.Count == 0);
            return country != null ? 1 : 0;
        }

        /// <summary>
        /// returns index of country with lowest number of available colors and with no color assigned or -1 in case of error
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public int GetLowestPossibleColorsRegionIndex(List<Country> countries)
        {
            var country =  countries.Where(x => x.Color.SelectedColor == ThreeColors.NONE).OrderBy(x => x.AvailableColors.Count).FirstOrDefault();
            return country != null ? countries.IndexOf(country) : -1;
        }

        /// <summary>
        /// Function for the Forward Checking Algorithm
        /// </summary>
        public void ForwardCheckingAlgorithm(List<Country> countries, Country country, ThreeColors color)
        {
            List<Country> countriesCopy = new List<Country>(countries);

            countriesCopy[countriesCopy.IndexOf(country)].Color.SelectedColor = color;

            foreach (var neighbour in countriesCopy[countriesCopy.IndexOf(country)].Neighbours)
            {
                foreach(var availableColor in neighbour.AvailableColors)
                    if (availableColor.SelectedColor == countriesCopy[countriesCopy.IndexOf(country)].Color.SelectedColor)
                    {
                        neighbour.AvailableColors.Remove(availableColor);
                        break;
                    }
            }

            if (HasNoSolution(countriesCopy) == 1)
            {
                Console.WriteLine("No solution for you.");
                Environment.Exit(-1);
            }

            if (IsSolution(countriesCopy))
            {
                PrintCountryColors(countriesCopy);
                Environment.Exit(-1);
            }
            else
            {
                List<Country> countryNeighbours = new List<Country>(country.Neighbours);
                List<Country> MRVNeighbours = new List<Country>();

                while (countryNeighbours.Count > 0)
                {
                    int index = GetLowestPossibleColorsRegionIndex(countryNeighbours);
                    if (index == -1)
                    {
                        foreach (var remaining in countryNeighbours)
                        {
                            MRVNeighbours.Add(remaining);
                        }
                        countryNeighbours.Clear();
                    }
                    else
                    {
                        MRVNeighbours.Add(country.Neighbours[index]);
                        countryNeighbours.RemoveAt(index);
                    }
                }

                foreach (var neighbour in MRVNeighbours)
                {
                    foreach (var availableColor in neighbour.AvailableColors)
                    {
                        ForwardCheckingAlgorithm(countriesCopy, neighbour, availableColor.SelectedColor);
                    }
                }
            }
        }
        #endregion

        static void Main(string[] args)
        {
            Program program = new Program();

            List<Country> countries = program.InitializationForThreeCountries();
            //List<Country> countries = program.InitializationForSevenCountries();

            program.PrintCountries(countries);
            program.PrintNeighboursConstraints(countries);
            program.PrintThreeColorDomain();
            program.PrintColorsConstraints(countries);

            int startIndex = program.GetLowestPossibleColorsRegionIndex(countries);
            List<Country> countriesCopy = new List<Country>(countries);
            
            foreach (var color in countries[startIndex].AvailableColors)
            {
                countries = countriesCopy;
                program.ForwardCheckingAlgorithm(countries, countries[startIndex], color.SelectedColor);
            }
        }
    }
}
