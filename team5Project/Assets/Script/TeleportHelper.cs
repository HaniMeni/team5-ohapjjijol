using Unity.XR.CoreUtils;
using UnityEngine;

public class TeleportHelper : MonoBehaviour
{
    public static void TeleportPlayer(Vector3 pos, Quaternion rot)
    {
        XROrigin origin = Object.FindFirstObjectByType<XROrigin>();
        if (origin == null) return;

        //// 1️⃣ 타겟의 Yaw(= forward 방향)로 XR Origin 회전
        //float targetYaw = rot.eulerAngles.y;
        //origin.transform.rotation = Quaternion.Euler(0, targetYaw, 0);

        //// 2️⃣ XR Origin을 카메라가 pos로 오도록 이동시키기
        //origin.MoveCameraToWorldLocation(pos);


        // 1️⃣ 회전 계산 (핵심 수정 부분)
        // 목표로 하는 월드 회전값(Y축)
        float targetYaw = rot.eulerAngles.y;

        // 현재 카메라(헤드셋)가 Rig 내부에서 얼마나 돌아가 있는지(로컬 회전) 확인
        float cameraLocalYaw = origin.Camera.transform.localEulerAngles.y;

        // 공식: Origin의 각도 = 목표 각도 - 카메라가 틀어진 각도
        // 이렇게 해야 "Origin + 카메라 각도 = 목표 각도"가 되어 정면을 보게 됨
        float newOriginYaw = targetYaw - cameraLocalYaw;

        origin.transform.rotation = Quaternion.Euler(0, newOriginYaw, 0);

        // 2️⃣ XR Origin 위치 이동 (카메라가 해당 위치에 오도록)
        origin.MoveCameraToWorldLocation(pos);
    }
}
