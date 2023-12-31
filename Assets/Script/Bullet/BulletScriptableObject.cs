﻿using UnityEngine;

namespace Bullet
{
    [CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObject/Create New Bullet Scriptable Object")]
    public class BulletScriptableObject : ScriptableObject
    {
        public string BulletName;
        public int damage;
        public float speed;
        public BulletView BulletView;
    }
}