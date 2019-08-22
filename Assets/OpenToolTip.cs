using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenToolTip : MonoBehaviour
{
    public Text ToolTip;
    //public Button ToolTipClose;

    public void Open()
    {
        ToolTip.gameObject.SetActive(true);
        //ToolTipClose.gameObject.SetActive(true);
    }

    public void Close()
    {
        ToolTip.gameObject.SetActive(false);
        //ToolTipClose.gameObject.SetActive(false);
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
