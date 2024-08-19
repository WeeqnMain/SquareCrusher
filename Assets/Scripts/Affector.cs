using UnityEngine;

public class Affector : MonoBehaviour
{
    [SerializeField] private float fallingSpeed;
    [SerializeField] private float duration;
    [SerializeField] private EffectType effectType;
    [SerializeField] private AudioClip collectableSound;
    [SerializeField] private GameObject pickUpEffect;

    public EffectType GetEffectType => effectType;

    private void Update()
    {
        transform.Translate(fallingSpeed * Time.deltaTime * Vector2.down);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerEffects player))
        {
            player.SetEffect(effectType, duration);
            AudioManager.Instance.PlaySFX(collectableSound);
            Instantiate(pickUpEffect, transform.position, Quaternion.identity, null);
            DestroyAffector();
        }
    }

    private void DestroyAffector()
    {
        Destroy(gameObject);
    }
}

public enum EffectType
{
    SizeIncrease,
    ExperienceMultiplier,
    OneTimeExplosion,
}