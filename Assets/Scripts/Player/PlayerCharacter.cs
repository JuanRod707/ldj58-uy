using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private Stats playerStats;
        [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private LayerMask layerMask;

        private List<Soul> souls = new List<Soul>();
        private void Start()
        {
            //TODO move to initializer
            StartCoroutine(Detect());
        }

        public void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            if (playerInput.actions["Move"].IsPressed())
            {
                Vector3 inputVector = playerInput.actions["Move"].ReadValue<Vector2>() *
                                      (Time.fixedDeltaTime * (playerStats.Speed));
                agent.Move(new Vector3(inputVector.x, 0, inputVector.y));
            }
        }

        private IEnumerator Detect()
        {
            while (true)
            {
                if (Physics.SphereCast(transform.position, playerStats.DetectionRadius, transform.forward, out var hit, 
                        playerStats.DetectionRadius))
                { 
                    Soul newSoul = hit.collider.gameObject.GetComponent<Soul>();

                    if (newSoul is not null)
                    {
                        if (souls.Count == 0)
                        { 
                            newSoul.SetFollowing(transform);
                            souls.Add(newSoul); 
                        }
                        else if(!souls.Contains(newSoul))
                        {
                            newSoul.SetFollowing(souls.Last().transform);
                            souls.Add(newSoul);
                        } 
                    }
                   
                }
                Debug.Log("Detecting");
                yield return new WaitForSeconds(playerStats.DetectionSpeed);
            } 
        } 
    }

}
 
[System.Serializable]
public struct Stats
{
    public int CurrentSoulsAmount;
    public float Speed;
    public float DetectionRadius;
    public float DetectionSpeed;
}