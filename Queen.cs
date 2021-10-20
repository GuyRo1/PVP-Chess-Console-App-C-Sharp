using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Queen : ChessPiece
    {

        public override ChessPiece cloneChessPiece()
        {
            return new Queen(this.getIsWhite());
        }
        public Queen(bool b) : base(b, false)
        { }
        public override bool canMove(int sourceC, int sourceR, int targetC, int targetR)
        {
            return ((targetR == sourceR) || (targetC == sourceC)) || (Math.Abs(sourceC - targetC) == Math.Abs(sourceR - targetR));
        }

        public override ChessPieceNum getPrintValue()
        {
           return getIsWhite() ? ChessPieceNum.WhiteQueen : ChessPieceNum.BlackQueen;
        }

        public override string CodeForLog()
        {
            return base.CodeForLog() + "Q";
        }
        public override string ToString()
        {
            return (getIsWhite() ? "White" : "Black") + "Queen";
        }
    }
}
