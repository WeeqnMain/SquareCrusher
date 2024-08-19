using UnityEngine.UI;

public class SizeEffectIcon : EffectIcon
{
    public override void SetVisibility(float value)
    {
        foreach (Image showImage in showImages)
        {
            showImage.fillAmount = value;
        }
    }
}