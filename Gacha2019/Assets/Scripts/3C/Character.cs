using UnityEngine;

public class Character : MonoBehaviour
{
    #region Public Methods
    public void TryMove(int _DeltaRow, int _DeltaColumn)
    {
        int rowDest = m_CurrentRow + _DeltaRow;
        int columnDest = m_CurrentColumn + _DeltaColumn;
        if (IsValidDestination(rowDest, columnDest))
        {
            MoveTo(rowDest, columnDest);
        }
    }

    public void Shoot()
    {

    }
    #endregion

    #region Private Methods
    private bool IsValidDestination(int _RowDestination, int _ColumnDest)
    {
        throw new System.NotImplementedException();
    }

    private void MoveTo(int _RowDestination, int _ColumnDestination)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Attributes
    private int m_CurrentRow = 0;
    private int m_CurrentColumn = 0;
    #endregion
}
