using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class SearchAlgorithmFactory<T>
    {

        public ISearcher<T> GetSearchAlgorithm(string algorithm)
        {
            switch (algorithm.ToLower())
            {
                case "bfs":
                    {
                        return new BfsSearcher<T>(new CostComperator<T>());
                    }
                case "dfs":
                    {
                        return new DfsSearcher<T>();
                    }

                default:
                    {
                        throw new Exception("there is no such search algorithm in the factory!");
                    }
            }



        }






    }
}