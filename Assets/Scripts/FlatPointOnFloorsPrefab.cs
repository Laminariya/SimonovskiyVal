using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlatPointOnFloorsPrefab : MonoBehaviour
{
    private MyFlat _myFlat;
    private Button b_Click;
    private TMP_Text _text;

    public void Init(MyFlat myFlat)
    {
        b_Click = GetComponent<Button>();
        _text = GetComponentInChildren<TMP_Text>(true);
        
        if (myFlat == null) //|| !myFlat.IsFree
        {
            b_Click.image.color = Color.clear;
            _text.color = Color.clear;
            return;
        }
        
        _myFlat = myFlat;
        b_Click.image.color = Color.white;
        _text.color = Color.white;
        _text.text = _myFlat.CountRooms.ToString();
        b_Click.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Debug.Log("Открываем панель квартиры " + _myFlat.Floor + " " + _myFlat.Number + " " + _myFlat.NumberOnFloor);
        GameManager.Instance.cartFlatPanel.Show(_myFlat);
    }
}
