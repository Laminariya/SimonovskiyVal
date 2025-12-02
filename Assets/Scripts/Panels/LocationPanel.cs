using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPanel : MonoBehaviour
{
    public Button b_Back;
    public Button b_ChoseFlatOnParemeters;
    public Button b_Infrastructura;
    public Button b_Galereya;

    private GameManager _manager;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        b_Back.onClick.AddListener(OnBack);
        b_ChoseFlatOnParemeters.onClick.AddListener(OnChoseFlat);
        b_Galereya.onClick.AddListener(OnGalereya);
        b_Infrastructura.onClick.AddListener(OnInfrastructura);
        Hide();
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    private void OnBack()
    {
        _manager.MessageOnDemo();
        Hide();
    }

    private void OnChoseFlat()
    {
        _manager.choseFlatPanel.Show();
        Hide();
    }

    private void OnGalereya()
    {
        _manager.galereyaPanel.Show();
        Hide();
    }

    private void OnInfrastructura()
    {
        _manager.infrastructuraPanel.Show();
        Hide();
    }
}
