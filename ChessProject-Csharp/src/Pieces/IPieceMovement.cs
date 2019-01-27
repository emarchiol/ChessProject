
namespace SolarWinds.MSP.Chess.Pieces
{
    public interface IPieceMovement
    {
        void Move(int newX, int newY);

        MovementType GetMovementType(int newX, int newY);

        string CurrentPositionAsString();
    }
}
