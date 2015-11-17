using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class GeneticAlgorithm
    {
        private static readonly double mutationRate = 0.015;
        private static readonly int tournamentSize = 5;
        private List<Tour> allTours = new List<Tour>();
        public List<Tour> getAllTours()
        {
            return allTours;
        }

        public void setAllTours(List<Tour> allTours)
        {
            this.allTours = allTours;
        }

        public static double getMutationrate()
        {
            return mutationRate;
        }

        public static int getTournamentsize()
        {
            return tournamentSize;
        }
        public List<Tour> mutateGeneration()
        {
            List<Tour> newTours = new List<Tour>(allTours);
            for (int i = 0; i < newTours.Count; i++)
            {
                newTours[i] = newTours[i].Mutate();
            }
            return newTours;
        }
        public List<Tour> crossoverGeneration()
        {
            List<Tour> newTours = new List<Tour>();
            for (int i = 0; i < allTours.Count - 1; i++)
            {
                newTours.Add(allTours[i].Mate(allTours[i + 1]));
            }
            return newTours;
        }
        public List<Tour> GenerateGeneration(bool mutate)
        {
            var newGeneration = new List<Tour>(allTours.Count * 2 - 1);
            if (mutate)
            {
                newGeneration.AddRange(new List<Tour>(allTours));
                newGeneration.AddRange(new List<Tour>(this.mutateGeneration()));
                newGeneration.Sort();
            }
            else
            {
                newGeneration.AddRange(new List<Tour>(allTours));
                newGeneration.AddRange(new List<Tour>(this.crossoverGeneration()));
                newGeneration.Sort();
            }
            return new List<Tour>(newGeneration.Take(allTours.Count));
        }
    }
}
