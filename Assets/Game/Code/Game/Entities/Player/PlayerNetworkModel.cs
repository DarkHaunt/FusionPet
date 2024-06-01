using Fusion;
using Game.Code.Game.Services;
using Game.Code.Game.Shooting;
using UnityEditor;
using UnityEngine;

namespace Game.Code.Game.Entities
{
    [ScriptHelp(BackColor = ScriptHeaderBackColor.Olive)]
    public class PlayerNetworkModel : NetworkBehaviour
    {
        [SerializeField] private PlayerGraphic _graphic;
        [SerializeField] private ShootModule _shoot;
        [SerializeField] private PhysicMove _move;
        [SerializeField] private Rigidbody2D _rigidbody;

        private PlayerHandleService _playerHandleService;

        public Color PlayerColor =>
            Data.Color;

        [Networked] private NetworkPlayerStaticData Data { get; set; }
        [Networked] private NetworkButtons ButtonsPrevious { get; set; }
        [Networked] private int Score { get; set; }


        [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.InputAuthority | RpcTargets.StateAuthority)]
        public void RPC_NetworkDataSetUp(NetworkPlayerStaticData staticData)
        {
            Data = staticData;
            Score = 0;

            UpdateNetworkDependentData();
        }

        public override void Spawned()
        {
            if (!HasStateAuthority)
                UpdateNetworkDependentData();
        }

        public void Construct(GameFactory gameFactory)
        {
            _shoot.Construct(gameFactory);
        }

        private void UpdateNetworkDependentData()
        {
            _graphic.SetColor(Data.Color);
            _move.Construct(Data.Speed);
        }

        public override void FixedUpdateNetwork()
        {
            if (Runner.TryGetInputForPlayer(Object.InputAuthority, out PlayerInputData input))
            {
                _move.RotateToFace(input.ShootDirection);
                _move.Move(input.MoveDirection, Runner.DeltaTime);

                if (input.Buttons.WasPressed(ButtonsPrevious, PlayerButtons.Shoot) && HasStateAuthority)
                    _shoot.Shoot();

                ButtonsPrevious = input.Buttons;
            }
        }
    }
}

