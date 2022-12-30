using System;
using System.Collections.Generic;
using UnityEngine;

public class Rate : MonoBehaviour
{
    private Dictionary<string, int> values = new Dictionary<string, int> {{"p", 1}, {"r", 5}, {"n", 3}, {"b", 3}, {"q", 9}, {"k", 0}};
    
    public float calPos(string fen)
    {
        float score = 0;
        foreach (char piece in fen.Split(' ')[0])
        {
            if (values.ContainsKey(piece.ToString().ToLower()))
            {
                int diff = values[piece.ToString().ToLower()];
                if (!Char.IsUpper(piece))
                {
                    diff *= -1;
                }
                score += diff;
            }
        }
        return score;
    }

    public float calRate(string fen, int depth)
    {
        return 0;
    }
}
