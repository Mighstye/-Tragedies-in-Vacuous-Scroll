using System;
using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class MainCameraSingleton : Singleton<MainCameraSingleton>
    {
        public void Shake()
        {
            Camera.current.DOShakePosition(1.5f, Vector3.one);
        }
    }
}