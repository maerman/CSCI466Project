// written by: Metin Erman
// tested by: Michael Quinn
// debugged by: Metin Erman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class closeErrorPanel : MonoBehaviour {

    public Button myBtn;
    public CanvasGroup canvasGrp;
    public GameObject errorPanel;

    public void closeErrPanel ()
    {
        canvasGrp.DOFade(0.0f, 2.0f);
        errorPanel.SetActive(false);
       
    }
}
