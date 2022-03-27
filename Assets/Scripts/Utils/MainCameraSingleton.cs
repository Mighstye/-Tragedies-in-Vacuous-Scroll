using System;
using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class MainCameraSingleton : Singleton<MainCameraSingleton>
    {
        public void Shake()
        {
            Camera.main.DOShakePosition(1.5f, new Vector3(1,1,0));
        }
    }
}