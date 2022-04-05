using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chessboard_valuer
{
    public class Move
    {
        public Point startPoint;
        public Point endPoint;

   
        public Move()
        {

        }

        public Move(Point inStartPoint, Point inEndPoint)
        {
            startPoint = inStartPoint;
            endPoint = inEndPoint; 

     
        }

        public Point GetStartPoint
        {
            get { return startPoint; } 
            set { startPoint = value; }
        
        }  

        public Point GetEndPoint
        {
            get { return endPoint; } 
            set { endPoint = value; }
        
        } 






        

    }
}
