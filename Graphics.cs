using System;
using System.Runtime.InteropServices;

namespace Chess
{
    class Graphics:IGraphics
    {
        //*I use this code only to resize the screen to full screen*//*
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        //* reall code starts here*//*
        private string spaceAfterFrame;
        private string spaceInBottomFrame;
        private string spaceAfterPiece;
        private string spaceAfterWhitePawn;
        private string spaceBeforPiece;
        private string seperatorLeft;
        private string seperatorRight;
        private string spaceBeforAll;
        private bool clearScreen;
        
        public Graphics()
        {
        spaceInBottomFrame = "        ";
        spaceAfterFrame = "      ";
        spaceAfterPiece = "   ";
        spaceAfterWhitePawn = "  ";
        spaceBeforPiece = "  ";
        seperatorLeft = "[";
        seperatorRight = "]";
        spaceBeforAll = "            ";
        clearScreen=true;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        ShowWindow(ThisConsole, MAXIMIZE);
        }

        public Graphics(bool clearScreen) : this()
        {
            this.clearScreen = clearScreen;
        }


     


        public void DrawBoard(ChessBoard b)
        {
            refresh(b, true);
            int frameIndex = 0;
            string[] graphicPieces = graphicBoard(b);
            string[] frame = getFrame();
            for (int i = 0; i < b.getNumberOfRows(); i++)
            {
                Console.Write(spaceBeforAll);
                Console.Write(frame[frameIndex]);
                Console.Write(spaceAfterFrame);
                frameIndex++;

                for (int j = 0; j < b.getNumberOfRows(); j++)
                {
                    Console.Write(graphicPieces[i * b.getNumberOfColumns() + j]);
                }

                Console.WriteLine();
                Console.WriteLine();              
            }
            Console.WriteLine();
            
            
            
            Console.Write(spaceBeforAll);
            Console.Write(frame[frameIndex]);
            frameIndex++;
            for (int i = frameIndex; i < frame.Length; i++)
                Console.Write(frame[i]);
            Console.WriteLine();

        }
        private void refresh(ChessBoard b, bool alreadyInDrawBoard)
        {
            if (clearScreen)
            {
                Console.Clear();
                if (!alreadyInDrawBoard)
                    DrawBoard(b);

            }
        }
        private string[] graphicBoard(ChessBoard b)
        {
           
            ChessPieceNum[] bs = b.getBoardState();
            string[] graphcPieces = new string[b.getBoardSize()];
            for (int i = 0; i < b.getBoardSize(); i++)
            {
                switch (bs[i])
                {
                    case ChessPieceNum.WhitePawn:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u265F" + spaceAfterWhitePawn + seperatorRight;
                        break;
                    case ChessPieceNum.WhiteRook:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u265C" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.WhiteKnight:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u265E" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.WhiteBishop:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u265D" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.WhiteQueen:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u265B" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.WhiteKing:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u265A" + spaceAfterPiece + seperatorRight;
                        break;
                   case ChessPieceNum.BlackPawn:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u2659" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.BlackRook:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u2656" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.BlackKnight:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u2658" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.BlackBishop:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u2657" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.BlackQueen:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u2655" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.BlackKing:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + "\u2654" + spaceAfterPiece + seperatorRight;
                        break;
                    case ChessPieceNum.NoPiece:
                    default:
                        graphcPieces[i] = seperatorLeft + spaceBeforPiece + " " + spaceAfterPiece + seperatorRight;
                        break;
                }

            }
            return graphcPieces;           
        }

        private string[] getFrame()
        {
            string[] graphicFrame =new string[17];
            graphicFrame[0] = seperatorLeft + spaceBeforPiece + "8" + spaceAfterPiece + seperatorRight;
            graphicFrame[1] = seperatorLeft + spaceBeforPiece + "7" + spaceAfterPiece + seperatorRight;
            graphicFrame[2] = seperatorLeft + spaceBeforPiece + "6" + spaceAfterPiece + seperatorRight;
            graphicFrame[3] = seperatorLeft + spaceBeforPiece + "5" + spaceAfterPiece + seperatorRight;
            graphicFrame[4] = seperatorLeft + spaceBeforPiece + "4" + spaceAfterPiece + seperatorRight;
            graphicFrame[5] = seperatorLeft + spaceBeforPiece + "3" + spaceAfterPiece + seperatorRight;
            graphicFrame[6] = seperatorLeft + spaceBeforPiece + "2" + spaceAfterPiece + seperatorRight;
            graphicFrame[7] = seperatorLeft + spaceBeforPiece + "1" + spaceAfterPiece + seperatorRight;
            graphicFrame[8] = spaceAfterFrame+spaceInBottomFrame;
            graphicFrame[9] = seperatorLeft + spaceBeforPiece +  "A" + spaceAfterPiece + seperatorRight;
            graphicFrame[10] = seperatorLeft + spaceBeforPiece + "B" + spaceAfterPiece + seperatorRight;
            graphicFrame[11] = seperatorLeft + spaceBeforPiece + "C" + spaceAfterPiece + seperatorRight;
            graphicFrame[12] = seperatorLeft + spaceBeforPiece + "D" + spaceAfterPiece + seperatorRight;
            graphicFrame[13] = seperatorLeft + spaceBeforPiece + "E" + spaceAfterPiece + seperatorRight;
            graphicFrame[14] = seperatorLeft + spaceBeforPiece + "F" + spaceAfterPiece + seperatorRight;
            graphicFrame[15] = seperatorLeft + spaceBeforPiece + "G" + spaceAfterPiece + seperatorRight;
            graphicFrame[16] = seperatorLeft + spaceBeforPiece + "H" + spaceAfterPiece + seperatorRight;
            return graphicFrame;
        }

        public void graphicExplanation()
        {
            Console.WriteLine("  Welcome to my Chess game");
            Console.WriteLine("  This game is using a bit of code to make the console more playable.");
            Console.WriteLine("  The most important thing is for you the user to be able to see the chess pieces.");
            Console.WriteLine("  If you dont see this chess piece: \u2654");
            Console.WriteLine("  Plese go to the console properties and change the font until you can see the piece.");
            Console.WriteLine("  In addition yoy should try what font size is the best for you.");
            Console.WriteLine("  Enjoy!");
            Console.WriteLine("  Press any key to start the game");
            Console.ReadKey();
            Console.Clear();
        }

        public void PrintMessages(string[] messages,ChessBoard b)
        {
            if(b!=null)
            refresh(b,false);
            foreach (string m in messages)
            {
                Console.WriteLine(m);
            }

        }

        public void PrintMessage(string message, ChessBoard b)
        {
            if(b!=null)
            refresh(b, false);
            Console.WriteLine(message);
        }
    }   
}
