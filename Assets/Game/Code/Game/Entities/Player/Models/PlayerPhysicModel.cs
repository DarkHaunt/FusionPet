using UnityEngine;

namespace Game.Code.Game.Entities
{
    public class PlayerPhysicModel : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        
        public void EnableCollider(bool enable) =>
            _collider.enabled = enable;
    }
}