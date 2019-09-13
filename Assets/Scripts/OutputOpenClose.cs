using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputOpenClose : MonoBehaviour
{
    public GameObject MainPage;
    public GameObject OutputPanel;

    public void OpenOutputPanel()
    {
        OutputPanel.SetActive(true);
    }
    public void CloseOutputPanel()
    {
        OutputPanel.SetActive(false);
    }
}
