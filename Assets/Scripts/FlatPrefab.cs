using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlatPrefab : MonoBehaviour
{

    private MyFlat _myFlat;

    public Image Image;
    public TMP_Text RoomsArea;
    public TMP_Text KorpusFloorNumber;
    public TMP_Text Price;
    public TMP_Text OldPrice;
    public TMP_Text MetrPrice;
    public TMP_Text Discount;
    
    private Button _button;
    
    public void Init(MyFlat myFlat)
    {
        _myFlat = myFlat;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        Image.sprite = _myFlat.FlatFurnitureSprite;
        RoomsArea.text = _myFlat.CountRooms + "-комнатная, " + _myFlat.Area + "м" + GameManager.Instance.SymvolQuadro;
        if(_myFlat.CountRooms == 0)
            RoomsArea.text = "Студия, " + _myFlat.Area + "м" + GameManager.Instance.SymvolQuadro;
        KorpusFloorNumber.text = _myFlat.Korpus + " корпус, " + _myFlat.Floor + " этаж, №" + _myFlat.Number;
        Price.text = GameManager.Instance.GetSplitPrice(_myFlat.Price);
        MetrPrice.text = GameManager.Instance.GetSplitPrice(_myFlat.PricePerMeter);
        OldPrice.text = "<s>" + GameManager.Instance.GetSplitPrice(_myFlat.OldPrice) + "</s>";
        Discount.text = "-" + _myFlat.Discount + "%";
    }

    private void OnClick()
    {
        GameManager.Instance.cartFlatPanel.Show(_myFlat);
    }
}
