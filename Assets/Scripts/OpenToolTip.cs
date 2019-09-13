using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenToolTip : MonoBehaviour
{
    public Text ToolTip;
    public Text ToolTipTitle;
    public Text ToolTipSubtitle;
    public GameObject ToolTipPanel;

    private void Open()
    {
        ToolTipPanel.gameObject.SetActive(true);
        ToolTip.gameObject.SetActive(true);
        ToolTipTitle.gameObject.SetActive(true);
        ToolTipSubtitle.gameObject.SetActive(true);
    }

    private void Close()
    {
        ToolTipPanel.gameObject.SetActive(false);
        ToolTip.gameObject.SetActive(false);
        ToolTipTitle.gameObject.SetActive(false);
        ToolTipSubtitle.gameObject.SetActive(false);
    }

    public void OpenCose()
    {
        if (ToolTip.IsActive())
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
