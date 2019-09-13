using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenToolTip : MonoBehaviour
{
    public Text ToolTip;

    private void Open()
    {
        ToolTip.gameObject.SetActive(true);
    }

    private void Close()
    {
        ToolTip.gameObject.SetActive(false);
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
