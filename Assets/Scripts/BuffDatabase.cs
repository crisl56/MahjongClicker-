using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Buff Database")]
public class BuffDatabase : ScriptableObject
{
    public List<BuffSO> AllBuffs;
    public BuffSO Find(string name) => AllBuffs.Find(b => b.Name == name);
}
