using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void DeathEvent();

    public event DeathEvent OnDeath;
    public void Death()
    {
        OnDeath?.Invoke();
    }
}
