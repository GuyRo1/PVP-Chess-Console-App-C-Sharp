using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Move
    {
        
        private int columnFrom;
        private int columnTo;
        private int rowFrom;
        private int rowTo;
        private int dir;
        private MoveType specailType;
        private ChessPiece piece;


        
        public Move(int columnFrom, int rowFrom,int columnTo , int rowTo)
        {
           
            this.columnFrom = columnFrom;
            this.columnTo = columnTo;
            this.rowFrom = rowFrom;
            this.rowTo = rowTo;
            this.piece = null;
            specailType = MoveType.NoMove;
            dir = setDir();        
        }

        public Move(Move m)
        {
           columnFrom = m.columnFrom;
           columnTo = m.columnTo;
           rowFrom = m.rowFrom;
           rowTo = m.rowTo;
           if(m.piece!=null)
           piece = m.piece.cloneChessPiece();
           specailType = m.specailType;
           dir = m.dir;

        }

        public MoveType getSpecailType()
        {
            return this.specailType;
        }

        public void setSpecailType(MoveType type)
        {
            this.specailType = type;
        }
        public int getDir()
        {
            return this.dir;
        }
        private int setDir()
        {
            int cDelta = Math.Abs(columnFrom - columnTo);
            int rDelta = Math.Abs(rowFrom - rowTo);

            if (columnFrom == columnTo)
            {
                if (rowFrom < rowTo)
                    return 1;//down
                else
                    return 2;//up
            }

            if (rowFrom == rowTo)
            {
                if (columnFrom < columnTo)
                    return 3;//right
                else
                    return 4;//left
            }

            if (Math.Abs(columnFrom - columnTo) == Math.Abs(rowFrom - rowTo))
            {
               if((rowFrom < rowTo)&& (columnFrom < columnTo))
                    return 5;//down right
               if ((rowFrom < rowTo)&& (columnFrom > columnTo))
                    return 6;//down left
                if ((rowFrom > rowTo)&& (columnFrom < columnTo))
                    return 7;//up right
                if ((rowFrom > rowTo)&& (columnFrom > columnTo))
                    return 8;//up left
            }


            if ((cDelta == 2 && rDelta == 1) || (cDelta == 1 && rDelta == 2))
                return 9;//knight Movement
            //Idk if needed

            return -1;

        }

        public ChessPiece GetPiece()
        {
            return this.piece;
        }

        public void SetPiece(ChessPiece p)
        {
            this.piece = p;
        }

        public int getColumnFrom()
        {
            return columnFrom;
        }

        public int getColumnTo()
        {
            return columnTo;
        }

        public int getRowFrom()
        {
            return rowFrom;
        }

        public int getRowTo()
        {
            return rowTo;
        }

        public int[] getDirInCRFormat()
        {
            int[] cr = new int[2];
            switch (dir)
            {
                case 1:
                    cr[0] = 0;
                    cr[1] = 1;
                    break;
                case 2:
                    cr[0] = 0;
                    cr[1] = -1;
                    break;
                case 3:
                    cr[0] = 1;
                    cr[1] = 0;
                    break;
                case 4:
                    cr[0] = -1;
                    cr[1] = 0;
                    break;
                case 5:
                    cr[0] = 1;
                    cr[1] = 1;
                    break;
                case 6:
                    cr[0] = -1;
                    cr[1] = 1;
                    break;
                case 7:
                    cr[0] = 1;
                    cr[1] = -1;
                    break;
                case 8:
                    cr[0] = -1;
                    cr[1] = -1;
                    break;
                
            }
            return cr;
        }

       

        public override string ToString()
        {
            char cFrom, cTo;
            int rFrom, rTo;
            cFrom = numberToLetter(columnFrom);
            cTo = numberToLetter(columnTo);
            rFrom = 8-rowFrom;
            rTo = 8 - rowTo;
            return this.piece + ":" + cFrom + rFrom + cTo + rTo;
        }

        private char numberToLetter(int n)
        {

            switch (n)
            {
                case 0:
                    return 'A';
                case 1:
                    return 'B';
                case 2:
                    return 'C';
                case 3:
                    return 'D';
                case 4:
                    return 'E';
                case 5:
                    return 'F';
                case 6:
                    return 'G';
                case 7:
                    return 'H';
                default:
                    return 'X';

            }
        }
    }
}
