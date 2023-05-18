using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnnemyParentScript : MonoBehaviour
{
    int maxHealth = 5, currentHealth;

    Animator ennemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        ennemyAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageVal)
    {
        currentHealth -= damageVal;

        // Play Hurt Animation
        ennemyAnimator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("ennemy dead");
        ennemyAnimator.SetTrigger("Death");
    }

    public async void DestroyBody()
    {
        await Task.Delay(1000);

        Destroy(gameObject);
    }
}
