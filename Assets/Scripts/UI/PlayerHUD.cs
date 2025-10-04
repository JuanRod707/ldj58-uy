using TMPro;
using Unity.AI.Navigation;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soulsAmount;
    [SerializeField] private TextMeshProUGUI speedMultiplier;

    public void SetSoulsAmount(string amount) => soulsAmount.text = amount;
    public void SetSpeedMultiplier(string mult) => speedMultiplier.text = mult;
}
