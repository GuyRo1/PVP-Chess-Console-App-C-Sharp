using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class BasicGraphics : IGraphics
    {
        public void DrawBoard(ChessBoard b)
        {       
           char letter = 'A';
           int rowIndex = b.getNumberOfRows();
           string[] gBoard=graphicBoard(b);
            Console.WriteLine();
            Console.WriteLine();
           for(int i=0;i<b.getNumberOfColumns();i++)
                Console.Write("{0,3}",letter++);
            Console.WriteLine();
            for (int i = 0; i < b.getNumberOfRows(); i++)
            {
                Console.Write(rowIndex--);
                for (int j = 0; j < b.getNumberOfColumns(); j++)
                {
                    Console.Write("{0,3}", gBoard[i*b.getNumberOfColumns()+j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        

       public void graphicExplanation()
        {
            
        }

        

        public void PrintMessage(string message, ChessBoard b)
        {
            Console.WriteLine(message);
        }

        public void PrintMessages(string[] messages, ChessBoard b)
        {
            foreach (string m in messages)
            {
                Console.WriteLine(m);
            }
        }

        private string[] graphicBoard(ChessBoard b)
        {

            ChessPieceNum[] bs = b.getBoardState();
            string[] graphcPieces = new string[b.getBoardSize()];
            for (int i = 0; i < b.getBoardSize(); i++)
            {
                switch (bs[i])
                {
                    
                    case ChessPieceNum.WhitePawn:
                        graphcPieces[i] = "WP";
                        break;
                    
                    case ChessPieceNum.WhiteRook:
                        graphcPieces[i] = "WR";
                        break;
                    case ChessPieceNum.WhiteKnight:
                        graphcPieces[i] = "WN";
                        break;
                    case ChessPieceNum.WhiteBishop:
                        graphcPieces[i] = "WB";
                        break;
                    case ChessPieceNum.WhiteQueen:
                        graphcPieces[i] = "WQ";
                        break;
                    case ChessPieceNum.WhiteKing:
                        graphcPieces[i] = "WK";
                        break;
                    case ChessPieceNum.BlackPawn:
                        graphcPieces[i] = "BP";
                        break;
                    case ChessPieceNum.BlackRook:
                        graphcPieces[i] = "BR";
                        break;
                    case ChessPieceNum.BlackKnight:
                        graphcPieces[i] = "BN";
                        break;
                    case ChessPieceNum.BlackBishop:
                        graphcPieces[i] = "BB";
                        break;
                    case ChessPieceNum.BlackQueen:
                        graphcPieces[i] = "BQ";
                        break;
                    case ChessPieceNum.BlackKing:
                        graphcPieces[i] = "BK";
                        break;
                    default:
                        graphcPieces[i] = "EE";
                        break;
                }

            }
            return graphcPieces;
        }
    }
}
