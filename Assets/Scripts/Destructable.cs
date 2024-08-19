using System;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private float velocityToBreak;
    [SerializeField] private float velocityDownToBreak;
    [SerializeField] private Color[] colors;
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private GameObject[] stages;

    [Header("Sounds")]
    [SerializeField] private AudioClip destructSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject destructEffect;
    [SerializeField] private GameObject explosionEffect;

    private int currentStage = 0;

    public event Action<bool> Destroyed;

    private void Awake()
    {
        PickColor();
    }

    private void PickColor()
    {
        var rand = UnityEngine.Random.Range(0, colors.Length);
        foreach (var sprite in sprites)
        {
            sprite.color = colors[rand];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            if (player.Velocity.magnitude > velocityToBreak && player.Velocity.y < -velocityDownToBreak)
            {
                if (player.ExplosionCount > 0)
                {
                    player.Explode();
                    Instantiate(explosionEffect, collision.contacts[0].point, Quaternion.identity, null);
                }
                

                ChangeStage();
            }
        }
    }

    public void ChangeStage()
    {
        currentStage++;
        if (currentStage >= stages.Length)
        {
            AudioManager.Instance.PlaySFX(destructSound);
            Break();
        }
        else
        {
            AudioManager.Instance.PlaySFX(hitSound);
            stages[currentStage - 1].SetActive(false);
            stages[currentStage].SetActive(true);
        }
    }

    private void Break()
    {
        Destroyed?.Invoke(stages.Length > 1);
        var effect = Instantiate(destructEffect, transform.position, Quaternion.identity, null);
        effect.GetComponent<ParticleSystem>().startColor = sprites[0].color;
        gameObject.SetActive(false);
    }
}
