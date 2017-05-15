using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsableButtonItem : MonoBehaviour
{
    private RDC.Animations.AlterSize[] alterSizeAnims;

    public TMPro.TMP_Text header;
    public TMPro.TMP_Text caption;

    public void Init()
    {
        alterSizeAnims = GetComponentsInChildren<RDC.Animations.AlterSize>();
    }

    public void SetCaption(string caption)
    {
        this.caption.text = caption;
    }

    public void SetHeader(string header)
    {
        this.header.text = header;
    }

    public void StartOpeningAnimation()
    {
        alterSizeAnims[0].Animate();
    }

    public void StartClosingAnimation()
    {
        alterSizeAnims[1].Animate();
    }
}
