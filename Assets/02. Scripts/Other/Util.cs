using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// 리스트 중간에 삭제할 요소에 마지막 요소를 덮어쓰고 마지막 요소를 삭제하는 메서드.
    /// 리스트의 중간이 아닌, 마지막을 제거함으로 요소 삭제 후 재졍렬을 최소화.
    /// 리스트의 순서 보장이 중요할 때에는 사용할 수 없음.
    /// </summary>
    /// <param name="list">삭제할 list</param>
    /// <param name="removeIndex">삭제할 index</param>
    public static void RemoveListAt<T>(this List<T> list, int removeIndex)
    {
        int last = list.Count - 1;
        list[removeIndex] = list[last];
        list.RemoveAt(last);
    }


    /// <summary>
    /// 한 점에서 랜덤의 각도로 특정 거리만큼 떨어진 지점을 반환하는 메서드
    /// </summary>
    /// <param name="center">중심 점</param>
    /// <param name="radius">특정 거리</param>
    public static Vector2 RandomPointOnCircle(this Vector2 center, float radius)
    {
        Vector2 randNormal = Random.insideUnitCircle.normalized;
        return center + randNormal * radius;
    }
    /// <summary>
    /// 한 점에서 랜덤의 각도로 특정 거리만큼 떨어진 지점을 반환하는 메서드
    /// </summary>
    /// <param name="center">중심 점</param>
    /// <param name="radius">특정 거리</param>
    public static Vector2 RandomPointOnCircle(this Vector3 center, float radius)
    {
        return RandomPointOnCircle((Vector2)center, radius);
    }


    /// <summary>
    /// center에서 end로 가는 방향벡터를 angle값 float로 반환해주는 메서드.
    /// </summary>
    /// <param name="startPosition">시작할 지점</param>
    /// <param name="endPosition">도착할 지점</param>
    public static float ComputeAngle2D(this Vector2 startPosition, Vector2 endPosition)
    {
        Vector2 dir = endPosition - startPosition;
        float angleRad = Mathf.Atan2(dir.y, dir.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;

        return angleDeg;
    }
    /// <summary>
    /// center에서 end로 가는 방향벡터를 angle값 float로 반환해주는 메서드.
    /// </summary>
    /// <param name="startPosition">시작할 지점</param>
    /// <param name="endPosition">도착할 지점</param>
    public static float ComputeAngle2D(this Vector3 startPosition, Vector3 endPosition)
    {
        Vector2 tempCenterPosition = startPosition;
        Vector2 tempEndPosition = endPosition;
        return tempCenterPosition.ComputeAngle2D(tempEndPosition);
    }
}
