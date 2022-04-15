using UnityEngine;

namespace RollicDeveloperCase.Player
{
    public class PlayerController : SingletonNonPersistant<PlayerController>
    {
        [SerializeField] private GameObject collectorAsistant;       

        public override void Awake()
        {
            base.Awake();
        }
        private void OnEnable()
        {
            PlayerCollisionController.OnHitTriggerColletctorAsistant += OpenCollectorAsistants;
            PlayerCollisionController.OnHitElevator += CloseCollectorAsistants;
        }
        private void OnDisable()
        {
            PlayerCollisionController.OnHitTriggerColletctorAsistant -= OpenCollectorAsistants;
            PlayerCollisionController.OnHitElevator -= CloseCollectorAsistants;
        }

        private void OpenCollectorAsistants()
        {
            collectorAsistant.SetActive(true);
        }

        private void CloseCollectorAsistants()
        {
            collectorAsistant.SetActive(false);
        }

    }
}