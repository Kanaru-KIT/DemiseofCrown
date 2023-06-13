using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private ScreenInput _input;
    [SerializeField]
    private float Speed = 0.02f;
    private bool MoveFlg;
    private Vector3 pos;

    // カメラ位置の更新
    void Update()
    {
        if (_input.GetNowSwipe() != ScreenInput.SwipeDirection.NONE)
        {
            if (!MoveFlg) pos = this.transform.localPosition;
            MoveFlg = true;

            this.transform.localPosition = new Vector3(
                pos.x - _input.GetSwipeRangeVec().x * Speed,
                pos.y,
                pos.z - _input.GetSwipeRangeVec().y * Speed);
        }
        else
        {
            MoveFlg = false;
        }
    }
}
