using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            gameObject.GetComponent<ZombieBehavior>().Die();
        }
    }

}
