using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public PlayerMove player;

    public void DashEnd() { player.DashEnd(); }
}
