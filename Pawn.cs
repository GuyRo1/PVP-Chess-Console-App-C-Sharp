using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Pawn : ChessPiece
    {

        private bool canEnPassant;

        public Pawn(bool b) : base(b, false)
        {
            canEnPassant = false;
        }

        public Pawn(bool b, bool ennPassant) : base(b, false)
        {
            canEnPassant = ennPassant;
        }

        public override ChessPiece cloneChessPiece()
        {
            return new Pawn(this.getIsWhite(),canEnPassant);
        }
        private int moveValue(int sourceR,int targetR)
        {        
                return targetR - sourceR;        
        }
        public override bool canMove(int sourceC, int sourceR, int targetC, int targetR)
        {
            if (targetC - sourceC == 0)
            {
                if (this.getIsWhite())
                    return moveValue(sourceR, targetR) == -1;
                else
                    return moveValue(sourceR, targetR) == 1;
            }

            return false;
        }

        public override ChessPieceNum getPrintValue()
        {
            return getIsWhite()?ChessPieceNum.WhitePawn:ChessPieceNum.BlackPawn;
        }

        public override bool canKill(int sourceC, int sourceR, int targetC, int targetR)
        {
            if (Math.Abs(targetC - sourceC) == 1)
            { 
                if(this.getIsWhite())
                    return targetR - sourceR == -1;
                else
                    return targetR - sourceR == 1;
            }
            return false;
        }

        public override bool canSpecailMove(int sourceC, int sourceR, int targetC, int targetR,MoveType type)
            {
            if(type==MoveType.DoubleJump)
            if (targetC - sourceC == 0)
            {
                if (this.getIsWhite())
                    return moveValue(sourceR, targetR) == -2;
                else
                    return moveValue(sourceR, targetR) == 2;
            }

            if (type == MoveType.EnPassant)
                return (canKill(sourceC, sourceR, targetC, targetR));

            return false;           
            }

        public void SetEnPassantRights(bool b)
        {
            canEnPassant = b;
        
        }

        public override string CodeForLog()
        {
            return base.CodeForLog() + "P" + (canEnPassant?"Y":"N");
        }

        public override string ToString()
        {
            return (getIsWhite() ? "White" : "Black") + "Pawn";
        }
    }
    }

