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
    
    public Button b_Location;
    public Button b_Infrastructura;
    public Button b_Galereya;

    private Sprite _planerNot;
    private Sprite _onFloorNot;
    private Sprite _sizeNot;
    private Sprite _windowNot;
    private Sprite _windowLoad;
    private Coroutine _coroutine;
    
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
        
        b_Location.onClick.AddListener(OnLocation);
        b_Galereya.onClick.AddListener(OnGalereya);
        b_Infrastructura.onClick.AddListener(OnInfrastructura);

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
        if(_coroutine!=null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(_manager.createImagePng.LoadSpriteFromUrl(_myFlat));
        
        _manager.MessageOffAllLight();
        _manager.MessageOnFlat(_myFlat.Korpus,1,_myFlat.Number);
    }

    public void Hide()
    {
        if (_windowLoad != null)
        {
            Destroy(_windowLoad.texture);
            Destroy(_windowLoad);
        }
        if(_coroutine!=null)
            StopCoroutine(_coroutine);
        
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
        Image.sprite = _windowLoad;
    }

    private void OffAllButtons()
    {
        b_Planer.image.sprite = _planerNot;
        b_Size.image.sprite = _sizeNot;
        b_Window.image.sprite = _windowNot;
        b_OnFloor.image.sprite = _onFloorNot;
    }
    
    private void OnLocation()
    {
        _manager.locationPanel.Show();
        _manager.choseFlatPanel.Hide();
        Hide();
    }

    private void OnGalereya()
    {
        _manager.galereyaPanel.Show();
        _manager.choseFlatPanel.Hide();
        Hide();
    }

    private void OnInfrastructura()
    {
        _manager.infrastructuraPanel.Show();
        _manager.choseFlatPanel.Hide();
        Hide();
    }

    public void SetWindowLoad(Sprite sprite)
    {
        _windowLoad = sprite;
        if (b_Window.image.sprite == WindowActive)
        {
            Image.sprite = _windowLoad;
        }
    }

}
