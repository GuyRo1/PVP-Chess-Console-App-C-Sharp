using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    interface IChessPiece
    {
        bool DidMove();

        void Moved();
        ChessPieceNum getPrintValue();
        bool getIsWhite();
        bool canMove(int sourceC, int sourceR, int targetC, int targetR);

        bool canSpecailMove(int sourceC, int sourceR, int targetC, int targetR,MoveType type);

        

        string CodeForLog();
        ChessPiece cloneChessPiece();
        


    }
}
