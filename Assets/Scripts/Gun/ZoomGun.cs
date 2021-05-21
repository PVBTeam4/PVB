using UnityEngine;

namespace Gun
{
    public class ZoomGun : MonoBehaviour
    {
        private bool _zoomed;
        private Camera _mainCamera;
        private float _defaultFov;

        [SerializeField] private float zoomSpeed;
        
        [SerializeField]
        private float zoomedFov;
        
        [SerializeField]
        private ScopeAnimation scopeAnimation;

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
            scopeAnimation.PlayZoomInAnimation();
        }

        public void ZoomOut()
        {
            // Return if already zoomed out
            if (!_zoomed) return;
            _zoomed = false;
            scopeAnimation.PlayZoomOutAnimation();
        }

        private void Update()
        {
            _mainCamera.fieldOfView = CalculateFov();
        }

        private float CalculateFov()
        {
            float currentFov = _mainCamera.fieldOfView;
            float difference = zoomSpeed * Time.deltaTime * (_zoomed ? -1 : 1);
            return Mathf.Clamp(currentFov + difference, zoomedFov, _defaultFov);
        }
    }
}
