using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Code.Game.Services.Models
{
    public class EntityGraphic : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _destroyParticle;
        [SerializeField] private ParticleSystem _fireParticle;

        public UniTask PlayDestroyGraphics()
        {
            _spriteRenderer.enabled = false;
            _destroyParticle.Play();

            return UniTask.WaitWhile(() => _destroyParticle.isPlaying);
        }

        public void PlayFireParticle(bool play)
        {
            if(play)
                _fireParticle.Play();
            else
                _fireParticle.Stop();
        }
    }
}