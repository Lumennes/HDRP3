using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class ViewPersonController : MonoBehaviour
{
    public bool thirdPersonView = true;
    public float viewTransitionSpeed = 10f;

    public float firstPersonCameraDistance = 0;
    public float minimalThridPresonCameraDistance = 1;
    public float maximalThridPresonCameraDistance = 10;
    public float currentThridPresonCameraDistance;

    public float mouseScrollWheelSpeed = 10;

    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] SkinnedMeshRenderer playerMesh;

    public CinemachineComponentBase componentBase;//
    public Cinemachine3rdPersonFollow thirdPersonFollow;//

    public bool inViewTransition;//



    // Start is called before the first frame update
    void Start()
    {
        componentBase = playerFollowCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is Cinemachine3rdPersonFollow)
            thirdPersonFollow = componentBase as Cinemachine3rdPersonFollow;

        currentThridPresonCameraDistance = thirdPersonFollow.CameraDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (thirdPersonView && !inViewTransition)
        {
            //TODO: скрывать меш игрока, если камера проходит скозь меш
            //if (thirdPersonFollow.CameraDistance < minimalThridPresonCameraDistance)
            //{
            //    if (playerMesh.enabled)
            //        playerMesh.enabled = false;
            //}
            //else
            //{
            //    if (!playerMesh.enabled)
            //        playerMesh.enabled = true;
            //}

            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScrollWheel > 0.1)
            {
                if (thirdPersonFollow.CameraDistance >= maximalThridPresonCameraDistance)
                {
                    thirdPersonFollow.CameraDistance = maximalThridPresonCameraDistance;
                }
                else
                {
                    thirdPersonFollow.CameraDistance += Time.deltaTime * mouseScrollWheelSpeed;/*Приближение*/
                }
                currentThridPresonCameraDistance = thirdPersonFollow.CameraDistance;
            }
            if (mouseScrollWheel < -0.1)
            {
                if (thirdPersonFollow.CameraDistance <= minimalThridPresonCameraDistance)
                {
                    thirdPersonFollow.CameraDistance = minimalThridPresonCameraDistance;
                }
                else
                {
                    thirdPersonFollow.CameraDistance -= Time.deltaTime * mouseScrollWheelSpeed;/*Отдаление*/
                }
                currentThridPresonCameraDistance = thirdPersonFollow.CameraDistance;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && !inViewTransition)
        {
            // если в третьем лице переходим к первому и наоборот
            StartCoroutine(ViewTransition(thirdPersonView));
        }

    }

    IEnumerator ViewTransition(bool toFirstPersonView)
    {
        inViewTransition = true;

        if (toFirstPersonView) // к первому лицу
        {
            while (thirdPersonFollow.CameraDistance >= firstPersonCameraDistance)
            {
                thirdPersonFollow.CameraDistance -= viewTransitionSpeed * Time.fixedDeltaTime;

                if (thirdPersonFollow.CameraDistance <= minimalThridPresonCameraDistance)
                {
                    if (playerMesh.enabled)
                        playerMesh.enabled = false;
                }

                if (thirdPersonFollow.CameraDistance <= firstPersonCameraDistance)
                {
                    thirdPersonFollow.CameraDistance = firstPersonCameraDistance;

                    EndViewTransition();
                }

                yield return new WaitForFixedUpdate();
            }
        }
        else // к третьему лицу
        {
            while (thirdPersonFollow.CameraDistance <= currentThridPresonCameraDistance)
            {
                thirdPersonFollow.CameraDistance += viewTransitionSpeed * Time.fixedDeltaTime;

                if (thirdPersonFollow.CameraDistance >= minimalThridPresonCameraDistance)
                {
                    if (!playerMesh.enabled)
                        playerMesh.enabled = true;
                }

                if (thirdPersonFollow.CameraDistance >= currentThridPresonCameraDistance)
                {
                    thirdPersonFollow.CameraDistance = currentThridPresonCameraDistance;

                    EndViewTransition();
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }

    void EndViewTransition()
    {
        thirdPersonView = !thirdPersonView;
        inViewTransition = false;
        StopAllCoroutines();
    }
}
