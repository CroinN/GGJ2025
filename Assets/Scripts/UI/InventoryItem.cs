using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;

    public void SetCount(int count)
    {
        _countText.text = count.ToString();
    }
}
