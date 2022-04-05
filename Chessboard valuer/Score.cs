using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessboard_valuer
{
    public class Score
    {
        public int score;
        public Move analysedMove;

        public Score()
        { 
        
        }
        public Score(int inScore,Move inAnalyzedMove)
        {
            score = inScore; 
            analysedMove = inAnalyzedMove;

        
        
        }

        public int getScore
        {
            get { return score; }
            set { score = value; }
        
        } 

        public Move getMove
        {
            get { return analysedMove; } 
            set { analysedMove = value; }
        
        }

        



        // inherets from move and holds int
    }
}
