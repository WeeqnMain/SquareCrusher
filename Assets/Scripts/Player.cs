using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerVisuals;
    [SerializeField] private TrailRenderer trailRenderer;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float distanceToGround;
    private float curDistanceToGround;

    [SerializeField] private float moveForce;
    [SerializeField] private float airMoveModificator;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip explosionSound;

    [Header("Effect Modificators")]
    [SerializeField] private float sizeModificator;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionDesctructRange;
    [SerializeField] private float explosionRange;
    public event Action Exploded;

    public int ExplosionCount {  get; private set; }   

    private bool canDash = true;
    private new Rigidbody2D rigidbody;
    private Vector2 moveDirection;
    public Vector2 Velocity {get; private set;}

    private bool isOnGround;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ExplosionCount = 0;
        SetNormalSize();
    }

    private void Update()
    {
        trailRenderer.enabled = !isOnGround;
        Velocity = rigidbody.velocity;
        HandleMovement();
    }

    private void FixedUpdate()
    {
        isOnGround = IsOnGround();
        if (isOnGround)
        {
            rigidbody.AddForce(moveForce * Time.fixedDeltaTime * moveDirection.normalized);
            canDash = true;
        }
        else
            rigidbody.AddForce(moveForce * airMoveModificator * Time.fixedDeltaTime * moveDirection.normalized);
    }

    private void HandleMovement()
    {
        moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x -= 1;
        }
        if (Input.GetKeyDown(KeyCode.W) && isOnGround)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.S) && !isOnGround && canDash)
        {
            canDash = false;
            Dash();
        }
    }

    private void Jump()
    {
        AudioManager.Instance.PlaySFX(jumpSound);
        rigidbody.AddForce(jumpForce * Time.fixedDeltaTime * Vector2.up);
    }

    private void Dash()
    {
        AudioManager.Instance.PlaySFX(dashSound);
        var randomAngle = UnityEngine.Random.Range(0.1f, 0.3f);
        Vector2 dashDirection = Vector2.Lerp(Vector2.down, moveDirection, randomAngle).normalized;
        rigidbody.AddForce(dashForce * Time.fixedDeltaTime * dashDirection);
    }

    private bool IsOnGround()
    {
        //return Physics2D.Raycast(transform.position, Vector2.down, distanceToGround, groundLayer);
        var pos = (Vector2)transform.position + Vector2.down * curDistanceToGround;
        return Physics2D.OverlapCircle(pos, transform.localScale.x/2, groundLayer);
    }

    public void SetBigSize()
    {
        transform.localScale = Vector3.one * sizeModificator;
        trailRenderer.time = 0.08f * sizeModificator;
        curDistanceToGround = distanceToGround * sizeModificator;
    }

    public void SetNormalSize()
    {
        transform.localScale = Vector3.one;
        trailRenderer.time = 0.08f;
        curDistanceToGround = distanceToGround;
    }

    public void ChargeExplosionAbulity()
    {
        ExplosionCount++;
    }

    public void Explode()
    {
        AudioManager.Instance.PlaySFX(explosionSound);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionDesctructRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Destructable destructable))
            {
                destructable.ChangeStage();
            }
        }

        colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D collider in colliders)
        {
            Vector2 direction = collider.transform.position - transform.position;
            if (collider.TryGetComponent(out Rigidbody2D rigidbody2D))
            {
                rigidbody2D.AddForce(direction * explosionForce);
            }
        }
        ExplosionCount--;
        Exploded?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        /*var pos = (Vector2)transform.position + Vector2.down * curDistanceToGround;
        Gizmos.DrawSphere(pos, transform.localScale.x / 2);
        Gizmos.DrawSphere(transform.position, explosionRange);
        Gizmos.DrawSphere(transform.position, explosionDesctructRange);*/

    }
}
