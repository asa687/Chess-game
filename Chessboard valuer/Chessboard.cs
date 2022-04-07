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
        private Chessboard board = new Chessboard();
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

        public int EvaluateBoard()
        {
            
            int value = 0;
            foreach (KeyValuePair<Point, Pawn> pawns in pawn)
            {

                if (pawns.Value.CurrentlyDrawn)
                {
                    value += pawns.Value.GetValue;
                    foreach (KeyValuePair<Point, King> kings in king)
                    {
                        if (pawns.Value.ValidPawnMove(new Move(pawns.Key, kings.Key), new Chessboard(pawn, rook, knight, bishop, king, queen)) && kings.Value.GetColor != pawns.Value.GetColor)
                        {
                            value += pawns.Value.GetValue * kings.Value.GetValue;
                        
                        }


                    }
                    
                }





            }

            foreach (KeyValuePair<Point, Rook> rooks in rook)
            {

                if (rooks.Value.CurrentlyDrawn)
                {
                    value += rooks.Value.GetValue;
                    foreach (KeyValuePair<Point, King> kings in king)
                    {
                        if (rooks.Value.ValidRookMove(new Move(rooks.Key, kings.Key), new Chessboard(pawn, rook, knight, bishop, king, queen)) && kings.Value.GetColor != rooks.Value.GetColor)
                        {
                            value += rooks.Value.GetValue * kings.Value.GetValue;

                        }


                    }

                }



            }

            foreach (KeyValuePair<Point, Knight> knights in knight)
            {
                if (knights.Value.CurrentlyDrawn)
                {
                    value += knights.Value.GetValue;
                    foreach (KeyValuePair<Point, King> kings in king)
                    {
                        if (knights.Value.ValidKnightMove(new Move(knights.Key, kings.Key), new Chessboard(pawn, rook, knight, bishop, king, queen)) && kings.Value.GetColor != knights.Value.GetColor)
                        {
                            value += knights.Value.GetValue * kings.Value.GetValue;

                        }


                    }
                }

            }

            foreach (KeyValuePair<Point, Bishop> bishops in bishop)
            {
                if (bishops.Value.CurrentlyDrawn)
                {
                    value += bishops.Value.GetValue;
                    foreach (KeyValuePair<Point, King> kings in king)
                    {
                        if (bishops.Value.ValidBishopMove(new Move(bishops.Key, kings.Key), new Chessboard(pawn, rook, knight, bishop, king, queen)) && kings.Value.GetColor != bishops.Value.GetColor)
                        {
                            value += bishops.Value.GetValue * kings.Value.GetValue;

                        }


                    }
                }

            }

            foreach (KeyValuePair<Point, King> kings in king)
            {
                if (kings.Value.CurrentlyDrawn)
                {
                    value += kings.Value.GetValue;
                    foreach (KeyValuePair<Point, King> kings2 in king)
                    {
                        if (kings.Value.ValidKingMove(new Move(kings.Key, kings2.Key), new Chessboard(pawn, rook, knight, bishop, king, queen)) && kings.Value.GetColor != kings.Value.GetColor)
                        {
                            value += kings.Value.GetValue * kings.Value.GetValue;

                        }


                    }
                }

            }

            foreach (KeyValuePair<Point, Queen> queens in queen)
            {
                if (queens.Value.CurrentlyDrawn)
                {
                    value += queens.Value.GetValue;
                    foreach (KeyValuePair<Point, King> kings in king)
                    {
                        if (queens.Value.ValidQueenMove(new Move(queens.Key, kings.Key), new Chessboard(pawn, rook, knight, bishop, king, queen)) && kings.Value.GetColor != queens.Value.GetColor)
                        {
                            value += queens.Value.GetValue * kings.Value.GetValue;

                        }


                    }
                }

            }
            return value;

        }


    }

    
}
