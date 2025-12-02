using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{

    public Button b_ChoseFlat;
    public Button b_Raspoolozhenie;
    public Button b_Infrastrucktura;
    public Button b_Galereya;

    public Button b_Kurer;
    public Button b_Car;

    private GameManager _manager;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        b_ChoseFlat.onClick.AddListener(OnChoseFlat);
        b_Raspoolozhenie.onClick.AddListener(OnRaspoolozhenie);
        b_Infrastrucktura.onClick.AddListener(OnInfrastrucktura);
        b_Galereya.onClick.AddListener(OnGalereya);
        
        b_Kurer.onClick.AddListener(OnKurier);
        b_Car.onClick.AddListener(OnCar);
    }

    private void OnChoseFlat()
    {
        _manager.choseFlatPanel.Show();
    }

    private void OnRaspoolozhenie()
    {
        _manager.locationPanel.Show();
    }

    private void OnInfrastrucktura()
    {
        _manager.infrastructuraPanel.Show();
    }

    private void OnGalereya()
    {
        _manager.galereyaPanel.Show();
    }

    private void OnKurier()
    {
        Debug.Log("Mess Kurier");
        _manager.sendComPort.AddMessage("000B0011FF551100"); //Погасить всё!!!
    }

    private void OnCar()
    {
        Debug.Log("Mess Car");
        _manager.sendComPort.AddMessage("000C0011FF551100"); //Погасить всё!!!
    }


}
