using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Bishop : ChessPiece
    {

        public Bishop(bool b) : base(b,false)
        { }
        public override bool canMove(int sourceC, int sourceR, int targetC, int targetR)
        {
            return (Math.Abs(sourceC - targetC) == Math.Abs(sourceR - targetR));
        }

        public override ChessPiece cloneChessPiece()
        {
            return new Bishop(this.getIsWhite());
        }

        public override ChessPieceNum getPrintValue()
        {
            return getIsWhite()? ChessPieceNum.WhiteBishop : ChessPieceNum.BlackBishop;
        }

        public override string CodeForLog()
        {
            return base.CodeForLog()+"B";
        }
        public override string ToString()
        {
            return (getIsWhite()?"White":"Black")+ "Bishop";
        }
    }
}
