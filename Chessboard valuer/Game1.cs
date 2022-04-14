using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using static Chessboard_valuer.ChessPiece;

namespace Chessboard_valuer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary> 
    ///  

    // todo remove mouse related rules


    public enum Turn
    {
        PLAYER, 
        AI

    
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D squareTexture, BishopTexture, KingTexture, KnightTexture, PawnTexture, QueenTexture, RookTexture;
        Square[,] squareArray; 

        int depth = 40;
        int value = 0;

        bool isCalled = false;
        bool turnComplete = false; 

        Chessboard AiBoard = new Chessboard();
        

        readonly List<Chessboard> playedBoards = new List<Chessboard>();
        readonly List<Chessboard> returnedBoards = new List<Chessboard>();

        Move bestMove = new Move();
        Move m = new Move();

        // 
        public List<Move> moveList = new List<Move>();
        public List<Point> pointList = new List<Point>();
        public Chessboard currentBoard = new Chessboard();
        public Chessboard chessboardLocal = new Chessboard();
        public Chessboard analysedBoard = new Chessboard();
        public List<Chessboard> localAiBoard = new List<Chessboard>();
        public Turn turn;
        Chessboard playerBoard = new Chessboard();
        bool isPressed = false;
        bool hasPromoted = false;

        int currentVal = 0;
        int bestVal = -1;
        int bestCurrentVal = -1;

        List<Move> AiMove = new List<Move>();
        List<Move> moves = new List<Move>();

        Chessboard initialBoard = new Chessboard();


        Color clickerColor = Color.MediumBlue;
        Color currentColor = Color.TransparentBlack;


        




        bool isStarted = false;

        int indexI = 0;
        int indexJ = 0;
        // create a value table

        Vector2 piecePosition = new Vector2();
        Rectangle pieceBounds = new Rectangle();







        public Dictionary<Point, King>.ValueCollection Kingposition { get; private set; }

        private List<Point> PossiblePointList()
        {
            List<Point> points = new List<Point>(); 
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Point p = new Point(x, y);  
                    points.Add(p);
                
                }
            
            }

            return points;
        
        }

        private void LoadWhiteQueen(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Queen> queens)
        {
            if (initialising)
            {
                x = 3;
                y = 7;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            queens.Add(new Point(x, y), new Queen(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 8, true));




        }

        private void LoadBlackQueen(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Queen> queens)
        {
            if (initialising)
            {
                x = 3;
                y = 0;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            queens.Add(new Point(x, y), new Queen(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -8, true));




        }

        private void LoadWhiteBishop1(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Bishop> bishops)
        {
            if (initialising)
            {
                x = 2;
                y = 7;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            bishops.Add(new Point(x, y), new Bishop(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 4, true));



        }

        private void LoadBlackBishop1(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Bishop> bishops)
        {
            if (initialising)
            {
                x = 2;
                y = 0;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            bishops.Add(new Point(x, y), new Bishop(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -4, true));



        }

        private void LoadWhiteBishop2(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Bishop> bishops)
        {
            if (initialising)
            {
                x = 5;
                y = 7;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            bishops.Add(new Point(x, y), new Bishop(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 4, true));



        }

        private void LoadBlackBishop2(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Bishop> bishops)
        {
            if (initialising)
            {
                x = 5;
                y = 0;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            bishops.Add(new Point(x, y), new Bishop(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -4, true));



        }

        private void LoadWhiteKing(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, King> kings)
        {
            if (initialising)
            {
                x = 4;
                y = 7;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            kings.Add(new Point(x, y), new King(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 1000, true));



        }
        

        private void LoadBlackKing(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, King> kings)
        {
            if (initialising)
            {
                x = 4;
                y = 0;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            kings.Add(new Point(x, y), new King(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -1000, true));


        }

        private void LoadWhiteKnight1(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Knight> knights)
        {
            if (initialising)
            {
                x = 1;
                y = 7;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            knights.Add(new Point(x, y), new Knight(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 3, true));


        }

        private void LoadBlackKnight1(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Knight> knights)
        {
            if (initialising)
            {
                x = 1;
                y = 0;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            knights.Add(new Point(x, y), new Knight(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -3, true));


        }

        private void LoadWhiteKnight2(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Knight> knights)
        {
            if (initialising)
            {
                x = 6;
                y = 7;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            knights.Add(new Point(x, y), new Knight(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 3, true));


        }

        private void LoadBlackKnight2(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Knight> knights)
        {
            if (initialising)
            {
                x = 6;
                y = 0;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            knights.Add(new Point(x, y), new Knight(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -3, true));


        }

        private void LoadWhiteRook1(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Rook> rooks)
        {
            if (initialising)
            {
                x = 0;
                y = 7;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            rooks.Add(new Point(x, y), new Rook(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 4, true));


        }

        private void LoadBlackRook1(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Rook> rooks)
        {
            if (initialising)
            {
                x = 0;
                y = 0;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            rooks.Add(new Point(x, y), new Rook(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -4, true));


        }

        private void LoadWhiteRook2(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Rook> rooks)
        {
            if (initialising)
            {
                x = 7;
                y = 7;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            rooks.Add(new Point(x, y), new Rook(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 4, true));


        }

        private void LoadBlackRook2(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Rook> rooks)
        {
            if (initialising)
            {
                x = 7;
                y = 0;

            }
            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            rooks.Add(new Point(x, y), new Rook(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -4, true));


        }

        private void LoadWhitePawn1(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 0;
                y = 6;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 1, true));


        }

        private void LoadWhitePawn2(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 1;
                y = 6;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 1, true));


        }

        private void LoadWhitePawn3(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 2;
                y = 6;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 1, true));


        }

        private void LoadWhitePawn4(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 3;
                y = 6;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 1, true));


        }

        private void LoadWhitePawn5(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 4;
                y = 6;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 1, true));


        }

        private void LoadWhitePawn6(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 5;
                y = 6;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 1, true));


        }

        private void LoadWhitePawn7(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 6;
                y = 6;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 1, true));


        }
        private void LoadWhitePawn8(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 7;
                y = 6;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.White, pieceBounds, 1, 1, true));


        }

        private void LoadBlackPawn1(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 0;
                y = 1;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -1, true));


        }

        private void LoadBlackPawn2(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 1;
                y = 1;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -1, true));


        }

        private void LoadBlackPawn3(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 2;
                y = 1;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -1, true));


        }

        private void LoadBlackPawn4(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 3;
                y = 1;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -1, true));


        }

        private void LoadBlackPawn5(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 4;
                y = 1;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -1, true));


        }

        private void LoadBlackPawn6(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 5;
                y = 1;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -1, true));


        }

        private void LoadBlackPawn7(Texture2D pieceTexture, int x, int y, bool initialising,  Dictionary<Point, Pawn> pawns)
        {
            if (initialising)
            {
                x = 6;
                y = 1;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -1, true));


        }
        private void LoadBlackPawn8(Texture2D pieceTexture, int x, int y, bool initialising, Dictionary<Point, Pawn> pawns)
        {

            if (initialising)
            {
                x = 7;
                y = 1;

            }

            Vector2 piecePosition = new Vector2(x * (float)pieceTexture.Width, y * (float)pieceTexture.Height);
            Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, pieceTexture.Width, pieceTexture.Height);
            pawns.Add(new Point(x, y), new Pawn(pieceTexture, piecePosition, Color.Black, pieceBounds, 1, -1, true));


        }




        private void InitialiseSquares(int inColumns, int inRows)
        {
            squareArray = new Square[inRows, inColumns];
            Vector2 squarePosition;
            Rectangle squareBounds;
            Color[] colorArray = new Color[2] { Color.TransparentBlack, Color.White };
            float scale = 1;
            int iteration = 0;
            for (int row = 0; row <= squareArray.GetUpperBound(0); row++)
            {
                for (int column = 0; column <= squareArray.GetUpperBound(1); column++)
                {
                    Color squareColor = colorArray[iteration];/*colour array needs to alternate between 1 and 0 */
                    squarePosition = new Vector2(column * (float)squareTexture.Width, row * (float)squareTexture.Height);
                    squareBounds = new Rectangle((int)squarePosition.X, (int)squarePosition.Y, squareTexture.Width, squareTexture.Height);
                    squareArray[row, column] = new Square(squareTexture, squarePosition, squareColor, squareBounds, scale);
                    iteration = Switch(iteration);


                }
                iteration = Switch(iteration);
            }
        }




        protected int Minimax(Chessboard board, int depth, Turn turns, int alpha, int beta, List<Move> playedMoves)
        {
                     
            
            
            if (depth == 0)
            {
                return board.EvaluateBoard();
            
            
            }


            List<Move> moves = AllMoves(turns, board);

            // add all the moves to the list 
            if (turns == Turn.PLAYER)
            {
                int value = int.MinValue;
                foreach(Move move in moves)
                {
                    playedMoves.Add(move);
                    Chessboard chessboardLocal = UpToDateBoard(playedMoves);
                    if (chessboardLocal != null)
                    {
                        int minimaxResult = Minimax(chessboardLocal, depth - 1, Turn.AI, alpha, beta, playedMoves) ;
                        value = Math.Max(value, minimaxResult);
                        alpha = Math.Max(alpha, value);
                        if (beta <= alpha)
                        {
                            break;

                        } 
                        
                            

                    }

                        

                    




                }
                return value;

            }
            else
            {
                int value = int.MinValue;               
                foreach (Move move in moves)
                {
                    playedMoves.Add(move);
                    Chessboard chessboardLocal = UpToDateBoard(playedMoves);

                    if (chessboardLocal != null)
                    {
                        int minimaxResult = Minimax(chessboardLocal, depth - 1, Turn.PLAYER, alpha, beta, playedMoves) ;
                        value = Math.Min(value, minimaxResult);
                        beta = Math.Min(alpha, value);
                        if (beta <= alpha)
                        {
                            break;

                        }
                            

                    }


                    





                }
                return value;

            }
            
            



        }

        
        private int BestBoard(Chessboard boards, Turn turns,int depth)
        {

            
           
            
            
                
            List<Move> playedMoves = moveList;
            currentVal = Minimax(boards, depth, Turn.PLAYER, int.MinValue, int.MaxValue, playedMoves);

                
            
            
            return currentVal;

        }


        private Chessboard UpToDateBoard(List<Move> moveList)
        {
            
            Chessboard firstBoard = DrawPieces();

            Color color = Color.White;
            foreach (Move m in moveList)
            {
                //the if statement checks for a promotion, indicated with an end X of 99, and allows up to date board to mimic promotions
                if (m.GetEndPoint.X == 99)
                {
                    int endX = m.GetStartPoint.X;
                    if (m.endPoint.Y == 10)
                    {

                        firstBoard.pawn.Remove(new Point(endX, 0));
                        piecePosition = new Vector2(endX * (float)RookTexture.Width, 0 * (float)RookTexture.Height);
                        pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, RookTexture.Width, RookTexture.Height);
                        firstBoard.rook.Add(new Point(endX, 0), new Rook(RookTexture, piecePosition, Color.White, pieceBounds, 1, -4, true));




                    }
                    else if (m.endPoint.Y == 11)
                    {

                        firstBoard.pawn.Remove(new Point(endX, 0));
                        piecePosition = new Vector2(endX * (float)KnightTexture.Width, 0 * (float)KnightTexture.Height);
                        pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, KnightTexture.Width, KnightTexture.Height);
                        firstBoard.knight.Add(new Point(endX, 0), new Knight(KnightTexture, piecePosition, Color.White, pieceBounds, 1, -3, true));




                    }
                    else if (m.endPoint.Y == 12)
                    {

                        firstBoard.pawn.Remove(new Point(endX, 0));
                        piecePosition = new Vector2(endX * (float)BishopTexture.Width, 0 * (float)BishopTexture.Height);
                        pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, BishopTexture.Width, BishopTexture.Height);
                        firstBoard.bishop.Add(new Point(endX, 0), new Bishop(BishopTexture, piecePosition, Color.White, pieceBounds, 1, -4, true));




                    }

                    else if (m.endPoint.Y == 13)
                    {

                        firstBoard.pawn.Remove(new Point(endX, 0));
                        Vector2 piecePosition = new Vector2(endX * (float)QueenTexture.Width, 0 * (float)QueenTexture.Height);
                        Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, QueenTexture.Width, QueenTexture.Height);
                        firstBoard.queen.Add(new Point(endX, 0), new Queen(QueenTexture, piecePosition, Color.White, pieceBounds, 1, -8, true));





                    }


                }
                else if (firstBoard != null)
                {
                    firstBoard = (MovePiecesReturner(m, color, firstBoard));
                    if (color == Color.White)
                    {
                        color = Color.Black;

                    }
                    else
                    {
                        color = Color.White;

                    }


                }
                else
                {
                    break;
                
                }

            }

            return firstBoard; 

        }


        public int Switch(int colour)
        {
            switch (colour)
            {
                case 0:
                    return 1;
                case 1:
                    return 0;
                default:
                    return 0;
            }
        }

        //a new draw class can be made allowing a move to be generated 
        //using dictionaries to store the initialised position as key an
        private Chessboard DrawPieces()
        {
            Dictionary<Point, Pawn> pawns = new Dictionary<Point, Pawn>();
            Dictionary<Point, Rook> rooks = new Dictionary<Point, Rook>();
            Dictionary<Point, Knight> knights = new Dictionary<Point, Knight>();
            Dictionary<Point, Bishop> bishops = new Dictionary<Point, Bishop>();
            Dictionary<Point, King> kings = new Dictionary<Point, King>();
            Dictionary<Point, Queen> queens = new Dictionary<Point, Queen>();


            LoadBlackBishop1(BishopTexture, 0, 0, true,  bishops);
            LoadBlackBishop2(BishopTexture, 0, 0, true,  bishops);
            LoadBlackKing(KingTexture, 0, 0, true,  kings);
            LoadBlackKnight1(KnightTexture, 0, 0, true,  knights);
            LoadBlackKnight2(KnightTexture, 0, 0, true,  knights);
            LoadBlackPawn1(PawnTexture, 0, 0, true,  pawns);
            LoadBlackPawn2(PawnTexture, 0, 0, true,  pawns);
            LoadBlackPawn3(PawnTexture, 0, 0, true,  pawns);
            LoadBlackPawn4(PawnTexture, 0, 0, true,  pawns);
            LoadBlackPawn5(PawnTexture, 0, 0, true,  pawns);
            LoadBlackPawn6(PawnTexture, 0, 0, true,  pawns);
            LoadBlackPawn7(PawnTexture, 0, 0, true,  pawns);
            LoadBlackPawn8(PawnTexture, 0, 0, true,  pawns);
            LoadBlackQueen(QueenTexture, 0, 0, true,  queens);
            LoadBlackRook1(RookTexture, 0, 0, true,  rooks);
            LoadBlackRook2(RookTexture, 0, 0, true,  rooks);

            LoadWhiteBishop1(BishopTexture, 0, 0, true,  bishops);
            LoadWhiteBishop2(BishopTexture, 0, 0, true,  bishops);
            LoadWhiteKing(KingTexture, 0, 0, true,  kings);
            LoadWhiteKnight1(KnightTexture, 0, 0, true,  knights);
            LoadWhiteKnight2(KnightTexture, 0, 0, true,  knights);
            LoadWhitePawn1(PawnTexture, 0, 0,  true, pawns);
            LoadWhitePawn2(PawnTexture, 0, 0, true,  pawns);
            LoadWhitePawn3(PawnTexture, 0, 0, true,  pawns);
            LoadWhitePawn4(PawnTexture, 0, 0, true,  pawns);
            LoadWhitePawn5(PawnTexture, 0, 0, true,  pawns);
            LoadWhitePawn6(PawnTexture, 0, 0, true,  pawns);
            LoadWhitePawn7(PawnTexture, 0, 0, true,  pawns);
            LoadWhitePawn8(PawnTexture, 0, 0, true,  pawns);
            LoadWhiteQueen(QueenTexture, 0, 0, true,  queens);
            LoadWhiteRook1(RookTexture, 0, 0, true,  rooks);
            LoadWhiteRook2(RookTexture, 0, 0, true,  rooks);

            return (new Chessboard(pawns, rooks, knights, bishops, kings, queens));

        }










        private Chessboard PieceRemove(Move move, Dictionary<Point, Pawn> pawnsL, Dictionary<Point, Rook> rookL, Dictionary<Point, Knight> knightsL, Dictionary<Point, Bishop> bishopsL, Dictionary<Point, King> kingsL, Dictionary<Point, Queen> queensL)
        {
            //checks the arrays for a point at endpoint, if oposite colour it is removed
            Point endOfMove = move.endPoint;
            if (pawnsL.ContainsKey(endOfMove))
            {
                pawnsL.Remove(endOfMove);

            }
            else if (rookL.ContainsKey(endOfMove))
            {
                rookL.Remove(endOfMove);
            }
            else if (knightsL.ContainsKey(endOfMove))
            {
                knightsL.Remove(endOfMove);
            }
            else if (bishopsL.ContainsKey(endOfMove))
            {
                bishopsL.Remove(endOfMove);

            }
            else if (kingsL.ContainsKey(endOfMove))
            {
                kingsL.Remove(endOfMove);
            }
            else if (queensL.ContainsKey(endOfMove))
            {
                queensL.Remove(endOfMove);
            }

            Chessboard chessboard = new Chessboard(pawnsL, rookL, knightsL, bishopsL, kingsL, queensL);
            return chessboard;
        }


















        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            IsMouseVisible = true;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here 
            // content missing, need to add pieces
            squareTexture = Content.Load<Texture2D>("Square");

            BishopTexture = Content.Load<Texture2D>("bishop");
            KingTexture = Content.Load<Texture2D>("king");
            KnightTexture = Content.Load<Texture2D>("knight");
            PawnTexture = Content.Load<Texture2D>("pawn");
            QueenTexture = Content.Load<Texture2D>("queen");
            RookTexture = Content.Load<Texture2D>("rook");







        }





        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }





        private Chessboard MovePiecesReturner(Move m, Color color, Chessboard chessboardLocal)
        {
            bool validEndpoint = true;
            

            if (chessboardLocal.pawn.ContainsKey(m.GetEndPoint))
            {
                if (chessboardLocal.pawn[m.GetEndPoint].GetColor == color)
                {
                    validEndpoint = false;
                }

            }
            else if (chessboardLocal.rook.ContainsKey(m.GetEndPoint))
            {
                if (chessboardLocal.rook[m.GetEndPoint].GetColor == color)
                {
                    validEndpoint = false;
                }
            }
            else if (chessboardLocal.knight.ContainsKey(m.GetEndPoint))
            {
                if (chessboardLocal.knight[m.GetEndPoint].GetColor == color)
                {
                    validEndpoint = false;
                }
            }
            else if (chessboardLocal.bishop.ContainsKey(m.GetEndPoint))
            {
                if (chessboardLocal.bishop[m.GetEndPoint].GetColor == color)
                {
                    validEndpoint = false;
                }

            }
            else if (chessboardLocal.king.ContainsKey(m.GetEndPoint))
            {
                if (chessboardLocal.king[m.GetEndPoint].GetColor == color)
                {
                    validEndpoint = false;
                }
            }
            else if (chessboardLocal.queen.ContainsKey(m.GetEndPoint))
            {
                if (chessboardLocal.queen[m.GetEndPoint].GetColor == color)
                {
                    validEndpoint = false;
                }
            }

            bool valid;
            if (validEndpoint == true)
            {
                if (chessboardLocal.pawn.ContainsKey(m.GetStartPoint) && chessboardLocal.pawn[m.GetStartPoint].GetColor == color)
                {
                    valid = chessboardLocal.pawn[m.GetStartPoint].ValidPawnMove(m, chessboardLocal);

                    if (valid == true)
                    {
                        
                        chessboardLocal = PieceRemove(m, chessboardLocal.pawn, chessboardLocal.rook, chessboardLocal.knight, chessboardLocal.bishop, chessboardLocal.king, chessboardLocal.queen);
                        chessboardLocal.pawn[m.GetStartPoint].GetVector = new Vector2(m.GetEndPoint.X * 100, m.GetEndPoint.Y * 100);
                        chessboardLocal.pawn.Add(m.GetEndPoint, chessboardLocal.pawn[m.GetStartPoint]);
                        chessboardLocal.pawn.Remove(m.GetStartPoint);
                        return chessboardLocal;
                    }


                }
                else if (chessboardLocal.rook.ContainsKey(m.GetStartPoint) && chessboardLocal.rook[m.GetStartPoint].GetColor == color)
                {
                    valid = chessboardLocal.rook[m.GetStartPoint].ValidRookMove(m, chessboardLocal);
                    if (valid == true)
                    {
                        chessboardLocal = PieceRemove(m, chessboardLocal.pawn, chessboardLocal.rook, chessboardLocal.knight, chessboardLocal.bishop, chessboardLocal.king, chessboardLocal.queen);
                        chessboardLocal.rook[m.GetStartPoint].GetVector = new Vector2(m.GetEndPoint.X * 100, m.GetEndPoint.Y * 100);
                        chessboardLocal.rook.Add(m.GetEndPoint, chessboardLocal.rook[m.GetStartPoint]);
                        chessboardLocal.rook.Remove(m.GetStartPoint);
                        return chessboardLocal;
                    }
                }
                else if (chessboardLocal.knight.ContainsKey(m.GetStartPoint) && chessboardLocal.knight[m.GetStartPoint].GetColor == color)
                {
                    valid = chessboardLocal.knight[m.GetStartPoint].ValidKnightMove(m, chessboardLocal);
                    if (valid == true)
                    {
                        chessboardLocal = PieceRemove(m, chessboardLocal.pawn, chessboardLocal.rook, chessboardLocal.knight, chessboardLocal.bishop, chessboardLocal.king, chessboardLocal.queen);
                        chessboardLocal.knight[m.GetStartPoint].GetVector = new Vector2(m.GetEndPoint.X * 100, m.GetEndPoint.Y * 100);
                        chessboardLocal.knight.Add(m.GetEndPoint, chessboardLocal.knight[m.GetStartPoint]);
                        chessboardLocal.knight.Remove(m.GetStartPoint);
                        return chessboardLocal;
                    }

                }
                else if (chessboardLocal.bishop.ContainsKey(m.GetStartPoint) && chessboardLocal.bishop[m.GetStartPoint].GetColor == color)
                {
                    valid = chessboardLocal.bishop[m.GetStartPoint].ValidBishopMove(m, chessboardLocal);
                    if (valid == true)
                    {
                        chessboardLocal = PieceRemove(m, chessboardLocal.pawn, chessboardLocal.rook, chessboardLocal.knight, chessboardLocal.bishop, chessboardLocal.king, chessboardLocal.queen);
                        chessboardLocal.bishop[m.GetStartPoint].GetVector = new Vector2(m.GetEndPoint.X * 100, m.GetEndPoint.Y * 100);
                        chessboardLocal.bishop.Add(m.GetEndPoint, chessboardLocal.bishop[m.GetStartPoint]);
                        chessboardLocal.bishop.Remove(m.GetStartPoint);
                        return chessboardLocal;
                    }

                }
                else if (chessboardLocal.king.ContainsKey(m.GetStartPoint) && chessboardLocal.king[m.GetStartPoint].GetColor == color)
                {
                    valid = chessboardLocal.king[m.GetStartPoint].ValidKingMove(m, chessboardLocal);
                    if (valid == true)
                    {
                        chessboardLocal = PieceRemove(m, chessboardLocal.pawn, chessboardLocal.rook, chessboardLocal.knight, chessboardLocal.bishop, chessboardLocal.king, chessboardLocal.queen);
                        chessboardLocal.king[m.GetStartPoint].GetVector = new Vector2(m.GetEndPoint.X * 100, m.GetEndPoint.Y * 100);
                        chessboardLocal.king.Add(m.GetEndPoint, chessboardLocal.king[m.GetStartPoint]);
                        chessboardLocal.king.Remove(m.GetStartPoint);
                        return chessboardLocal;
                    }

                }
                else if (chessboardLocal.queen.ContainsKey(m.GetStartPoint) && chessboardLocal.queen[m.GetStartPoint].GetColor == color)
                {
                    valid = chessboardLocal.queen[m.GetStartPoint].ValidQueenMove(m, chessboardLocal);
                    if (valid == true)
                    {
                        chessboardLocal = PieceRemove(m, chessboardLocal.pawn, chessboardLocal.rook, chessboardLocal.knight, chessboardLocal.bishop, chessboardLocal.king, chessboardLocal.queen);
                        chessboardLocal.queen[m.GetStartPoint].GetVector = new Vector2(m.GetEndPoint.X * 100, m.GetEndPoint.Y * 100);
                        chessboardLocal.queen.Add(m.GetEndPoint, chessboardLocal.queen[m.GetStartPoint]);
                        chessboardLocal.queen.Remove(m.GetStartPoint);
                        return chessboardLocal;
                    }

                }


            }
            return null;




        }


        
        private List<Move>  AllMoves(Turn turn, Chessboard chessboard)
        {
            //generates a list of all possiblre boards by creating a list of every possible change in position on the chessboard and mapping them to chesspieces


            Chessboard chessboard1 = new Chessboard();
            returnedBoards.Clear();
            for (int i = 0; i < 999; i++)
            {
                returnedBoards.Add(chessboard);


            }
            
            List<Move> returnedMoves = new List<Move>();
            
            Color c;
            if (turn == Turn.PLAYER)
            {
                c = Color.White;

            }
            else
            {
                c = Color.Black;
            
            }


            Chessboard originalBoard = new Chessboard();
            List<Point> points = PossiblePointList(); 
            List<Move> moves = new List<Move>();

            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    if (points[i] != points[j])
                    {

                        moves.Add(new Move(points[i], points[j]));

                    }
                    
                
                
                }
            
            }

            for(int index = 0; index <= moves.Count - 1; index++)
            {
                if (chessboard.pawn.ContainsKey(moves[index].startPoint))
                {
                    if (chessboard.pawn[moves[index].startPoint].ValidPawnMove(moves[index], chessboard) )
                    {
                        returnedMoves.Add(moves[index]);


                    }
                
                }
                else if (chessboard.rook.ContainsKey(moves[index].startPoint))
                {
                    if (chessboard.rook[moves[index].startPoint].ValidRookMove(moves[index], chessboard) )
                    {
                        returnedMoves.Add(moves[index]);


                    }

                }
                else if (chessboard.knight.ContainsKey(moves[index].startPoint))
                {
                    if (chessboard.knight[moves[index].startPoint].ValidKnightMove(moves[index], chessboard) )
                    {
                        returnedMoves.Add(moves[index]);


                    }

                }
                else if (chessboard.bishop.ContainsKey(moves[index].startPoint))
                {
                    if (chessboard.bishop[moves[index].startPoint].ValidBishopMove(moves[index], chessboard))
                    {
                        returnedMoves.Add(moves[index]);


                    }

                }
                else if (chessboard.king.ContainsKey(moves[index].startPoint))
                {
                    if (chessboard.king[moves[index].startPoint].ValidKingMove(moves[index], chessboard))
                    {
                        returnedMoves.Add(moves[index]);


                    }

                }
                else if (chessboard.queen.ContainsKey(moves[index].startPoint))
                {
                    if (chessboard.queen[moves[index].startPoint].ValidQueenMove(moves[index], chessboard))
                    {
                        returnedMoves.Add(moves[index]);


                    }

                }









            }
            foreach (Move move in returnedMoves)
            {
                if (move.startPoint == move.endPoint)
                { 
                    returnedMoves.Remove(move);
                                
                }
            
            }

            return returnedMoves;
        }


        static int IndexBoundCheck(int indexI)
        {
            if (indexI > 7)
            {
                indexI = 0;

            }
            else if (indexI < 0)
            {
                indexI = 7;

            }
            else
            {
                return indexI;

            }
            return indexI;



        }




        static Color ColourAlternator(Color color)
        {
            if (color == Color.TransparentBlack)
            {
                return Color.White;
            
            } 
            return Color.TransparentBlack;
        
        } 

        





        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            //single use of an initialisation to draw the bord and initialise its pieces
            if (isStarted == false)
            {
                
                InitialiseSquares(8, 8);
                depth = 4;
                initialBoard = DrawPieces();
                
                
                squareArray[indexI, indexJ].spriteColor = clickerColor;
                playedBoards.Add(initialBoard);
                localAiBoard.Add(initialBoard);
                isStarted = true;

            }


            if (turn == Turn.PLAYER && isCalled == false)
            {
                Color color = Color.White;
                turnComplete = false;

                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                {
                    depth = 10;
                    isPressed = true;

                }

                if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    depth = 20;
                    isPressed = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D3))
                {
                    depth = 30;
                    isPressed = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D4))
                {
                    depth = 40;
                    isPressed = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D5))
                {
                    depth = 50;
                    isPressed = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D6))
                {
                    depth = 60;
                    isPressed = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D7))
                {
                    depth = 70;
                    isPressed = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D7))
                {
                    depth = 80;
                    isPressed = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D9))
                {
                    depth = 90;
                    isPressed = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D0))
                {
                    depth = 100;
                    isPressed = true;

                }





                if (Keyboard.GetState().IsKeyDown(Keys.S) && isPressed == false)
                {

                    squareArray[indexI, indexJ].spriteColor = currentColor;
                    indexI += 1;
                    indexI = IndexBoundCheck(indexI);
                    squareArray[indexI, indexJ].spriteColor = clickerColor;
                    currentColor = ColourAlternator(currentColor);

                    isPressed = true;




                }
                else if (Keyboard.GetState().IsKeyDown(Keys.W) && isPressed == false)
                {

                    squareArray[indexI, indexJ].spriteColor = currentColor;
                    indexI -= 1;
                    indexI = IndexBoundCheck(indexI);
                    squareArray[indexI, indexJ].spriteColor = clickerColor;
                    currentColor = ColourAlternator(currentColor);
                    isPressed = true;




                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A) && isPressed == false)
                {

                    squareArray[indexI, indexJ].spriteColor = currentColor;
                    indexJ -= 1;
                    indexJ = IndexBoundCheck(indexJ);
                    squareArray[indexI, indexJ].spriteColor = clickerColor;
                    currentColor = ColourAlternator(currentColor);
                    isPressed = true;




                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D) && isPressed == false)
                {

                    squareArray[indexI, indexJ].spriteColor = currentColor;
                    indexJ += 1;
                    indexJ = IndexBoundCheck(indexJ);
                    squareArray[indexI, indexJ].spriteColor = clickerColor;
                    currentColor = ColourAlternator(currentColor);
                    isPressed = true;




                }

                else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && isPressed == false)
                {

                    pointList.Add(new Point(indexJ, indexI));

                    isPressed = true;


                }


                if (Keyboard.GetState().GetPressedKeys().Length == 0)
                {
                    isPressed = false;

                }



               
                if (pointList.Count % 2 == 0  && pointList.Count >= 2 && turnComplete == false)
                {
                    
                    moveList.Add(new Move(pointList[pointList.Count - 2], pointList[pointList.Count - 1]));
                    m = moveList[moveList.Count - 1];
                    


                    playerBoard = MovePiecesReturner(m, color, playedBoards[playedBoards.Count - 1]);
                    


                    pointList = new List<Point>();
                    if (playerBoard != null)
                    {
                        playedBoards.Add(playerBoard);
                        localAiBoard.Add(playerBoard);
                        turnComplete = true;
                        isCalled = true;
                        turn = Turn.AI;




                    }
                    else if (playerBoard == null)
                    {
                        moveList.RemoveAt(moveList.Count - 1);
                        pointList.Clear();

                    }







                }

                for (int endX = 0; endX < 8; endX++)
                {
                    // checks if a piece has reached the end during the playrs turn, and as a result can be promoted
                    if (playedBoards[playedBoards.Count - 1].pawn.ContainsKey(new Point(endX, 0)))
                    {
                        if (playedBoards[playedBoards.Count - 1].pawn[new Point(endX, 0)].GetColor == Color.White)
                        {
                            Console.WriteLine("You can promote a piece! choose what you want to promote to [R]ook, [K]night, [B]ishop, [Q]ueen");
                            if (Keyboard.GetState().IsKeyDown(Keys.R) && isPressed == false)
                            {

                                playedBoards[playedBoards.Count - 1].pawn.Remove(new Point(endX, 0));
                                piecePosition = new Vector2(endX * (float)RookTexture.Width, 0 * (float)RookTexture.Height);
                                pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, RookTexture.Width, RookTexture.Height);
                                playedBoards[(playedBoards.Count - 1)].rook.Add(new Point(endX, 0), new Rook(RookTexture, piecePosition, Color.White, pieceBounds, 1, -4, true));

                                hasPromoted = true;
                                isPressed = true;
                                moveList.Add(new Move(new Point(endX, 0), new Point(99, 10)));



                            }
                            else if (Keyboard.GetState().IsKeyDown(Keys.K) && isPressed == false)
                            {

                                playedBoards[playedBoards.Count - 1].pawn.Remove(new Point(endX, 0));
                                piecePosition = new Vector2(endX * (float)KnightTexture.Width, 0 * (float)KnightTexture.Height);
                                pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, KnightTexture.Width, KnightTexture.Height);
                                playedBoards[(playedBoards.Count - 1)].knight.Add(new Point(endX, 0), new Knight(KnightTexture, piecePosition, Color.White, pieceBounds, 1, -3, true));

                                hasPromoted = true;
                                isPressed = true;
                                moveList.Add(new Move(new Point(endX, 0), new Point(99, 11)));



                            }
                            else if (Keyboard.GetState().IsKeyDown(Keys.B) && isPressed == false)
                            {

                                playedBoards[playedBoards.Count - 1].pawn.Remove(new Point(endX, 0));
                                piecePosition = new Vector2(endX * (float)BishopTexture.Width, 0 * (float)BishopTexture.Height);
                                pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, BishopTexture.Width, BishopTexture.Height);
                                playedBoards[(playedBoards.Count - 1)].bishop.Add(new Point(endX, 0), new Bishop(BishopTexture, piecePosition, Color.White, pieceBounds, 1, -4, true));

                                hasPromoted = true;
                                isPressed = true;
                                moveList.Add(new Move(new Point(endX, 0), new Point(99, 12)));



                            }

                            else if (Keyboard.GetState().IsKeyDown(Keys.Q) && isPressed == false)
                            {

                                playedBoards[playedBoards.Count - 1].pawn.Remove(new Point(endX, 0));
                                Vector2 piecePosition = new Vector2(endX * (float)QueenTexture.Width, 0 * (float)QueenTexture.Height);
                                Rectangle pieceBounds = new Rectangle((int)piecePosition.X, (int)piecePosition.Y, QueenTexture.Width, QueenTexture.Height);
                                playedBoards[(playedBoards.Count - 1)].queen.Add(new Point(endX, 0), new Queen(QueenTexture, piecePosition, Color.White, pieceBounds, 1, -8, true));

                                hasPromoted = true;
                                isPressed = true;
                                moveList.Add(new Move(new Point(endX, 0), new Point(99, 13)));



                            }


                        }

                    }
                    if (hasPromoted == true && turnComplete == true)
                    {
                        hasPromoted = false;
                        isCalled = true;
                        turn = Turn.AI;
                        break;
                    
                    }




                }









                








            }

            //during the AI turn a new board is created as well as a list of possible moves
            if (turn == Turn.AI && isCalled == true)
            {



                int currentVal = 0; 
                bestMove = new Move();
                currentBoard = UpToDateBoard(moveList); 

                AiMove = AllMoves(Turn.AI, currentBoard);
                foreach (Move moves in AiMove)
                {
                    // each move is evaluated to se if it is better than the previous
                    analysedBoard = MovePiecesReturner(moves, Color.Black, UpToDateBoard(moveList));
                    moveList.Add(moves);
                    if (analysedBoard != null)
                    {
                        currentVal = BestBoard(UpToDateBoard(moveList), Turn.AI, depth);
                        if (currentVal > bestCurrentVal )
                        {
                            bestCurrentVal = currentVal;
                            bestMove = moves;
                            


                        }



                    }
                    moveList.RemoveAt(moveList.Count - 1);

                }

                AiBoard = MovePiecesReturner(bestMove, Color.Black, UpToDateBoard(moveList));
                playedBoards.Add(AiBoard);
                moveList.Add(bestMove);
                isCalled = false;
                turnComplete = false;
                bestCurrentVal = -1;
                currentVal = 0;


                turn = Turn.PLAYER;


            }

            //checks if a king has been captured and if so ends the game
            if (playedBoards[playedBoards.Count - 1].king.Count < 2)
            {
                King[] remainingKings = new King[1];
                Kingposition = playedBoards[playedBoards.Count - 1].king.Values;
                Kingposition.CopyTo(remainingKings, 0);
                Color winColor = remainingKings[0].GetColor;
                if (winColor == Color.White)
                {
                    Console.WriteLine("the player has won");
                    Exit();
                }
                else if (winColor == Color.Black)
                {
                    Console.WriteLine("the AI has won");
                    Exit();

                }


            }
            








            // TODO: Add your update logic here

            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Aquamarine);
            spriteBatch.Begin();

            
            foreach (Square squares in squareArray)
            {
                squares.Draw(spriteBatch);
            }

            //check the dictionary for if there if a key and load the piece if that is the case
            foreach (KeyValuePair<Point, Pawn> pawn in playedBoards[playedBoards.Count - 1].pawn)
            {

                if (pawn.Value.CurrentlyDrawn)
                {
                    (pawn.Value).Draw(spriteBatch);
                }


                
                
            
            }

            foreach (KeyValuePair<Point, Rook> rook in playedBoards[playedBoards.Count - 1].rook)
            {

                if (rook.Value.CurrentlyDrawn)
                {
                    (rook.Value).Draw(spriteBatch);
                }



            }

            foreach (KeyValuePair<Point, Knight> knight in playedBoards[playedBoards.Count - 1].knight)
            {
                if (knight.Value.CurrentlyDrawn)
                {
                    (knight.Value).Draw(spriteBatch);
                }

            }

            foreach (KeyValuePair<Point, Bishop> bishop in playedBoards[playedBoards.Count - 1].bishop)
            {
                if (bishop.Value.CurrentlyDrawn)
                {
                    (bishop.Value).Draw(spriteBatch);
                }

            }

            foreach (KeyValuePair<Point, King> king in playedBoards[playedBoards.Count - 1].king)
            {
                if (king.Value.CurrentlyDrawn)
                {
                    (king.Value).Draw(spriteBatch);
                }

            }

            foreach (KeyValuePair<Point, Queen> queen in playedBoards[playedBoards.Count - 1].queen)
            {
                if (queen.Value.CurrentlyDrawn)
                {
                    (queen.Value).Draw(spriteBatch);
                }

            }












            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
