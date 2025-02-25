using UnityEngine;
using UnityEngine.AI;

namespace _Game.GameMechanics
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovingActor : ActorComponent
    {
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;
        public bool IsMoving => _navMeshAgent.velocity.sqrMagnitude>  0.1f;
        private NavMeshAgent _navMeshAgent;
        

        public override void Initialize(BaseCharacter character)
        {
            base.Initialize(character);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = speed;
            _navMeshAgent.angularSpeed = rotationSpeed;
        }
        
        public void Move(Vector2 direction)
        {
            _navMeshAgent.Move(new Vector3(direction.x, 0, direction.y));
        }

        public void Stop()
        {
            _navMeshAgent.ResetPath();
        }
    }
}