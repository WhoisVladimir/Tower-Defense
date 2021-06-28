using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooterable 
{
    public event GameObjectsInteractionDelegate OnShot;
    public void Shoot();
}
