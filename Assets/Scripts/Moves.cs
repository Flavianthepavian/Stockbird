using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using Chess;
using UnityEngine;

public class Moves : MonoBehaviour
{
    public static List<string> getMoves(Dictionary<FEN.FieldPos, Piece> pieces)
    {
        List<string> moves = new List<string>();
        
        foreach (FEN.FieldPos pos in pieces.Keys)
        {
            Piece piece = pieces[pos];
            if ((FEN.toMove == Side.White && piece.ToString().StartsWith("Black")) || (FEN.toMove == Side.Black && !piece.ToString().StartsWith("Black")))
            {
                continue;
            }
            if (piece == Piece.Pawn)
            {
                // forward moves
                if (pieces[new FEN.FieldPos(pos.x, pos.y + 1)] == Piece.Empty)
                {
                    moves.Add("P" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, pos.y + 1));
                }
                if(pos.y == -6)
                {
                    if (pieces[new FEN.FieldPos(pos.x, pos.y + 1)] == Piece.Empty)
                    {
                        if (pieces[new FEN.FieldPos(pos.x, pos.y + 2)] == Piece.Empty)
                        {
                            moves.Add("P" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, pos.y + 2));
                        }
                    }
                }
                
                
                //take moves
                if (pos.x != 7)
                {
                    if (pieces[new FEN.FieldPos(pos.x + 1, pos.y + 1)].ToString().StartsWith("Black"))
                    {
                        moves.Add("P" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x + 1, pos.y + 1));
                    }
                    if (getFieldNotation(pos.x + 1, pos.y + 1) == FEN.enPassant)
                    {
                        moves.Add("P" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x + 1, pos.y + 1));
                    }
                }
                if(pos.x != 0)
                {
                    if (pieces[new FEN.FieldPos(pos.x - 1, pos.y + 1)].ToString().StartsWith("Black"))
                    {
                        moves.Add("P" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x - 1, pos.y + 1));
                    }
                    if (getFieldNotation(pos.x - 1, pos.y + 1) == FEN.enPassant)
                    {
                        moves.Add("P" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x - 1, pos.y + 1));
                    }
                }
            }
            if (piece == Piece.BlackPawn)
            {
                //forward moves
                if (pieces[new FEN.FieldPos(pos.x, pos.y - 1)] == Piece.Empty)
                {
                    moves.Add("P" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, pos.y - 1));
                }

                if (pos.y == -1)
                {
                    if (pieces[new FEN.FieldPos(pos.x, pos.y - 1)] == Piece.Empty)
                    {
                        if (pieces[new FEN.FieldPos(pos.x, pos.y - 2)] == Piece.Empty)
                        {
                            moves.Add("P" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, pos.y - 2));
                        }
                    }
                }

                //take moves
                if (pos.x != 7)
                {
                    if (!pieces[new FEN.FieldPos(pos.x + 1, pos.y - 1)].ToString().StartsWith("Black") && 
                        pieces[new FEN.FieldPos(pos.x + 1, pos.y - 1)] != Piece.Empty)
                    {
                        moves.Add("P" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x + 1, pos.y - 1));
                    }
                    if (getFieldNotation(pos.x + 1, pos.y - 1) == FEN.enPassant)
                    {
                        moves.Add("P" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x + 1, pos.y - 1));
                    }
                }
                if(pos.x != 0)
                {
                    if (!pieces[new FEN.FieldPos(pos.x - 1, pos.y - 1)].ToString().StartsWith("Black") && 
                        pieces[new FEN.FieldPos(pos.x - 1, pos.y - 1)] != Piece.Empty)
                    {
                        moves.Add("P" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x - 1, pos.y - 1));
                    }
                    if (getFieldNotation(pos.x - 1, pos.y - 1) == FEN.enPassant)
                    {
                        moves.Add("P" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x - 1, pos.y - 1));
                    }
                }
                
                //en passant
            }
            if(piece == Piece.Rook)
            {
                //up
                for (int i = pos.y + 1; i < 1; i++)
                {
                    if (pieces[new FEN.FieldPos(pos.x, i)] == Piece.Empty)
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, i));
                    }
                    else if (pieces[new FEN.FieldPos(pos.x, i)].ToString().StartsWith("Black"))
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x, i));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //down
                for (int i = pos.y - 1; i > -8; i--)
                {
                    if (pieces[new FEN.FieldPos(pos.x, i)] == Piece.Empty)
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, i));
                    }
                    else if (pieces[new FEN.FieldPos(pos.x, i)].ToString().StartsWith("Black"))
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x, i));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //right
                for (int i = pos.x + 1; i < 8; i++)
                {
                    if (pieces[new FEN.FieldPos(i, pos.y)] == Piece.Empty)
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(i, pos.y));
                    }
                    else if (pieces[new FEN.FieldPos(i, pos.y)].ToString().StartsWith("Black"))
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(i, pos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //left
                for (int i = pos.x - 1; i > -1; i--)
                {
                    if (pieces[new FEN.FieldPos(i, pos.y)] == Piece.Empty)
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(i, pos.y));
                    }
                    else if (pieces[new FEN.FieldPos(i, pos.y)].ToString().StartsWith("Black"))
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(i, pos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(piece == Piece.BlackRook)
            {
                //up
                for (int i = pos.y + 1; i < 1; i++)
                {
                    if (pieces[new FEN.FieldPos(pos.x, i)] == Piece.Empty)
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, i));
                    }
                    else if (!pieces[new FEN.FieldPos(pos.x, i)].ToString().StartsWith("Black"))
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x, i));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //down
                for (int i = pos.y - 1; i > -8; i--)
                {
                    if (pieces[new FEN.FieldPos(pos.x, i)] == Piece.Empty)
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, i));
                    }
                    else if (!pieces[new FEN.FieldPos(pos.x, i)].ToString().StartsWith("Black"))
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x, i));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //right
                for (int i = pos.x + 1; i < 8; i++)
                {
                    if (pieces[new FEN.FieldPos(i, pos.y)] == Piece.Empty)
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(i, pos.y));
                    }
                    else if (!pieces[new FEN.FieldPos(i, pos.y)].ToString().StartsWith("Black"))
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(i, pos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //left
                for (int i = pos.x - 1; i > -1; i--)
                {
                    if (pieces[new FEN.FieldPos(i, pos.y)] == Piece.Empty)
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(i, pos.y));
                    }
                    else if (!pieces[new FEN.FieldPos(i, pos.y)].ToString().StartsWith("Black"))
                    {
                        moves.Add("R" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(i, pos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            if(piece == Piece.Knight)
            {
                foreach (FEN.FieldPos newPos in new []
                         {
                             new FEN.FieldPos(pos.x + 1, pos.y + 2), new FEN.FieldPos(pos.x + 2, pos.y + 1), new FEN.FieldPos(pos.x + 2, pos.y - 1),
                             new FEN.FieldPos(pos.x + 1, pos.y - 2), new FEN.FieldPos(pos.x - 1, pos.y - 2), new FEN.FieldPos(pos.x - 2, pos.y - 1),
                             new FEN.FieldPos(pos.x - 2, pos.y + 1), new FEN.FieldPos(pos.x - 1, pos.y + 2)
                         })
                {
                    if (!pieces.ContainsKey(newPos))
                    {
                        continue;
                    }
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("N" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("N" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                    }
                }
            }
            if(piece == Piece.BlackKnight)
            {
                foreach (FEN.FieldPos newPos in new []
                         {
                             new FEN.FieldPos(pos.x + 1, pos.y + 2), new FEN.FieldPos(pos.x + 2, pos.y + 1), new FEN.FieldPos(pos.x + 2, pos.y - 1),
                             new FEN.FieldPos(pos.x + 1, pos.y - 2), new FEN.FieldPos(pos.x - 1, pos.y - 2), new FEN.FieldPos(pos.x - 2, pos.y - 1),
                             new FEN.FieldPos(pos.x - 2, pos.y + 1), new FEN.FieldPos(pos.x - 1, pos.y + 2)
                         })
                {
                    if (!pieces.ContainsKey(newPos))
                    {
                        continue;
                    }
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("N" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (!pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("N" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                    }
                }
            }
            if(piece == Piece.Bishop)
            {
                //up right
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x + 1, pos.y + 1); pieces.ContainsKey(newPos); newPos.x++, newPos.y++)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //up left
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x - 1, pos.y + 1); pieces.ContainsKey(newPos); newPos.x--, newPos.y++)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //down left
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x - 1, pos.y - 1); pieces.ContainsKey(newPos); newPos.x--, newPos.y--)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //down right
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x + 1, pos.y - 1); pieces.ContainsKey(newPos); newPos.x++, newPos.y--)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(piece == Piece.BlackBishop)
            {
                //up right
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x + 1, pos.y + 1); pieces.ContainsKey(newPos); newPos.x++, newPos.y++)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (!pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //up left
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x - 1, pos.y + 1); pieces.ContainsKey(newPos); newPos.x--, newPos.y++)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (!pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //down left
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x - 1, pos.y - 1); pieces.ContainsKey(newPos); newPos.x--, newPos.y--)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (!pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //down right
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x + 1, pos.y - 1); pieces.ContainsKey(newPos); newPos.x++, newPos.y--)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (!pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("B" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(piece == Piece.Queen)
            {
                //up right
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x + 1, pos.y + 1); pieces.ContainsKey(newPos); newPos.x++, newPos.y++)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //up left
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x - 1, pos.y + 1); pieces.ContainsKey(newPos); newPos.x--, newPos.y++)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //down left
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x - 1, pos.y - 1); pieces.ContainsKey(newPos); newPos.x--, newPos.y--)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //down right
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x + 1, pos.y - 1); pieces.ContainsKey(newPos); newPos.x++, newPos.y--)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //up
                for (int i = pos.y + 1; i < 1; i++)
                {
                    if (pieces[new FEN.FieldPos(pos.x, i)] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, i));
                    }
                    else if (pieces[new FEN.FieldPos(pos.x, i)].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x, i));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //right
                for (int i = pos.x + 1; i < 8; i++)
                {
                    if (pieces[new FEN.FieldPos(i, pos.y)] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(i, pos.y));
                    }
                    else if (pieces[new FEN.FieldPos(i, pos.y)].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(i, pos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //left
                for (int i = pos.x - 1; i > -1; i--)
                {
                    if (pieces[new FEN.FieldPos(i, pos.y)] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(i, pos.y));
                    }
                    else if (pieces[new FEN.FieldPos(i, pos.y)].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(i, pos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //down
                for (int i = pos.y - 1; i > -8; i--)
                {
                    if (pieces[new FEN.FieldPos(pos.x, i)] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, i));
                    }
                    else if (pieces[new FEN.FieldPos(pos.x, i)].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x, i));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(piece == Piece.BlackQueen)
            {
                //up right
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x + 1, pos.y + 1); pieces.ContainsKey(newPos); newPos.x++, newPos.y++)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (!pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //up left
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x - 1, pos.y + 1); pieces.ContainsKey(newPos); newPos.x--, newPos.y++)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (!pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //down left
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x - 1, pos.y - 1); pieces.ContainsKey(newPos); newPos.x--, newPos.y--)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (!pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //down right
                for (FEN.FieldPos
                     newPos = new FEN.FieldPos(pos.x + 1, pos.y - 1); pieces.ContainsKey(newPos); newPos.x++, newPos.y--)
                {
                    if (pieces[newPos] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(newPos.x, newPos.y));
                    }
                    else if (!pieces[newPos].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(newPos.x, newPos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //up
                for (int i = pos.y + 1; i < 1; i++)
                {
                    if (pieces[new FEN.FieldPos(pos.x, i)] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, i));
                    }
                    else if (!pieces[new FEN.FieldPos(pos.x, i)].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x, i));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //right
                for (int i = pos.x + 1; i < 8; i++)
                {
                    if (pieces[new FEN.FieldPos(i, pos.y)] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(i, pos.y));
                    }
                    else if (!pieces[new FEN.FieldPos(i, pos.y)].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(i, pos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                //left
                for (int i = pos.x - 1; i > -1; i--)
                {
                    if (pieces[new FEN.FieldPos(i, pos.y)] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(i, pos.y));
                    }
                    else if (!pieces[new FEN.FieldPos(i, pos.y)].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(i, pos.y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //down
                for (int i = pos.y - 1; i > -8; i--)
                {
                    if (pieces[new FEN.FieldPos(pos.x, i)] == Piece.Empty)
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "-" + getFieldNotation(pos.x, i));
                    }
                    else if (!pieces[new FEN.FieldPos(pos.x, i)].ToString().StartsWith("Black"))
                    {
                        moves.Add("Q" + getFieldNotation(pos.x, pos.y) + "x" + getFieldNotation(pos.x, i));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (piece == Piece.King)
            {
                foreach (FEN.FieldPos newPos in new []
                         {
                             new FEN.FieldPos(pos.x, pos.y + 1), new FEN.FieldPos(pos.x + 1, pos.y + 1), new FEN.FieldPos(pos.x + 1, pos.y),
                             new FEN.FieldPos(pos.x, pos.y + 1)
                             
                         })
                {
                    
                }
            }
        }
        return moves;
    }

    static string getFieldNotation(int x, int y)
    {
        string field = "";
        if (x == 0)
        {
            field += "a";
        }

        if (x == 1)
        {
            field += "b";
        }

        if (x == 2)
        {
            field += "c";
        }

        if (x == 3)
        {
            field += "d";
        }

        if (x == 4)
        {
            field += "e";
        }

        if (x == 5)
        {
            field += "f";
        }

        if (x == 6)
        {
            field += "g";
        }

        if (x == 7)
        {
            field += "h";
        }

        field += (y + 8).ToString();

        return field;
    }
}











