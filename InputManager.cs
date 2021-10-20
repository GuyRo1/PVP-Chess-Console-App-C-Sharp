using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class InputManager
    {

        public bool isMoveList(string[] moveList)
        {
            for (int i = 0; i < moveList.Length; i++)
            {
                if (getMove(moveList[i]) == null)
                    return false;   
            }

            return true;
        
        
        
        }
        public Move getMove(string s)
        {
            
            Move m=null;
            int column1, row1, column2, row2;
            if (s.Length != 4)
                return m;
            
            if (s[1] >= '1' && s[1] <= '8')
                row1 = 8 - int.Parse("" + s[1]);
            else
                return m;
            if (s[3] >= '1' && s[3] <= '8')
                row2 = 8 - int.Parse("" + s[3]);
            else
                return m;

            column1 = LetterToNum(s[0]);
            if (column1 == -1)
            {
                
                return m;
            }

            column2 = LetterToNum(s[2]);
            if (column2 == -1)
            {
                
                return m;
            }
            if (column1 == column2 && row1 == row2)
                return m;
            m = new Move(column1, row1, column2, row2);
            return m;
        }


        private int LetterToNum(char c)
        {

            switch (c)
            {
                case 'a':
                case 'A':
                    return 0;

                case 'b':
                case 'B':
                    return 1;
                case 'C':
                case 'c':
                    return 2;
                case 'd':
                case 'D':
                    return 3;
                case 'e':
                case 'E':
                    return 4;
                case 'f':
                case 'F':
                    return 5;
                case 'g':
                case 'G':
                    return 6;
                case 'h':
                case 'H':
                    return 7;
                default:
                    return -1;
            }
        }

        public bool checkInputForCT(string s)
        {
            if (s == "1" || s == "2" || s == "3" || s == "4")
                return true;
            return false;
        
        }

        public bool checkIfStartNewGame(string s)
        {
            return s == "Y"||s=="y";
        }

        public string getInput(bool wait,string s)
        {
            if (s == null)
            {
                s = getInput();

            }
            else if (wait)
                Console.ReadKey();
                return s;
        }

        public string getInput()
        {
            Console.Write(" ");
            return Console.ReadLine();
        }

    }
}
