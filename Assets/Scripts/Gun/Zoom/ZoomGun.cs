using UnityEngine;
using UnityEngine.Events;

namespace Gun.Zoom
{
    public class ZoomGun : MonoBehaviour
    {
        private bool _zoomed;
        private Camera _mainCamera;
        private float _defaultFov;

        public bool Zoomed => _zoomed;

        [SerializeField] private float zoomSpeed;
        
        [SerializeField]
        private float zoomedFov;
        
        [SerializeField]
        private ScopeAnimation scopeAnimation;

        [SerializeField] private UnityEvent zoomingInEvent, zoomInStartEvent;
        [SerializeField] private UnityEvent zoomingOutEvent, zoomOutStartEvent, zoomOutEndEvent;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _defaultFov = _mainCamera.fieldOfView;
        }

        public void ZoomIn()
        {
            // Return if already zoomed in
            if (_zoomed) return;
            _zoomed = true;
            zoomInStartEvent?.Invoke();
            scopeAnimation.PlayZoomInAnimation();
        }

        public void ZoomOut()
        {
            // Return if already zoomed out
            if (!_zoomed) return;
            _zoomed = false;
            zoomOutStartEvent?.Invoke();
            scopeAnimation.PlayZoomOutAnimation();
        }

        private void Update()
        {
            bool zoomingOut = IsZoomingOut();
            _mainCamera.fieldOfView = CalculateFov();
            
            if (IsZoomingIn())
            {
                zoomingInEvent?.Invoke();
            }
            
            if (IsZoomingOut())
            {
                zoomingOutEvent?.Invoke();
            }
            else if (zoomingOut)
            {
                zoomOutEndEvent?.Invoke();
            }
        }

        private float CalculateFov()
        {
            float currentFov = _mainCamera.fieldOfView;
            float difference = zoomSpeed * Time.deltaTime * (_zoomed ? -1 : 1);
            return Mathf.Clamp(currentFov + difference, zoomedFov, _defaultFov);
        }

        private bool IsZoomingIn()
        {
            return _zoomed && _mainCamera.fieldOfView > zoomedFov;
        }
        
        private bool IsZoomingOut()
        {
            return !_zoomed && _mainCamera.fieldOfView < _defaultFov;
        }
    }
}
