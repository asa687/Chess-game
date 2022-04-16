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
    public class Chessboard 

    {
        
        public Dictionary<Point, Pawn> pawn;
        public Dictionary<Point, Rook> rook;
        public Dictionary<Point, Knight> knight;
        public Dictionary<Point, Bishop> bishop;
        public Dictionary<Point, King> king;
        public Dictionary<Point, Queen> queen;
        public Chessboard()
        {

        }

        public Chessboard(Dictionary<Point, Pawn> inPawns, Dictionary<Point, Rook> inRooks, Dictionary<Point, Knight> inKnights, Dictionary<Point, Bishop> inBishops, Dictionary<Point, King> inKings, Dictionary<Point, Queen> inQueens)
        {
            pawn = inPawns;
            rook = inRooks;
            knight = inKnights;
            bishop = inBishops;
            king = inKings;
            queen = inQueens;
        
        }

        public Dictionary<Point, Pawn> GetPawn
        {
            get { return pawn; }
            set {pawn = value; }
        
        }
        public Dictionary<Point, Rook> GetRook
        {
            get { return rook; }
            set { rook = value; }

        }
        public Dictionary<Point, Knight> GetKnight
        {
            get { return knight; }
            set {knight = value; }

        }
        public Dictionary<Point, Bishop> GetBishop
        {
            get { return bishop; }
            set {bishop = value; }

        }
        public Dictionary<Point, King> GetKing
        {
            get { return king; }
            set { king = value; }

        }

        public Dictionary<Point, Queen> GetQueen
        {
            get { return queen;  }
            set { queen = value; }
        }


        public Chessboard DeepCopy()
        {
            Chessboard other = (Chessboard)this.MemberwiseClone();
            other.pawn = new Dictionary<Point, Pawn>(pawn);
            other.rook = new Dictionary<Point, Rook>(rook);
            other.knight = new Dictionary<Point, Knight>(knight); 
            other.bishop = new Dictionary<Point, Bishop>(bishop);
            other.king = new Dictionary<Point, King>(king);
            other.queen = new Dictionary<Point, Queen>(queen);

            return other;
        
        }




    }

    
}
