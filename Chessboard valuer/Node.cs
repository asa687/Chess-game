using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessboard_valuer
{
    internal class Node
    {
        public int head;
        public Chessboard board;
        public int tail;

        public Node(int inHeader, Chessboard inBoard, int inTail)
        {
            head = inHeader;
            board = inBoard;
            tail = inTail;
        
        }

        
    }
}
