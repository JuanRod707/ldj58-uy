using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BattlePanel : MonoBehaviour
    {
        [SerializeField] Slider slider;

        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);

        public void UpdateBattle(float progress) => 
            slider.value = progress;
    }
}
