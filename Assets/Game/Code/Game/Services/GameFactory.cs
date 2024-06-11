using System.Threading.Tasks;
using Code.Infrastructure.AssetManaging;
using Game.Code.Game.Projectiles;
using Game.Code.Game.StaticData;
using Game.Code.Game.Entities;
using Cysharp.Threading.Tasks;
using Game.Code.Game.Level;
using Game.Code.Game.UI;
using UnityEngine;
using Fusion;
using static Game.Code.Game.StaticData.Indents.AddressableIndents;

namespace Game.Code.Game.Services
{
    public class GameFactory
    {
        private readonly GameStaticDataProvider _dataProvider;
        private readonly AssetProvider _assetProvider;

        private readonly NetworkRunner _runner;

        private UIRoot _uiRoot;


        public GameFactory(AssetProvider assetProvider, GameStaticDataProvider dataProvider, NetworkMonoServiceLocator networkServiceLocator)
        {
            _assetProvider = assetProvider;
            _dataProvider = dataProvider;

            _runner = networkServiceLocator.Runner;
        }
        
        public async UniTask<PlayerNetworkModel> CreatePlayer(Vector2 pos, PlayerRef player, string nickName, Color color)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerNetworkModel>(PlayerAssetPath);
            var obj = await _runner.SpawnAsync(prefab, pos, Quaternion.identity, player);

            var model = obj.GetComponent<PlayerNetworkModel>();
            var staticData = _dataProvider.PlayerConfig;

            var playerData = new NetworkPlayerStaticData
            {
                Nickname = nickName,
                Color = color,
                Speed = staticData.MoveSpeed,
                Cooldown = staticData.ShootCooldown
            };
            
            model.SetNetworkData(playerData);

            return model;    
        }

        public async UniTask<EnemyNetworkModel> CreateEnemy(Vector2 pos)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<EnemyNetworkModel>(EnemyAssetPath);
            var obj = await _runner.SpawnAsync(prefab, position: pos);

            var model = obj.GetComponent<EnemyNetworkModel>();
            model.Construct(_dataProvider.EnemyConfig);

            return model;
        }

        public async UniTask<PlayerUIView> CreatePlayerUI()
        {
            var prefab = await _assetProvider.LoadAndGetComponent<PlayerUIView>(PlayerUIAssetPath);
            var obj = Object.Instantiate(prefab);

            var view = obj.GetComponent<PlayerUIView>();
            var rect = view.GetComponent<RectTransform>();
            rect.SetParent(_uiRoot.PlayerViewsContainer);

            return view;
        }

        public async UniTask<UIRoot> CreateUIRoot(Transform parent)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<UIRoot>(RootUIAssetPath);
            var root = Object.Instantiate(prefab, parent, true);

            return _uiRoot = root;
        }

        public async UniTask<ProjectileNetworkedModel> CreateProjectile(Vector2 pos, PlayerRef owner)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<ProjectileNetworkedModel>(ProjectileAssetPath);
            var obj = await _runner.SpawnAsync(prefab, position: pos, inputAuthority: owner);

            var model = obj.GetComponent<ProjectileNetworkedModel>();
            model.Construct(_dataProvider.ProjectileConfig, owner);

            return model;
        }

        public async UniTask<LevelModel> CreateLevel()
        {
            var prefab = await _assetProvider.LoadAndGetComponent<LevelModel>(LevelAssetPath);
            var obj = await _runner.SpawnAsync(prefab);

            var model = obj.GetComponent<LevelModel>();

            return model;
        }

        public async UniTask<CameraService> CreateCameraService(Camera camera)
        {
            var prefab = await _assetProvider.LoadAndGetComponent<CameraService>(CameraServiceAssetPath);
            var obj = await _runner.SpawnAsync(prefab);

            Debug.Log($"<color=white>prefab - {prefab.name}</color>");
            
            var service = obj.GetComponent<CameraService>();
            //service.Construct(camera);

            return service;
        }
    }
}