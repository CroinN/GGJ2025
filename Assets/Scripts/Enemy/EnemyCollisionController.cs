using UnityEngine;

public class EnemyCollisionController : MonoBehaviour
{
    private const string BubbleTag = "Bubble";
    
    [SerializeField] private EnemyHealthController _enemyHealthController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(BubbleTag))
        {
            Bubble bubble = other.GetComponent<Bubble>();
            int damage = bubble.Damage;
            _enemyHealthController.GetDamage(damage);
        }
    }
}
