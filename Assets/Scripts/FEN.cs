using System;
using System.Collections.Generic;
using System.Text;
using Chess;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FEN : MonoBehaviour
{
    public static Dictionary<FieldPos, Piece> pieces = new Dictionary<FieldPos, Piece>();
    public Tilemap piecesMap;
    public static string fen;

    public string InputFEN;
    
    public static Side toMove;

    public static CastleRight CastleRight;

    public static string enPassant;

    public static int halfMove;

    public static int move;

    public TileBase[] pieceSources;

    public static Dictionary<string, TileBase> Bases = new Dictionary<string, TileBase>();

    public struct FieldPos
    {
        public int x;
        public int y;
        
        public FieldPos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private void Start()
    {
        foreach (TileBase i in pieceSources)
        {
            string tile = i.name;
            if (tile.Length > 1)
            {
                tile = tile.Remove(1);
                tile = tile.ToLower();
            }
            Bases.Add(tile, i);
        }
    }

    public static FieldPos getFieldPos(int index)
    {
        return new FieldPos(index % 8, (index - index % 8) / 8 * -1);
    }

    Vector3Int getVector(int index)
    {
        return new Vector3Int(index % 8, (index - index % 8) / 8 * -1, 0);
    }

    Vector3Int getVectorFromFieldPos(FieldPos fieldPos)
    {
        Vector3Int vec = new Vector3Int();
        vec.x = fieldPos.x;
        vec.y = fieldPos.y;
        return vec;
    }

    public void calFEN()
    {
        LoadFEN(InputFEN);
    }

    // Start is called before the first frame update
    public void LoadFEN(string newFen)
    {
        piecesMap.ClearAllTiles();
        fen = newFen;
        int index = 0;
        pieces = new Dictionary<FieldPos, Piece>();
        for (int i = 0; i < 64; i++)
        {
            pieces.Add(getFieldPos(i), Piece.Empty);
        }
        foreach (char c in fen.Split(' ')[0])
        {
            if ("prnbqkPRNBQK".Contains(c.ToString()))
            {
                piecesMap.SetTile(getVector(index), Bases[c.ToString()]);
                pieces[getFieldPos(index)] = Pieces.GetPiece(c.ToString());
                index += 1;
            }
            else if(c != '/')
            {
                index += Int16.Parse(c.ToString());
            }
        }

        toMove = Side.White;
        if (!fen.Split(' ')[1].Equals("w"))
        {
            toMove = Side.Black;
        }

        CastleRight = new CastleRight();
        string rights = fen.Split(' ')[2];
        
        CastleRight.WhiteQueen = rights.Contains("Q");
        CastleRight.WhiteKing = rights.Contains("K");
        CastleRight.BlackQueen = rights.Contains("q");
        CastleRight.BlackKing = rights.Contains("k");

        if (fen.Split(' ')[3] != "-")
        {
            enPassant = fen.Split(' ')[3];
        }
        
        print(fen);
        halfMove = Int32.Parse(fen.Split(' ')[4]);

        move = Int32.Parse(fen.Split(' ')[5]);
    }

    int getFieldIndex(string field)
    {
        int index = 0;

        if (field[0] == 'b')
        {
            index += 1;
        }
        else if (field[0] == 'c')
        {
            index += 2;
        }
        else if (field[0] == 'd')
        {
            index += 3;
        }
        else if (field[0] == 'e')
        {
            index += 4;
        }
        else if (field[0] == 'f')
        {
            index += 5;
        }
        else if (field[0] == 'g')
        {
            index += 6;
        }
        else if (field[0] == 'h')
        {
            index += 7;
        }
        
        if (field[1] == '7')
        {
            index += 8;
        }
        else if (field[1] == '6')
        {
            index += 16;
        }
        else if (field[1] == '5')
        {
            index += 24;
        }
        else if (field[1] == '4')
        {
            index += 32;
        }
        else if (field[1] == '3')
        {
            index += 40;
        }
        else if (field[1] == '2')
        {
            index += 48;
        }
        else if (field[1] == '1')
        {
            index += 56;
        }
        
        return index;
    }

    public static string GetFenFromDict(Dictionary<FieldPos, Piece> dict)
    {
        string fen = "";

        foreach (int y in new []{0, -1 , -2, -3, -4, -5, -6, -7})
        {
            int space = 0;
            foreach (int x in new []{0, 1, 2, 3, 4, 5, 6, 7})
            {
                if (dict[new FieldPos(x, y)] == Piece.Empty)
                {
                    space++;
                }
                else
                {
                    if (space != 0)
                    {
                        fen += space.ToString();
                    }

                    fen += Pieces.GetPieceNotation(dict[new FieldPos(x, y)]);
                    space = 0;
                }
                
            }

            if (space != 0)
            {
                fen += space.ToString();
            }
            fen += "/";
        }

        fen = fen.TrimStart('/');
        fen = fen.TrimEnd('/');
        return fen;
    }
}