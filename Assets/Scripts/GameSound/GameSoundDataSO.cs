using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptObject/GameSoundData",fileName ="New GameSoundGroups")]
public class GameSoundDataSO : ScriptableObject
{
    List<GameSoundGroupDataSO> gameSoundGroups = new List<GameSoundGroupDataSO>();
}
