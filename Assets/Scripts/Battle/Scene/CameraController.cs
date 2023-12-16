#region

using UnityEngine;

#endregion

namespace Battle.Scene
{
    public class CameraController : MonoBehaviour
    {
        public Camera target;
        public float moveSpeed = 1f;
        public Vector4 screenMargins = new(0.1f, 0.1f, 0.9f, 0.9f);


        // Update is called once per frame
        private void Update()
        {
            CameraMove();
        }

        private void CameraMove()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 viewPortPos = target.ScreenToViewportPoint(mousePosition);

            if (IsOutOfScreen(viewPortPos))
                return;

            Transform cameraTransform = target.transform;
            float moveDistance = Time.deltaTime * moveSpeed;

            //左右
            if (mousePosition.x >= Screen.width * screenMargins.z) //右边界旁边
                cameraTransform.Translate(Vector3.right * moveDistance);
            else if (mousePosition.x < Screen.width * screenMargins.x) //左边界旁边
                cameraTransform.Translate(Vector3.left * moveDistance);

            //上下
            if (mousePosition.y >= Screen.height * screenMargins.w)
                cameraTransform.Translate(Vector3.up * moveDistance);
            else if (mousePosition.y < Screen.height * screenMargins.y)
                cameraTransform.Translate(Vector3.down * moveDistance);
        }

        private static bool IsOutOfScreen(Vector3 viewPortPos)
        {
            return viewPortPos.x < 0 || viewPortPos.y > 1;
        }
    }
}