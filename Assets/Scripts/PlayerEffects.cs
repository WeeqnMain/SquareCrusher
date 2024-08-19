using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEffects : MonoBehaviour
{
    [SerializeField] ScoreView scoreView;
    [SerializeField] EffectsView effectsView;

    private Player player;

    private Coroutine sizeCoroutine;
    private Coroutine expCoroutine;

    private void Awake()
    {
        player = GetComponent<Player>();
        player.Exploded += PlayerBombExploded;
    }

    private void PlayerBombExploded()
    {
        effectsView.RemoveBombEffect();
    }

    public void SetEffect(EffectType effectType, float duration)
    {
        switch (effectType)
        {
            case EffectType.SizeIncrease:
                if (sizeCoroutine != null)
                {
                    StopCoroutine(sizeCoroutine);
                    CancelSizeIncreaseEffect();
                }
                sizeCoroutine = StartCoroutine(SizeIncreaseRoutine(duration));
                break;
            case EffectType.ExperienceMultiplier:
                if (expCoroutine != null)
                {
                    StopCoroutine(expCoroutine);
                    CancelExperienceMultiplierEffect();
                }
                expCoroutine = StartCoroutine(ExperienceMultiplierRoutine(duration));
                break;
            case EffectType.OneTimeExplosion:
                OneTimeExplosion();
                break;
        }
    }

    private IEnumerator SizeIncreaseRoutine(float duration)
    {
        effectsView.AddEffect(EffectType.SizeIncrease, duration);
        player.SetBigSize();
        yield return new WaitForSeconds(duration);
        player.SetNormalSize();
    }

    private void CancelSizeIncreaseEffect()
    {
        player.SetNormalSize();
    }

    private IEnumerator ExperienceMultiplierRoutine(float duration)
    {
        effectsView.AddEffect(EffectType.ExperienceMultiplier, duration);
        scoreView.SetScoreDoubled();
        yield return new WaitForSeconds(duration);
        scoreView.SetScoreNormal();
    }

    private void CancelExperienceMultiplierEffect()
    {
        scoreView.SetScoreNormal();
    }

    private void OneTimeExplosion()
    {
        effectsView.AddBombEffect(EffectType.OneTimeExplosion);
        player.ChargeExplosionAbulity();
    }
}