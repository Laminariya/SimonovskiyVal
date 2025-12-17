using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoFlatsPrefab : MonoBehaviour
{
    
    public TMP_Text TypeFlat;
    public TMP_Text Price;
    public TMP_Text CountFlats;

    private MyBuilding _myBuilding;
    private int _rooms;
    private Button b_Click;

    public void Init(MyBuilding building, int rooms, GameManager manager)
    {
        _myBuilding = building;
        _rooms = rooms;

        b_Click = GetComponent<Button>();
        b_Click.onClick.AddListener(OnClick);
        TypeFlat.text = GetTypeFlat(rooms);
        Price.text = "от " + _myBuilding.GetMinShortPrice(rooms) + " млн " + manager.SymvolRuble;
        CountFlats.text = _myBuilding.GetCountFlats(rooms).ToString();
        var horizontal_s = CountFlats.transform.parent.GetComponent<HorizontalLayoutGroup>();
        horizontal_s.spacing++;
        horizontal_s.spacing--;
        Canvas.ForceUpdateCanvases();
    }

    private void OnClick()
    {
        Debug.Log("Открываем Выбор по параметрам");
        GameManager.Instance.choseFlatPanel.OnChoseFlayOnParameters();
        GameManager.Instance.choseFlatPanel._choseFlatOnParameterPanel.ShowOnParameters(_myBuilding, _rooms);
    }

    private string GetTypeFlat(int rooms)
    {
        if (rooms == 0)
        {
            return "Студия";
        }

        return rooms + "-комнатная";
    }
}
