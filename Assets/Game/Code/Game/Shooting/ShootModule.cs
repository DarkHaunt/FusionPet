using Game.Code.Game.Services;
using Game.Code.Extensions;
using UnityEngine;

namespace Game.Code.Game.Shooting
{
    public class ShootModule : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        
        private GameFactory _gameFactory;

        public void Construct(GameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public async void Shoot()
        {
            var projectile = await _gameFactory.CreateProjectile(_shootPoint.position);
            projectile.SetMoveDirection(GetShootDirection());
        }

        private Vector2 GetShootDirection() =>
            Vector2Extensions.Direction(transform.position, _shootPoint.position);
    }
}