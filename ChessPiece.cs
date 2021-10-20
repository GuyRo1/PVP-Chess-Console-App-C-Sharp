using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    abstract class ChessPiece:IChessPiece
    {
        private bool white;
        private bool jump;
        private bool didMove;

        public ChessPiece(bool white,bool jump)
        {
            this.white = white;
            this.jump = jump;
            didMove = false;
        }

        public ChessPiece(bool white, bool jump,bool didMove)
        {
            this.white = white;
            this.jump = jump;
            didMove = false;
        }


        public abstract ChessPieceNum getPrintValue();
        public bool getIsWhite()
        {
            return white;
        }

        public bool getIsJump()
        {
            return jump;
        }

        public abstract bool canMove(int sourceC,int sourceR, int targetC, int targetR);


        public virtual bool canKill(int sourceC, int sourceR, int targetC, int targetR)
        {
            return canMove(sourceC,sourceR,targetC,targetR);
        }

        public virtual bool canSpecailMove(int sourceC, int sourceR, int targetC, int targetR,MoveType type)
        {
            return false;
        }

        

       

        public bool DidMove()
        {
            return didMove;
        }

        public void Moved()
        {
            didMove = true;
        }

        public abstract ChessPiece cloneChessPiece();

        public virtual string CodeForLog()
        {
            return white ? "W" : "B";
        }
    }
}
