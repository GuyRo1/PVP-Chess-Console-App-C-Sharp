using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Rook : ChessPiece
    {

       
        public Rook(bool b) : base(b, false)
        { }

        public override ChessPiece cloneChessPiece()
        {
            return new Rook(this.getIsWhite());
        }

        public override bool canMove(int sourceC, int sourceR, int targetC, int targetR)
        {
            return (targetR == sourceR )|| (targetC == sourceC);
        }

        public override ChessPieceNum getPrintValue()
        {
            return getIsWhite() ? ChessPieceNum.WhiteRook : ChessPieceNum.BlackRook;
        }

        public override string CodeForLog()
        {
            return base.CodeForLog() + "R";
        }
        public override string ToString()
        {
            return (getIsWhite() ? "White" : "Black") + "Rook";
        }
    }
}
