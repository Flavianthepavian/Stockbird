using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragPiece : MonoBehaviour
{
    public Tilemap map;

    public Tile selected;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tileVec = map.WorldToCell(position);
            if (map.GetTile(tileVec) != null)
            {
                return;
            }
            map.ClearAllTiles();
            FEN.FieldPos pos = FEN.getFieldPos((tileVec.y - 3) * -8 + tileVec.x + 4);
            string field = Moves.getFieldNotation(pos.x, pos.y);
            List<string> moves = Moves.getMoves(FEN.pieces);
            foreach (string move in moves)
            {
                string from = move[1].ToString() + move[2].ToString();
                if (from == field)
                {
                    FEN.FieldPos placeVec = Moves.getVectorPosition(move[4].ToString() + move[5].ToString());
                    map.SetTile(new Vector3Int(placeVec.x - 4, placeVec.y + 3, 0), selected);
                }
            }
        }
    }
}
