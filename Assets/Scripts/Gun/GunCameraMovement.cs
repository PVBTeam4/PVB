using Gun.Zoom;
using ToolSystem.Tools;
using UnityEngine;

namespace Gun
{
    public class GunCameraMovement : MonoBehaviour
    {

        [SerializeField] private float rotationPointDistance;
        [SerializeField] private float maxLength;

        [SerializeField] private float lerpSpeed;

        private Vector3 _originCameraTarget;

        private ZoomGun _zoomGun;
        private CannonTool _cannonTool;

        private void Awake()
        {
            _zoomGun = GetComponent<ZoomGun>();
            _cannonTool = GetComponent<CannonTool>();
            _originCameraTarget = CalculateRotationPointOfCamera();
        }

        // Update is called once per frame
        void Update()
        {
            MoveCameraTowardsMouse();
        }

        private Vector3 CalculateRotationPointOfCamera()
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 direction = cameraTransform.forward.normalized;
            direction *= rotationPointDistance;
            return cameraTransform.position + direction;
        }

        private void MoveCameraTowardsMouse()
        {
            Vector2 mouseOffset = CalculateMouseOffsetFromCenter(UnityEngine.Input.mousePosition);

            Vector3 cameraTargetPosition = CalculateCameraTargetPosition(mouseOffset);

            Debug.DrawLine(Camera.main.transform.position, _originCameraTarget, Color.red);
            
            Debug.DrawLine(Camera.main.transform.position, cameraTargetPosition, Color.yellow);
            
            // Camera.main.transform.LookAt(cameraTargetPosition);

            
            Transform cameraTransform = Camera.main.transform;
            Quaternion lookOnLook =
                Quaternion.LookRotation(cameraTargetPosition - cameraTransform.position);
            
            cameraTransform.rotation =
                Quaternion.Slerp(cameraTransform.rotation, lookOnLook, Time.deltaTime * lerpSpeed);
            
        }

        private Vector2 CalculateMouseOffsetFromCenter(Vector2 mousePosition)
        {
            return new Vector2(mousePosition.x - (Screen.width * .5f), mousePosition.y - (Screen.height * .5f));
        }

        private Vector3 CalculateCameraTargetPosition(Vector2 mouseOffsetFromScreenCenter)
        {
            mouseOffsetFromScreenCenter = ClampVectorWithinRange(mouseOffsetFromScreenCenter, maxLength);
            
            if (_zooming)
            {
                mouseOffsetFromScreenCenter += _screenPointZoomOffset;
            }

            Vector3 targetPosition = _originCameraTarget + (new Vector3(mouseOffsetFromScreenCenter.x, mouseOffsetFromScreenCenter.y) * .001f);
            return targetPosition;
        }

        private Vector2 ClampVectorWithinRange(Vector2 toClamp, float range)
        {
            toClamp = new Vector2(toClamp.x, toClamp.y);
            if (toClamp.x > range)
            {
                toClamp.Set(range, toClamp.y);
            } else if (toClamp.x < -range)
            {
                toClamp.Set(-range, toClamp.y);
            }

            if (toClamp.y > range)
            {
                toClamp.Set(toClamp.x, range);
            } else if (toClamp.y < -range)
            {
                toClamp.Set(toClamp.x, -range);
            }

            return toClamp;
        }

        // Handle zooming
        
        private bool _zooming;
        private Vector3 _worldPointOfZoom;
        private Vector2 _screenPointZoomOffset;

        public void OnZoomingInEvent()
        {
            Vector2 mouseOffset = CalculateMouseOffsetFromCenter(UnityEngine.Input.mousePosition);
            mouseOffset = ClampVectorWithinRange(mouseOffset, maxLength);
            
            Vector2 screenPoint = CalculateMouseOffsetFromCenter(Camera.main.WorldToScreenPoint(_worldPointOfZoom));

            _screenPointZoomOffset = screenPoint - mouseOffset;
        }

        public void OnZoomInStartEvent()
        {
            _worldPointOfZoom = _cannonTool.GetTargetPointWithConstraints();
            _zooming = true;
        }

        public void OnZoomingOutEvent()
        {
            Vector2 mouseOffset = CalculateMouseOffsetFromCenter(UnityEngine.Input.mousePosition);
            mouseOffset = ClampVectorWithinRange(mouseOffset, maxLength);
            
            Vector2 screenPoint = CalculateMouseOffsetFromCenter(Camera.main.WorldToScreenPoint(_worldPointOfZoom));

            _screenPointZoomOffset = screenPoint - mouseOffset;
        }

        public void OnZoomOutStartEvent()
        {
            _worldPointOfZoom = _cannonTool.GetTargetPointWithConstraints();
        }
        
        public void OnZoomOutEndEvent()
        {
            _zooming = false;
        }
    }
}
