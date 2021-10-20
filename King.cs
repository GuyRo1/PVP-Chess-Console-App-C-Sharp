using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class King : ChessPiece
    {
        
        public King(bool b) : base(b, false)
        { 
        
        }

        public King(bool b, bool move) : base(b, false, move)
        { 
            
        
        }

        public override ChessPiece cloneChessPiece()
        {
            return new King(this.getIsWhite(),DidMove());
            
        }
        public override bool canMove(int sourceC, int sourceR, int targetC, int targetR)
        {
            int cDelta = Math.Abs(sourceC - targetC);
            int rDelta = Math.Abs(sourceR - targetR);
            return cDelta <= 1&&rDelta<=1;
        }

        public override ChessPieceNum getPrintValue()
        {
            return getIsWhite() ? ChessPieceNum.WhiteKing : ChessPieceNum.BlackKing;
        }

        public override bool canSpecailMove(int sourceC, int sourceR, int targetC, int targetR, MoveType type)
        {
            if (type==MoveType.CastleLeft||type==MoveType.CastleRight)
                if (!DidMove())
                    if (targetR == sourceR)
                    {
                        

                        if (type == MoveType.CastleLeft)//left castling
                            if (targetC - sourceC == -2)
                                return true;


                        if(type == MoveType.CastleRight)//right castling
                            if (targetC - sourceC == 2)
                                return true;
                    }
            return false;            
        }

        public override string CodeForLog()
        {
            return base.CodeForLog() + "K" +(DidMove()?"Y":"N");
        }
        public override string ToString()
        {
            return (getIsWhite() ? "White" : "Black") + "King";
        }

      








       

        
    }
}
