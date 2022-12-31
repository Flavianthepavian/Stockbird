using System.Collections.Generic;
using Chess;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragPiece : MonoBehaviour
{
    public Tilemap map;

    public string activePiece;

    public Tile selected;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tileVec = map.WorldToCell(position);
            FEN.FieldPos pos = FEN.getFieldPos((tileVec.y - 3) * -8 + tileVec.x + 4);
            string field = Moves.getFieldNotation(pos.x, pos.y);
            if (map.GetTile(tileVec) != null)
            {
                map.ClearAllTiles();
                Dictionary<FEN.FieldPos, Piece> board = FEN.pieces;
                board[Moves.getVectorPosition(activePiece[1].ToString() + activePiece[2].ToString())] = Piece.Empty;
                string figure = activePiece[0].ToString();
                if (FEN.toMove == Side.Black)
                {
                    figure = figure.ToLower();
                }
                board[Moves.getVectorPosition(field)] = Pieces.GetPiece(figure);
                string fen = FEN.GetFenFromDict(board);
                foreach (string part in FEN.fen.Split(' '))
                {
                    string newPart = part;
                    if (newPart.Contains("/"))
                    {
                        continue;
                    }
                    
                    if (newPart == "w")
                    {
                        newPart = "b";
                    }
                    else if (newPart == "b")
                    {
                        newPart = "w";
                    }
                    fen += " " + newPart;
                }
                GameObject.Find("Manager").GetComponent<FEN>().LoadFEN(fen);
                return;
            }
            map.ClearAllTiles();
            List<string> moves = Moves.getMoves(FEN.pieces);
            foreach (string move in moves)
            {
                string from = move[1].ToString() + move[2].ToString();
                if (from == field)
                {
                    activePiece = move[0].ToString() + move[1].ToString() + move[2].ToString();
                    FEN.FieldPos placeVec = Moves.getVectorPosition(move[4].ToString() + move[5].ToString());
                    map.SetTile(new Vector3Int(placeVec.x - 4, placeVec.y + 3, 0), selected);
                }
            }
        }
    }
}
