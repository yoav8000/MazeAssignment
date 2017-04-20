using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class DfsSearcher<T> : StackBasedSearcher<T>
    {

        public override Solution<T> Search(ISearchable<T> searchable)// Searcher's abstract method overriding
        {
            State<T> initialState = searchable.GetInitialState();
            MyStack.Push(initialState);
            HashSet<State<T>> discovered = new HashSet<State<T>>();
            while (MyStackSize > 0)
            {
                State<T> currentState = PopStack(); // inherited from Searcher, removes the best state

                if (currentState.Equals(searchable.GetGoalState()))
                {
                    return Backtrace(searchable.GetGoalState(), GetNumberOfNodesEvaluated()); // private method, back traces through the parents
                }
                if (!discovered.Contains(currentState))
                {
                    discovered.Add(currentState);
                    // calling the delegated method, returns a list of state 
                    List<State<T>> succerssors = searchable.GetAllPossibleStates(currentState);
                    foreach (State<T> currentSuccessor in succerssors)
                    {
                        if (!discovered.Contains(currentSuccessor))
                        {
                            currentSuccessor.CameFrom = currentState;
                            MyStack.Push(currentSuccessor);
                        }
                    }
                }
            }
            return null;
        }
    }
}