using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace SearchAlgorithmsLib
{
    public class SolutionAdapter
    {
        private Solution<Position> solution;
        private string name;



        public SolutionAdapter(Solution<Position> solution1, string name1 = null)
        {
            this.solution = solution1;
            this.name = name1;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public Solution<Position> Solution
        {
            get
            {
                return this.solution;
            }
        }



        public string ToJson()
        {

            JObject jobject = new JObject();
            jobject["Name"] = name;
            StringBuilder sb = new StringBuilder();
            int size = solution.PathToDestination.Count;
            for (int i = size - 1; i > 0; --i)
            {
                State<Position> currentState = solution.PathToDestination[i];
                int currentStateRow = currentState.StateIdentifier.Row;
                int currentStateCol = currentState.StateIdentifier.Col;
                State<Position> neighborState = solution.PathToDestination[i - 1];
                int neighborStateRow = neighborState.StateIdentifier.Row;
                int neighborStateCol = neighborState.StateIdentifier.Col;
                if (neighborStateCol < currentStateCol)//moved left
                {
                    sb.Append((int)Direction.Left);
                }
                else if (neighborStateCol > currentStateCol)
                {
                    sb.Append((int)Direction.Right);
                }
                else if (neighborStateRow < currentStateRow)
                {
                    sb.Append((int)Direction.Up);//check if it should be the other way arount up<->down
                }
                else
                {
                    sb.Append((int)Direction.Down);
                }
            }

            jobject["Path"] = sb.ToString();
            jobject["NodesEvaluated"] = Solution.NodesEvaluated;

            return jobject.ToString();


        }

    }
}