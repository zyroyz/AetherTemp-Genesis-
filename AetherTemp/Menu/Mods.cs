using System;
using System.Collections.Generic;
using System.Text;
using BepInEx;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StupidTemplate.Menu
{
    class mods
    {
        public static void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        
        public static void placeholder()
        {

        }

        public static float num = 169f;

        public static void GunSmoothNess()
        {
            if (num == 169f)
                num = 8f;  // Normal
            else if (num == 8f)
                num = 66f; // Keyboard
            else
                num = 169f; // Creamy
        }


        private static GameObject GunSphere;
        private static LineRenderer lineRenderer;
        private static float timeCounter = 0f;
        private static Vector3[] linePositions;
        private static Vector3 previousControllerPosition;

        public static void GunTemplate()
        {
            if (ControllerInputPoller.instance.rightControllerGripFloat > 0.1f || UnityInput.Current.GetMouseButton(1))
            {
                if (Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out var hitinfo))
                {
                    if (Mouse.current.rightButton.isPressed)
                    {
                        Camera cam = GameObject.Find("Shoulder Camera").GetComponent<Camera>();
                        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                        Physics.Raycast(ray, out hitinfo, 100);
                    }

                    if (GunSphere == null)
                    {
                        GunSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        GunSphere.transform.localScale = new Vector3(0f, 0f, 0f);
                        GunSphere.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                        GunSphere.GetComponent<Renderer>().material.color = Color.white;
                        GameObject.Destroy(GunSphere.GetComponent<BoxCollider>());
                        GameObject.Destroy(GunSphere.GetComponent<Rigidbody>());
                        GameObject.Destroy(GunSphere.GetComponent<Collider>());

                        lineRenderer = GunSphere.AddComponent<LineRenderer>();
                        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                        lineRenderer.widthCurve = AnimationCurve.Linear(0, 0.01f, 1, 0.01f);
                        lineRenderer.startColor = Color.white;
                        lineRenderer.endColor = Color.white;

                        linePositions = new Vector3[50];
                        for (int i = 0; i < linePositions.Length; i++)
                        {
                            linePositions[i] = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                        }
                    }

                    GunSphere.transform.position = hitinfo.point;

                    timeCounter += Time.deltaTime;

                    Vector3 pos1 = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                    Vector3 direction = (hitinfo.point - pos1).normalized;
                    float distance = Vector3.Distance(pos1, hitinfo.point);

                    Vector3 controller = pos1 - previousControllerPosition;
                    previousControllerPosition = pos1;

                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f || Mouse.current.leftButton.isPressed)
                    {
                        //you code here
                    }

                    for (int i = 0; i < linePositions.Length; i++)
                    {
                        float t = i / (float)(linePositions.Length - 1);
                        Vector3 nigmax = Vector3.Lerp(pos1, hitinfo.point, t);

                        linePositions[i] += controller * 0.5f;
                        linePositions[i] += UnityEngine.Random.insideUnitSphere * 0.01f;
                        linePositions[i] = Vector3.Lerp(linePositions[i], nigmax, Time.deltaTime * 5f);
                    }

                    lineRenderer.positionCount = linePositions.Length;
                    lineRenderer.SetPositions(linePositions);

                    float gayanalsex = Mathf.PingPong(timeCounter, 1f);
                    Color fuckingcolor = Color.Lerp(Color.white, Color.cyan, gayanalsex);
                    lineRenderer.startColor = fuckingcolor;
                    lineRenderer.endColor = fuckingcolor;
                }
            }

            if (GunSphere != null && (ControllerInputPoller.instance.rightControllerGripFloat <= 0.1f && !UnityInput.Current.GetMouseButton(1)))
            {
                GameObject.Destroy(GunSphere);
                GameObject.Destroy(lineRenderer);
                timeCounter = 0f;
                linePositions = null;
            }
        }



    }
}
