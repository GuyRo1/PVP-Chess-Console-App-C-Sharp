using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    interface IGraphics
    {
       
       void DrawBoard(ChessBoard b);

      void graphicExplanation();

      void PrintMessages(string[] messages,ChessBoard b);

      void PrintMessage(string message, ChessBoard b);

     

    }
}
