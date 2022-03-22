using UnityEngine;
using Utils;

namespace Control
{
    /// <summary>
    /// Class <c>FieldBoundaries</c> will read the positions of the boundaries based on
    /// the <c>transform.position</c> of 2 <c>GameObjects</c> on class load.
    /// The two <c>GameObjects</c> should define the <c>Upper Left</c> corner and
    /// <c>Lower Right</c> corner of the movement field.</summary>
    /// <remarks>This class is a Singleton.</remarks>
    /// <remarks>Will display the boundaries when Gizmos is activated</remarks>
    public class FieldBoundaries : Singleton<FieldBoundaries>
    {
        
        public float left { get; private set; }
        public float right { get; private set; }
        public float up { get; private set; }
        public float down { get; private set; }
        
        public Vector3 hitRecoveryStartPos { get; private set; }
        
        public Vector3 hitRecoveryEndPos { get; private set; }
        [SerializeField] private GameObject upperLeft;
        [SerializeField] private GameObject lowerRight;
        [SerializeField] private GameObject hitRecoveryStartPosition;
        [SerializeField] private GameObject hitRecoveryEndPosition;

        protected override void Awake()
        {
            base.Awake();
            var ul = upperLeft.transform.position;
            var lr = lowerRight.transform.position;
            left = ul.x;
            right = lr.x;
            up = ul.y;
            down = lr.y;
            hitRecoveryEndPos = hitRecoveryEndPosition.transform.position;
            hitRecoveryStartPos = hitRecoveryStartPosition.transform.position;
            upperLeft.SetActive(false);
            lowerRight.SetActive(false);
            hitRecoveryEndPosition.SetActive(false);
            hitRecoveryStartPosition.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            var ul = new Vector3(left, up, 0);
            var ur = new Vector3(right, up, 0);
            var dl = new Vector3(left, down, 0);
            var dr = new Vector3(right, down, 0);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(ul,ur);
            Gizmos.DrawLine(ul,dl);
            Gizmos.DrawLine(ur,dr);
            Gizmos.DrawLine(dl,dr);
        }
    }
}
