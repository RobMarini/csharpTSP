using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            var cr = new CityReader();
            int numTours = 30;
            int numTrials = 10000;
            try
            {
                Tour tour = new Tour(cr.readCities(@"C:\Users\Rob\workspace\TravelingSalesman\files\30cities.txt"));
                GeneticAlgorithm mutate = new GeneticAlgorithm();
                GeneticAlgorithm crossover = new GeneticAlgorithm();
                mutate.setAllTours(tour.seed(numTours));
                for (int i = 0; i < numTrials; i++)
                {
                    mutate.setAllTours(mutate.GenerateGeneration(true));
                }
                crossover.setAllTours(tour.seed(numTours));
                for (int i = 0; i < numTrials; i++)
                {
                    crossover.setAllTours(crossover.GenerateGeneration(true));
                }
                Console.WriteLine("The results from Mutation Genetic Algorithm");
                Console.WriteLine("best distance: " + mutate.getAllTours()[0].getDistance());
                Console.WriteLine("Path: " + mutate.getAllTours()[0].ToString());
                Console.WriteLine("");
                Console.WriteLine("The results from Crossover Genetic Algorithm");
                Console.WriteLine("best distance: " + crossover.getAllTours()[0].getDistance());
                Console.WriteLine("Path: " + crossover.getAllTours()[0].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
    public class CityReader
    {
        private List<City> tour = new List<City>();
        int numCities = 0;
        int maxVal = 0;
        public List<City> readCities(string filename)
        {
            var stream = new StreamReader(filename);
            try
            {
                var line = "";
                numCities = Convert.ToInt32(stream.ReadLine());
                maxVal = Convert.ToInt32(stream.ReadLine());
                int i = 0;
                while ((line = stream.ReadLine()) != null)
                {
                    var temp = line.Split('\t');
                    List<int> d = new List<int>();
                    foreach (var entry in temp)
                    {
                        d.Add(Convert.ToInt32(entry));
                    }
                    if (d.Count > 0)
                    {
                        City c = new City(i, d);
                        tour.Add(c);
                        i++;
                    }
                }
            }
            catch { }
            finally { stream.Close(); }
            return tour;
        }
        public List<City> getTours()
        {
            return tour;
        }
    }
    public class City
    {
        //The City's index in the array of distances, valid numbers are 0,maxVal
        private int cityNumber = -1;
        //the list of distances between this city and any other city, equivalent to a row in the matrix
        private List<int> distances;

        public City(int i, List<int> d)
        {
            this.cityNumber = i;
            this.distances = new List<int>(d); //copy construct that shiz
        }
        public int getCityNumber()
        {
            return cityNumber;
        }
        public void setCityNumber(int cityNumber)
        {
            this.cityNumber = cityNumber;
        }
        public List<int> getDistances()
        {
            return distances;
        }
        public void setDistances(List<int> distances)
        {
            this.distances = distances;
        }
        public override string ToString()
        {
            return cityNumber.ToString();
        }
    }

}
