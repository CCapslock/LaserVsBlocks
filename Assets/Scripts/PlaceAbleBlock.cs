using UnityEngine;

public class PlaceAbleBlock : MonoBehaviour
{
    public SingleBlock PlacedBlock;
    public int RowNum;
    public int ColumnNum;
    public bool IsBusy = false;

    //освобождает ячейку
    public void SetFree()
    {
        //Debug.Log("хозяин подарил Доби носок");
        PlacedBlock = null;
        IsBusy = false;
    }
    //заполняет ячейку
    public void SetBusy(SingleBlock Block)
    {
        //Debug.Log("oh no");
        PlacedBlock = Block;
        IsBusy = true;
    }
}
