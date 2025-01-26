using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private BubbleShooter _bubbleShooter;

    public void Shoot()
    {
        _bubbleShooter.Shoot();
    }
}
