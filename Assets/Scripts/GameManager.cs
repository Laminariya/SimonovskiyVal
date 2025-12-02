using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    
    public GameObject LoadPanel;
    public TMP_Text LoadText;
    
    [HideInInspector] public SerializeXML serializeXML;
    [HideInInspector] public CreateMyData createMyData;
    
    [HideInInspector] public MainPanel mainPanel;
    [HideInInspector] public GalereyaPanel galereyaPanel;
    [HideInInspector] public LocationPanel locationPanel;
    [HideInInspector] public InfrastructuraPanel infrastructuraPanel;
    [HideInInspector] public ChoseFlatPanel choseFlatPanel;
    [HideInInspector] public CreateImagePNG createImagePng;
    [HideInInspector] public CartFlatPanel cartFlatPanel;
    [HideInInspector] public SendComPort sendComPort;

    [HideInInspector] public MyData MyData;
    [HideInInspector] public string SymvolQuadro = "<sup>2</sup>";
    [HideInInspector] public string SymvolRuble = "\u20BD";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        serializeXML = FindObjectOfType<SerializeXML>(true);
        createMyData = FindObjectOfType<CreateMyData>(true);
        mainPanel = FindObjectOfType<MainPanel>(true);
        galereyaPanel = FindObjectOfType<GalereyaPanel>(true);
        locationPanel = FindObjectOfType<LocationPanel>(true);
        infrastructuraPanel = FindObjectOfType<InfrastructuraPanel>(true);
        choseFlatPanel = FindObjectOfType<ChoseFlatPanel>(true);
        createImagePng = FindObjectOfType<CreateImagePNG>(true);
        cartFlatPanel = FindObjectOfType<CartFlatPanel>(true);
        sendComPort = FindObjectOfType<SendComPort>(true);

        LoadData();
    }

    private async Task LoadData()
    {
        LoadPanel.SetActive(true);
        cartFlatPanel.Init(this);
        LoadText.text = "Loading Feed" + "\r\n";
        await serializeXML.Init();
        LoadText.text += "LoadFeed Complete" + "\r\n";
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        
        createMyData.Init(this);
        
        mainPanel.Init(this);
        galereyaPanel.Init(this);
        locationPanel.Init(this);
        infrastructuraPanel.Init(this);
        choseFlatPanel.Init(this);
        sendComPort.Init();
        

        yield return StartCoroutine(createImagePng.Init(this));

        // foreach (var building in MyData.Buildings)
        // {
        //     Debug.Log(building.Flats[0].UrlFlat);
        //     Debug.Log(building.Flats[0].UrlWindows);
        //     Debug.Log(building.Flats[0].UrlFloor);
        //     Debug.Log(building.Flats[0].UrlFlatFurniture);
        // }
        LoadPanel.SetActive(false);
        MessageOnDemo();
        yield return null;
    }

    public void OnShowGalereya()
    {
        galereyaPanel.Show();
    }

    public void OnShowChoseFlat()
    {
        
    }

    public void OnShowLocation()
    {
        locationPanel.Show();
    }

    public void OnShowInfrastructura()
    {
        infrastructuraPanel.Show();
    }

    public string GetSplitPrice(int price)
    {
        string result = price.ToString();
        int count = result.Length;

        if (count > 3)
            result = result.Insert(result.Length - 3, " ");
        if(count > 6)
            result = result.Insert(result.Length - 7, " ");
        if(count > 9)
            result = result.Insert(result.Length - 11, " ");
        return result;
    }

    public string GetShortPrice(int price)
    {
        string p = (price / 1000000f).ToString();
        if(p.Length>=4)
            p = p.Substring(0, 4);
        return p;
    }

    public void MessageOnHouse(int house, int porch, bool isOn = true)
    {
        //Debug.Log(house+" " + porch);
        //HH02PP0300000000
        string str = house.ToString("X");
        if(str.Length==1) str = "0" + str;
        str += "02";
        string por = porch.ToString("X");
        if(por.Length==1) por = "0" + por;
        str += por;
        if (isOn) str += "0300000000";
        else str += "0000000000";
        Debug.Log("Mess House");
        sendComPort.AddMessage(str);
    }

    public void MessageOnFlat(int house, int porch, int flat, bool isOn = true)
    {
        //HH01FFFF03000000
        string str = house.ToString("X");
        if(str.Length==1) str = "0" + str;
        str += "01";
        string f = flat.ToString("X");
        if (f.Length == 1) f = "000" + f;
        else if (f.Length == 2) f = "00" + f;
        else if (f.Length == 3) f = "0" + f;
        if (isOn) f += "03000000";
        else f += "00000000";
        str += f;
        Debug.Log("Mess Flat");
        sendComPort.AddMessage(str);
    }

    public void MessageOnFloor(int house, int porch, int floor)
    {
        //HH03SSXX03000000
        string str = house.ToString("X");
        if(str.Length==1) str = "0" + str;
        str += "03";
        string f = floor.ToString("X");
        if (f.Length == 1) f = "0" + f;
        str += f;
        string s = porch.ToString("X");
        if (s.Length == 1) s = "0" + s;
        str += s + "03000000";
        Debug.Log("Mess Floor");
        sendComPort.AddMessage(str);
    }

    public void MessageOffAllLight()
    {
        Debug.Log("Mess OffAll");
        sendComPort.AddMessage("007F060100000000"); //Погасить всё!!!
    }

    public void MessageOnDemo()
    {
        Debug.Log("Mess Demo");
        sendComPort.AddMessage("0064010000000000"); //Включить демо!
    }

}
