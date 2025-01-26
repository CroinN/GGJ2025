using UnityEngine;

public class EnemyCollisionController : MonoBehaviour
{
    private const string BubbleTag = "Bubble";
    
    [SerializeField] private EnemyHealthController _enemyHealthController;
    [SerializeField] private EnemyMovement _enemyMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(BubbleTag))
        {
            Bubble bubble = other.GetComponent<Bubble>();
            int damage = bubble.Damage;
            _enemyHealthController.GetDamage(damage);
        }

        if (other.CompareTag("Freeze"))
        {
            _enemyMovement.FreezeEffect();
        }

        if (other.CompareTag("Fire"))
        {
            _enemyHealthController.FireEffect();
        }
    }
}
