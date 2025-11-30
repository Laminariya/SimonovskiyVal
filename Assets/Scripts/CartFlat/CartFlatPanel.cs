using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartFlatPanel : MonoBehaviour
{

    private GameManager _manager;
    private MyFlat _myFlat;
    private FeaturePanel _featurePanel;

    public Button b_Back;

    public Button b_Planer;
    public Button b_OnFloor;
    public Button b_Size;
    public Button b_Window;
    public Image Image;
    public TMP_Text Price;
    public TMP_Text TypeFlatArea;
    public TMP_Text KorpusRoomNumber;
    public TMP_Text Otdelka;

    public Sprite PlanerActive;
    public Sprite OnFloorActive;
    public Sprite SizeActive;
    public Sprite WindowActive;

    private Sprite _planerNot;
    private Sprite _onFloorNot;
    private Sprite _sizeNot;
    private Sprite _windowNot;
    
    [HideInInspector] public TMP_Text CeilingHeight; //Этого нет в фиде
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        _featurePanel = GetComponentInChildren<FeaturePanel>(true);
        _featurePanel.Init();
        b_Back.onClick.AddListener(OnBack);
        b_OnFloor.onClick.AddListener(OnOnFloor);
        b_Planer.onClick.AddListener(OnPlaner);
        b_Size.onClick.AddListener(OnSize);
        b_Window.onClick.AddListener(OnWindow);

        _planerNot = b_Planer.image.sprite;
        _onFloorNot = b_OnFloor.image.sprite;
        _sizeNot = b_Size.image.sprite;
        _windowNot = b_Window.image.sprite;
        
        Hide();
    }

    public void Show(MyFlat myFlat)
    {
        gameObject.SetActive(true);
        _myFlat = myFlat;
        _featurePanel.Show(_myFlat);
        Price.text = _manager.GetSplitPrice(_myFlat.Price) + " " + _manager.SymvolRuble;
        TypeFlatArea.text = _myFlat.CountRooms + "-комнатная, " + _myFlat.Area + "м" + _manager.SymvolQuadro;
        if(_myFlat.CountRooms==0)
            TypeFlatArea.text = "Студия, " + _myFlat.Area + "м" + _manager.SymvolQuadro;
        KorpusRoomNumber.text = _myFlat.Korpus + " корпус, " + _myFlat.Floor + " этаж, №" + _myFlat.Number;
        Otdelka.text = "Отделка: " + GetDecoration();
        OnPlaner();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private string GetDecoration()
    {
        foreach (var decoration in _manager.serializeXML.FeedClass.Decorations.AllDecoration)
        {
            if (decoration.Id == _myFlat.Decoration)
                return decoration.ShortName;
        }

        return "";
    }

    private void OnBack()
    {
        Hide();
    }

    private void OnPlaner()
    {
        OffAllButtons();
        Image.sprite = _myFlat.FlatFurnitureSprite;
        b_Planer.image.sprite = PlanerActive;
    }

    private void OnOnFloor()
    {
        OffAllButtons();
        Image.sprite = _myFlat.FloorSprite;
        b_OnFloor.image.sprite = OnFloorActive;
    }

    private void OnSize()
    {
        OffAllButtons();
        Image.sprite = _myFlat.FlatSprite;
        b_Size.image.sprite = SizeActive;
    }

    private void OnWindow()
    {
        OffAllButtons();
        b_Window.image.sprite = WindowActive;
    }

    private void OffAllButtons()
    {
        b_Planer.image.sprite = _planerNot;
        b_Size.image.sprite = _sizeNot;
        b_Window.image.sprite = _windowNot;
        b_OnFloor.image.sprite = _onFloorNot;
    }

}
