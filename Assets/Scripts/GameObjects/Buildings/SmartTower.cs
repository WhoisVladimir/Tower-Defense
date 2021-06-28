using System;
using System.Collections;
using UnityEngine;

public class SmartTower : MonoBehaviour
{
    public IDetector Detector { get; private set; }
    public IShooterable Shooter { get; private set; }
    public IAttackable Attackable { get; private set; }

    public bool IsInitialize { get; private set; }

    private void FixedUpdate()
    {
        Detector?.DetectTarget();
        Shooter?.Shoot();
    }

    public void InitializeTower(IDetector detector, IShooterable shooter, IAttackable attackable)
    {
        if (detector != null) this.Detector = detector;
        if (shooter != null) this.Shooter = shooter;
        if (attackable != null) this.Attackable = attackable;
        IsInitialize = true;
    }

    private void DrawRangeGizmo()
    {
        float range = 20f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    void OnDrawGizmos()
    {
        DrawRangeGizmo();
    }
}
