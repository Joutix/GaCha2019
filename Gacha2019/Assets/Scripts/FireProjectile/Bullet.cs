using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private EEntityColor m_Color = EEntityColor.Red;

    [SerializeField]
    private int m_Damage = 1;

    protected float Speed { get => m_Speed; }

    public EEntityColor Color { get => m_Color; }

    public int Damage { get => m_Damage; }

}
