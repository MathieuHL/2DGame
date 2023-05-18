using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator playerAnimator;
    private int attackDamage = 1;

    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask ennemiesLayers;
    [SerializeField] float attackRate = 2f;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextAttackTime)
        {
            // Trigger the animation
            playerAnimator.SetTrigger("Attack");

            // Detect ennemies in range
            Collider2D[] hitEnnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, ennemiesLayers);

            // Damage them
            foreach(Collider2D ennemy in hitEnnemies)
            {
                ennemy.GetComponent<EnnemyParentScript>().TakeDamage(attackDamage);
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
