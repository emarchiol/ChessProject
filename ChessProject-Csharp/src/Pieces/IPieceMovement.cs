
namespace SolarWinds.MSP.Chess.Pieces
{
    public interface IPieceMovement
    {
        void Move(int newX, int newY);

        MovementType DefineMovementType(int newX, int newY);

        string CurrentPositionAsString();
    }
}
