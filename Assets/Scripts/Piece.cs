namespace Chess
{
    public enum Piece
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King,
        BlackPawn,
        BlackRook,
        BlackKnight,
        BlackBishop,
        BlackQueen,
        BlackKing,
        Empty
    }
    
    public class Pieces
    {
        public static Piece GetPiece(string type)
        {
            if (type.Equals("P"))
            {
                return Piece.Pawn;
            }
            if (type.Equals("p"))
            {
                return Piece.BlackPawn;
            }
            if (type.Equals("R"))
            {
                return Piece.Rook;
            }
            if (type.Equals("r"))
            {
                return Piece.BlackRook;
            }
            if (type.Equals("N"))
            {
                return Piece.Knight;
            }
            if (type.Equals("n"))
            {
                return Piece.BlackKnight;
            }
            if (type.Equals("B"))
            {
                return Piece.Bishop;
            }
            if (type.Equals("b"))
            {
                return Piece.BlackBishop;
            }
            if (type.Equals("Q"))
            {
                return Piece.Queen;
            }
            if (type.Equals("q"))
            {
                return Piece.BlackQueen;
            }
            if (type.Equals("K"))
            {
                return Piece.King;
            }
            if (type.Equals("k"))
            {
                return Piece.BlackKing;
            }
            return Piece.Empty;
        }
        
        public static string GetPieceNotation(Piece piece)
        {
            if (piece == Piece.Pawn)
            {
                return "P";
            }
            if (piece == Piece.BlackPawn)
            {
                return "p";
            }
            if (piece == Piece.Rook)
            {
                return "R";
            }
            if (piece == Piece.BlackRook)
            {
                return "r";
            }
            if (piece == Piece.Knight)
            {
                return "N";
            }
            if (piece == Piece.BlackKnight)
            {
                return "n";
            }
            if (piece == Piece.Bishop)
            {
                return "B";
            }
            if (piece == Piece.BlackBishop)
            {
                return "b";
            }
            if (piece == Piece.Queen)
            {
                return "Q";
            }
            if (piece == Piece.BlackQueen)
            {
                return "q";
            }
            if (piece == Piece.King)
            {
                return "K";
            }
            return "k";
        }
    }
}