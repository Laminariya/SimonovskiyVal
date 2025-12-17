using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChoseFlatOnParameterPanel : MonoBehaviour
{
    public Color ActiveColor;
    public Color NotActiveColor;
    
    public DubleSlider DubleSliderArea;
    public DubleSlider DubleSliderPrice;
    public DubleSlider DubleSliderFloor;
    public Button b_Close;
    public Button b_Reset;
    public Button b_ShowFlat;
    public Button b_St;
    public Button b_1;
    public Button b_2;
    public Button b_3;
    public Button b_4;
    public Button b_5;
    public TMP_Text MinArea;
    public TMP_Text MaxArea;
    public TMP_Text MinPrice;
    public TMP_Text MaxPrice;
    public TMP_Text MinFloor;
    public TMP_Text MaxFloor;

    public Button b_K1;
    public Button b_K2;
    public Button b_K3;

    public Button b_Ot1;
    public Button b_Ot2;
    public Button b_Ot3;

    public Transform ParentPrefabFlat;
    public GameObject PrefabFlat;
    
    public Slider Slider;
    public Scrollbar Scrollbar;
    public ScrollRect ScrollRect;

    private List<FlatPrefab> _flatPrefabs = new List<FlatPrefab>();

    private int _St;
    private int _1;
    private int _2;
    private int _3;
    private int _4;
    private int _5;

    private int _k1;
    private int _k2;
    private int _k3;

    private int _ot1;
    private int _ot2;
    private int _ot3;
    
    private float _minArea;
    private float _maxArea;
    private float _minPrice;
    private float _maxPrice;
    private int _minFloor;
    private int _maxFloor;

    public void Init()
    {
        b_St.onClick.AddListener(OnSt);
        b_1.onClick.AddListener(On1);
        b_2.onClick.AddListener(On2);
        b_3.onClick.AddListener(On3);
        b_4.onClick.AddListener(On4);
        b_5.onClick.AddListener(On5);
        b_K1.onClick.AddListener(OnK1);
        b_K2.onClick.AddListener(OnK2);
        b_K3.onClick.AddListener(OnK3);
        b_Ot1.onClick.AddListener(OnOt1);
        b_Ot2.onClick.AddListener(OnOt2);
        b_Ot3.onClick.AddListener(OnOt3);
        //b_Close.onClick.AddListener(OnClose);
        b_Reset.onClick.AddListener(OnReset);
        b_ShowFlat.onClick.AddListener(OnShowFlat);
        DubleSliderArea.Action += OnDoubleSliderArea;
        DubleSliderPrice.Action += OnDoubleSliderPrice;
        DubleSliderFloor.Action += OnDoubleSliderFloor;
        Hide();
    }

    public void ShowOnParameters(MyBuilding myBuilding, int rooms)
    {
        Show();
        //if(myBuilding.Korpus==1) OnK1();
        //if(myBuilding.Korpus==2) OnK2();
        //if(myBuilding.Korpus==3) OnK3();
        //OnOt1();
        OnOt2();
        OnOt3();
        if(rooms==0) OnSt();
        if(rooms==1) On1();
        if(rooms==2) On2();
        if(rooms==3) On3();
        if(rooms==4) On4();
        if(rooms==5) On5();

        if (rooms == -1)
        {
            OnSt();
            On1();
            On2();
            On3();
            On4();
            On5();
        }

        OnShowFlat();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnReset();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        GameManager.Instance.MessageOffAllLight();
        GameManager.Instance.MessageOnDemo();
    }

    public void OnReset()
    {
        _St = 0;
        _1 = 1;
        _2 = 2;
        _3 = 3;
        _4 = 4;
        _5 = 5;
        b_St.image.color = ActiveColor;
        b_1.image.color = ActiveColor;
        b_2.image.color = ActiveColor;
        b_3.image.color = ActiveColor;
        b_4.image.color = ActiveColor;
        b_5.image.color = ActiveColor;
        OnSt();
        On1();
        On2();
        On3();
        On4();
        On5();
        DubleSliderArea.LeftSlider.value = 0f;
        DubleSliderArea.RightSlider.value = 1f;
        DubleSliderFloor.LeftSlider.value = 0f;
        DubleSliderFloor.RightSlider.value = 1f;
        DubleSliderPrice.LeftSlider.value = 0f;
        DubleSliderPrice.RightSlider.value = 1f;
        ReloadSliders();
        _ot1 = -1;
        _ot2 = 30;
        _ot3 = 10;
        _k1 = -1;
        _k2 = -1;
        _k3 = -1;
        OnOt1();
        OnOt2();
        OnOt3();
        OnK3();
        for (int i = 0; i < _flatPrefabs.Count; i++)
        {
            Destroy(_flatPrefabs[i].gameObject);
        }

        _flatPrefabs.Clear();
    }

    private void OnShowFlat()
    {
        GameManager.Instance.MessageOffAllLight();

        for (int i = 0; i < _flatPrefabs.Count; i++)
        {
            Destroy(_flatPrefabs[i].gameObject);
        }

        _flatPrefabs.Clear();

        foreach (var building in GameManager.Instance.MyData.Buildings)
        {
            foreach (var myFlat in building.Flats)
            {
                Debug.Log(myFlat.Price +"  " + myFlat.Area + " " + myFlat.Floor + " " + myFlat.Decoration + " " + myFlat.CountRooms);
                
                if ((myFlat.CountRooms == _St || myFlat.CountRooms == _1 || myFlat.CountRooms == _2
                     || myFlat.CountRooms == _3 || myFlat.CountRooms == _4 || myFlat.CountRooms == _5)
                    && (myFlat.Korpus == _k1 || myFlat.Korpus == _k2 || myFlat.Korpus == _k3)
                    && (myFlat.Decoration == _ot1 || myFlat.Decoration == _ot2 || myFlat.Decoration == _ot3)
                    && myFlat.Area <= _maxArea && myFlat.Area >= _minArea &&
                    myFlat.Price <= _maxPrice && myFlat.Price >= _minPrice &&
                    myFlat.Floor <= _maxFloor && myFlat.Floor >= _minFloor)
                {
                    FlatPrefab flat = Instantiate(PrefabFlat, ParentPrefabFlat)
                        .GetComponent<FlatPrefab>();
                    flat.Init(myFlat);
                    _flatPrefabs.Add(flat);
                }
            }
        }
    }

    private void OnSt()
    {
        CheckResetButtons();
        if (_St==0)
        {
            b_St.image.color = NotActiveColor;
            _St = -1;
        }
        else
        {
            b_St.image.color = ActiveColor;
            _St = 0;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }

    private void On1()
    {
        CheckResetButtons();
        if (_1==1)
        {
            b_1.image.color = NotActiveColor;
            _1 = -1;
        }
        else
        {
            b_1.image.color = ActiveColor;
            _1 = 1;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }
    
    private void On2()
    {
        CheckResetButtons();
        if (_2==2)
        {
            b_2.image.color = NotActiveColor;
            _2 = -1;
        }
        else
        {
            b_2.image.color = ActiveColor;
            _2 = 2;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }
    
    private void On3()
    {
        CheckResetButtons();
        if (_3==3)
        {
            b_3.image.color = NotActiveColor;
            _3 = -1;
        }
        else
        {
            b_3.image.color = ActiveColor;
            _3 = 3;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }
    
    private void On4()
    {
        CheckResetButtons();
        if (_4==4)
        {
            b_4.image.color = NotActiveColor;
            _4 = -1;
        }
        else
        {
            b_4.image.color = ActiveColor;
            _4 = 4;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }
    
    private void On5()
    {
        CheckResetButtons();
        if (_5==5)
        {
            b_5.image.color = NotActiveColor;
            _5 = -1;
        }
        else
        {
            b_5.image.color = ActiveColor;
            _5 = 5;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }

    private void OnK1()
    {
        if (_k1==1)
        {
            b_K1.image.color = NotActiveColor;
            _k1 = -1;
        }
        else
        {
            b_K1.image.color = ActiveColor;
            _k1 = 1;
        }
        ReloadSliders();
    }
    
    private void OnK2()
    {
        if (_k2==2)
        {
            b_K2.image.color = NotActiveColor;
            _k2 = -1;
        }
        else
        {
            b_K2.image.color = ActiveColor;
            _k2 = 2;
        }
        ReloadSliders();
    }
    
    private void OnK3()
    {
        if (_k3==3)
        {
            b_K3.image.color = NotActiveColor;
            _k3 = -1;
        }
        else
        {
            b_K3.image.color = ActiveColor;
            _k3 = 3;
        }
        ReloadSliders();
    }
    
    private void OnOt1()
    {
        if (_ot1==0)
        {
            b_Ot1.image.color = NotActiveColor;
            _ot1 = -1;
        }
        else
        {
            b_Ot1.image.color = ActiveColor;
            _ot1 = 0;
        }
        ReloadSliders();
    }
    
    private void OnOt2()
    {
        if (_ot2==30)
        {
            b_Ot2.image.color = NotActiveColor;
            _ot2 = -1;
        }
        else
        {
            b_Ot2.image.color = ActiveColor;
            _ot2 = 30;
        }
        ReloadSliders();
    }
    
    private void OnOt3()
    {
        if (_ot3==10)
        {
            b_Ot3.image.color = NotActiveColor;
            _ot3 = -1;
        }
        else
        {
            b_Ot3.image.color = ActiveColor;
            _ot3 = 10;
        }
        ReloadSliders();
    }
    

    private void OnDoubleSliderArea(float value)
    {
        float max = 0;
        float min = int.MaxValue;
        foreach (var building in GameManager.Instance.MyData.Buildings)
        {
            foreach (var myFlat in building.Flats)
            {
                if ((myFlat.CountRooms == _St || myFlat.CountRooms == _1 || myFlat.CountRooms == _2
                     || myFlat.CountRooms == _3 || myFlat.CountRooms == _4 || myFlat.CountRooms == _5)
                    && myFlat.Area > max
                    && (myFlat.Korpus == _k1 || myFlat.Korpus == _k2 || myFlat.Korpus == _k3)
                    && (myFlat.Decoration == _ot1 || myFlat.Decoration == _ot2 || myFlat.Decoration == _ot3)
                    )
                {
                    max = myFlat.Area;
                }
            }

            foreach (var myFlat in building.Flats)
            {
                if ((myFlat.CountRooms == _St || myFlat.CountRooms == _1 || myFlat.CountRooms == _2
                     || myFlat.CountRooms == _3 || myFlat.CountRooms == _4 || myFlat.CountRooms == _5)
                    && myFlat.Area < min
                    && (myFlat.Korpus == _k1 || myFlat.Korpus == _k2 || myFlat.Korpus == _k3)
                    && (myFlat.Decoration == _ot1 || myFlat.Decoration == _ot2 || myFlat.Decoration == _ot3)
                    )
                {
                    min = myFlat.Area;
                }
            }
        }
        
        if (min > 1000000000) min = 0;

        float _delta = max - min;
        _minArea = min + DubleSliderArea.LeftSlider.value * _delta;
        _maxArea = max - (1 - DubleSliderArea.RightSlider.value) * _delta;
        string min1Str = Math.Round(min, 1).ToString();
        string max1Str = Math.Round(max, 1).ToString();
        string min2Str = Math.Round(_minArea, 1).ToString();
        string max2Str = Math.Round(_maxArea, 1).ToString();
        MinArea.text = min2Str;
        MaxArea.text = max2Str;

        if (min1Str != min2Str || max1Str != max2Str)
        {
            //FilterPointArea.Show(Math.Round(_minArea, 1) + "-" + Math.Round(_maxArea, 1) + "м2");
        }
        else
        {
            //FilterPointArea.Hide();
        }
        //ReloadCountFlat();
        
        //AreaRect.offsetMax+=Vector2.one;
        Canvas.ForceUpdateCanvases();
        //AreaRect.offsetMax-=Vector2.one;
    }

    private void OnDoubleSliderPrice(float value)
    {
        float max = 0;
        float min = int.MaxValue;
        foreach (var building in GameManager.Instance.MyData.Buildings)
        {
            foreach (var myFlat in building.Flats)
            {
                if ((myFlat.CountRooms == _St || myFlat.CountRooms == _1 || myFlat.CountRooms == _2
                     || myFlat.CountRooms == _3 || myFlat.CountRooms == _4 || myFlat.CountRooms == _5)
                    && myFlat.Price > max
                    && (myFlat.Korpus == _k1 || myFlat.Korpus == _k2 || myFlat.Korpus == _k3)
                    && (myFlat.Decoration == _ot1 || myFlat.Decoration == _ot2 || myFlat.Decoration == _ot3)
                    )
                {
                    max = myFlat.Price;
                }
            }

            foreach (var myFlat in building.Flats)
            {
                if ((myFlat.CountRooms == _St || myFlat.CountRooms == _1 || myFlat.CountRooms == _2
                     || myFlat.CountRooms == _3 || myFlat.CountRooms == _4 || myFlat.CountRooms == _5)
                    && myFlat.Price < min
                    && (myFlat.Korpus == _k1 || myFlat.Korpus == _k2 || myFlat.Korpus == _k3)
                    && (myFlat.Decoration == _ot1 || myFlat.Decoration == _ot2 || myFlat.Decoration == _ot3)
                    )
                {
                    min = myFlat.Price;
                }
            }
        }
        
        if (min > 1000000000) min = 0;

        float _delta = max - min;
        _minPrice = min + DubleSliderPrice.LeftSlider.value * _delta;
        _maxPrice = max - (1 - DubleSliderPrice.RightSlider.value) * _delta;
        MinPrice.text = GameManager.Instance.GetShortPrice((int)_minPrice); //Math.Round(_minPrice, 1).ToString(); //_manager.GetShortPrice()
        MaxPrice.text = GameManager.Instance.GetShortPrice((int)_maxPrice); //Math.Round(_maxPrice, 1).ToString(); //_manager.GetShortPrice()
        
        string min1Str = Math.Round(min, 1).ToString();
        string max1Str = Math.Round(max, 1).ToString();
        string min2Str = Math.Round(_minPrice, 1).ToString();
        string max2Str = Math.Round(_maxPrice, 1).ToString();
        
        if(min1Str != min2Str || max1Str != max2Str)
        { 
            // FilterPointPrice.Show(GameManager.Instance.GetShortPrice((int)_minPrice) + "-" +
            //                     GameManager.Instance.GetShortPrice((int)_maxPrice)  + "Р");
        }
        else
        {
            //FilterPointPrice.Hide();
        }
        //PriceRect.offsetMax+=Vector2.one;
        Canvas.ForceUpdateCanvases();
        //PriceRect.offsetMax-=Vector2.one;
    }
    
    private void OnDoubleSliderFloor(float value)
    {
        float max = 0;
        float min = int.MaxValue;
        foreach (var building in GameManager.Instance.MyData.Buildings)
        {
            foreach (var myFlat in building.Flats)
            {
                if ((myFlat.CountRooms == _St || myFlat.CountRooms == _1 || myFlat.CountRooms == _2
                     || myFlat.CountRooms == _3 || myFlat.CountRooms == _4 || myFlat.CountRooms == _5)
                    && myFlat.Floor > max
                    && (myFlat.Korpus == _k1 || myFlat.Korpus == _k2 || myFlat.Korpus == _k3)
                    && (myFlat.Decoration == _ot1 || myFlat.Decoration == _ot2 || myFlat.Decoration == _ot3)
                    )
                {
                    max = myFlat.Floor;
                }
            }

            foreach (var myFlat in building.Flats)
            {
                if ((myFlat.CountRooms == _St || myFlat.CountRooms == _1 || myFlat.CountRooms == _2
                     || myFlat.CountRooms == _3 || myFlat.CountRooms == _4 || myFlat.CountRooms == _5)
                    && myFlat.Floor < min
                    && (myFlat.Korpus == _k1 || myFlat.Korpus == _k2 || myFlat.Korpus == _k3)
                    && (myFlat.Decoration == _ot1 || myFlat.Decoration == _ot2 || myFlat.Decoration == _ot3)
                    )
                {
                    min = myFlat.Floor;
                }
            }
        }
        
        if (min > 1000000000) min = 0;

        float _delta = max - min;
        _minFloor = (int)(min + DubleSliderFloor.LeftSlider.value * _delta);
        _maxFloor = (int)(max - (1 - DubleSliderFloor.RightSlider.value) * _delta);
        MinFloor.text = _minFloor.ToString();
        MaxFloor.text = _maxFloor.ToString();
        
        if (_minFloor != (int)min || _maxFloor != (int)max)
        {
            //FilterPointFloor.Show(_minFloor + "-" + _maxFloor);
        }
        else
        {
            //FilterPointFloor.Hide();
        }
        
        //FloorRect.offsetMax+=Vector2.one;
        Canvas.ForceUpdateCanvases();
        //FloorRect.offsetMax-=Vector2.one;
    }
    
    public void ReloadSliders()
    {
        DubleSliderArea.Init();
        DubleSliderFloor.Init();
        DubleSliderPrice.Init();
        OnDoubleSliderArea(1f);
        OnDoubleSliderFloor(1f);
        OnDoubleSliderPrice(1f);
    }
    
    private void CheckAllOffButtons()
    {
        if (_St == -1 && _1 == -1 && _2 == -1 && _3 == -1 && _4 == -1 && _5 == -1)
        {
            _St = 9;
            _1 = 1;
            _2 = 2;
            _3 = 3;
            _4 = 4;
            _5 = 4;
        }
    }
    
    private void CheckResetButtons()
    {
        if (_St != -1 && _1 != -1 && _2 != -1 && _3 != -1 && _4 != -1 && _5 != -1)
        {
            if (b_St.image.color == ActiveColor && b_1.image.color == ActiveColor && b_2.image.color == ActiveColor &&
                b_3.image.color == ActiveColor && b_4.image.color == ActiveColor && b_5.image.color == ActiveColor) return;
            _St = -1;
            _1 = -1;
            _2 = -1;
            _3 = -1;
            _4 = -1;
            _5 = -1;
        }
    }

    public void SendMessageOnComPort()
    {
        GameManager.Instance.MessageOffAllLight();
        foreach (var prefab in _flatPrefabs)
        {
            //prefab.OnSendMessageOnComPort();
        }
    }

    public void OnSliderArea(float value)
    {
        Scrollbar.value = 1f-Slider.value;
    }

    private void Update()
    {
        if(!gameObject.activeSelf) return;
        Slider.value = 1f-Scrollbar.value;
    }
}
