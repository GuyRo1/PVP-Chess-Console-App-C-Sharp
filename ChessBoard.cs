using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class ChessBoard
    {
        ChessPiece[] pieces;

        private int numberOfColumns;
        private int numberOfrows;
       

        public ChessBoard(int columns, int rows)
        {
            numberOfColumns = columns;
            numberOfrows = rows;
            pieces = new ChessPiece[numberOfColumns * numberOfrows];
        }

        public void InitializeBoard()
        {
            this.pieces = new ChessPiece[numberOfColumns * numberOfrows];
        }

        public ChessBoard(ChessBoard source):this(source.numberOfColumns,source.numberOfrows)
        {
            ChessPiece newPiece;
            for (int i = 0; i < numberOfrows * numberOfColumns; i++)
            {
                if (source.pieces[i] != null)
                {
                    newPiece = source.pieces[i].cloneChessPiece();
                    pieces[i] = newPiece;
                }
            }
        
        }
        public ChessBoard cloneChessBoard()
        {
            ChessBoard newBoard = new ChessBoard(numberOfColumns , numberOfrows);
            ChessPiece[] p = new ChessPiece[newBoard.numberOfColumns*newBoard.numberOfrows];
            for (int i = 0; i < newBoard.getBoardSize(); i++)
            {
                if(pieces[i]!=null)
                newBoard.pieces[i] = pieces[i].cloneChessPiece();
            }
            return newBoard;
        }
        public ChessPieceNum[] getBoardState()
        {
                     
            ChessPieceNum[] bs = new ChessPieceNum[pieces.Length];
            for (int i = 0; i < numberOfColumns*numberOfrows; i++)
            {
                if (pieces[i] != null)
                    bs[i] = pieces[i].getPrintValue();
                else
                    bs[i] = ChessPieceNum.NoPiece;
            }
            return bs;             
        }



        public string SerilizeBoard()
        {
            bool blackRightRookMoved = false, blackLeftRookMoved = false, whiteRightRookMoved = false,
                whiteLeftRookMoved = false, whiteKingMoved = false, blackKingMoved = false;

            string bs = "";
            for (int i = 0; i < numberOfColumns * numberOfrows; i++)
            {
                if (pieces[i] != null)
                {
                    bs += pieces[i].CodeForLog();
                    switch (i)
                    {
                        case 0:
                            if (pieces[i].DidMove())
                                blackLeftRookMoved = true;
                            break;
                        case 7:
                            if (pieces[i].DidMove())
                                blackRightRookMoved = true;
                            break;
                        case 56:
                            if (pieces[i].DidMove())
                                whiteLeftRookMoved = true;
                            break;
                        case 63:
                            if (pieces[i].DidMove())
                                whiteRightRookMoved = true;
                            break;
                        case 60:
                            if (pieces[i].DidMove())
                                whiteKingMoved = true;
                            break;
                        case 4:
                            if (pieces[i].DidMove())
                                blackKingMoved = true;
                            break;
                        default:
                            break;
                    }

                }
                else
                    bs += "EE";

                bs += "/";
            }
            bs += (!whiteKingMoved && !whiteLeftRookMoved) ? "WCLA/" : "WCLNA/";
            bs += (!whiteKingMoved && !whiteRightRookMoved) ? "WCRA/" : "WCRNA/";
            bs += (!blackKingMoved && !blackRightRookMoved) ? "BCRA/" : "BCRNA/";
            bs += (!blackKingMoved && !blackLeftRookMoved) ? "BCLA/" : "BCLNA/";
            return bs;
        }

            public void AddNewPiece(ChessPiece p,int column,int row)
        {
            pieces[spotToPlace(column, row)] = p;                  
        }

        public void AddNewPiece(ChessPiece p, int location)
        {
            pieces[location] = p;        
        }
        public ChessPiece GetPiece(int column,int row)
        {
            return pieces[spotToPlace(column, row)];
        }

        public ChessPiece GetPiece(int location)
        {
            return pieces[location];
        
        }

        

        public bool RemovePiece(int column,int row)
        {
           
            pieces[spotToPlace(column, row)] = null;
            return true;
        }


        private int spotToPlace(int column, int row)
        {
            return numberOfColumns * row + column; 
        
        }

        public int getRow(int Location)
        {
            return Location / numberOfColumns;
        }

        public int getColumn(int location)
        {
            return location % numberOfColumns;
        }

        public int[] getColumnRowLocation(int location)
        {
            int[] crLocation = new int[2];
            crLocation[0] = getColumn(location);
            crLocation[1] = getRow(location);
            return crLocation;
        }

        public int[] getCordinates(ChessPieceNum chessPiece)
        {
            for (int i = 0; i < numberOfrows * numberOfColumns; i++)
                if (pieces[i] != null && pieces[i].getPrintValue() == chessPiece)
                {
                    return getColumnRowLocation(i);
                }

            int[] error= new int[1];
            error[0] = -1;
            return error;

        }

        public int getBoardSize()
        {
            return numberOfColumns * numberOfrows;
        }

        public int getNumberOfColumns()
        {
            return numberOfColumns;
        }

        public int getNumberOfRows()
        {
            return numberOfrows;
        }

        

        


    }
}
