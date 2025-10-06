using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] Button upgradeSpeed;
        [SerializeField] Button upgradeCarry;
        [SerializeField] Button upgradeCombat;
        [SerializeField] Button upgradeCapture;

        Stats stats;
        GameObject[] upgrades;

        public void Initialize(Stats stats)
        {
            this.stats = stats;

            upgrades = new[] { upgradeSpeed.gameObject, upgradeCapture.gameObject, upgradeCarry.gameObject, upgradeCombat.gameObject };

            upgradeSpeed.onClick.AddListener(() => { stats.UpgradeSpeed(); Close();});
            upgradeCarry.onClick.AddListener(() => { stats.UpgradeCarryCap(); Close();});
            upgradeCombat.onClick.AddListener(() => { stats.UpgradeStrength(); Close();});
            upgradeCapture.onClick.AddListener(() => { stats.UpgradeCapture(); Close();});
        }

        void Close()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        public void Open()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);

            foreach(var u in upgrades)
                u.SetActive(false);

            upgrades.Where(u => !u.activeInHierarchy).PickOne().SetActive(true);
            upgrades.Where(u => !u.activeInHierarchy).PickOne().SetActive(true);
        }
    }
}
