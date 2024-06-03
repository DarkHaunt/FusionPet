using System.Collections.Generic;
using UnityEngine;

namespace Game.Code.Game.Level
{
    public class LevelModel : MonoBehaviour
    {
        [field: Header("--- Enemies ---")]
        [field: SerializeField] public Transform LeftBottomEnemySpawnPoint { get; private set; } 
        [field: SerializeField] public Transform RightTopEnemySpawnPoint { get; private set; }
        
        [field: Header("--- Environment ---")]
        [field: SerializeField] public Transform LeftBottomCameraBorder { get; private set; } 
        [field: SerializeField] public Transform RightTopCameraBorder { get; private set; }
    }
}