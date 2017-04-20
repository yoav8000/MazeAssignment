using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academy.Collections;
using Academy.Collections.Generic;
namespace SearchAlgorithmsLib
{
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        private PriorityQueue<State<T>> openList;


        public PrioritySearcher(int capacity, IComparer<State<T>> comparer)
        {
            this.openList = new PriorityQueue<State<T>>(capacity, comparer);
        }
        public PriorityQueue<State<T>> OpenList
        {
            get
            {
                return this.openList;
            }

        }

        public int OpenListSize
        {
            get
            {
                return this.openList.Count;
            }
        }

        protected State<T> PopOpenList()
        {
            IncreaseNodesEvaluated();
            return openList.Dequeue();
        }

        protected void AdjustPriorityForState(State<T> currentSuccessor, State<T> currentState, double newCost)///check if works and fix if not.
        {

            State<T>[] array = EmptyQueueIntoArray();
            int size = array.Length;
            for (int i = size - 1; i >= 0; --i)
            {
                if (!array[i].Equals(currentSuccessor))
                {
                    openList.Enqueue(array[i]);
                    continue;

                }
                else
                {
                    if (array[i].Cost > newCost)
                    {
                        array[i].CameFrom = currentState;
                        array[i].Cost = newCost;
                    }
                    openList.Enqueue(array[i]);
                }

            }


        }

        private State<T>[] EmptyQueueIntoArray()
        {
            int size = OpenListSize;
            State<T>[] array = new State<T>[size];
            for (int i = 0; i < size; ++i)
            {
                array[i] = OpenList.Dequeue();
            }
            return array;
        }
    }
}