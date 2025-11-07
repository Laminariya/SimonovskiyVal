using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [HideInInspector] public SerializeXML serializeXML;
    [HideInInspector] public CreateMyData createMyData;
    
    [HideInInspector] public MainPanel mainPanel;
    [HideInInspector] public GalereyaPanel galereyaPanel;
    
    void Start()
    {
        serializeXML = FindObjectOfType<SerializeXML>(true);
        createMyData = FindObjectOfType<CreateMyData>(true);
        mainPanel = FindObjectOfType<MainPanel>(true);
        galereyaPanel = FindObjectOfType<GalereyaPanel>(true);

        LoadData();
    }

    private async Task LoadData()
    {
        await serializeXML.Init();

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        createMyData.Init(this);
        
        mainPanel.Init();
        galereyaPanel.Init();

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
        
    }

    public void OnShowInfrastructura()
    {
        
    }

}
