using System;
using UnityEngine;

namespace CodeReverie
{
    public class MapMenuManager : MenuManager
    {
        
        Vector3 lastMousePosition;

        public float speed;
        public bool useInput2;
        private Vector3 dragOrigin;
        public SpriteRenderer mapRenderer;
        private float mapMinX, mapMaxX, mapMinY, mapMaxY;


        public float zoom;
        public float zoomMultiplier = 4f;
        public float minZoom = 2f; 
        public float maxZoom = 20f; 
        public float velocity = 0f; 
        public float smoothTime = 0.25f; 
        
        
        private void OnEnable()
        {
            // mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2;
            // mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2;
            // mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2;
            // mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2;
        }

        private void OnDisable()
        {
            CameraManager.Instance.ResetMapCamera();
        }

        private void Update()
        {
            PanCamera();
            ZoomCamera();
        }

        // private void LateUpdate()
        // {
        //
        //     if (useInput2)
        //     {
        //         MouseInputs2();
        //     }
        //     else
        //     {
        //         MouseInputs();
        //     }
        //     
        // }

        void MouseInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if(Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;

                Vector3 position = CameraManager.Instance.mapCamera.transform.position - (delta);

                CameraManager.Instance.mapCamera.transform.position = position * speed * Time.deltaTime;
                lastMousePosition = Input.mousePosition;
            }
        }
        
        
        void MouseInputs2()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = (CameraManager.Instance.mapCamera.ScreenToWorldPoint(Input.mousePosition)) -
                                    CameraManager.Instance.mapCamera.transform.position;
            }
            else if(Input.GetMouseButton(0))
            {
                Vector3 position = CameraManager.Instance.mapCamera.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;

                //Vector3 position = CameraManager.Instance.mapCamera.transform.position - (delta);

                CameraManager.Instance.mapCamera.transform.position = position;
               
            }
        }
        
        private void MoveCamera(float xInput, float zInput)
        {
            float zMove = Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * zInput - Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * xInput;
            float xMove = Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180) * zInput + Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180) * xInput;
 
            //CameraManager.Instance.mapCamera.transform.position = transform.position + new Vector3(xMove, 0, 0);
            CameraManager.Instance.mapCamera.transform.position = transform.position + new Vector3(xMove, 0, 0);
        }
        
        
        void PanCamera()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = CameraManager.Instance.mapCamera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 difference = dragOrigin - CameraManager.Instance.mapCamera.ScreenToWorldPoint(Input.mousePosition);

                //CameraManager.Instance.mapCamera.transform.position = ClampCamera(CameraManager.Instance.mapCamera.transform.position + difference) ;
                CameraManager.Instance.mapCamera.transform.position = CameraManager.Instance.mapCamera.transform.position + difference ;

            }
        }
        
        Vector3 ClampCamera(Vector3 targetPosition)
        {
            float camHeigth = CameraManager.Instance.mapCamera.orthographicSize;
            float camWidth = CameraManager.Instance.mapCamera.orthographicSize * CameraManager.Instance.mapCamera.aspect;
            float minX = mapMinX + camWidth;
            float maxX = mapMaxX - camWidth;
            float minY = mapMinY + camHeigth;
            float maxY = mapMaxY - camHeigth;


            float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
            float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

            return new Vector3(newX, newY, targetPosition.z);
            
        }

        public void ZoomCamera()
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                Debug.Log("yayayayyaya");
                
                zoom = CameraManager.Instance.mapCamera.orthographicSize;
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                zoom -= scroll * zoomMultiplier;
                zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
                CameraManager.Instance.mapCamera.orthographicSize -= scroll;
                CameraManager.Instance.mapCamera.orthographicSize = Mathf.Clamp( CameraManager.Instance.mapCamera.orthographicSize, minZoom, maxZoom);
                // CameraManager.Instance.mapCamera.orthographicSize =
                //     Mathf.SmoothDamp(CameraManager.Instance.mapCamera.orthographicSize, zoom, ref velocity, smoothTime);
            }
            
           
        }
        
    }
}