using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [HideInInspector] public SerializeXML serializeXML;
    [HideInInspector] public CreateMyData createMyData;
    
    void Start()
    {
        serializeXML = FindObjectOfType<SerializeXML>(true);
        createMyData = FindObjectOfType<CreateMyData>(true);

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

        yield return null;
    }

}
