using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Game
    {

        ChessBoard chessBoard;
        IGraphics g;
        InputManager i;
        int idCounter;
        Move lastMove;
        readonly int fiftyRuleMax;
        readonly int xFoldNumber;
        string[] movesLog;
        int fiftyRuleCounter;



        public Game()
        {
            g = new BasicGraphics();
            g.graphicExplanation();
            i = new InputManager();           
            fiftyRuleMax = 75;
            xFoldNumber = 5;
            movesLog = new string[fiftyRuleMax+1];
        }

        private bool threeFoldRepition()
            {
            string sourceBoard;
            int repitionCounter = 0;
            for (int i = 0; i < fiftyRuleCounter; i++)
            {
                sourceBoard = movesLog[i];
                repitionCounter = 1;
                for (int j = i + 1; j < fiftyRuleCounter; j++)
                    if (sourceBoard == movesLog[j])
                    {
                        repitionCounter++;
                        if (repitionCounter == xFoldNumber)
                            return true;


                    }
                repitionCounter = 0;
            }
            return false;
            }
            

            
           

            
        private void addToLog(string boardState)
        {
            movesLog[fiftyRuleCounter] = boardState;
        
        }


        private EndGameStatus isGameEnded(bool player)
        {
            bool check, stailMate;
            check = IsInCheck(player);
            stailMate = IsStalMate(player);
            if (stailMate && check)
                return player ? EndGameStatus.WhiteWon : EndGameStatus.BlackWon;
            else if (stailMate||isDeadPosition()||fiftyRuleCounter==fiftyRuleMax||threeFoldRepition())
                return EndGameStatus.Tie;
            else
                return EndGameStatus.Normal;
        }
        private bool endGame(EndGameStatus status)
        {
            string input;
            string[] endGameMessages = new string[2];
            switch (status)
            {
                case EndGameStatus.WhiteWon:
                    endGameMessages[0] = "The game is ended White won !";
                    break;
                case EndGameStatus.BlackWon:
                    endGameMessages[0] = "The game is ended Black won !";
                    break;
                case EndGameStatus.Tie:
                    endGameMessages[0] = "The game is ended with a tie!";
                    break;
                default:
                    break;
            }
            endGameMessages[1] = "type Y to start a new game, type anything else to close the program";

            g.PrintMessages(endGameMessages, chessBoard);
            input=Console.ReadLine();
            return i.checkIfStartNewGame(input);
        }
        private EndGameStatus gameLogic(bool wait,string[] inputMoves)
        {
            int movesCounter=0; // for auto moves
            EndGameStatus status;
            Move move;
            bool player = true;
            bool valid;
            string s = "";
            do
            {
                valid = false;
                string playerColor = player ? "White" : "Black";
                g.PrintMessage(" "+playerColor + ", Please make a move", null);
                s = i.getInput(wait, ((inputMoves != null) && (movesCounter < inputMoves.Length)) ? inputMoves[movesCounter] : null);
                do
                {
                    move = i.getMove(s);
                    if (move != null)
                    {
                        ChessBoard clone = new ChessBoard(chessBoard);
                        if (tryAMove(player, move, false,clone))
                        {
                            if (inputMoves != null && movesCounter < inputMoves.Length + 1)
                                movesCounter++;

                            valid = true;
                            g.DrawBoard(chessBoard);
                        }

                        else
                        {
                            if (inputMoves != null && movesCounter < inputMoves.Length + 1)
                            {
                                g.PrintMessage(" This move is not legal please check your script or continue playing manualy", chessBoard);
                                movesCounter = inputMoves.Length + 1;
                     
                                s = i.getInput();
                            }
                            else
                            {
                                g.PrintMessage(" "+playerColor + ", This move is not legal please try another ", chessBoard);
                                
                                s = i.getInput();
                            }
                        }
                            
                    }
                    else
                    {
                        if (inputMoves != null && movesCounter < inputMoves.Length)
                        {
                            g.PrintMessage(" this is not a chess move please check your script, you can continue playing manualy", chessBoard);
                            movesCounter = inputMoves.Length+1;
                            
                            s = i.getInput();
                        }
                        else
                        {
                            g.PrintMessage(" "+playerColor+" this is not a chess move please try again", chessBoard);
                            
                            s = i.getInput();
                        } 
                    }
                } while (valid == false);

                pawnTransformation(player, lastMove);
                setEnPassant(!player, move);
                addToLog(chessBoard.SerilizeBoard());
                status = isGameEnded(!player);
                if (status != EndGameStatus.Normal)
                    return status;
                player = swithPlayer(player);
            } while (true);
        }
        public void startNewGame()
        {
            
            fiftyRuleCounter = 0;
            movesLog = new string[fiftyRuleMax + 1];
            EndGameStatus status;
            chessBoard = new ChessBoard(8,8);
            initializeBoard();
            g.DrawBoard(chessBoard);
            addToLog(chessBoard.SerilizeBoard());
            status = gameLogic(false, null);
            if (endGame(status))
                startNewGame();
        }

        private void initializeBoard()
        {
           
            chessBoard.InitializeBoard();
            int counter = idCounter;
            chessBoard.AddNewPiece(new Rook(false),0,0);
            chessBoard.AddNewPiece(new Knight(false), 1,0);
            chessBoard.AddNewPiece(new Bishop(false), 2, 0);
            chessBoard.AddNewPiece(new Queen(false), 3, 0);
            chessBoard.AddNewPiece(new King(false), 4, 0);
            chessBoard.AddNewPiece(new Bishop(false), 5, 0);
            chessBoard.AddNewPiece(new Knight(false), 6, 0);
            chessBoard.AddNewPiece(new Rook(false), 7, 0);
           
            for(int i=0;i<8;i++)
                chessBoard.AddNewPiece(new Pawn(false),i,1);

            chessBoard.AddNewPiece(new Rook(true), 0, 7);
            chessBoard.AddNewPiece(new Knight(true), 1, 7);
            chessBoard.AddNewPiece(new Bishop(true), 2, 7);
            chessBoard.AddNewPiece(new Queen(true), 3, 7);
            chessBoard.AddNewPiece(new King(true), 4, 7);
            chessBoard.AddNewPiece(new Bishop(true), 5, 7);
            chessBoard.AddNewPiece(new Knight(true), 6, 7);
            chessBoard.AddNewPiece(new Rook(true), 7, 7);

            for (int i = 0; i < 8; i++)
                chessBoard.AddNewPiece(new Pawn(true), i, 6);

            idCounter = counter;

        }

        private bool isRightColor(bool player,ChessPiece p)
        {
            
            return (p != null && player == p.getIsWhite());
        }

        private bool isOppositeColor(bool player, ChessPiece p)
        {
            return !isRightColor(player, p);
        }

        private void makeMove(Move m)
        {
            ChessPiece newPiece = m.GetPiece().cloneChessPiece();
            newPiece.Moved();
            chessBoard.RemovePiece(m.getColumnFrom(), m.getRowFrom());
            chessBoard.AddNewPiece(newPiece, m.getColumnTo(), m.getRowTo());
            
        }

        private void RemovePiece(int column,int row)
        {
            chessBoard.RemovePiece(column, row);
        }

        private bool isNotBlocked(Move m, MoveType type, bool player)
        {
            
            int[] direction = m.getDirInCRFormat();

            if (direction[0] != 0 || direction[1] != 0)
            {
                int dirC = direction[0];
                int dirR = direction[1];
                int cFrom = m.getColumnFrom();
                int cTo = m.getColumnTo();
                int rFrom = m.getRowFrom();
                int rTo = m.getRowTo();


                while (true)
                {
                    cFrom += dirC;
                    rFrom += dirR;
                    if (rFrom == rTo && cFrom == cTo)
                        return true;
                    if (chessBoard.GetPiece(cFrom, rFrom) != null || (type == MoveType.CastleRight || type == MoveType.CastleLeft) && isTileInCheck(player, cFrom, rFrom))
                        return false;
                }
            }
          
                
            return true;
        }

        private MoveType specailMove(ChessPiece piece,Move m,bool player)
        {
            MoveType specailMove = chooseSpecailMove(piece,m);
            if (specailMove != MoveType.NoMove)
                if ((piece.canSpecailMove(m.getColumnFrom(), m.getRowFrom(), m.getColumnTo(), m.getRowTo(), specailMove)) && (piece.getIsJump() || isNotBlocked(m,specailMove,player)))
                    return specailMove;
              

            return MoveType.NoMove;
        }

        private MoveType chooseSpecailMove(ChessPiece piece, Move m)
        {
            
            int rowNumber;
            bool white = piece.getIsWhite();
            ChessPiece p;

            
            if (m.getRowFrom() == 1 && !white || m.getRowFrom() == 6 && white)
            {
                return MoveType.DoubleJump;
            }

            
            if(lastMove!=null)
            if (lastMove.getSpecailType()==MoveType.DoubleJump)
            {
                if (lastMove.getRowTo() == m.getRowFrom())
                {
                    if (lastMove.getColumnTo() == m.getColumnTo())
                        return MoveType.EnPassant;                
                }
            }

            
           
            rowNumber=white?7:0;
            if (m.getRowFrom() == rowNumber && m.getRowTo() == rowNumber)
            {
                if (!IsInCheck(white))
                {
                    p = chessBoard.GetPiece(0, rowNumber);
                    if ((m.getColumnTo() < m.getColumnFrom()) && (p!=null)&&(!p.DidMove()))
                        return MoveType.CastleLeft; //castle left

                    p = chessBoard.GetPiece(7, rowNumber);
                    if ((m.getColumnTo() > m.getColumnFrom()) && (p != null) && (!p.DidMove()))
                        return MoveType.CastleRight;//castle right
                }
            }

            return MoveType.NoMove;        
        }

        private bool MoveToEmptySpace(ChessPiece piece, Move m,bool player)
        
        {
            return (piece.canMove(m.getColumnFrom(), m.getRowFrom(), m.getColumnTo(), m.getRowTo())) && (((piece.getIsJump()) || isNotBlocked(m,0,player)));
        }

        private bool MoveToKill(ChessPiece piece, Move m,bool player)

        {
            return (piece.canKill(m.getColumnFrom(), m.getRowFrom(), m.getColumnTo(), m.getRowTo())) && (((piece.getIsJump()) || isNotBlocked(m,MoveType.Capture,player)));
        }

        private MoveType checkMoveValidity(bool player,Move move,ChessPiece piece)
        {
            ChessPiece sourcePiece;
            MoveType sideEffect;

            if (move != null)
            {
                sourcePiece = piece;
                move.SetPiece(sourcePiece);
                if (isRightColor(player, sourcePiece))
                {
                    ChessPiece targetPiece = chessBoard.GetPiece(move.getColumnTo(), move.getRowTo());
                    if (targetPiece == null)
                    {
                        sideEffect = specailMove(sourcePiece, move, player);
                        if (sideEffect != MoveType.NoMove)
                            return sideEffect;
                        else if (MoveToEmptySpace(sourcePiece, move, player))
                            return MoveType.Regular;
                    }

                    else if (isOppositeColor(player, targetPiece))
                        if (MoveToKill(sourcePiece, move, player))
                            return MoveType.Capture;
                }
            }
            return MoveType.NoMove;
        }
        private bool tryAMove(bool player,Move sourceMove,bool simulation,ChessBoard clone)
            
        {
                bool validMove;
                MoveType sideEffect;
                ChessPiece sourcePiece;
                Move move = new Move(sourceMove);
                sourcePiece = chessBoard.GetPiece(move.getColumnFrom(), move.getRowFrom());
                sideEffect = checkMoveValidity(player, move,sourcePiece);
                validMove = sideEffect != MoveType.NoMove ? true : false;
                
            if (validMove)
            {
                move.SetPiece(sourcePiece);
                move.setSpecailType(sideEffect);
                makeMove(move);
                executeSideEffect(sideEffect, move);
                if (IsInCheck(player))
                {
                    chessBoard = new ChessBoard(clone);
                    return false;
                }
                move.setSpecailType(sideEffect);
                if (!simulation)
                {
                    if (sideEffect != MoveType.Capture || !(sourcePiece is Pawn))
                        fiftyRuleCounter++;
                    else
                    {
                        fiftyRuleCounter = 0;
                        movesLog = new string[fiftyRuleMax + 1];
                    }
                    lastMove = move;
                }

                else
                    chessBoard = new ChessBoard(clone);
                
                return true;

            }
            return false;            
        }

        private bool swithPlayer(bool player)
        {
            return !player;
        }

        private void executeSideEffect(MoveType sideEffect,Move move)
        {
            if (sideEffect == MoveType.EnPassant)
                RemovePiece(move.getColumnTo(), move.getRowFrom());


            if (sideEffect == MoveType.CastleRight)
            {
                Move m = new Move(7, move.getRowFrom(), move.getColumnTo() - 1, move.getRowFrom());
                m.SetPiece(chessBoard.GetPiece(7, move.getRowFrom()));  
                makeMove(m);
            }
            if (sideEffect == MoveType.CastleLeft)
            {
                Move m = new Move(0, move.getRowFrom(), move.getColumnTo() + 1, move.getRowFrom());
                m.SetPiece(chessBoard.GetPiece(0, move.getRowFrom()));
                makeMove(m);
            }
        }

        private void setEnPassant(bool player, Move move)
        {
            for (int i = 0; i < chessBoard.getBoardSize(); i++)
            {
                bool enPassant = move.getSpecailType() == MoveType.EnPassant;
                int pawnRow=-1;//Always takes new value if ennPassant then the value changes anyways
                if(enPassant)
                    pawnRow = player?4:3;
                ChessPiece piece;
                Pawn pawn;
                piece = chessBoard.GetPiece(i);
                if (piece != null)
                {
                    if(piece is Pawn)
                    {
                        pawn = (Pawn)piece;
                        if(enPassant&&isRightColor(player,pawn))
                        {
                            int pawnC, pawnR;
                           
                            pawnR = chessBoard.getRow(i);
                            if(pawnR == pawnRow)
                            {
                                pawnC = chessBoard.getColumn(i);
                                if (pawnC == move.getColumnTo() + 1 || pawnC == move.getColumnTo() - 1)
                                    pawn.SetEnPassantRights(true);
                                else
                                    pawn.SetEnPassantRights(false);
                            }
                            else
                            {
                                pawn.SetEnPassantRights(false);
                            }                     
                        }
                        else
                        {
                            pawn.SetEnPassantRights(false);
                        }
                    }
               
                }
            }       
        }


        private bool IsStalMate(bool player)
        {
            bool validMove;
            int boardSize = chessBoard.getBoardSize();
            ChessBoard clone =  new ChessBoard(chessBoard);
            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                    if ((i != j)&& (i == 1 && j==16))
                    {
                         validMove = isValid(player, i, j, clone);
                        if (validMove)
                            return false;
                    }
                       
            return true;
        }

        private bool isValid(bool player, int source, int target,ChessBoard clone)
        {
            Move simulation = new Move(source % 8, source / 8, target % 8, target / 8);
            return tryAMove(player, simulation, true,clone);

        }
        
        private int[] getKingCordinates(bool player)
        {
            int[] target = chessBoard.getCordinates(player?(ChessPieceNum.WhiteKing) : (ChessPieceNum.BlackKing));
            return target;
        }

        private bool IsInCheck(bool player)
        {
           int[] kingCords = getKingCordinates(player);
           return isTileInCheck(player, kingCords[0], kingCords[1]);
        }

        private bool isTileInCheck(bool player, int cFrom,int rFrom)
        {
            
            ChessPiece piece;
            int kingC = cFrom;
            int kingR = rFrom;
            for (int i = 0; i < chessBoard.getBoardSize(); i++)
            {
                piece = chessBoard.GetPiece(i);
                if (piece != null && (isOppositeColor(player,piece)))
                {
                    Move source = new Move(chessBoard.getColumn(i), chessBoard.getRow(i), kingC, kingR);
                    source.SetPiece(piece);
                    source.setSpecailType(MoveType.Capture);
                    if (MoveToKill(piece, source, player))
                        return true;
                }
            }
            return false;           
        }
        private void pawnTransformation(bool player, Move m)
        {
            if ((lastMove.getRowTo() == 0 || lastMove.getRowTo() == 7) && lastMove.GetPiece() is Pawn)
            {
                ChessPiece p = null ;
            string[] messages = new string[6];
            messages[0] = " Your pawn riched the" + (player ? "top" : "bottom") + "row";
            messages[1] = " please choose a number between 1-4";
            messages[2] = " 1-Rook";
            messages[3] = " 2-Knight";
            messages[4] = " 3-Bishop";
            messages[5] = " 4-Queen";
            g.PrintMessages(messages, chessBoard);
            
                do
                {
                    Console.Write(" ");
                    string input = Console.ReadLine();

                    if (i.checkInputForCT(input))
                    {
                        switch (input)
                        {
                            case "1":
                                p = new Rook(player);

                                break;
                            case "2":
                                p = new Knight(player);

                                break;
                            case "3":
                                p = new Bishop(player);

                                break;
                            case "4":
                                p = new Queen(player);

                                break;
                            default:
                                p = null;
                                break;
                        }
                        if (p != null)
                        {
                            Move fakeMove = new Move(m.getColumnTo(), m.getRowTo(), m.getColumnTo(), m.getRowTo());
                            fakeMove.SetPiece(p);
                            makeMove(fakeMove);
                            g.DrawBoard(chessBoard);       
                        }

                       
                    }
                    else
                        g.PrintMessage(" Wrong Input please try again", chessBoard);

                } while (p==null);
            }
            
        }

        private bool isDeadPosition() 
        {
            bool whiteBishop = false, whiteKnight = false, blackBishop = false, blackKnight = false;
            ChessPieceNum[] numBoard = chessBoard.getBoardState();
            int blackBishopLocation=-1, whiteBishopLocation=-1;
            for (int i = 0; i < chessBoard.getBoardSize(); i++)
            {
                switch (numBoard[i])
                {
                    case ChessPieceNum.NoPiece: case ChessPieceNum.WhiteKing: case ChessPieceNum.BlackKing:
                        break;
                    case ChessPieceNum.WhiteKnight:
                        if (whiteBishop || whiteKnight || blackBishop || blackKnight)
                            return false;
                        else
                            whiteKnight = true;
                        break;

                    case ChessPieceNum.WhiteBishop:
                        if (whiteKnight || blackKnight || whiteBishop)
                            return false;
                        else
                        {
                            whiteBishop = true;
                            whiteBishopLocation = i;
                        }
                        break;

                    case ChessPieceNum.BlackKnight:
                        if (whiteBishop || whiteKnight || blackBishop || blackKnight)
                            return false;
                        else
                            blackKnight = true;
                        break;

                    case ChessPieceNum.BlackBishop:
                        if (whiteKnight || blackKnight || blackBishop)
                            return false;
                        else
                        {
                            blackBishopLocation = i;
                            blackBishop = true;
                        }
                        break;
                    default:
                        return false;
                }

            }

            if (whiteBishop && blackBishop)
            {
                int blackBishopC = chessBoard.getColumn(blackBishopLocation);
                int blackBishopR = chessBoard.getRow(blackBishopLocation);
                int whiteBishopC = chessBoard.getColumn(whiteBishopLocation);
                int whiteBishopR = chessBoard.getRow(whiteBishopLocation);
                if (blackBishopC * blackBishopR % 2 == whiteBishopC * whiteBishopR % 2)
                    return true;
                else
                    return false;
            }
                   

            return true;
        
        }

        private bool differentIndexes(int[] indexes)
        {
            for (int i=0; i < indexes.Length; i++) 
                for (int j = 0; j < indexes.Length; j++)     
                    if(i!=j)
                        if (indexes[i] == indexes[j])
                            return false;
            return true;
        }

        private bool fakeBoard(int[,] cordinates,ChessPiece[] pieces,int whiteKingC,int whiteKingR, int blackKingC,int BlackKingR)
        {
            chessBoard.InitializeBoard();

            if (blackKingC != whiteKingC || BlackKingR != whiteKingR)
            {
                int counter = idCounter;
                chessBoard.AddNewPiece(new King(false), blackKingC, BlackKingR);
                chessBoard.AddNewPiece(new King(true), whiteKingC, whiteKingR);
                if (pieces != null && cordinates != null & pieces.Length == (cordinates.Length)/2)
                    for (int i = 0; i < pieces.Length; i++)
                        if (!(pieces[i] is King))
                            chessBoard.AddNewPiece(pieces[i], cordinates[0, i], cordinates[1, i]);
                idCounter = counter;
                return true;
            }
            else
                g.PrintMessage("The kings cant start at the same position", null);
            return false;
            
        }

        
        public void startTestGame(bool wait,string[] moveList,int[,] cordinates, ChessPiece[] pieces, int whiteKingC, int whiteKingR, int blackKingC, int blackKingR)
        {
            EndGameStatus status;
            chessBoard = new ChessBoard(8,8);
            if (fakeBoard(cordinates, pieces, whiteKingC, whiteKingR, blackKingC, blackKingR))
            {
                g.DrawBoard(chessBoard);
                addToLog(chessBoard.SerilizeBoard());
                if (moveList != null)
                    if (!i.isMoveList(moveList))
                        g.PrintMessage("The move list contains illegal moves", null);                   
             
                status = gameLogic(wait, moveList);
                if (endGame(status))
                    startTestGame(wait, moveList, cordinates, pieces, whiteKingC, whiteKingR, blackKingC, blackKingR);
            }
        }


       
    }
}
