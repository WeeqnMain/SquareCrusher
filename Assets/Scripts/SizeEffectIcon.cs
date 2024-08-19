using UnityEngine.UI;

public class ExpEffectIcon : EffectIcon
{
    public override void SetVisibility(float value)
    {
        foreach (Image showImage in showImages)
        {
            showImage.fillAmount = value;
        }
    }
}