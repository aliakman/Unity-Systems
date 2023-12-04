using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class Utilities
    {
        public static Vector3 LookAtPos(Camera _cam, Vector3 _currentPos, Vector3 _lookPos)
        {
            Ray _ray = _cam.ScreenPointToRay(_lookPos);
            Plane _plane = new Plane(Vector3.up, Vector3.up * 1.5f);
            float _rayLength;

            if (_plane.Raycast(_ray, out _rayLength))
            {
                Vector3 _pointToLook = new Vector3(_ray.GetPoint(_rayLength).x, _currentPos.y, _ray.GetPoint(_rayLength).z);
                return _pointToLook;
            }

            return Vector3.zero;
        
        }

        public static async Task WaitFrame()
        {
            var currnet = Time.frameCount;

            while (currnet == Time.frameCount)
            {
                await Task.Yield();
            }

            await Task.CompletedTask;
        }

        public static void PlayAnimationClip(this Animator _animator, string _clipName)
        {
            _animator.Play(_clipName);
        }

        public static void PlayAnimationClip(this Animator _animator, string[] _clipNames)
        {
            _animator.Play(_clipNames[Random.Range(0, _clipNames.Length)]);
        }

        public static void SetAnimationTrigger(this Animator _animator, string _triggerValue)
        {
            _animator.SetTrigger(_triggerValue);
        }

        public static float GetCosAngle(Vector3 _a, Vector3 _b, Vector3 _c)
        {
            Vector3 _ba = _b - _a;
            float _cos = Vector3.Dot(_ba, _c) / Vector3.Magnitude(_ba);
            return _cos;
        }

    }
}
