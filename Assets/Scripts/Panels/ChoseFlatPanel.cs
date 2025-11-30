using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoseFlatPanel : MonoBehaviour
{
    public Button b_Back;
    public Button b_Location;
    public Button b_Infrastructura;
    public Button b_Galereya;

    public Button b_ChoseFlatOnParameters;
    public Button b_OnGenplan;
    public Button b_OnPlan;

    private ChoseFlatOnParameterPanel _choseFlatOnParameterPanel;
    private OnPlanPanel _onPlanPanel;
    private OnGenplanPanel _onGenplanPanel;

    private GameManager _manager;
    
    public void Init(GameManager manager)
    {
        _manager = manager;

        _choseFlatOnParameterPanel = GetComponentInChildren<ChoseFlatOnParameterPanel>(true);
        _choseFlatOnParameterPanel.Init(manager);
        _onGenplanPanel = GetComponentInChildren<OnGenplanPanel>(true);
        _onGenplanPanel.Init(manager);
        _onPlanPanel = GetComponentInChildren<OnPlanPanel>(true);
        _onPlanPanel.Init(manager);
        
        b_Back.onClick.AddListener(OnBack);
        b_Location.onClick.AddListener(OnLocation);
        b_Galereya.onClick.AddListener(OnGalereya);
        b_Infrastructura.onClick.AddListener(OnInfrastructura);
        
        b_ChoseFlatOnParameters.onClick.AddListener(OnChoseFlayOnParameters);
        b_OnGenplan.onClick.AddListener(OnGenplan);
        b_OnPlan.onClick.AddListener(OnPlan);
        Hide();
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        OnGenplan();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _onPlanPanel.Hide();
        _choseFlatOnParameterPanel.Hide();
    }
    
    private void OnBack()
    {
        Hide();
    }

    private void OnLocation()
    {
        _manager.locationPanel.Show();
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

    private void OnChoseFlayOnParameters()
    {
        _choseFlatOnParameterPanel.Show();
        _onGenplanPanel.Hide();
        _onPlanPanel.Hide();
    }

    private void OnGenplan()
    {
        _onGenplanPanel.Show();
        _onPlanPanel.Hide();
        _choseFlatOnParameterPanel.Hide();
    }

    private void OnPlan()
    {
        _onPlanPanel.Show();
        _onGenplanPanel.Hide();
        _choseFlatOnParameterPanel.Hide();
    }
}
