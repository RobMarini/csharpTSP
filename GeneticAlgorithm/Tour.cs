using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm
{
    public class Tour: IComparable
    {
        private Random random = new Random();
        List<City> tour;
        public Tour(List<City> c)
        {
            tour = new List<City>(c);
        }
        public Double getFitness()
        {
            int totalDistance = 0;
            for (int i = 0; i < tour.Count; i++)
            {
                var c1 = tour[i];
                var c2 = tour[i + 1];
                totalDistance += c1.getDistances()[c2.getCityNumber()];
            }
            return 1 / (double)totalDistance;
        }
        public int getDistance()
        {
            int totalDistance = 0;
            for (int i = 0; i < tour.Count-1; i++)
            {
                var c1 = tour[i];
                var c2 = tour[i + 1];
                totalDistance += c1.getDistances()[c2.getCityNumber()];
            }
            return totalDistance;
        }
        public Tour Mutate()
        {
            int random1 = random.Next(tour.Count);
            int random2 = random.Next(tour.Count);
            if(random1 == random2){return this;}
            List<City> temp = new List<City>(tour);
            City c1 = temp[random1];
            City c2 = temp[random2];
            temp.Remove(c1);
            temp.Insert(random2, c1);
            temp.Remove(c2);
            temp.Insert(random1, c2);
            return new Tour(temp);
        }
        public Tour Mate(Tour t)
        {
            var temp = t.tour;
            var random = new Random();
            int random1 = random.Next(tour.Count);
            var child = new List<City>(tour.Take(random1));
            for(int i=0;i<child.Count;i++)
            {
                temp.Remove(child[i]);
            }
            foreach (var city in temp)
            {
                if (city != null)
                {
                    child.Add(city);
                }
            }
            return new Tour(child);
        }
        public List<Tour> seed(int i)
        {
            List<Tour> tours = new List<Tour>(i);
            for (int j = 0; j < i; j++)
            {
                tours.Add(new Tour(this.Mutate().tour));
            }
            return tours;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var city in this.tour)
            {
                sb.Append(city.ToString());
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1,1);
            sb.Append("]");
            return sb.ToString();
        }
        public int CompareTo(object obj)
        {
            if (obj == null) { return 1; }
            Tour other = obj as Tour;
            if (other.getDistance() != null)
            {
                return this.getDistance().CompareTo(other.getDistance());
            }
            else
            {
                throw new ArgumentException("Object is not a Tour");
            }
        }
    }

}
