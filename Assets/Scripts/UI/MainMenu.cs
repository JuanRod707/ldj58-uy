using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] Button newGameBtn;
        [SerializeField] Button exitBtn;

        void Start()
        {
            newGameBtn.onClick.AddListener(() => SceneManager.LoadScene("JuanchoTestScene"));
            exitBtn.onClick.AddListener(() => Application.Quit());
        }
    }
}
