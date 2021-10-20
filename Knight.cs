using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Knight : ChessPiece
    {

        public Knight(bool b) : base(b, true)
        { }

        public override ChessPiece cloneChessPiece()
        {
            return new Knight(this.getIsWhite());
        }
        public override bool canMove(int sourceC, int sourceR, int targetC, int targetR)
        {
            int cDelta = Math.Abs(sourceC - targetC);
            int rDelta = Math.Abs(sourceR - targetR);
            return (cDelta == 2 && rDelta == 1) || (cDelta == 1 && rDelta == 2);
        }

        public override ChessPieceNum getPrintValue()
        {
            return getIsWhite() ? ChessPieceNum.WhiteKnight : ChessPieceNum.BlackKnight;
        }

        public override string CodeForLog()
        {
            return base.CodeForLog() + "N";
        }
        public override string ToString()
        {
            return (getIsWhite() ? "White" : "Black") + "Knight";
        }
    }
}
