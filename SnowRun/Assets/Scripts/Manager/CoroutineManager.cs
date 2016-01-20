using UnityEngine;
using System.Collections;

public static class CoroutineManager
{
    /// <summary>
    /// USE WITH START_COROUTINE!!!
    /// </summary>
    public static IEnumerator ObjMove(GameObject go, Vector3 startPos, Vector3 endPos, float time, bool isScaled = true, bool isLocalMove = false, GameObject messageObj = null, string methodName = null)
    {
        yield return new WaitForEndOfFrame();
        float elapseTime = 0.0f;

        while (elapseTime < time)
        {
            elapseTime += Time.deltaTime;
            Vector3 Temp;
            if (elapseTime >= time)
                elapseTime = time;

            Temp = startPos + (endPos - startPos) * (elapseTime / time);
            if (isLocalMove)
                go.transform.localPosition = Temp;
            else
                go.transform.position = Temp;



            yield return new WaitForEndOfFrame();
        }
        if (messageObj != null)
        {
            messageObj.SendMessage(methodName);
        }
    }
    public static IEnumerator LerpMove(GameObject go, Vector3 startPos, Vector3 endPos, float speed, bool isScaled = true, bool isLocalMove = false, GameObject messageObj = null, string methodName = null)
    {
        yield return new WaitForEndOfFrame();
        float elapseTime = 0.0f;

        Vector3 movedDistance;  // 이전에 이동한 거리
        Vector3 prevPos;
        while (true)
        {
            if (isScaled)
            {
                elapseTime += Time.deltaTime * speed;
            }
            else
            {
                elapseTime += Time.unscaledDeltaTime * speed;
            }
            if (isLocalMove)
            {
                prevPos = go.transform.localPosition;
                go.transform.localPosition = Vector3.Lerp(startPos, endPos, elapseTime);
                movedDistance = go.transform.localPosition - prevPos;
            }
            else
            {
                prevPos = go.transform.position;
                go.transform.position = Vector3.Lerp(startPos, endPos, elapseTime);
                movedDistance = go.transform.position - prevPos;
            }

            if (movedDistance.magnitude < 0.001f)
            {
                if (isLocalMove)
                    go.transform.localPosition = endPos;
                else
                    go.transform.position = endPos;
                break;
            }
            yield return new WaitForEndOfFrame();

        }
        if (messageObj != null)
        {
            messageObj.SendMessage(methodName);
        }
    }
}