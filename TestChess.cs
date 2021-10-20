using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class TestChess
    {
        private bool wait;
        private int[,] pieceCordinates;
        private ChessPiece[] pieces;
        private string[] chessMoves;
        int wKC, wKR, bKC, bKR;


        public void RunTest(int senario)
        {
           
            Game game = new Game();
            wait = true; // asking for pressKey after each move!
            setData(senario);
            setMoves(senario);
            game.startTestGame(wait, chessMoves, pieceCordinates, pieces, wKC, wKR, bKC, bKR);
        }


        private void setMoves(int senario)
        {
            int id = 0;
            string[] localMoves = new string[1000];

            switch (senario)
            {

                // Checking three fold repition - WORKS!
                case 1:
                    //starting position is original position
                    localMoves[id++] = "G1F3";
                    localMoves[id++] = "G8F6";
                    localMoves[id++] = "F3G1";
                    localMoves[id++] = "F6G8";//original position
                    localMoves[id++] = "G1F3";
                    localMoves[id++] = "G8F6";
                    localMoves[id++] = "F3G1";
                    localMoves[id++] = "F6G8";//original position - game should end with a tie 3X
                    localMoves[id++] = "G1F3";
                    localMoves[id++] = "G8F6";
                    localMoves[id++] = "F3G1";
                    localMoves[id++] = "F6G8";//original position 
                    localMoves[id++] = "G1F3";
                    localMoves[id++] = "G8F6";
                    localMoves[id++] = "F3G1";
                    localMoves[id++] = "F6G8";//original position - game should end with a tie 5X

                    break;

                case 2:
                    localMoves[id++] = "E2E4";
                    localMoves[id++] = "d7d5";
                    localMoves[id++] = "e1e2";
                    localMoves[id++] = "d8d6";
                    localMoves[id++] = "e2f3";
                    localMoves[id++] = "d6f6";
                    break;

                default:
                    chessMoves = null;
                    return;
            }
                    chessMoves = new string[id];
                    for (int i = 0; i < id; i++)
                        chessMoves[i] = localMoves[i];


            
        }

     

        private void setOrigChessBoard()
        {
            int index = 0;
            int numberOfPieces = 30;
            pieceCordinates = new int[2, numberOfPieces];
            pieces = new ChessPiece[numberOfPieces];

            wKC = 4;
            wKR = 7;
            bKC = 4;
            bKR = 0;

            pieces[index++] = new Rook(false);
            pieces[index++] = new Knight(false);
            pieces[index++] = new Bishop(false);
            pieces[index++] = new Queen(false);
            pieces[index++] = new Bishop(false);
            pieces[index++] = new Knight(false);
            pieces[index++] = new Rook(false);

            for (int i = 0; i < 8; i++)
                pieces[index++] = new Pawn(false);

            for (int i = 0; i < 8; i++)
                pieces[index++] = new Pawn(true);

            pieces[index++] = new Rook(true);
            pieces[index++] = new Knight(true);
            pieces[index++] = new Bishop(true);
            pieces[index++] = new Queen(true);
            pieces[index++] = new Bishop(true);
            pieces[index++] = new Knight(true);
            pieces[index++] = new Rook(true);



            int counter = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 0 && j == 4)
                        j++;

                    pieceCordinates[0, counter] = j;
                    pieceCordinates[1, counter] = i;

                    counter++;
                }
            }

            for (int i = 6; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 7 && j == 4)
                        j++;

                    pieceCordinates[0, counter] = j;
                    pieceCordinates[1, counter] = i;

                    counter++;

                }
            }
        }

        private void setData(int senario)
        {
            int numberOfPieces = 0, index=0;


            if (senario >= 0 && senario < 3)
                setOrigChessBoard();
            else
                switch (senario)
                {
                    case 3:
                        index = 0;
                        numberOfPieces = 2;
                        pieceCordinates = new int[2, numberOfPieces];
                        pieces = new ChessPiece[numberOfPieces];
                        wKC = 4;
                        wKR = 7;
                        bKC = 4;
                        bKR = 0;
                        pieces[index++] = new Knight(true);
                        pieces[index++] = new Pawn(false);
                        int knightSpot = textToSpot("A8");
                        int pawnLocation = textToSpot("F2");
                        pieceCordinates[0, 0] = knightSpot % 8;
                        pieceCordinates[1, 0] = knightSpot / 8;
                        pieceCordinates[0, 1] = pawnLocation % 8;
                        pieceCordinates[1, 1] = pawnLocation / 8;
                        break;
                    case 4:
                        index = 0;
                        numberOfPieces = 2;
                        pieceCordinates = new int[2, numberOfPieces];
                        pieces = new ChessPiece[numberOfPieces];
                        wKC = 4;
                        wKR = 7;
                        bKC = 4;
                        bKR = 0;
                        pieces[index++] = new Rook(true);
                        pieces[index++] = new Rook(false);
                        int rook1Spot = textToSpot("H1");
                        int rook2Spot = textToSpot("E7");
                        pieceCordinates[0, 0] = rook1Spot % 8;
                        pieceCordinates[1, 0] = rook1Spot / 8;
                        pieceCordinates[0, 1] = rook2Spot % 8;
                        pieceCordinates[1, 1] = rook2Spot / 8;
                        break;
                    default:
                        break;
                }
        }

        private int textToSpot(string s)
        {
            int c = translate(s[0]);
            int r;
            if (s[1] >= '1' && s[1] <= '8')
                r = 8 - int.Parse("" + s[1]);
            else
                r = -1;
            if (c != -1 && r != -1)
                return 8 * r + c;
            else
                throw new Exception();               
        }

      

        private int translate(char c)
        {
            switch (c)
            {
                case 'A':
                    return 0;
                case 'B':
                    return 1;
                case 'C':
                    return 2;
                case 'D':
                    return 3;
                case 'E':
                    return 4;
                case 'F':
                    return 5;
                case 'G':
                    return 6;
                case 'H':
                    return 7;
                default:
                    return -1;
                    
            }


        }

       

       
        }
}
